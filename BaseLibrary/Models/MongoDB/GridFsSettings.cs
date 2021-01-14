using System;
using System.Collections.Generic;
using System.Text;

namespace BaseLibrary.Models.MongoDB
{
    public class GridFsSettings
    {
        public GridFsSettings()
        {
            BucketName = "fs";
            BucketSize = 262144;
        }

        public string BucketName { get; set; }
        public int BucketSize { get; set; }
    }
}
