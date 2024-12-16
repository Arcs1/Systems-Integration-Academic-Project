using Test_Application.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Test_Application
{
    public partial class Modules : Form
    {
        string baseURI = @"http://localhost:54975"; //TODO: needs to be updated!
        RestClient client = null;
        ApplicationModel application = null;
        List<ModuleModel> modules = null;

        public Modules(int idApp)
        {
            InitializeComponent();

            client = new RestClient(baseURI);
            application = loadApp(idApp);
            if(application == null)
            {
                return;
            }

            load_lbModules();

            Text = "Application " + application.Name;
            labelApplication.Text = application.Name;

        }

        private ApplicationModel loadApp(int id)
        {
            var request = new RestRequest("api/somiod/applications/{id}", Method.Get);
            request.AddUrlSegment("id", id);
            request.RequestFormat = DataFormat.Xml;
            var app = client.Execute<ApplicationModel>(request).Data;

            if (app == null)
            {
               MessageBox.Show("No Application", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
               this.Dispose();
                return null;
            }

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

        public class ListItem
        {
            public string Name { get; set; }
            public int Id { get; set; }

            public override string ToString()
            {
                return Name;
            }
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
                lb_modules.Items.Add(new ListItem { Name = module.Name, Id = module.Id });
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
                        var form = new Commands(module.Id, application.Name);
                        form.ShowDialog();
                    }
                }
            }
        }

        private void lb_modules_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lb_modules.SelectedIndex != -1)
            {
                int id = ((ListItem)lb_modules.SelectedItem).Id;
                var form = new Commands(id, application.Name);
                form.ShowDialog();

                load_lbModules();
            }   
        }
    }
}
