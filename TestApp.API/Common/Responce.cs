using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace TestApp.API.Common
{
    public enum MessageType { 
        Success = 1,
        Warning = 2,
        Fail = 3
    }
    public class Responce
    {
        public string Message { get; set; }
        public object Data { get; set; }
        public MessageType MessageType { get; set; }
        public int TotalCount { get; set; } = 0;
    }
}