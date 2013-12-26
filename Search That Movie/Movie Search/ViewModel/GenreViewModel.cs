using Movie_Search.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Movie_Search.ViewModel
{
    public class GenreViewModel
    {
        private ObservableCollection<Genre> _Genres = new ObservableCollection<Genre>();

        public ObservableCollection<Genre> Genres
        {
            get { return _Genres; }
            set { _Genres = value; }
        }

        public void GetGenres()
        {
            Genre genre;

            genre = new Genre();
            genre.id = 55;
            genre.name = "Comedy";

            Genres.Add(new Genre() { name = "comedy", id = 9 });
            Genres.Add(new Genre() { name = "horror", id = 9 });
            Genres.Add(new Genre() { name = "action", id = 9 });
            Genres.Add(new Genre() { name = "fight", id = 9 });
            Genres.Add(new Genre() { name = "herp", id = 9 });
            Genres.Add(new Genre() { name = "derp", id = 9 });
            Genres.Add(new Genre() { name = "herpderp", id = 9 });
        }
    }
}
