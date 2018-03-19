using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto_BD_WPF
{
    class Movie
    {
        public int movieId;
        public TimeSpan duration;
        public string description;
        public string age_restriction;
        public int rating;
        public int studio_id;
        public int director_ssn;

        public Movie() 
        {
 
        }

        public Movie(int movieId, TimeSpan duration, string description, string age_restriction, int rating, int studio_id, int director_ssn) 
        {
            this.movieId = movieId;
            this.duration = duration;
            this.description = description;
            this.age_restriction = age_restriction;
            this.rating = rating;
            this.studio_id = studio_id;
            this.director_ssn = director_ssn;

        }

        public Movie(int movieId, string duration, string description, string age_restriction, int rating, int studio_id, int director_ssn)
        {
            this.movieId = movieId;
            this.description = description;
            this.age_restriction = age_restriction;
            this.rating = rating;
            this.studio_id = studio_id;
            this.director_ssn = director_ssn;

            char[] d = {':'};
            string[] time = duration.Split(d);
            this.duration = new TimeSpan(Convert.ToInt32(time[0]), Convert.ToInt32(time[1]), Convert.ToInt32(time[2]));
            
        }
    }
}
