

namespace Test_Application.Models
{
    public class SubscriptionModel : ResourceModel
    {
        public string Name { get; set; }
        public int Parent { get; set; }
        public string Event { get; set; }
        public string Endpoint { get; set; }
    }
}