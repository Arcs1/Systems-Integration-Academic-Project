using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using System.Xml;
using System.Xml.Linq;
using Test_Application.Models;

namespace Test_Application.CommandsXML
{
    public class HandlerXML
    {
        public string XmlFilePath { get; set; }

        public HandlerXML()
        {

            XmlFilePath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "Systems-Integration-Academic-Project\\Test_Application\\CommandsXML\\CommandsFile.xml"); 
           
        }

        public bool isValid(int idApp, int idMod, String nameCommand, String textCommand)
        {
            XmlDocument docDefinitive = new XmlDocument();
            docDefinitive.Load(XmlFilePath);
            XmlNode node = docDefinitive.SelectSingleNode("//commands/command[name='"+nameCommand+"'][textCommand='"+textCommand+"'][idApp='"+idApp+"'][idMod='"+idMod+"']");
            if(node == null)
            {
                return true;
            }
            return false;
        }

        public void AddCommand(int idApp, int idMod, String nameCommand, String textCommand)
        {
            if (String.IsNullOrEmpty(nameCommand) || String.IsNullOrEmpty(textCommand))
            {
                return;
            }
            XmlDocument docDefinitive = new XmlDocument();
            docDefinitive.Load(XmlFilePath);
            XmlNode nodeCommand = docDefinitive.CreateElement("command");

            XmlNode nodeAux = docDefinitive.CreateElement("name");
            nodeAux.InnerText = nameCommand;
            nodeCommand.AppendChild(nodeAux);

            nodeAux = docDefinitive.CreateElement("textCommand");
            nodeAux.InnerText = textCommand;
            nodeCommand.AppendChild(nodeAux);

            nodeAux = docDefinitive.CreateElement("idApp");
            nodeAux.InnerText = idApp.ToString();
            nodeCommand.AppendChild(nodeAux);

            nodeAux = docDefinitive.CreateElement("idMod");
            nodeAux.InnerText = idMod.ToString();
            nodeCommand.AppendChild(nodeAux);

            docDefinitive.SelectSingleNode("//commands").AppendChild(nodeCommand);

            docDefinitive.Save(XmlFilePath);
        }


        public void EditCommand(int idApp, int idMod, string nameCommand, String textCommand, String oldnameCommand, String oldtextCommand)
        {
            if (String.IsNullOrEmpty(nameCommand) || String.IsNullOrEmpty(textCommand))
            {
                return;
            }

            XmlDocument docDefinitive = new XmlDocument();
            docDefinitive.Load(XmlFilePath);

            XmlNode node = docDefinitive.SelectSingleNode("//command[name='"+oldnameCommand+"'][textCommand='"+oldtextCommand+"'][idApp='"+idApp+"'][idMod='"+idMod+"']");

            node.SelectSingleNode("name").InnerText = nameCommand;
            node.SelectSingleNode("textCommand").InnerText = textCommand;

            docDefinitive.Save(XmlFilePath);

        }

        public void Remove(int idApp, int idMod, string nameCommand, String textCommand, String oldnameCommand, String oldtextCommand)
        {
            XmlDocument docDefinitive = new XmlDocument();
            docDefinitive.Load(XmlFilePath);
            XmlNode node = docDefinitive.SelectSingleNode("//command[name='" + oldnameCommand + "'][textCommand='" + oldtextCommand + "'][idApp='" + idApp + "'][idMod='" + idMod + "']");


            docDefinitive.DocumentElement.RemoveChild(node);
            docDefinitive.Save(XmlFilePath);
        }

        public List<Command> getCommands(int idMod)
        {
            XmlDocument docDefinitive = new XmlDocument();
            docDefinitive.Load(XmlFilePath);
            XmlNodeList nodes = docDefinitive.SelectNodes("//command[idMod='"+idMod+"']");
            List<Command> commands = new List<Command>();
            

            foreach(XmlNode node in nodes)
            {
                Command command = new Command();
                command.IdApp = node.SelectSingleNode("idApp").InnerText;
                command.IdMod = node.SelectSingleNode("idMod").InnerText;
                command.Name = node.SelectSingleNode("name").InnerText;
                command.TextCommand = node.SelectSingleNode("textCommand").InnerText;

                commands.Add(command);
            }

            return commands;
        }

    }
}
