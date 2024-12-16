using App_Devices.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace App_Devices
{
    public partial class Modules_view : Form
    {
        Boolean activeflag = true;
        string baseURI = @"http://localhost:54975";
        RestClient client = null;
        List<ApplicationModel> applications = null;
        bool isAtive = false;

        public Modules_view(List<ApplicationModel> applications)
        {
            InitializeComponent();
            this.applications = new List<ApplicationModel>(applications);
            load_PredModules();
            load_lbApplications();
            client = new RestClient(baseURI);
            lBModulesPred.Visible = false;
            btnAddPredefined.Visible = false;
            pb_image.Visible = true;
            lbsomiod.Visible = true;
            lbsomiodnames.Visible = true;
            gbPredefined.Text = "";
        }

        private void load_PredModules()
        {
            lBModulesPred.Items.Clear();
            lBModulesPred.Items.Add("Lampada");
            lBModulesPred.Items.Add("Candeeiro");
            lBModulesPred.Items.Add("Estoro");
            lBModulesPred.Items.Add("Porta");
            lBModulesPred.Items.Add("Janela");
            lBModulesPred.Items.Add("Tomada");
        }

        private void load_lbApplications()
        {

            lb_application.Items.Clear();

            foreach (ApplicationModel app in this.applications)
            {
                lb_application.Items.Add(app.Name);
            }
        }

        private void btnEnable_Click(object sender, EventArgs e)
        {
            if (activeflag)
            {
                gbNewModule.Enabled = false;
                lBModulesPred.Visible = true;
                btnAddPredefined.Visible = true;
                pb_image.Visible = false;
                lbsomiod.Visible = false;
                lbsomiodnames.Visible = false;
                gbPredefined.Text = "Predefined Modules";
                activeflag = false;
            }
            else
            {
                gbNewModule.Enabled = true;
                lBModulesPred.Visible = false;
                btnAddPredefined.Visible = false;
                pb_image.Visible = true;
                lbsomiod.Visible = true;
                lbsomiodnames.Visible = true;
                gbPredefined.Text = "";
                activeflag = true;
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
            request.AddUrlSegment("id", applications[lb_application.SelectedIndex].Id);
            request.AddUrlSegment("moduleName", textBox1.Text);
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
            int numItens = lb_application.SelectedItems.Count;
            string name = textBox1.Text;

            if (numItens != 1)
            {
                MessageBox.Show("Correctly select an Application to which you want to assign this module");
                return;
            }

            if (name == "")
            {
                MessageBox.Show("Please enter a name for the module");
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

            int id = applications[lb_application.SelectedIndex].Id;

            string rawXml = "<module><name>" + name + "</name><parent>" + id + "</parent></module>";

            var request = new RestRequest("/api/somiod/{appName}", Method.Post);
            request.AddUrlSegment("appName", applications[lb_application.SelectedIndex].Name);
            request.AddHeader("Content-Type", "text/xml");
            request.AddHeader("Accept", "text/xml");
            request.AddParameter("text/xml", rawXml, ParameterType.RequestBody);

            RestResponse response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //create a subscription
                rawXml = "<subscription><event>both</event></subscription>";

                request = new RestRequest("/api/somiod/{appName}/{modName}", Method.Post);
                request.AddUrlSegment("appName", applications[lb_application.SelectedIndex].Name);
                request.AddUrlSegment("modName", name);
                request.AddHeader("Content-Type", "text/xml");
                request.AddHeader("Accept", "text/xml");
                request.AddParameter("text/xml", rawXml, ParameterType.RequestBody);

                response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show("Module and Subcription successfully added", "STATUS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Clear();
                }
            }
            isAtive = true;
        }

        private ApplicationModel searchApplication(string name)
        {
            var request = new RestRequest("api/somiod/applications/{name}/", Method.Get);
            request.AddUrlSegment("name", name);
            request.RequestFormat = DataFormat.Xml;

            var application = client.Execute<ApplicationModel>(request).Data;

            if (application == null)
            {
                MessageBox.Show("No Module", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            return application;
        }


        private void tb_search_Click(object sender, EventArgs e)
        {
            string appplicationToSearch;
            ApplicationModel application = null;

            appplicationToSearch = tb_search_app.Text;
            if (appplicationToSearch != "")
            {
                application = searchApplication(appplicationToSearch);
                if (application != null)
                {
                    int index = lb_application.FindString(application.Name);

                    if (application.Name != "")
                    {
                        lb_application.SelectedIndex = index;
                    }
                }
            }
        }

        public bool getIsAtive()
        {
            return isAtive;
        }

        private void btnAddPredefined_Click(object sender, EventArgs e)
        {

            if (lb_application.SelectedItems.Count == 1 && lBModulesPred.SelectedItems.Count == 1)
            {
                int id = applications[lb_application.SelectedIndex].Id;
                string nameMod = lBModulesPred.SelectedItem.ToString();

                string rawXml = "<module><name>" + nameMod + "</name><parent>" + id + "</parent></module>";

                var request = new RestRequest("/api/somiod/{appName}", Method.Post);
                request.AddUrlSegment("appName", applications[lb_application.SelectedIndex].Name);
                request.AddHeader("Content-Type", "text/xml");
                request.AddHeader("Accept", "text/xml");
                request.AddParameter("text/xml", rawXml, ParameterType.RequestBody);

                RestResponse response = client.Execute(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //create a subscription
                    rawXml = "<subscription><event>both</event></subscription>";

                    request = new RestRequest("/api/somiod/{appName}/{modName}", Method.Post);
                    request.AddUrlSegment("appName", applications[lb_application.SelectedIndex].Name);
                    request.AddUrlSegment("modName", nameMod);
                    request.AddHeader("Content-Type", "text/xml");
                    request.AddHeader("Accept", "text/xml");
                    request.AddParameter("text/xml", rawXml, ParameterType.RequestBody);

                    response = client.Execute(request);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        MessageBox.Show("Module and Subcription successfully added", "STATUS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox1.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Module already exists", "STATUS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Correctly select an Application to which you want to assign this module");
            }
        }
    }
}
