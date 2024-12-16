using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Schema;
using System.Xml;
using System.IO;
using System.Diagnostics;
using System.Xml.Linq;
using System.Web.Hosting;
using Application = api.Models.Application;
using api.Models;
using System.Runtime.InteropServices;

namespace Api.XML
{
    public class HandlerXML
    {
        private bool isValid = true;

        private string validationMessage;
        public string XmlFileTempPath { get; set; }
        public string XmlFilePath { get; set; }
        public string XsdFilePathApplications { get; set; }
        public string XsdFilePathModules { get; set; }
        public string XsdFilePathData { get; set; }
        public string XsdFileSubscribes { get; set; }
        public string XsdFilePathSomiod { get; set; }

        public HandlerXML()
        {
            XmlFileTempPath = HostingEnvironment.MapPath("~/XML/XML_Files/temp.xml");

            XmlFilePath = HostingEnvironment.MapPath("~/XML/XML_Files/applications.xml");

            XsdFilePathSomiod = HostingEnvironment.MapPath("~/XML/Schemas/somiod.xsd");

            XsdFilePathApplications = HostingEnvironment.MapPath("~/XML/Schemas/applications.xsd");

            XsdFilePathModules = HostingEnvironment.MapPath("~/XML/Schemas/modules.xsd");

            XsdFilePathData = HostingEnvironment.MapPath("~/XML/Schemas/data.xsd");

            XsdFileSubscribes = HostingEnvironment.MapPath("~/XML/Schemas/subscribes.xsd");
        }
        
        public string ValidationMessage
        {
            get { return validationMessage; }
        }

        #region Validate XML String Request

        public bool IsValidStringXML(string xmlStr)
        {
            try
            {
                if (!string.IsNullOrEmpty(xmlStr))
                {
                    System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                    xmlDoc.LoadXml(xmlStr);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Xml.XmlException)
            {
                return false;
            }
        }
        #endregion

        #region Validate XML with XML Schema (xsd)
        
        public bool ValidateXML(string file, string XsdFilePath)
        {
            isValid = true;
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(file);
                ValidationEventHandler eventHandler = new ValidationEventHandler(MyValidateMethod);
                doc.Schemas.Add(null, XsdFilePath);
                doc.Validate(eventHandler);
            }
            catch (XmlException ex)
            {
                isValid = false;
                validationMessage = string.Format("ERROR: {0}", ex.ToString());
            }
            return isValid;
        }

