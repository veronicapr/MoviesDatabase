using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto_BD_WPF
{
    class Writer
    {
        public int ssn;
        public string name;
        public DateTime bdate;
        public int rank;

        public Writer()
        {
        }

        public Writer(int ssn, string name, DateTime birth_date, int rank)
        {
            this.ssn = ssn;
            this.name = name;
            this.bdate = birth_date;
            this.rank = rank;
        }
    }

}
