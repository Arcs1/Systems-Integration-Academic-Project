using RestSharp;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Test_Application.ConfigFiles;
using Test_Application.Models;

namespace Test_Application
{
    public partial class UnsubscribeApp : Form
    {
        RestClient client = null;
        List<ApplicationModel> applications = null;
        bool isAtive = false;
        public UnsubscribeApp()
        {
            InitializeComponent();
            client = new RestClient(Configs.baseURI);
            loadApps();
        }

        public void loadApps()
        {

            lb_apps.Items.Clear();

            var request = new RestRequest("api/somiod/modules/applications", Method.Get);
            request.RequestFormat = DataFormat.Xml;

            var response = client.Execute<List<ApplicationModel>>(request).Data;
            this.applications = response;

            if (applications != null)
            {
                foreach (ApplicationModel app in applications)
                {
                    lb_apps.Items.Add(app.Name);
                }
            }
        }

        private void btn_unsub_app_Click(object sender, System.EventArgs e)
        {

          

            isAtive = false;
            if (lb_apps.SelectedItems.Count == 1)
            {
                Debug.Print("[DEBUG] - " + applications[lb_apps.SelectedIndex].Name + "_command");
                var requestMod = new RestRequest("api/somiod/modules/{name}", Method.Get);
                requestMod.AddUrlSegment("name", applications[lb_apps.SelectedIndex].Name + "_command");
                requestMod.RequestFormat = DataFormat.Xml;

                ModuleModel module = client.Execute<ModuleModel>(requestMod).Data;

                var request = new RestRequest("/api/somiod/modules/{id}", Method.Delete);
                request.AddUrlSegment("id", module.Id);

                RestResponse response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    btn_sub_app.BackColor = System.Drawing.Color.Red;
                    btn_sub_app.Text = "Unsubscribed";
                    btn_sub_app.Enabled = false;

                    loadApps();
                    isAtive = true;
                }
            }
            else
            {
                MessageBox.Show("Please select one application");
            }

        }

        private void lb_apps_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            btn_sub_app.BackColor = SystemColors.Control;
            btn_sub_app.Text = "Unsubscribe Application";
            btn_sub_app.Enabled = true;
        }

        public bool getIsAtive()
        {
            return isAtive;
        }
    }
}
