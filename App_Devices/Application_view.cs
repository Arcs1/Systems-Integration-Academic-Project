using App_Devices.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt.Exceptions;
using System.Threading;
using System.IO;

namespace App_Devices
{
    public partial class Application_view : Form
    {
        string baseURI = @"http://localhost:54975"; //TODO: needs to be updated!
        RestClient client = null;
        Boolean activeflag = true;
        ApplicationModel application = null;
        List<ModuleModel> modules = null;
        Form1 parent;
        bool isAtive = false;
        MqttClient mClient;
        List<string> topicsSubscribed = new List<string>();
        List<SubscriptionModel> subscriptionsStatus = new List<SubscriptionModel>();


        public Application_view(int idApp, Form1 parent)
        {
            InitializeComponent();
            FormClosing += Application_View_FormClosing;

            client = new RestClient(baseURI);
            application = loadApp(idApp);
            load_lbModules();
            this.parent = parent;
            lb_Commands.Visible = false;
            pb_image.Visible = true;
            lbsomiod.Visible = true;
            lbsomiodnames.Visible = true;
            gbCommands.Text = "";
            btnCommands.Enabled = false;
            btn_update.Enabled = false;
            btn_delete.Enabled = false;
            btn_subscriptions.Enabled = false;
            btn_connections.Enabled = false;

        }

