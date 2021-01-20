namespace BaseLibrary.Tool.Models.HttpClientProxy
{
    public class HttpClientHandler
    {
        public HttpClientHandler()
        {
            PreAuthenticate = false;
            UseDefaultCredentials = false;
        }

        public bool PreAuthenticate { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public HttpClientCredentials Credentials { get; set; }
    }
}
