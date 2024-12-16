using App_Devices.Models;
using RestSharp;
using System;
using System.Windows.Forms;

namespace App_Devices
{
    public partial class Update : Form
    {
        public string newAppName { get; set; }
        string baseURI = @"http://localhost:54975"; //TODO: needs to be updated!
        RestClient client = null;
        int id;
        char type;
        bool isAtive = false;
        ModuleModel module = null;
        public Update ( int id, char type)
        {
            InitializeComponent();
            client = new RestClient(baseURI);
            this.id = id;
            this.type = type;
            if (type == 'A')
            {
                Text = "Update Application";
            }
            else
            {
                Text = "Upadate Module";
                module = loadMod(id);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            isAtive = false;
            var request = new RestRequest();
            if(newName.Text == "") {
                MessageBox.Show("Please enter a name ", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (isValidName() != true)
            {
                MessageBox.Show("Name already exists", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (type == 'A') { 
            string rawXml = "<application><name>" + newName.Text + "</name></application>";

            request = new RestRequest("api/somiod/applications/{id}", Method.Put);
            request.AddUrlSegment("id", id);
            request.AddHeader("Content-Type", "text/xml");
            request.AddHeader("Accept", "text/xml");
            request.AddParameter("text/xml", rawXml, ParameterType.RequestBody);

            }
            else
            {
                string rawXml = "<module><name>" + newName.Text + "</name></module>";

                request = new RestRequest("api/somiod/modules/{id}", Method.Put);
                request.AddUrlSegment("id", id);
                request.AddHeader("Content-Type", "text/xml");
                request.AddHeader("Accept", "text/xml");
                request.AddParameter("text/xml", rawXml, ParameterType.RequestBody);

            }
            RestResponse response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                newAppName = newName.Text;
                btnAdd.BackColor = System.Drawing.Color.Green;
                btnAdd.Text = "Updated";
                btnAdd.Enabled = false;
                newName.Enabled = false;

                isAtive = true;
            }

        }
        private bool isValidName()
        {
            var request = new RestRequest();

            if (type == 'M')
            {
                request = new RestRequest("/api/somiod/applications/{id}/modules/{moduleName}", Method.Get);
                request.AddUrlSegment("id", module.Parent);
                request.AddUrlSegment("moduleName", newName.Text);
                request.RequestFormat = DataFormat.Xml;

            }
            else
            {
                request = new RestRequest("/api/somiod/applications/{appName}", Method.Get);
                request.AddUrlSegment("appName", newName.Text);
                request.RequestFormat = DataFormat.Xml;
            }
            var response = client.Execute<ModuleModel>(request);
            if (response.StatusCode != System.Net.HttpStatusCode.NotFound)
            {
                return false;
            }

            return true;
        }

        private ModuleModel loadMod(int id)
        {
            var request = new RestRequest("api/somiod/modules/{id}", Method.Get);
            request.AddUrlSegment("id", id);
            request.RequestFormat = DataFormat.Xml;
            var mod = client.Execute<ModuleModel>(request).Data;

            if (mod == null)
            {
                MessageBox.Show("No Modules", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return mod;
        }

        public bool getIsAtive()
        {
            return isAtive;
        }
    }
}
