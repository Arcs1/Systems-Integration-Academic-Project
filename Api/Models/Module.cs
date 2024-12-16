
namespace api.Models
{
    public class Module : Resource
    {
        public string Name { get; set; }
        public int Parent { get; set; }
    }
}