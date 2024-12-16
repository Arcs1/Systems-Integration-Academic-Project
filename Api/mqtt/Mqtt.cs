using System;
using System.Text;
using uPLibrary.Networking.M2Mqtt;

namespace Api.mqtt
{
    public class Mqtt
    {
        public MqttClient mClient { get; set; }
        public string Name { get; set; }
        public string Endpoint { get; set; }


        public void connectToEndpoint(string endpoint)
        {
            mClient = new MqttClient(endpoint);
            mClient.Connect(Guid.NewGuid().ToString());
        }

        public void publish(string message)
        {
            mClient.Publish("application/module", Encoding.UTF8.GetBytes(message));
        }
    }
}