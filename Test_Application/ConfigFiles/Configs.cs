using RestSharp;


namespace Test_Application.ConfigFiles
{

    public class Configs
    {
        public static string baseURI = @"http://localhost:54975"; //TODO: needs to be updated!

        public Configs()
        {
            RestClient client = new RestClient(baseURI);
        }
    }
}
