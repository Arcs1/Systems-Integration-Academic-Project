using App_Devices.ConfigFiles;
using App_Devices.Models;
using RestSharp;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace App_Devices
{
    public partial class AddAplication : Form
    {

        RestClient client = null;
        bool isAtive = false;
        
        public AddAplication()
        {
            InitializeComponent();
            client = new RestClient(Configs.baseURI);
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            isAtive = false;

            if (textBox1.Text == "")
            {
                MessageBox.Show("Name is required", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (isValidName() != true)
            {
                MessageBox.Show("Name already exists", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!regexName(textBox1.Text))
            {
                return;
            }

    
            var appName = textBox1.Text;

            
            string rawXml = "<application><name>" + appName + "</name></application>";

            var request = new RestRequest("/api/somiod", Method.Post);
            request.AddHeader("Content-Type", "text/xml");
            request.AddHeader("Accept", "text/xml");
            request.AddParameter("text/xml", rawXml, ParameterType.RequestBody);


            RestResponse response = client.Execute(request);

            btnAdd.BackColor = System.Drawing.Color.Green;
            btnAdd.Text = "Added";
            btnAdd.Enabled = false;
            textBox1.Enabled = false;

            isAtive = true;
        }

        //Valida se o nome da aplicação já existe (True == é valida)
        private bool isValidName()
        {
            var request = new RestRequest("/api/somiod/applications/{Name}", Method.Get);
            request.AddUrlSegment("Name", textBox1.Text);
            request.RequestFormat = DataFormat.Xml;
            
            var response = client.Execute<ApplicationModel>(request);

            if (response.StatusCode != System.Net.HttpStatusCode.NotFound)
            {
                return false;
            }

            ApplicationModel application = response.Data;

            return true;
        }

        public bool getIsAtive()
        {
            return isAtive;
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

    }
}
