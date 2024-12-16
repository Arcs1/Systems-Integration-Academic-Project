

namespace Test_Application.Models
{
    public class Command
    {
        public string Name { get; set; }
        public string TextCommand { get; set; }
        public string IdMod { get; set; }
        public string IdApp { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

   
}
