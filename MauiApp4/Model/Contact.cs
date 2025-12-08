using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp4.Model
{
    class NContact
    {
        public string Icon { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public string DisplayImage =>
            !string.IsNullOrEmpty(Icon) ? Icon : "user.png";

        public NContact(string name, string phone, string email, string image = null)
        {
            Name = name;
            Phone = phone;
            Email = email;
            Icon = image;

        }
    }
}
