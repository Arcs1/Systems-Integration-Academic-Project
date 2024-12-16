

namespace api.Models
{
    public class Subscription : Resource
    {
        public string Name { get; set; }
        public int Parent { get; set; }
        public string Event { get; set; }
        public string Endpoint { get; set; }
    }
}