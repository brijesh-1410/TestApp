using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestApp.API.Common
{
    public class GridParam
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public string searchString { get; set; }
        public string orderByField { get; set; }
        public string orderBy { get; set; }
    }
}