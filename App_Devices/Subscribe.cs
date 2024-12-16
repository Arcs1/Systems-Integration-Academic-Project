using App_Devices.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace App_Devices
{
    public partial class Subscribe : Form
    {
        string baseURI = @"http://localhost:54975"; //TODO: needs to be updated!
        RestClient client = null;
        List<SubscriptionModel> subscriptions = null;
        int moduleId;
        int moduleParent;

        public Subscribe(int moduleId, int moduleParent, string moduleName)
        {
            InitializeComponent();
            this.moduleId = moduleId;
            this.moduleParent = moduleParent;
            client = new RestClient(baseURI);
            load_lbSubscribes(moduleId);

            //Add Items to the combo box
            cb_events.Items.Add("both");
            cb_events.Items.Add("creation");
            cb_events.Items.Add("deletion");
            cb_events.SelectedIndex = 0;
            cb_events.DropDownStyle = ComboBoxStyle.DropDownList;
            lb_SubModName.Text = "Subscripions of " + moduleName;
        }

        private void load_lbSubscribes(int moduleId)
        {
            this.subscriptions = loadSubscriptions(moduleId);

            if (this.subscriptions == null)
            {
                lb_subscriptions.Items.Clear();
                return;
            }

            lb_subscriptions.Items.Clear();
            foreach (SubscriptionModel subscription in this.subscriptions)
            {
                lb_subscriptions.Items.Add(subscription.Name + " - " + subscription.Event.ToUpper() + " - " + subscription.Endpoint);
            }
        }

        private List<SubscriptionModel> loadSubscriptions(int moduleId)
        {
            var request = new RestRequest("api/somiod/modules/{id}/subscriptions", Method.Get);
            request.AddUrlSegment("id", moduleId);
            
            request.RequestFormat = DataFormat.Xml;

            var response = client.Execute<List<SubscriptionModel>>(request).Data;

            if (response == null)
            {
                MessageBox.Show("No subscriptions", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            return response;
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            int numItens = lb_subscriptions.SelectedItems.Count;

            if (numItens != 1)
            {
                MessageBox.Show("Correctly select an Subscription to delete");
                return;
            }

            int id = subscriptions[lb_subscriptions.SelectedIndex].Id;

            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete '" + subscriptions[lb_subscriptions.SelectedIndex].Name + "'?", "Delete Subscription", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                var request = new RestRequest("/api/somiod/subscriptions/{id}", Method.Delete);
                request.AddUrlSegment("id", id);

                RestResponse response = client.Execute(request);
                
                subscriptions.Remove(subscriptions[lb_subscriptions.SelectedIndex]);

                lb_subscriptions.Items.RemoveAt(lb_subscriptions.SelectedIndex);
                lb_subscriptions.SelectedIndex = -1;

                if (response == null)
                {
                    MessageBox.Show("Error deleting subscription");
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

        private bool regexEndpoint(string endpoint)
        {
            var regex = new Regex("^(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");

            if (!regex.IsMatch(endpoint))          
            {
                MessageBox.Show("Only IPV4 are allowed (Eg: 192.168.1.1) ", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool isValidName()
        {
            var request = new RestRequest("api/somiod/modules/{id}/subscriptions/{subscriptionName}", Method.Get);
            request.AddUrlSegment("id", moduleId);
            request.AddUrlSegment("moduleName", tb_nameSubscription.Text);
            request.RequestFormat = DataFormat.Xml;

            var response = client.Execute<SubscriptionModel>(request);

            if (response.StatusCode != System.Net.HttpStatusCode.NotFound)
            {
                return false;
            }

            return true;
        }

        private ApplicationModel getAppById(int id)
        {
            var request = new RestRequest("api/somiod/applications/{id}", Method.Get);
            request.AddUrlSegment("id", id);
            request.RequestFormat = DataFormat.Xml;

            var response = client.Execute<ApplicationModel>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

            return response.Data;
        }

        private ModuleModel getModuleById(int id)
        {
            var request = new RestRequest("api/somiod/modules/{id}", Method.Get);
            request.AddUrlSegment("id", id);
            request.RequestFormat = DataFormat.Xml;

            var response = client.Execute<ModuleModel>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

            return response.Data;
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            
            string name = tb_nameSubscription.Text;
            string endpoint = tb_subscriptionEndpoint.Text;
            string selectedEvent = this.cb_events.GetItemText(this.cb_events.SelectedItem);

            //Validações nome
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

            //Validações endpoint
            if (endpoint == "")
            {
                MessageBox.Show("Please fill in the endpoint field", "ENDPOINT EMPTY", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!regexEndpoint(endpoint))
            {
                return;
            }
            
            string rawXml = "<subscription><name>" + name + "</name><event>" + selectedEvent + "</event><endpoint>" + endpoint + "</endpoint></subscription>";

            var request = new RestRequest("/api/somiod/{appName}/{modName}", Method.Post);

            ApplicationModel app = getAppById(moduleParent);
            ModuleModel mod = getModuleById(moduleId);

            if (app == null || mod == null)
            {
                MessageBox.Show("Error getting application or module", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
              
            request.AddUrlSegment("appName", app.Name);
            request.AddUrlSegment("modName", mod.Name);
            request.AddHeader("Content-Type", "text/xml");
            request.AddHeader("Accept", "text/xml");
            request.AddParameter("text/xml", rawXml, ParameterType.RequestBody);

            RestResponse response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var newSubscriptionGet = new RestRequest("api/somiod/modules/{id}/subscriptions/{subscriptionName}", Method.Get);
                newSubscriptionGet.AddUrlSegment("id", moduleId);
                newSubscriptionGet.AddUrlSegment("subscriptionName", name);
                newSubscriptionGet.RequestFormat = DataFormat.Xml;
                var newSubscription = client.Execute<SubscriptionModel>(newSubscriptionGet);

                if (newSubscription.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    subscriptions.Add(newSubscription.Data);
                    lb_subscriptions.Items.Add(name + " - " + selectedEvent.ToUpper() + " - " + endpoint);
                    lb_subscriptions.SelectedItem = name + " - " + selectedEvent.ToUpper() + " - " + endpoint;

                    tb_nameSubscription.Clear();
                    tb_subscriptionEndpoint.Clear();
                    cb_events.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("Error getting new subscription", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Error adding new subscription", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
