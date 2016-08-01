using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCT.Models.Domains
{
    public class Product
    {
        public string Seq { get; set; }
        public string ProductCode { get; set; }
        public string ModelName { get; set; }
        public string ProductName { get; set; }
        public string Unit { get; set; }
        public string Price { get; set; }
        public string Contents { get; set; }
        public string Enabled { get; set; }
        public string Modified { get; set; }
        public string Created { get; set; }
    }
}