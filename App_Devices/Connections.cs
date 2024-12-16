using App_Devices.Models;
using RestSharp;
using System.Collections.Generic;
using System.Windows.Forms;

namespace App_Devices
{
    public partial class Connections : Form
    {
        List<SubscriptionModel> subscriptions = null;
        int idModule;
        string baseURI = @"http://localhost:54975"; //TODO: needs to be updated!
        RestClient client = null;

        public Connections(List<SubscriptionModel> subscriptionsReceived, int idModule)
        {
            InitializeComponent();
            this.idModule = idModule;
            subscriptions = subscriptionsReceived;
            client = new RestClient(baseURI);

            foreach (var subscription in getConnectedSubs())
            {
                string status = subscription.Connection;
         
                lb_connections.Items.Add(subscription.Name + "  (" + subscription.Endpoint + ")   --  " + status);
            }

        }
        private List<SubscriptionModel> getAllSubscriptions()
        {
            var request = new RestRequest("/api/somiod/modules/{id}/subscriptions", Method.Get);
            request.AddUrlSegment("id", idModule);         
            request.RequestFormat = DataFormat.Xml;

            var response = client.Execute<List<SubscriptionModel>>(request);

            if (response.StatusCode != System.Net.HttpStatusCode.NotFound)
            {
                return response.Data;
            }
            //MessageBox.Show("Error: " + response.StatusCode);
            return null;
        }

        private List<SubscriptionModel> getConnectedSubs()
        {
            List<SubscriptionModel> allSubs = getAllSubscriptions();

            foreach (var subscription in allSubs)
            {
                foreach (var subConnected in subscriptions)
                {
                    if (subConnected.Id == subscription.Id)
                    {
                        subscription.Connection = subConnected.Connection;                       
                    }
                }
            }
            return allSubs;
        }

    }
}
