using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCT.Models.Domains
{
    public class Menu
    {
        public string Code { get; set; }
        public string ParentCode { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Enabled { get; set; }
        public string Created { get; set; }
    }
}