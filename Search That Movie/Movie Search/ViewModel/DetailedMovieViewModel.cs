using Newtonsoft.Json;
using PortableClassLibrary.Model;
using System;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Movie_Search.ViewModel
{
    public class DetailedMovieViewModel : INotifyPropertyChanged
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

        private DetailedMovie result;

        public DetailedMovie MovieResult
        {
            get { return result; }
            set
            {
                result = value;
                this.OnPropertyChanged(new PropertyChangedEventArgs("MovieResult"));
            }
        }



        private ImageBrush _BackgroundBrush;
        public ImageBrush BackgroundBrush
        {
            get { return _BackgroundBrush; }
            set
            {
                _BackgroundBrush = value;
                this.OnPropertyChanged(new PropertyChangedEventArgs("BackgroundBrush"));
            }
        } 

        public void GetMovie(int id)
        {
            if (isLoading)
                return;
            if (MovieResult == null)
            {
                isLoading = true;
                string webaddr = App.Current.DetailedMovieAPI_1 + id.ToString() + App.Current.DetailedMovieAPI_2;
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
                MovieResult = JsonConvert.DeserializeObject<DetailedMovie>(json);
                if (!string.IsNullOrWhiteSpace(MovieResult.backdrop_path))
                {
                    Uri uri = new Uri(App.Current.ImagesBaseLink + "w1280" + MovieResult.backdrop_path, UriKind.Absolute);

                    ImageBrush brush = new ImageBrush
                    {
                        ImageSource = new System.Windows.Media.Imaging.BitmapImage(uri),
                        Opacity = 0.4d,
                        Stretch = Stretch.UniformToFill,
                        AlignmentX = AlignmentX.Center
                    };
                    BackgroundBrush = brush;
                }

                assignElements();

                //foreach (var v in result.results)
                //    Movies.Add(v);
                isLoading = false;
            }
        }

        private void assignElements()
        {
            var temp = MovieResult;

            temp.combined_genres = null;
            temp.combined_languages = null;
            temp.combined_companies = null;
            temp.combined_countries = null;

            foreach (var v in temp.genres)
            {
                if (string.IsNullOrEmpty(temp.combined_genres))
                    temp.combined_genres = v.name;
                else
                    temp.combined_genres = temp.combined_genres + " | " + v.name;
            }
            if (string.IsNullOrEmpty(temp.combined_genres))
                temp.combined_genres = "-";

            foreach (var v in temp.spoken_languages)
            {
                if (string.IsNullOrEmpty(temp.combined_languages))
                    temp.combined_languages = v.name;
                else
                    temp.combined_languages = temp.combined_languages + " | " + v.name;
            }
            if (string.IsNullOrEmpty(temp.combined_languages))
                temp.combined_languages = "-";

            foreach (var v in temp.production_companies)
            {
                if (string.IsNullOrEmpty(temp.combined_companies))
                    temp.combined_companies = v.name;
                else
                    temp.combined_companies = temp.combined_companies + " | " + v.name;
            }
            if (string.IsNullOrEmpty(temp.combined_companies))
                temp.combined_companies ="-";

            foreach (var v in temp.production_countries)
            {
                if (string.IsNullOrEmpty(temp.combined_countries))
                    temp.combined_countries = v.name;
                else
                    temp.combined_countries = temp.combined_countries + " | " + v.name;
            }
            if (string.IsNullOrEmpty(temp.combined_countries))
                temp.combined_countries = "-";

            MovieResult = temp;
        }
    }
}
