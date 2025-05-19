using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMB.Models
{
    public class User
    {
        public string Email { get; set; }
        public string PasswordHashWithSalt { get; set; }
    }

}
