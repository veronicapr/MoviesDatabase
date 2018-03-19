using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto_BD_WPF
{
    class Release
    {
        public int id;
        public DateTime date;
        public string title;
        public string country;
        public int movieID;

        public Release()
        {
            date = new DateTime();
        }
    }
}
