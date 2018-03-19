using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto_BD_WPF
{
    class Trailer
    {
        public int id;
        public DateTime date;
        public string title;
        public TimeSpan duration;
        public string language;
        public int movieID;

        public Trailer()
        {
            date = new DateTime();
            duration = new TimeSpan();
        }
    }
}
