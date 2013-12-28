using System.Collections.Generic;

namespace PortableClassLibrary.Model
{
    public class DetailedGenre
    {
        public int id { get; set; }
        public int page { get; set; }
        public List<Movie> results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }
}
