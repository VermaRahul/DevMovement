using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortableClassLibrary.Model
{


    public class PersonCast
    {
        public int id { get; set; }
        public string title { get; set; }
        public string character { get; set; }
        public string original_title { get; set; }
        public string poster_path { get; set; }
        public string release_date { get; set; }
        public bool adult { get; set; }
    }

    public class PersonCrew
    {
        public int id { get; set; }
        public string title { get; set; }
        public string original_title { get; set; }
        public string department { get; set; }
        public string job { get; set; }
        public string poster_path { get; set; }
        public string release_date { get; set; }
        public bool adult { get; set; }
    }

    public class MovieCredits
    {
        public List<PersonCast> cast { get; set; }
        public List<PersonCrew> crew { get; set; }
    }

    public class DetailedPerson : Person
    {
        public List<string> also_known_as { get; set; }
        public string biography { get; set; }
        public string birthday { get; set; }
        public string deathday { get; set; }
        public string homepage { get; set; }
        public string imdb_id { get; set; }
        public string place_of_birth { get; set; }
        public MovieCredits movie_credits { get; set; }
    }
}