        private void MyValidateMethod(object sender, ValidationEventArgs args)
        {
            isValid = false;
            switch (args.Severity)
            {
                case XmlSeverityType.Error:
                    validationMessage = string.Format("ERROR: {0}", args.Message);
                    break;
                case XmlSeverityType.Warning:
                    validationMessage = string.Format("WARNING: {0}", args.Message);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Methods for Applications XML
        
        // --> Primeiro a ser chamado nas applications
        public bool IsValidApplicationSchemaXML(string rawXml)
        {
            XmlDocument docTemp = new XmlDocument();
            docTemp.Load(XmlFileTempPath);
            
            XmlNode node = docTemp.SelectSingleNode("//somiod");
            
            node.InnerXml += rawXml;
            
            docTemp.Save(XmlFileTempPath);

            // If valid Schema in XML 
            if (ValidateXML(XmlFileTempPath, XsdFilePathApplications))
            {
                return true;
            }

            Debug.Print("[DEBUG] 'Invalid Schema in XML' | IsValidApplicationsSchemaXML() in HandlerXML");
            RefreshTempFile();
            return false;               
        }

        // --> Segundo a ser chamado na application
        //Neste metodo vamos ver o que veio no request
        public string DealRequestApplication()
        {
            XmlDocument docTemp = new XmlDocument();
            docTemp.Load(XmlFileTempPath);

            string appName = docTemp.SelectSingleNode("//somiod/application/name").InnerText;

            Debug.Print("[DEBUG] 'App name: " + appName + "' | DealRequestApplication() in HandlerXML");
            RefreshTempFile();

            return appName;
        }

        // --> Terceiro a ser chamado na application
        public void AddApplication(Application application)
        {
            XmlDocument docDefinitive = new XmlDocument();
            docDefinitive.Load(XmlFilePath);

            XmlNode node = docDefinitive.CreateElement("application");

            XmlNode nodeAux = docDefinitive.CreateElement("id");
            nodeAux.InnerText = application.Id.ToString();
            node.AppendChild(nodeAux);

            nodeAux = docDefinitive.CreateElement("creation_dt");
            nodeAux.InnerText = application.Creation_dt.ToString();
            node.AppendChild(nodeAux);

            nodeAux = docDefinitive.CreateElement("name");
            nodeAux.InnerText = application.Name;
            node.AppendChild(nodeAux);

            docDefinitive.SelectSingleNode("//applications").AppendChild(node);

            docDefinitive.Save(XmlFilePath);
            Debug.Print("[DEBUG] 'Applications inserted with success' | AddApplication() in HandlerXML");
        }

        //Neste metodo vamos Editar a aplicação
        public void UpdateApplication(Application application)
        {
            XmlDocument docDefinitive = new XmlDocument();
            docDefinitive.Load(XmlFilePath);

            XmlNode node = docDefinitive.SelectSingleNode("//application[id ='"+ application.Id +"']");

            node.SelectSingleNode("name").InnerText = application.Name;

            docDefinitive.Save(XmlFilePath);
            Debug.Print("[DEBUG] 'Applications update with success' | UpdateApplication() in HandlerXML");
        }

        //Neste metodo vamos Eliminar a aplicação
        public void DeleteApplication(Application application)
        {
            
            XmlDocument docDefinitive = new XmlDocument();
            docDefinitive.Load(XmlFilePath);

            XmlNode node = docDefinitive.SelectSingleNode("//application[id ='" + application.Id + "']");

            docDefinitive.DocumentElement.RemoveChild(node);

            docDefinitive.Save(XmlFilePath);

            Debug.Print("[DEBUG] 'Applications delete with success' | DeleteApplication() in HandlerXML");
        }
        #endregion

        #region Methods for Modules XML

        // --> Primeiro a ser chamado nos modules
        public bool IsValidModulesSchemaXML(string rawXml)
        {
            XmlDocument docTemp = new XmlDocument();
            docTemp.Load(XmlFileTempPath);
            XmlNode node = docTemp.SelectSingleNode("//somiod");


            node.InnerXml += rawXml;

            docTemp.Save(XmlFileTempPath);

            // If valid Schema in XML 
            if (ValidateXML(XmlFileTempPath, XsdFilePathModules))
            {
                return true;
            }

            Debug.Print("[DEBUG] 'Invalid Schema in XML' | IsValidModulesSchemaXML() in HandlerXML");
            RefreshTempFile();
            return false;
        }

        // --> Segundo a ser chamado no module
        //Neste metodo vamos ver o que veio no request
        public string DealRequestModule()
        {
            XmlDocument docTemp = new XmlDocument();
            docTemp.Load(XmlFileTempPath);

            string moduleName = docTemp.SelectSingleNode("//somiod/module/name").InnerText;

            Debug.Print("[DEBUG] 'Module name: " + moduleName + "' | DealRequestModule() in HandlerXML");
            RefreshTempFile();

            return moduleName;
        }

        // --> Terceiro a ser chamado na application
        public void AddModule(Module module)
        {
            XmlDocument docDefinitive = new XmlDocument();
            docDefinitive.Load(XmlFilePath);
            XmlNode nodeApplication = docDefinitive.SelectSingleNode("//applications/application[id='" + module.Parent + "']/modules");

            //Inserir tag <modules>
            if (nodeApplication == null)
            {
                nodeApplication = docDefinitive.CreateElement("modules");
                docDefinitive.SelectSingleNode("//applications/application[id='" + module.Parent + "']").AppendChild(nodeApplication);
            }

            //Inserir tag <module>
            XmlNode xmlModule = docDefinitive.CreateElement("module");

            XmlNode nodeAux = docDefinitive.CreateElement("id");
            nodeAux.InnerText = module.Id.ToString();
            xmlModule.AppendChild(nodeAux);

            nodeAux = docDefinitive.CreateElement("creation_dt");
            nodeAux.InnerText = module.Creation_dt.ToString();
            xmlModule.AppendChild(nodeAux);

            nodeAux = docDefinitive.CreateElement("name");
            nodeAux.InnerText = module.Name;
            xmlModule.AppendChild(nodeAux);

            nodeAux = docDefinitive.CreateElement("parent");
            nodeAux.InnerText = module.Parent.ToString();
            xmlModule.AppendChild(nodeAux);

            docDefinitive.SelectSingleNode("//applications/application[id='" + module.Parent + "']/modules").AppendChild(xmlModule);

            docDefinitive.Save(XmlFilePath);
        }

        public void UpdateModule(Module module)
        {
            
            XmlDocument docDefinitive = new XmlDocument();
            docDefinitive.Load(XmlFilePath);

            XmlNode node = docDefinitive.SelectSingleNode("//application[id ='" + module.Parent + "']/modules/module[id ='" + module.Id + "']");

            node.SelectSingleNode("name").InnerText = module.Name;

            docDefinitive.Save(XmlFilePath);
            Debug.Print("[DEBUG] 'Module update with success' | UpdateModule() in HandlerXML");

        }

        public void DeleteModule(Module module)
        {
            
            XmlDocument docDefinitive = new XmlDocument();
            docDefinitive.Load(XmlFilePath);

            // Obter No Pai
            XmlNode nodeDad = docDefinitive.SelectSingleNode("//application[id ='" + module.Parent + "']/modules");
            int numMod = nodeDad.ChildNodes.Count;
            // Obter No a eliminar
            XmlNode node = docDefinitive.SelectSingleNode("//module[id ='" + module.Id + "']");
          
            // Eliminar node
            node.ParentNode.RemoveChild(node);
            if(numMod == 1)
            {
                nodeDad.ParentNode.RemoveChild(nodeDad);
            }
            
            docDefinitive.Save(XmlFilePath);
            Debug.Print("[DEBUG] 'Module delete with success' | DeleteModule() in HandlerXML");

        }

        #endregion

        #region Methods for Subscriptions XML
        public bool IsValidSubscriptionsSchemaXML(string rawXml)
        {
            XmlDocument docTemp = new XmlDocument();
            docTemp.Load(XmlFileTempPath);
            XmlNode node = docTemp.SelectSingleNode("//somiod");

            node.InnerXml += rawXml;

            docTemp.Save(XmlFileTempPath);

            // If valid Schema in XML 
            if (ValidateXML(XmlFileTempPath, XsdFileSubscribes))
            {
                return true;
            }

            Debug.Print("[DEBUG] 'Invalid Schema in XML' | IsValidSubscriptionsSchemaXML() in HandlerXML");
            RefreshTempFile();
            return false;
        }

        //Neste metodo vamos ver o que veio no request
        public Subscription DealRequestSubscription()
        {
            XmlDocument docTemp = new XmlDocument();
            Subscription subscription = new Subscription();
            docTemp.Load(XmlFileTempPath);

            XmlNode node = docTemp.SelectSingleNode("//somiod/subscription");
            if (node.SelectSingleNode("name") != null)
            {
                subscription.Name = node.SelectSingleNode("name").InnerText;
            } 
            
            if(node.SelectSingleNode("event") != null)
            {
                subscription.Event = node.SelectSingleNode("event").InnerText;
            }
            
            if (node.SelectSingleNode("endpoint") != null)
            {
                subscription.Endpoint = node.SelectSingleNode("endpoint").InnerText;
            }
            
            RefreshTempFile();
            return subscription;
        }

        public void AddSubscription(Subscription subscription)
        {
            XmlDocument docDefinitive = new XmlDocument();
            docDefinitive.Load(XmlFilePath);
            XmlNode nodeSubscriptions = docDefinitive.SelectSingleNode("//applications/application/modules/module[id='" + subscription.Parent + "']/subscriptions");

            //Inserir tag <modules>
            if (nodeSubscriptions == null)
            {
                nodeSubscriptions = docDefinitive.CreateElement("subscriptions");
                docDefinitive.SelectSingleNode("//applications/application/modules/module[id='" + subscription.Parent + "']").AppendChild(nodeSubscriptions);
            }

            //Inserir tag <module>
            XmlNode xmlSubscription = docDefinitive.CreateElement("subscription");

            XmlNode nodeAux = docDefinitive.CreateElement("id");
            nodeAux.InnerText = subscription.Id.ToString();
            xmlSubscription.AppendChild(nodeAux);

            nodeAux = docDefinitive.CreateElement("creation_dt");
            nodeAux.InnerText = subscription.Creation_dt.ToString();
            xmlSubscription.AppendChild(nodeAux);

            nodeAux = docDefinitive.CreateElement("name");
            nodeAux.InnerText = subscription.Name;
            xmlSubscription.AppendChild(nodeAux);

            nodeAux = docDefinitive.CreateElement("parent");
            nodeAux.InnerText = subscription.Parent.ToString();
            xmlSubscription.AppendChild(nodeAux);

            nodeAux = docDefinitive.CreateElement("event");
            nodeAux.InnerText = subscription.Event.ToString();
            xmlSubscription.AppendChild(nodeAux);

            nodeAux = docDefinitive.CreateElement("endpoint");
            nodeAux.InnerText = subscription.Endpoint.ToString();
            xmlSubscription.AppendChild(nodeAux);

            docDefinitive.SelectSingleNode("//applications/application/modules/module[id='" + subscription.Parent + "']/subscriptions").AppendChild(xmlSubscription);

            docDefinitive.Save(XmlFilePath);
        }

        public void DeleteSubscription(Subscription subscription)
        {

            XmlDocument docDefinitive = new XmlDocument();
            docDefinitive.Load(XmlFilePath);
            XmlNode subs = docDefinitive.SelectSingleNode("//module[id ='" + subscription.Parent + "']/subscriptions");
            int numSubs = subs.ChildNodes.Count;
            XmlNode node = docDefinitive.SelectSingleNode("//subscription[id ='" + subscription.Id + "']");
            node.ParentNode.RemoveChild(node);
            if(numSubs == 1)
            {
                subs.ParentNode.RemoveChild(subs);
            }
            docDefinitive.Save(XmlFilePath);

            Debug.Print("[DEBUG] 'Subscriptions delete with success' | DeleteSubscription() in HandlerXML");
        }


        #endregion

        #region Methods for Data XML
        public bool IsValidDataSchemaXML(string rawXml)
        {
            XmlDocument docTemp = new XmlDocument();
            docTemp.Load(XmlFileTempPath);
            XmlNode node = docTemp.SelectSingleNode("//somiod");

            node.InnerXml += rawXml;

            docTemp.Save(XmlFileTempPath);

            // If valid Schema in XML 
            if (ValidateXML(XmlFileTempPath, XsdFilePathData))
            {
                return true;
            }
            
            Debug.Print("[DEBUG] 'Invalid Schema in XML' | IsValidDataSchemaXML() in HandlerXML");
            RefreshTempFile();
            return false;
        }

        //Neste metodo vamos ver o que veio no request
        public Data DealRequestData()
        {
            XmlDocument docTemp = new XmlDocument();
            Data data = new Data();
            docTemp.Load(XmlFileTempPath);

            XmlNode node = docTemp.SelectSingleNode("//somiod/data");
            
            if (node.SelectSingleNode("content") != null)
            {
                data.Content = node.SelectSingleNode("content").InnerText;
            }

            RefreshTempFile();
            return data;
        }

        public void AddData(Data data)
        {
            XmlDocument docDefinitive = new XmlDocument();
            docDefinitive.Load(XmlFilePath);
            XmlNode nodeDatas = docDefinitive.SelectSingleNode("//applications/application/modules/module[id='" + data.Parent + "']/datas");

            //Inserir tag <modules>
            if (nodeDatas == null)
            {
                nodeDatas = docDefinitive.CreateElement("datas");
                docDefinitive.SelectSingleNode("//applications/application/modules/module[id='" + data.Parent + "']").AppendChild(nodeDatas);
            }

            //Inserir tag <module>
            XmlNode xmlData = docDefinitive.CreateElement("data");

            XmlNode nodeAux = docDefinitive.CreateElement("id");
            nodeAux.InnerText = data.Id.ToString();
            xmlData.AppendChild(nodeAux);

            nodeAux = docDefinitive.CreateElement("creation_dt");
            nodeAux.InnerText = data.Creation_dt.ToString();
            xmlData.AppendChild(nodeAux);

            nodeAux = docDefinitive.CreateElement("Content");
            nodeAux.InnerText = data.Content;
            xmlData.AppendChild(nodeAux);

            nodeAux = docDefinitive.CreateElement("parent");
            nodeAux.InnerText = data.Parent.ToString();
            xmlData.AppendChild(nodeAux);

            docDefinitive.SelectSingleNode("//applications/application/modules/module[id='" + data.Parent + "']/datas").AppendChild(xmlData);

            docDefinitive.Save(XmlFilePath);
        }

        public void DeleteData(Data data)
        {
            XmlDocument docDefinitive = new XmlDocument();
            docDefinitive.Load(XmlFilePath);
            
            XmlNode datas = docDefinitive.SelectSingleNode("//module[id ='" + data.Parent + "']/datas");
            int numDatas = datas.ChildNodes.Count;
            XmlNode node = docDefinitive.SelectSingleNode("//data[id ='" + data.Id + "']");
            node.ParentNode.RemoveChild(node);
            if (numDatas == 1)
            {
                datas.ParentNode.RemoveChild(datas);
            }
            docDefinitive.Save(XmlFilePath);

            Debug.Print("[DEBUG] 'Datas delete with success' | DeleteData() in HandlerXML");
        }
        #endregion

        #region Methods for XML
        public void RefreshTempFile()
        {
            XmlDocument docTemp = new XmlDocument();
            docTemp.Load(XmlFileTempPath);

            XmlNode node = docTemp.SelectSingleNode("//somiod");

            while (node.HasChildNodes)
            {
                node.RemoveChild(node.FirstChild);
            }

            docTemp.Save(XmlFileTempPath);
        }
        #endregion
    }
}