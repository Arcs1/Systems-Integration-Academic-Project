using App_Devices.ConfigFiles;
using App_Devices.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace App_Devices
{
    public partial class Form1 : Form
    {

        RestClient client = null;
        List<ApplicationModel> applications = null;
        List<ModuleModel> modules = null;

        Font fontBtnAcoes = new Font("Arial", 18);
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
            var request = new RestRequest("api/somiod/applications", Method.Get);
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
                    btn.Click += btn_newModule_Click;
                    fl.Controls.Add(btn);
                }
            }
            btn_addPanelFlow();
        }

        //carrega os modulos
        public void loadModls()
        {
            var request = new RestRequest("api/somiod/modules/noCommands", Method.Get);
            request.RequestFormat = DataFormat.Xml;

            var response = client.Execute<List<ModuleModel>>(request).Data;
            this.modules = response;

            if (modules != null)
            {
                fl.Controls.Clear();

                foreach (ModuleModel mod in modules)
                {
                    Button btn = new Button();

                    btn.Text = "-" + mod.Name.ToString() + "-\n[" + findApp(mod.Parent).Name + "]";
                    //btn.Tag = mod.Id.ToString();
                    btn.Height = 120;
                    btn.Width = 147;
                    btn.Font = fontBtnApps;
                    //btn.Click += btn_newModule_Click;
                    fl.Controls.Add(btn);
                }
            }
            btn_addPanelFlow();
        }

        //Este método cria o botão de adicionar aplicação
        private void btn_addPanelFlow()
        {
            Button btnadicionar = new Button();
            btnadicionar.Text = "+";
            btnadicionar.Height = 120;
            btnadicionar.Width = 147;
            btnadicionar.Font = fontBtnAcoes;
            btnadicionar.Name = "btn_add";
            if (cb_App_Mod.SelectedIndex == 0)
            {
                btnadicionar.Click += btn_add_Click;
            }
            else
            {
                btnadicionar.Click += btn_newModule_Click_1;
            }
            fl.Controls.Add(btnadicionar);
        }


        private void btn_add_Click(object sender, EventArgs e)
        {
            var form = new AddAplication();
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

        private void btn_add_app_Click(object sender, EventArgs e)
        {
            var form = new AddAplication();
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

        private void btn_newModule_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32((sender as Button).Tag);
            var form = new Application_view(id, this);
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

        private void btn_newModule_Click_1(object sender, EventArgs e)
        {
            var form = new Modules_view(this.applications);
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

        private ApplicationModel findApp(int id)
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
    }
}


