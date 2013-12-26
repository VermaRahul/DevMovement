using Newtonsoft.Json;
using PortableClassLibrary.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;

namespace Movie_Search.ViewModel
{
    public class DetailedGenreViewModel : INotifyPropertyChanged
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

        private  int page_number;

        private WebClient _webClient = new WebClient();

        private DetailedGenre result;

        public void GetGenres(int id)
        {
            if (result == null || page_number < result.total_pages)
            {
                isLoading = true;
                page_number++;
                string webaddr = App.Current.DetailedGenresAPI_1 + id.ToString() + App.Current.DetailedGenresAPI_2 + page_number.ToString();
                _webClient.DownloadStringCompleted += webClient_DownloadStringCompleted;
                _webClient.DownloadStringAsync(new Uri(webaddr));
            }
        }

        public void FetchNextPage()
        {
            GetGenres(result.id);
        }

        private void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            _webClient.DownloadStringCompleted -= webClient_DownloadStringCompleted;
            string json = e.Result;
            if (!string.IsNullOrEmpty(json))
            {
                result = JsonConvert.DeserializeObject<DetailedGenre>(json);
                foreach (var v in result.results)
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
