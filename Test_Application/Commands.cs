using RestSharp;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Test_Application.CommandsXML;
using Test_Application.Models;

namespace Test_Application
{
    public partial class Commands : Form
    {
        string baseURI = @"http://localhost:54975"; //TODO: needs to be updated!
        RestClient client = null;
        ModuleModel module = null;
        HandlerXML handler;
        List<Command> commands = null;
        int idMod;
        string applicationName;

        public Commands(int idMod, string nameApp)
        {
            InitializeComponent();
            this.idMod = idMod;
            client = new RestClient(baseURI);
            module = loadMod(idMod);
            if(module == null)
            {
                return;
            }
            Text = "Module " + module.Name;
            labelModule.Text = module.Name;
            handler = new HandlerXML();
            load_lbCommands();
            gbcommand.Visible = false;
            gb_images.Visible = true;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            btn_Execute.Enabled = false;
            btnSaveEdit.Visible = false;
            this.applicationName = nameApp;
            client = new RestClient(baseURI);
        }

        private ModuleModel loadMod(int id)
        {
            
            var request = new RestRequest("api/somiod/modules/{id}", Method.Get);
            request.AddUrlSegment("id", id);
            request.RequestFormat = DataFormat.Xml;
            var mod = client.Execute<ModuleModel>(request).Data;
            if (mod == null)
            {
                MessageBox.Show("No Module", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Dispose();
                return null;
            }

            return mod;
        }

        private void load_lbCommands()
        {

            lb_commands.Items.Clear();
            
            commands = new List<Command>(handler.getCommands(idMod));
            foreach (Command command in commands)
            {
                lb_commands.Items.Add(new Command { Name = command.Name, IdApp = command.IdApp, IdMod = command.IdMod, TextCommand = command.TextCommand });
            }
        }
        private void btnCreate_Click(object sender, EventArgs e)
        {
            lb_commands.SelectedItem = null;
            button3.Enabled = false;
            tb_search.Enabled = false;
            btnSave.Visible = true;
            btnCreate.Enabled = false;
            gbcommand.Visible = true;
            gb_images.Visible = false;
            btnSaveEdit.Visible = false;
            lb_commands.Enabled = false;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            btn_Execute.Enabled = false;
            textNameCommand.Text = "";
            textCommand.Text = "";
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnSave.Visible = false;
            btnSaveEdit.Visible = true;
            textCommand.Text = ((Command)lb_commands.SelectedItem).TextCommand;
            textNameCommand.Text = ((Command)lb_commands.SelectedItem).Name;
            gbcommand.Visible = true;
            gb_images.Visible = false;
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int idApp = Convert.ToInt32(((Command)lb_commands.SelectedItem).IdApp);
            int idMod = Convert.ToInt32(((Command)lb_commands.SelectedItem).IdMod);
            string newTextCommand = textCommand.Text;
            string newNameCommand = textNameCommand.Text;
            string oldNameCommand = ((Command)lb_commands.SelectedItem).Name;
            string oldTextCommand = ((Command)lb_commands.SelectedItem).TextCommand;


            handler.Remove(idApp, idMod, newNameCommand, newTextCommand, oldNameCommand, oldTextCommand);
            gbcommand.Visible = false;
            gb_images.Visible = true;
            textCommand.Text = "";
            textNameCommand.Text = "";
            textNameCommand.Text = "";
            load_lbCommands();
            btnCreate.Enabled = true;
            btnSaveEdit.Visible = false;
            btnSave.Visible = true;
            //MessageBox.Show("Success Delete Command", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (textNameCommand.Text == "" || textCommand.Text == "")
            {
                MessageBox.Show("Please insert name and Command ", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (handler.isValid(module.Parent, module.Id, textNameCommand.Text, textCommand.Text))
            {
                handler.AddCommand(module.Parent, module.Id, textNameCommand.Text, textCommand.Text);
                gbcommand.Visible = false;
                gb_images.Visible = true;
                textCommand.Text = "";
                textNameCommand.Text = "";
                load_lbCommands();
                btnCreate.Enabled = true;
                lb_commands.Enabled = true;
                button3.Enabled = true;
                tb_search.Enabled = true;
                //MessageBox.Show("Success Create Command", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Command already exists", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            button3.Enabled = true;
            textCommand.Text = "";
            textNameCommand.Text = "";
            gbcommand.Visible = false;
            gb_images.Visible = true;
            btnCreate.Enabled = true;
            lb_commands.Enabled = true;
            tb_search.Enabled = true;
        }

        private void lb_commands_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lb_commands.SelectedIndex != -1)
            {
                textCommand.Text = ((Command)lb_commands.SelectedItem).TextCommand;
                textNameCommand.Text = ((Command)lb_commands.SelectedItem).Name;
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                btn_Execute.Enabled = true;
            }
        }

        private void btnSaveEdit_Click(object sender, EventArgs e)
        {
            int idApp = Convert.ToInt32(((Command)lb_commands.SelectedItem).IdApp);
            int idMod = Convert.ToInt32(((Command)lb_commands.SelectedItem).IdMod);
            string newTextCommand = textCommand.Text;
            string newNameCommand = textNameCommand.Text;
            string oldNameCommand = ((Command)lb_commands.SelectedItem).Name;
            string oldTextCommand = ((Command)lb_commands.SelectedItem).TextCommand;

            if (newNameCommand == "" || newTextCommand == "")
            {
                MessageBox.Show("Please insert name and Command ", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (handler.isValid(idApp, idMod, newNameCommand, newTextCommand))
            {
                handler.EditCommand(idApp, idMod, newNameCommand, newTextCommand, oldNameCommand, oldTextCommand);
                gbcommand.Visible = false;
                gb_images.Visible = true;
                textCommand.Text = "";
                textNameCommand.Text = "";
                load_lbCommands();
                btnCreate.Enabled = true;
                btnSaveEdit.Visible = false;
                btnSave.Visible = true;
                //MessageBox.Show("Success Edit Commad", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Command already exists", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void button3_Click(object sender, EventArgs e)
        {

            foreach (Command command in commands)
            {

                if (command.Name == tb_search.Text)
                {
                    int index = lb_commands.FindString(command.Name);
                    lb_commands.SelectedIndex = index;
                }

            }
        }

        private void btn_Execute_Click(object sender, EventArgs e)
        {

            // Ver se foi selecionado um comando
            if (lb_commands.SelectedIndex != -1)
            {
                
                string command = ((Command)lb_commands.SelectedItem).TextCommand;
                string nameModule = module.Name;
                string nameApp = applicationName;

                // Post
                string rawXml = "<data><content>" + command + "</content></data>";

                var request = new RestRequest("api/somiod/{appName}/{modName}", Method.Post);
                request.AddUrlSegment("appName", nameApp);
                request.AddUrlSegment("modName", nameModule);
                request.AddHeader("Content-Type", "text/xml");
                request.AddHeader("Accept", "text/xml");
                request.AddParameter("text/xml", rawXml, ParameterType.RequestBody);

                var response = client.Execute <DataModel>(request);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    MessageBox.Show("Fail Execute", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
