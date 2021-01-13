using System;
using System.Collections.Generic;
using System.Text;

namespace BaseLibrary.Test.Models
{
    internal class HttpClientCommentResponse
    {
        public int postId { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string body { get; set; }
    }
}
