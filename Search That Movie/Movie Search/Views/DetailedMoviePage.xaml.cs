using Microsoft.Phone.Controls;
using Movie_Search.ViewModel;
using System;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Movie_Search.Views
{
    public partial class DetailedMoviePage : PhoneApplicationPage
    {
        private int movie_id;
        private string movie_name;

        public DetailedMoviePage()
        {
            InitializeComponent();
            DetailedMovieViewModel detailedMovieViewModel = new DetailedMovieViewModel();
            this.DataContext = detailedMovieViewModel;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.NavigationMode == NavigationMode.Forward || e.NavigationMode == NavigationMode.New)
            {
                movie_id = Convert.ToInt32(NavigationContext.QueryString["id"]);
                movie_name = NavigationContext.QueryString["title"];
                panoramaItem.Title = movie_name;
               
                (this.DataContext as DetailedMovieViewModel).GetMovie(movie_id);

            }
        }

        private void PeopleListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                //Person selectedItem = (Person)e.AddedItems[0];
                // reset selection of ListBox 
                ((ListBox)sender).SelectedIndex = -1;
                //NavigationService.Navigate(new Uri("/Views/DetailedGenrePage.xaml?id=" + (selectedItem.id).ToString() + "&name=" + selectedItem.name, UriKind.Relative));
            }
        }
    }
}