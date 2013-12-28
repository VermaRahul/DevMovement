using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortableClassLibrary.Model
{

    public class Dates
    {
        public string minimum { get; set; }
        public string maximum { get; set; }
    }

    public class Upcoming
    {
        public Dates dates { get; set; }
        public int page { get; set; }
        public List<Movie> results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }

    public class NowPlaying
    {
        public Dates dates { get; set; }
        public int page { get; set; }
        public List<Movie> results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }

    public class Popular
    {
        public int page { get; set; }
        public List<Movie> results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }

    public class TopRated
    {
        public int page { get; set; }
        public List<Movie> results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }

    public class HomePageItems
    {
        public int status_code { get; set; }
        public string status_message { get; set; }
        public Upcoming upcoming { get; set; }
        public NowPlaying now_playing { get; set; }
        public Popular popular { get; set; }
        public TopRated top_rated { get; set; }
    }
}
