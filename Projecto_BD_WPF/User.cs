using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto_BD_WPF
{
    class User
    {
        public string username;
        public string name;
        public DateTime bdate;
        public string email;
        public string country;

        public User()
        {

        }
        public User(string username, string name, DateTime bdate, string email, string country)
        {
            this.username = username;
            this.name = name;
            this.bdate = bdate;
            this.email = email;
            this.country = country;
        }
    }
}
