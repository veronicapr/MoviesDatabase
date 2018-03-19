using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto_BD_WPF
{
    class Actor
    {
        public int ssn;
        public DateTime Bdate;
        public string name;
        public string bio;
        public int rank;

        public Actor() 
        {
            Bdate = new DateTime();
        }
    }
}
