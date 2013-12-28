using Newtonsoft.Json;
using PortableClassLibrary.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Windows;

namespace Movie_Search.ViewModel
{
    public class SearchResultPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Movie> _Movies = new ObservableCollection<Movie>();

        public ObservableCollection<Movie> Movies
        {
            get { return _Movies; }
            set { _Movies = value; }
        }

        private bool _isLoading;

        public bool isLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                this.OnPropertyChanged(new PropertyChangedEventArgs("isLoading"));
            }
        }

        private int page_number;

        private WebClient _webClient = new WebClient();

        private Movies movies;

        public void GetResults(string query)
        {

            GetMovieResults(query);
        }

        private string que;

        private void GetMovieResults(string query)
        {
            if (isLoading)
                return;
            if (movies == null || page_number < movies.total_pages)
            {
                isLoading = true;
                page_number++;
                if (String.IsNullOrEmpty(que))
                    que = String.Copy(query);
                string webaddr = App.Current.SearchMoviesAPI + page_number.ToString() + "&query=" + query;
                _webClient.DownloadStringCompleted += webClient_DownloadStringCompleted;
                _webClient.DownloadStringAsync(new Uri(webaddr));
            }
        }

        public void FetchNextMoviePage()
        {
            GetMovieResults(que);
        }

        private void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            _webClient.DownloadStringCompleted -= webClient_DownloadStringCompleted;
            if (e == null)
            {
                MessageBox.Show("Seems like the internet connection is down or the website refused the request.\n:(\nWe are sad too that you couldnt continue.", "Error", MessageBoxButton.OK);
                isLoading = false;
                return; //TODO: give a error here
            }
            string json = e.Result;
            if (!string.IsNullOrEmpty(json))
            {
                movies = JsonConvert.DeserializeObject<Movies>(json);
                foreach (var v in movies.results)
                    Movies.Add(v);
                isLoading = false;
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
