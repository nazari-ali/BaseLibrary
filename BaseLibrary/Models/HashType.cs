﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BaseLibrary.Models
{
    public enum HashType
    {
        HMAC, 
        HMACMD5,
        HMACSHA1,
        HMACSHA256,
        HMACSHA384, 
        HMACSHA512,
        MD5, 
        SHA1, 
        SHA256, 
        SHA384, 
        SHA512
    }
}
