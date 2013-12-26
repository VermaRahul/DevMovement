using Movie_Search.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;

namespace Movie_Search.ViewModel
{
    public class AllGenresViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Genre> _Genres = new ObservableCollection<Genre>();

        public ObservableCollection<Genre> Genres
        {
            get { return _Genres; }
            set { _Genres = value; }
        }

        private bool _isLoading;

        public bool isLoading
        {
            get { return _isLoading; }
            set { 
                _isLoading = value;
                this.OnPropertyChanged(new PropertyChangedEventArgs("isLoading"));
            }
        }
        

        public void GetGenres()
        {

            WebClient webClient = new WebClient();
            webClient.DownloadStringCompleted += webClient_DownloadStringCompleted;
            webClient.DownloadStringAsync(new Uri(App.Current.AllGenresAPI));
        }

        private void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            string json = e.Result;
            if (!string.IsNullOrEmpty(json))
            {
                AllGenres results = JsonConvert.DeserializeObject<AllGenres>(json);
                foreach (var v in results.genres)
                    Genres.Add(v);
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
