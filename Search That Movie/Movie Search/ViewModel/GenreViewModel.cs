using Movie_Search.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
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

            WebClient webClient = new WebClient();
            webClient.DownloadStringCompleted += webClient_DownloadStringCompleted;
            webClient.DownloadStringAsync(new Uri("https://api.themoviedb.org/3/genre/list?api_key=c9271ad7bfc80b904833986c70d34b6f&language=en"));
        }

        private void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            string json = e.Result;
            if (!string.IsNullOrEmpty(json))
            {
                Genres results = JsonConvert.DeserializeObject<Genres>(json);
                foreach (var v in results.genres)
                    Genres.Add(v);
            }
        }
    }
}
