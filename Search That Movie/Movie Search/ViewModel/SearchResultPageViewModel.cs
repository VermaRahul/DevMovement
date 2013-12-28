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

        private ObservableCollection<Person> _People = new ObservableCollection<Person>();

        public ObservableCollection<Person> People
        {
            get { return _People; }
            set { _People = value; }
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

        private bool _peopleisLoading;

        public bool peopleisLoading
        {
            get { return _peopleisLoading; }
            set
            {
                _peopleisLoading = value;
                this.OnPropertyChanged(new PropertyChangedEventArgs("peopleisLoading"));
            }
        }

        private int moviepage_number;

        private int peoplepage_number;

        private WebClient _moviewebClient = new WebClient();

        private WebClient _peoplewebClient = new WebClient();

        private Movies movies;

        private People people;

        public void GetResults(string query)
        {
            GetMovieResults(query);
            GetPeopleResults(query);
        }

        private string que;

        private void GetMovieResults(string query)
        {
            if (movieisLoading)
                return;
            if (movies == null || moviepage_number < movies.total_pages)
            {
                movieisLoading = true;
                moviepage_number++;
                if (String.IsNullOrEmpty(que))
                    que = String.Copy(query);
                string webaddr = App.Current.SearchMoviesAPI + moviepage_number.ToString() + "&query=" + query;
                _moviewebClient.DownloadStringCompleted += moviewebClient_DownloadStringCompleted;
                _moviewebClient.DownloadStringAsync(new Uri(webaddr));
            }
        }

        private void GetPeopleResults(string query)
        {
            if (peopleisLoading)
                return;
            if (people == null || peoplepage_number < people.total_pages)
            {
                peopleisLoading = true;
                peoplepage_number++;
                if (String.IsNullOrEmpty(que))
                    que = String.Copy(query);
                string webaddr = App.Current.SearchPeopleAPI +peoplepage_number.ToString() + "&query=" + query;
                _peoplewebClient.DownloadStringCompleted += peoplewebClient_DownloadStringCompleted;
                _peoplewebClient.DownloadStringAsync(new Uri(webaddr));
            }
        }

        public void FetchNextMoviePage()
        {
            GetMovieResults(que);
            GetPeopleResults(que);
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

        private void peoplewebClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            _peoplewebClient.DownloadStringCompleted -= peoplewebClient_DownloadStringCompleted;
            if (e == null)
            {
                MessageBox.Show("Seems like the internet connection is down or the website refused the request.\n:(\nWe are sad too that you couldnt continue.", "Error", MessageBoxButton.OK);
                movieisLoading = false;
                return; //TODO: give a error here
            }
            string json = e.Result;
            if (!string.IsNullOrEmpty(json))
            {
                people = JsonConvert.DeserializeObject<People>(json);
                foreach (var v in people.results)
                    People.Add(v);
                peopleisLoading = false;
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
