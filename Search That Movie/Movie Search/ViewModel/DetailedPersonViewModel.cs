using Newtonsoft.Json;
using PortableClassLibrary.Model;
using System;
using System.ComponentModel;
using System.Net;
using System.Windows;

namespace Movie_Search.ViewModel
{
    public class DetailedPersonViewModel : INotifyPropertyChanged
    {
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            // get handler (usually a local event variable or just the event)
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, e);
            }
        }

        private WebClient _webClient = new WebClient();

        private DetailedPerson result;

        public DetailedPerson PersonResult
        {
            get { return result; }
            set
            {
                result = value;
                this.OnPropertyChanged(new PropertyChangedEventArgs("PersonResult"));
            }
        }

        public void GetPerson(int id)
        {
            if (isLoading)
                return;
            if (PersonResult == null)
            {
                isLoading = true;
                string webaddr = App.Current.DetailedPersonAPI_1 + id.ToString() + App.Current.DetailedPersonAPI_2;
                _webClient.DownloadStringCompleted += webClient_DownloadStringCompleted;
                _webClient.DownloadStringAsync(new Uri(webaddr));
            }
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
                PersonResult = JsonConvert.DeserializeObject<DetailedPerson>(json);

                //foreach (var v in result.results)
                //    Movies.Add(v);
                isLoading = false;
            }
        }


    }
}
