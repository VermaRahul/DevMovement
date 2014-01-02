using Movie_Search.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortableClassLibrary.Model
{
    public class BelongsToCollection
    {
        public int id { get; set; }
        public string name { get; set; }
        public string poster_path { get; set; }
        public string backdrop_path { get; set; }
    }

    public class ProductionCompany
    {
        public string name { get; set; }
        public int id { get; set; }
    }

    public class ProductionCountry
    {
        public string iso_3166_1 { get; set; }
        public string name { get; set; }
    }

    public class SpokenLanguage
    {
        public string iso_639_1 { get; set; }
        public string name { get; set; }
    }

    public class SimilarMovies : Movies
    {
    }

    public class Title
    {
        public string iso_3166_1 { get; set; }
        public string title { get; set; }
    }

    public class AlternativeTitles
    {
        public List<Title> titles { get; set; }
    }

    public class Keyword
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Keywords
    {
        public List<Keyword> keywords { get; set; }
    }

    public class Country
    {
        public string iso_3166_1 { get; set; }
        public string certification { get; set; }
        public string release_date { get; set; }
    }

    public class Releases
    {
        public List<Country> countries { get; set; }
    }

    public class Source
    {
        public string size { get; set; }
        public string source { get; set; }
    }

    public class Quicktime
    {
        public string name { get; set; }
        public List<Source> sources { get; set; }
    }

    public class Youtube
    {
        public string name { get; set; }
        public string size { get; set; }
        public string source { get; set; }
        public string type { get; set; }
    }

    public class Trailers
    {
        public List<Quicktime> quicktime { get; set; }
        public List<Youtube> youtube { get; set; }
    }

    public class Result2
    {
        public string id { get; set; }
        public string author { get; set; }
        public string content { get; set; }
        public string url { get; set; }
    }

    public class Reviews
    {
        public int page { get; set; }
        public List<Result2> results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }

    public class MovieCast
    {
        public int id { get; set; }
        public string name { get; set; }
        public string character { get; set; }
        public int order { get; set; }
        public int cast_id { get; set; }
        public string profile_path { get; set; }
    }

    public class MovieCrew
    {
        public int id { get; set; }
        public string name { get; set; }
        public string department { get; set; }
        public string job { get; set; }
        public string profile_path { get; set; }
    }

    public class Credits
    {
        public List<MovieCast> cast { get; set; }
        public List<MovieCrew> crew { get; set; }
    }

    public class Backdrop
    {
        public double aspect_ratio { get; set; }
        public string file_path { get; set; }
        public int height { get; set; }
        public string iso_639_1 { get; set; }
        public object vote_average { get; set; }
        public int vote_count { get; set; }
        public int width { get; set; }
    }

    public class Poster
    {
        public double aspect_ratio { get; set; }
        public string file_path { get; set; }
        public int height { get; set; }
        public string iso_639_1 { get; set; }
        public object vote_average { get; set; }
        public int vote_count { get; set; }
        public int width { get; set; }
    }

    public class Images
    {
        public List<Backdrop> backdrops { get; set; }
        public List<Poster> posters { get; set; }
    }

    public class DetailedMovie
    {
        public bool adult { get; set; }
        public string backdrop_path { get; set; }
        public BelongsToCollection belongs_to_collection { get; set; }
        public int budget { get; set; }
        public List<Genre> genres { get; set; }
        public string combined_genres { get; set; }
        public string homepage { get; set; }
        public int id { get; set; }
        public string imdb_id { get; set; }
        public string original_title { get; set; }
        public string overview { get; set; }
        public string popularity { get; set; }
        public string poster_path { get; set; }
        public List<ProductionCompany> production_companies { get; set; }
        public string combined_companies { get; set; }
        public List<ProductionCountry> production_countries { get; set; }
        public string combined_countries { get; set; }
        public string release_date { get; set; }
        public int revenue { get; set; }
        public int runtime { get; set; }
        public List<SpokenLanguage> spoken_languages { get; set; }
        public string combined_languages { get; set; }
        public string status { get; set; }
        public string tagline { get; set; }
        public string title { get; set; }
        public double vote_average { get; set; }
        public int vote_count { get; set; }
        public SimilarMovies similar_movies { get; set; }
        public AlternativeTitles alternative_titles { get; set; }
        public Keywords keywords { get; set; }
        public Releases releases { get; set; }
        public Trailers trailers { get; set; }
        public Reviews reviews { get; set; }
        public Credits credits { get; set; }
        public Images images { get; set; }
    }
}
