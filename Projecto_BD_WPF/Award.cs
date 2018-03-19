using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto_BD_WPF
{
    class Award
    {
        public int year;
        public string type;
        public string designation;
        public int movie_id;

        public Award()
        {

        }

        public Award(int year, string type, string designation, int movie_id)
        {
            this.year = year;
            this.type = type;
            this.designation = designation;
            this.movie_id = movie_id;
        }
    }
}
