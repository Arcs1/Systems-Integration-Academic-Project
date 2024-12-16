using RestSharp;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Test_Application.ConfigFiles;
using Test_Application.Models;

namespace Test_Application
{
    public partial class SubscribeApp : Form
    {
        RestClient client = null;
        List<ApplicationModel> applications = null;
        bool isAtive = false;
        public SubscribeApp()
        {
            InitializeComponent();
            client = new RestClient(Configs.baseURI);
            loadApps();
        }

        public void loadApps()
        {

            lb_apps.Items.Clear();

            var request = new RestRequest("api/somiod/modules/no/applications", Method.Get);
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

        private void btn_sub_app_Click(object sender, System.EventArgs e)
        {
            isAtive = false;
            if (lb_apps.SelectedItems.Count == 1)
            {
                string rawXml = "<module><name>" + applications[lb_apps.SelectedIndex].Name + "_command</name><parent>" + applications[lb_apps.SelectedIndex].Id + "</parent></module>";

                var request = new RestRequest("/api/somiod/{appName}", Method.Post);
                request.AddUrlSegment("appName", applications[lb_apps.SelectedIndex].Name);
                request.AddHeader("Content-Type", "text/xml");
                request.AddHeader("Accept", "text/xml");
                request.AddParameter("text/xml", rawXml, ParameterType.RequestBody);

                RestResponse response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //MessageBox.Show("Application successfully added", "STATUS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btn_sub_app.BackColor = System.Drawing.Color.Green;
                    btn_sub_app.Text = "Subscribed";
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
            btn_sub_app.Text = "Subscribe Application";
            btn_sub_app.Enabled = true;
        }

        public bool getIsAtive()
        {
            return isAtive;
        }
    }
}
