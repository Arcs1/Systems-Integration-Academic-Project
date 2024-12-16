using Test_Application.ConfigFiles;
using Test_Application.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;

namespace Test_Application
{
    public partial class Form1 : Form
    {

        RestClient client = null;
        List<ApplicationModel> applications = null;
        List<ModuleModel> modules = null;

        Font fontBtnApps = new Font("Arial", 13);


        public Form1()
        {
            this.WindowState = FormWindowState.Maximized;
            InitializeComponent();
            client = new RestClient(Configs.baseURI);
            cb_App_Mod.Font = new Font("Arial", 13); ;
            cb_App_Mod.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_App_Mod.Items.Add("Applications");
            cb_App_Mod.Items.Add("Modules");
            cb_App_Mod.SelectedIndex = 0;

            layoutInit();
        }

        //Este método é responsável por criar os botões de ações e os botões de aplicações
        private void layoutInit()
        {
            fl.Name = "Flowpanel";
            fl.Visible = true;
            this.Controls.Add(fl);

        }

        //Este método faz o Get e atualiza o flowpanel
        //carrega as aplicações
        public void loadApps()
        {
            var request = new RestRequest("api/somiod/modules/applications", Method.Get);
            request.RequestFormat = DataFormat.Xml;

            var response = client.Execute<List<ApplicationModel>>(request).Data;
            this.applications = response;

            if (applications != null)
            {
                fl.Controls.Clear();

                foreach (ApplicationModel app in applications)
                {
                    Button btn = new Button();


                    btn.Text = app.Name.ToString();
                    btn.Tag = app.Id.ToString();
                    btn.Height = 120;
                    btn.Width = 147;
                    btn.Font = fontBtnApps;
                    btn.Click += btn_app;
                    fl.Controls.Add(btn);
                }
            }
        }

        //carrega os modulos
        public void loadModls()
        {
            //todos os modulos das applicações que tem um modulo com _command e subtrair os modulos com _commnad
            var request = new RestRequest("api/somiod/modules/noCommand", Method.Get);
            request.RequestFormat = DataFormat.Xml;

            var response = client.Execute<List<ModuleModel>>(request).Data;
            this.modules = response;

            if (modules != null)
            {
                fl.Controls.Clear();

                foreach (ModuleModel mod in modules)
                {
                    Button btn = new Button();

                    btn.Text = "-" + mod.Name.ToString() + "-\n[" + find(mod.Parent).Name + "]";
                    btn.Tag = mod;
                    btn.Height = 120;
                    btn.Width = 147;
                    btn.Font = fontBtnApps;
                    btn.Click += btn_mod;
                    fl.Controls.Add(btn);
                }
            }
        }

        private void cb_App_Mod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_App_Mod.SelectedIndex == 0)
            {
                loadApps();
            }
            else
            {
                loadModls();
            }
        }

        private void btn_mod(object sender, EventArgs e)
        {

            ModuleModel mod = (ModuleModel)(sender as Button).Tag;
            var form = new Commands(mod.Id, find(mod.Parent).Name);
            if (!form.IsDisposed)
            {
                form.ShowDialog();
            }
            loadModls();
        }

        private void btn_app(object sender, EventArgs e)
        {
            int id = Convert.ToInt32((sender as Button).Tag);
            var form = new Modules(id);
            if (!form.IsDisposed)
            {
                form.ShowDialog();
            }
            loadApps();

        }

        private ApplicationModel find(int id)
        {
            foreach (var app in this.applications)
            {
                if (app.Id == id)
                {
                    return app;
                }
            }
            return null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form = new SubscribeApp();
            var result = form.ShowDialog();

            if (result != DialogResult.OK && form.getIsAtive())
            {
                if (cb_App_Mod.SelectedIndex == 0)
                {
                    loadApps();
                }
                else
                {
                    loadModls();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var form = new UnsubscribeApp();
            var result = form.ShowDialog();

            if (result != DialogResult.OK && form.getIsAtive())
            {
                if (cb_App_Mod.SelectedIndex == 0)
                {
                    loadApps();
                }
                else
                {
                    loadModls();
                }
            }

        }
    }
}


