using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public double MobileNumber { get; set; }
    }
}
