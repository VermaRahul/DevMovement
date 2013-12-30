using Newtonsoft.Json;
using PortableClassLibrary.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Windows;

namespace Movie_Search.ViewModel
{
    public class PeopleViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Person> _People = new ObservableCollection<Person>();

        public ObservableCollection<Person> People
        {
            get { return _People; }
            set { _People = value; }
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

        private int peoplepage_number;

        private WebClient _peoplewebClient = new WebClient();

        private People people;

        public void GetResults()
        {
            GetPeopleResults();
        }

        private void GetPeopleResults()
        {
            if (peopleisLoading)
                return;
            if (people == null || peoplepage_number < people.total_pages)
            {
                peopleisLoading = true;
                peoplepage_number++;
                string webaddr = App.Current.PopularPeopleAPI + peoplepage_number;
                _peoplewebClient.DownloadStringCompleted += peoplewebClient_DownloadStringCompleted;
                _peoplewebClient.DownloadStringAsync(new Uri(webaddr));
            }
        }

        public void FetchNextPage()
        {
            GetResults();
        }

        private void peoplewebClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            _peoplewebClient.DownloadStringCompleted -= peoplewebClient_DownloadStringCompleted;
            if (e == null)
            {
                MessageBox.Show("Seems like the internet connection is down or the website refused the request.\n:(\nWe are sad too that you couldnt continue.", "Error", MessageBoxButton.OK);
                peopleisLoading = false;
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
