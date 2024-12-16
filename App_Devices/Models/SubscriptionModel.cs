

namespace App_Devices.Models
{
    public class SubscriptionModel : ResourceModel
    {
        public string Name { get; set; }
        public int Parent { get; set; }
        public string Event { get; set; }
        public string Endpoint { get; set; }
        public string Connection { get; set; }

        public SubscriptionModel()
        {
            Connection = "Connecting...";
        }

    }
}

        