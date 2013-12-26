using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Movie_Search.Model
{
    public class Genre
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Genres
    {
        public List<Genre> genres { get; set; }
    }
}
