namespace BaseLibrary.Tool.Models.HttpClientProxy
{
    public class HttpClientProxy
    {
        public HttpClientProxy()
        {
            UseDefaultCredentials = false;
            NeedCredential = false;
            NeedServerAuthentication = false;
        }

        public string Url { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public HttpClientCredentials Credentials { get; set; }
        public bool NeedCredential { get; set; }
        public HttpClientHandler HttpClientHandler { get; set; }
        public bool NeedServerAuthentication { get; set; }
    }
}
