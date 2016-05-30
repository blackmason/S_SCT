using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCT.Models.Domains
{
    public class Board
    {
        public string No { get; set; }
        public string Category { get; set; }
        public string Fixed { get; set; }
        public string Subject { get; set; }
        public string Contents { get; set; }
        public string Created { get; set; }
    }
}