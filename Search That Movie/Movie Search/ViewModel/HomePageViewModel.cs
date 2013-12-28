using Newtonsoft.Json;
using PortableClassLibrary.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Windows;

namespace Movie_Search.ViewModel
{
    public class HomePageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Movie> _UpcomingMovies = new ObservableCollection<Movie>();

        public ObservableCollection<Movie> UpcomingMovies
        {
            get { return _UpcomingMovies; }
            set { _UpcomingMovies = value; }
        }

        private ObservableCollection<Movie> _NowPlayingMovies = new ObservableCollection<Movie>();

        public ObservableCollection<Movie> NowPlayingMovies
        {
            get { return _NowPlayingMovies; }
            set { _NowPlayingMovies = value; }
        }

        private ObservableCollection<Movie> _PopularMovies = new ObservableCollection<Movie>();

        public ObservableCollection<Movie> PopularMovies
        {
            get { return _PopularMovies; }
            set { _PopularMovies = value; }
        }

        private ObservableCollection<Movie> _TopRatedMovies = new ObservableCollection<Movie>();

        public ObservableCollection<Movie> TopRatedMovies
        {
            get { return _TopRatedMovies; }
            set { _TopRatedMovies = value; }
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

        private bool _nowisLoading;

        public bool nowisLoading
        {
            get { return _nowisLoading; }
            set
            {
                _nowisLoading = value;
                this.OnPropertyChanged(new PropertyChangedEventArgs("nowisLoading"));
            }
        }

        private bool _upcomingisLoading;

        public bool upcomingisLoading
        {
            get { return _upcomingisLoading; }
            set
            {
                _upcomingisLoading = value;
                this.OnPropertyChanged(new PropertyChangedEventArgs("upcomingisLoading"));
            }
        }

        private bool _popularisLoading;

        public bool popularisLoading
        {
            get { return _popularisLoading; }
            set
            {
                _popularisLoading = value;
                this.OnPropertyChanged(new PropertyChangedEventArgs("popularisLoading"));
            }
        }

        private bool _topisLoading;

        public bool topisLoading
        {
            get { return _topisLoading; }
            set
            {
                _topisLoading = value;
                this.OnPropertyChanged(new PropertyChangedEventArgs("topisLoading"));
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

        private WebClient _nowwebClient = new WebClient();
        private WebClient _topwebClient = new WebClient();
        private WebClient _upcomingwebClient = new WebClient();
        private WebClient _popwebClient = new WebClient();
        //private HomePageItems result;

        private Upcoming upcoming;
        private TopRated top;
        private NowPlaying now;
        private Popular pop;

        public void LoadHomeItems()
        {
            if (isLoading)
                return;

            isLoading = true;

            upcomingisLoading = true;
            nowisLoading = true;
            popularisLoading = true;
            topisLoading = true;


            _upcomingwebClient.DownloadStringCompleted += upcoming_DownloadStringCompleted;
            _upcomingwebClient.DownloadStringAsync(new Uri(App.Current.UpcomingAPI + "1"));


            _nowwebClient.DownloadStringCompleted += now_DownloadStringCompleted;
            _nowwebClient.DownloadStringAsync(new Uri(App.Current.NowPlayingAPI + "1"));

            _popwebClient.DownloadStringCompleted += pop_DownloadStringCompleted;
            _popwebClient.DownloadStringAsync(new Uri(App.Current.PopularAPI + "1"));

            _topwebClient.DownloadStringCompleted += top_DownloadStringCompleted;
            _topwebClient.DownloadStringAsync(new Uri(App.Current.TopRatedAPI + "1"));
        }

        private void top_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            _topwebClient.DownloadStringCompleted -= top_DownloadStringCompleted;
            if (e == null && isLoading)
            {
                MessageBox.Show("Seems like the internet connection is down or the website refused the request.\n:(\nWe are sad too that you couldnt continue.", "Error", MessageBoxButton.OK);
                isLoading = false;
                return; //TODO: give a error here
            }
            string json = e.Result;
            if (!string.IsNullOrEmpty(json))
            {
                top = JsonConvert.DeserializeObject<TopRated>(json);
                foreach (var v in top.results)
                    TopRatedMovies.Add(v);
                topisLoading = false;
                isLoadingCheck();
            }
        }

        private void pop_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            _popwebClient.DownloadStringCompleted -= pop_DownloadStringCompleted;
            if (e == null && isLoading)
            {
                MessageBox.Show("Seems like the internet connection is down or the website refused the request.\n:(\nWe are sad too that you couldnt continue.", "Error", MessageBoxButton.OK);
                isLoading = false;
                return; //TODO: give a error here
            }
            string json = e.Result;
            if (!string.IsNullOrEmpty(json))
            {
                pop = JsonConvert.DeserializeObject<Popular>(json);
                foreach (var v in pop.results)
                    PopularMovies.Add(v);
                popularisLoading = false;
                isLoadingCheck();
            }
        }

        private void now_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            _nowwebClient.DownloadStringCompleted -= now_DownloadStringCompleted;
            if (e == null && isLoading)
            {
                MessageBox.Show("Seems like the internet connection is down or the website refused the request.\n:(\nWe are sad too that you couldnt continue.", "Error", MessageBoxButton.OK);
                isLoading = false;
                return; //TODO: give a error here
            }
            string json = e.Result;
            if (!string.IsNullOrEmpty(json))
            {
                now = JsonConvert.DeserializeObject<NowPlaying>(json);
                foreach (var v in now.results)
                    NowPlayingMovies.Add(v);
                nowisLoading = false;
                isLoadingCheck();
            }
        }

        private void upcoming_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            _upcomingwebClient.DownloadStringCompleted -= upcoming_DownloadStringCompleted;
            if (e == null && isLoading)
            {
                MessageBox.Show("Seems like the internet connection is down or the website refused the request.\n:(\nWe are sad too that you couldnt continue.", "Error", MessageBoxButton.OK);
                isLoading = false;
                return; //TODO: give a error here
            }
            string json = e.Result;
            if (!string.IsNullOrEmpty(json))
            {
                upcoming = JsonConvert.DeserializeObject<Upcoming>(json);
                foreach (var v in upcoming.results)
                    UpcomingMovies.Add(v);
                upcomingisLoading = false;
                isLoadingCheck();
            }
        }

        private void isLoadingCheck()
        {
            if (!upcomingisLoading && !topisLoading && !nowisLoading && !topisLoading)
                isLoading = false;
        }

    }
}
