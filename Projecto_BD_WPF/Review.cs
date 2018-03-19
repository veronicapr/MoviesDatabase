using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto_BD_WPF
{
    class Review
    {
        public int id;
        public int rating;
        public string review;
        public int movie_id;
        public DateTime date;
        public string username;

        public Review()
        {

        }

        public Review(int id, int rating, string review, int movie_id, DateTime date, string username)
        {
            this.id = id;
            this.rating = rating;
            this.review = review;
            this.movie_id = movie_id;
            this.date = date;
            this.username = username;
        }
    }
}