        private void Application_View_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mClient != null && mClient.IsConnected)
            {
                mClient.Unsubscribe(topicsSubscribed.ToArray());
                mClient.Disconnect();
            }
        }

        private void btnEnable_Click(object sender, EventArgs e)
        {
            if (activeflag)
            {
                btnCommands.Text = "New Module";
                gbNewModule.Enabled = false;
                lb_Commands.Visible = true;
                pb_image.Visible = false;
                lbsomiod.Visible = false;
                lbsomiodnames.Visible = false;
                gbCommands.Text = "Commands";
                activeflag = false;
            }
            else
            {
                btnCommands.Text = "Commands";
                gbNewModule.Enabled = true;
                lb_Commands.Visible = false;
                pb_image.Visible = true;
                lbsomiod.Visible = true;
                lbsomiodnames.Visible = true;
                gbCommands.Text = "";
                activeflag = true;
            }
        }

        private ApplicationModel loadApp(int id)
        {
            var request = new RestRequest("api/somiod/applications/{id}", Method.Get);
            request.AddUrlSegment("id", id);
            request.RequestFormat = DataFormat.Xml;
            var app = client.Execute<ApplicationModel>(request).Data;

            if (app == null)
            {
                MessageBox.Show("No Applications", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Text = "Application " + app.Name;
            labelApplication.Text = app.Name;

            return app;
        }

        private List<ModuleModel> loadModules(int idApp)
        {
            var request = new RestRequest("api/somiod/modules/noCommand/{id}", Method.Get);
            request.AddUrlSegment("id", idApp);
            request.RequestFormat = DataFormat.Xml;

            var response = client.Execute<List<ModuleModel>>(request).Data;

            if (response == null)
            {
                MessageBox.Show("No Modules", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            this.modules = response;

            return response;
        }

        private ModuleModel searchModule(string name)
        {
            var request = new RestRequest("api/somiod/modules/{name}/", Method.Get);
            request.AddUrlSegment("name", name);
            request.RequestFormat = DataFormat.Xml;

            var module = client.Execute<ModuleModel>(request);

            if (module.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                MessageBox.Show("No Module", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            return module.Data;
        }


        private void load_lbModules()
        {
            List<ModuleModel> modules = loadModules(this.application.Id);

            if (modules == null)
            {
                lb_modules.Items.Clear();
                return;
            }

            lb_modules.Items.Clear();
            foreach (ModuleModel module in modules)
            {
                lb_modules.Items.Add(module.Name);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string moduleToSearch;
            ModuleModel module = null;

            moduleToSearch = tb_search.Text;

            if (moduleToSearch != "")
            {
                module = searchModule(moduleToSearch);
                if (module != null)
                {
                    int index = lb_modules.FindString(module.Name);

                    if (module.Name != "")
                    {
                        lb_modules.SelectedIndex = index;
                    }
                }
            }
        }

        private bool regexName(string name)
        {
            var regex = new Regex(@"[^a-zA-Z0-9\s]");
            if (regex.IsMatch(name))
            {
                MessageBox.Show("Only letters and numbers are allowed", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool isValidName()
        {
            var request = new RestRequest("/api/somiod/applications/{id}/modules/{moduleName}", Method.Get);
            request.AddUrlSegment("id", this.application.Id);
            request.AddUrlSegment("moduleName", tb_nameModule.Text);
            request.RequestFormat = DataFormat.Xml;

            var response = client.Execute<ModuleModel>(request);

            if (response.StatusCode != System.Net.HttpStatusCode.NotFound)
            {
                return false;
            }

            return true;
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            isAtive = false;
            string name = tb_nameModule.Text;
            if (name == "")
            {
                MessageBox.Show("Please fill in the name field", "NAME EMPTY", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!regexName(name))
            {
                return;
            }

            if (isValidName() != true)
            {
                MessageBox.Show("Name already exists", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string rawXml = "<module><name>" + name + "</name><parent>" + application.Id + "</parent></module>";

            var request = new RestRequest("/api/somiod/{appName}", Method.Post);
            request.AddUrlSegment("appName", application.Name);
            request.AddHeader("Content-Type", "text/xml");
            request.AddHeader("Accept", "text/xml");
            request.AddParameter("text/xml", rawXml, ParameterType.RequestBody);

            RestResponse response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var newModuleGet = new RestRequest("api/somiod/modules/{name}", Method.Get);
                newModuleGet.AddUrlSegment("name", name);
                request.RequestFormat = DataFormat.Xml;
                var newModule = client.Execute<ModuleModel>(newModuleGet).Data;

                //create a subscription
                rawXml = "<subscription><event>both</event></subscription>";

                request = new RestRequest("/api/somiod/{appName}/{modName}", Method.Post);
                request.AddUrlSegment("appName", application.Name);
                request.AddUrlSegment("modName", name);
                request.AddHeader("Content-Type", "text/xml");
                request.AddHeader("Accept", "text/xml");
                request.AddParameter("text/xml", rawXml, ParameterType.RequestBody);

                response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    this.modules.Add(newModule);
                    lb_modules.Items.Add(name);
                    lb_modules.SelectedItem = name;

                    tb_nameModule.Clear();
                }
            }
            isAtive = true;
        }

        private void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            lb_Commands.BeginInvoke((MethodInvoker)delegate
            {
                string message = Encoding.UTF8.GetString(e.Message);
                lb_Commands.Items.Add(message);
                if (lb_Commands.Items.Count > 0)
                {
                    lb_Commands.SelectedItem = message;
                }
            });
        }

        private void subscribeToTopic()
        {
            btn_connections.ForeColor = Color.Black;
            btn_connections.BackColor = Color.Transparent;

            if (mClient != null && mClient.IsConnected)
            {
                mClient.Unsubscribe(topicsSubscribed.ToArray());
                mClient.Disconnect();
            }

            subscriptionsStatus.Clear();

            var request = new RestRequest("api/somiod/subscriptions/{id}", Method.Get);
            request.AddUrlSegment("id", modules[lb_modules.SelectedIndex].Id);
            request.RequestFormat = DataFormat.Xml;
            List<SubscriptionModel> subs = client.Execute<List<SubscriptionModel>>(request).Data;

            if (subs.Count == 0) return;

            foreach (SubscriptionModel sub in subs)
            {
                //create a new thread for each subscription
                Thread thread = new Thread(() =>
                {
                    try
                    {                       
                        MqttClient mClient = new MqttClient(sub.Endpoint);
                        mClient.Connect(Guid.NewGuid().ToString());
                        if (!mClient.IsConnected)
                        {
                            MessageBox.Show("Error connecting to message broker...");                            
                            return;
                        }
                        sub.Connection = "Connected";
                        subscriptionsStatus.Add(sub);

                        mClient.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

                        List<string> topics = new List<string>();
                        List<byte> qosLevels = new List<byte>();

                        switch (sub.Event)
                        {
                            case "both":
                                topics.Add(sub.Name + "/creation");
                                topics.Add(sub.Name + "/deletion");
                                topicsSubscribed = topics;
                                qosLevels.Add(MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE);
                                qosLevels.Add(MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE);
                                break;

                            case "creation":
                                topics.Add(sub.Name + "/creation");
                                topicsSubscribed = topics;
                                qosLevels.Add(MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE);
                                break;

                            case "deletion":
                                topics.Add(sub.Name + "/deletion");
                                topicsSubscribed = topics;
                                qosLevels.Add(MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE);
                                break;
                        }
                        mClient.Subscribe(topics.ToArray(), qosLevels.ToArray());
                        this.mClient = mClient;                       
                    }
                    catch (MqttConnectionException)
                    {
                        sub.Connection = "Disconnected";
                        subscriptionsStatus.Add(sub);
                    }
                    if (subscriptionsStatus.Count() == subs.Count())
                    {
                        btn_connections.ForeColor = Color.White;
                        btn_connections.BackColor = Color.Green;

                        foreach (SubscriptionModel sub_btn in subscriptionsStatus)
                        {
                            if (sub_btn.Connection == "Disconnected")
                            {
                                btn_connections.ForeColor = Color.White;
                                btn_connections.BackColor = Color.Red;
                            }
                        }
                    }
                });
                thread.Start();              
            }           
        }

        private void lb_modules_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lb_modules.SelectedIndex != -1)
            {
                btnCommands.Text = "New Module";
                btnCommands.Enabled = true;
                btn_update.Enabled = true;
                btn_delete.Enabled = true;
                btn_subscriptions.Enabled = true;
                btn_connections.Enabled = true;
                lb_Commands.Visible = true;
                pb_image.Visible = false;
                lbsomiod.Visible = false;
                lbsomiodnames.Visible = false;
                gbCommands.Text = "Commands";
                gbNewModule.Enabled = false;
                activeflag = false;
                
                subscribeToTopic();
                loadModuleMessages();
                lb_Commands.SelectedIndex = lb_Commands.Items.Count - 1;
            }
        }

        private void loadModuleMessages()
        {
            lb_Commands.Items.Clear();

            var request = new RestRequest("api/somiod/data/{id}", Method.Get);
            request.AddUrlSegment("id", modules[lb_modules.SelectedIndex].Id);
            request.RequestFormat = DataFormat.Xml;
            List<DataModel> dataSets = client.Execute<List<DataModel>>(request).Data;

            if (dataSets.Count == 0) return;

            dataSets.ForEach(data => lb_Commands.Items.Add("'" + data.Content + "'  -  " + data.Creation_dt + "  -  " + data.Id));
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            int numItens = lb_modules.SelectedItems.Count;

            if (numItens != 1)
            {
                MessageBox.Show("Correctly select an Application to which you want to assign this module");
                return;
            }

            int id = this.modules[lb_modules.SelectedIndex].Id;

            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete '" + modules[lb_modules.SelectedIndex].Name + "'?", "Delete Module", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                var request = new RestRequest("/api/somiod/modules/{id}", Method.Delete);
                request.AddUrlSegment("id", id);

                RestResponse response = client.Execute(request);

                mClient.Unsubscribe(new string[] { lb_modules.SelectedIndex.ToString() }); // Ta a dar erro aqui 
                mClient.Disconnect();
                this.modules.Remove(this.modules[lb_modules.SelectedIndex]);

                lb_modules.Items.RemoveAt(lb_modules.SelectedIndex);
                lb_modules.SelectedIndex = -1;
                lb_Commands.Items.Clear();

                if (response == null)
                {
                    MessageBox.Show("Error deleting module");
                }
            }
        }

        private void btn_apagarApp_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete '" + application.Name + "'?", "Delete Application", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                foreach (var module in modules)
                {
                    var requestDelMod = new RestRequest("/api/somiod/modules/{id}", Method.Delete);
                    requestDelMod.AddUrlSegment("id", module.Id);

                    RestResponse responseDelMod = client.Execute(requestDelMod);

                    if (responseDelMod == null)
                        MessageBox.Show("Error deleting application");
                }

                var request = new RestRequest("api/somiod/applications/{id}", Method.Delete);
                request.AddUrlSegment("id", this.application.Id);

                RestResponse response = client.Execute(request);

                if (response == null)
                    MessageBox.Show("Error deleting application");

                try
                {
                    this.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Error closing form");
                }

                this.parent.loadApps();
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            if (lb_modules.SelectedIndex != -1)
            {
                int id = this.modules[lb_modules.SelectedIndex].Id;

                var form = new Update(id, 'M');
                var result = form.ShowDialog();
                if (result != DialogResult.OK && form.getIsAtive())
                {
                    mClient.Unsubscribe(topicsSubscribed.ToArray());
                    subscribeToTopic();
                    load_lbModules();

                    btnCommands.Text = "Commands";
                    btnCommands.Enabled = false;
                    btn_update.Enabled = false;
                    btn_delete.Enabled = false;
                    btn_subscriptions.Enabled = false;
                    btn_connections.BackColor = Color.Transparent;
                    btn_connections.Enabled = false;
                    lb_Commands.Visible = false;
                    pb_image.Visible = true;
                    lbsomiod.Visible = true;
                    lbsomiodnames.Visible = true;
                    gbCommands.Text = "";
                    gbNewModule.Enabled = true;
                    activeflag = true;
                }
            }
        }

        public bool getIsAtive()
        {
            return isAtive;
        }

        private void btn_updateApp_Click(object sender, EventArgs e)
        {
            var form = new Update(application.Id, 'A');
            var result = form.ShowDialog();
            if (result != DialogResult.OK && form.getIsAtive())
            {
                application.Name = form.newAppName;
                loadApp(application.Id);
                isAtive = true;
            }
        }

        private void btn_Subscriptions_Click(object sender, EventArgs e)
        {
            int numItens = lb_modules.SelectedItems.Count;

            if (numItens != 1)
            {
                MessageBox.Show("Correctly select an Module");
                return;
            }

            var form = new Subscribe(modules[lb_modules.SelectedIndex].Id, modules[lb_modules.SelectedIndex].Parent, modules[lb_modules.SelectedIndex].Name);
            form.ShowDialog();

            subscribeToTopic();

        }

        private void btn_connections_Click(object sender, EventArgs e)
        {
            int numItens = lb_modules.SelectedItems.Count;

            if (numItens != 1)
            {
                MessageBox.Show("Correctly select an Module");
                return;
            }

            var form = new Connections(this.subscriptionsStatus, modules[lb_modules.SelectedIndex].Id);
            form.ShowDialog();

            subscribeToTopic();
        }
    }
}
