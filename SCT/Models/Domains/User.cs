using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCT.Models.Domains
{
    public class User
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public string Role { get; set; }
        public string Enabled { get; set; }
        public string Created { get; set; }
    }
}