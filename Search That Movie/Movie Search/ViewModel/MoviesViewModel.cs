using Newtonsoft.Json;
using PortableClassLibrary.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Windows;

namespace Movie_Search.ViewModel
{
    public class MoviesViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Movie> _Movies = new ObservableCollection<Movie>();

        public ObservableCollection<Movie> Movies
        {
            get { return _Movies; }
            set { _Movies = value; }
        }

        private bool _movieisLoading;

        public bool movieisLoading
        {
            get { return _movieisLoading; }
            set
            {
                _movieisLoading = value;
                this.OnPropertyChanged(new PropertyChangedEventArgs("movieisLoading"));
            }
        }

        private int moviepage_number;

        private WebClient _moviewebClient = new WebClient();

        private Movies movies;

        public void GetResults(string _type)
        {
            GetMovieResults(_type);
        }

        private string type;

        private void GetMovieResults(string _type)
        {
            if (movieisLoading)
                return;
            if (movies == null || moviepage_number < movies.total_pages)
            {
                movieisLoading = true;
                moviepage_number++;
                if (String.IsNullOrEmpty(type))
                    type = String.Copy(_type);
                string webaddr = App.Current.MovieTypeAPI_1 + type + App.Current.MovieTypeAPI_2 + moviepage_number;
                _moviewebClient.DownloadStringCompleted += moviewebClient_DownloadStringCompleted;
                _moviewebClient.DownloadStringAsync(new Uri(webaddr));
            }
        }

        public void FetchNextPage()
        {
            GetResults(type);
        }

        private void moviewebClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            _moviewebClient.DownloadStringCompleted -= moviewebClient_DownloadStringCompleted;
            if (e == null)
            {
                MessageBox.Show("Seems like the internet connection is down or the website refused the request.\n:(\nWe are sad too that you couldnt continue.", "Error", MessageBoxButton.OK);
                movieisLoading = false;
                return; //TODO: give a error here
            }
            string json = e.Result;
            if (!string.IsNullOrEmpty(json))
            {
                movies = JsonConvert.DeserializeObject<Movies>(json);
                foreach (var v in movies.results)
                    Movies.Add(v);
                movieisLoading = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            // get handler (usually a local event variable or just the event)
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, e);
            }
        }

    }
}
