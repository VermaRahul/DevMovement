using Coding4Fun.Toolkit.Controls;
using Microsoft.Phone.Controls;
using Movie_Search.ViewModel;
using PortableClassLibrary.Model;
using System;
using System.Windows;
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

        private void SearchAppBarButton_Click(object sender, EventArgs e)
        {
            var input = new InputPrompt();
            //input.Completed += input_Completed;
            input.Title = "Search";
            input.BorderThickness = new Thickness(1);
            input.IsCancelVisible = true;
            input.Completed += input_Completed;
            input.Message = "Enter any keywords";
            input.Show();
        }

        private void input_Completed(object sender, PopUpEventArgs<string, PopUpResult> e)
        {
            if (e.PopUpResult.ToString() == "Ok")
            {
                if (!string.IsNullOrWhiteSpace(e.Result))
                {
                    NavigationService.Navigate(new Uri("/Views/SearchResultPage.xaml?query=" + e.Result, UriKind.Relative));
                }
            }
        }

        private void Cast_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                MovieCast selectedItem = (MovieCast)e.AddedItems[0];
                // reset selection of ListBox 
                ((ListBox)sender).SelectedIndex = -1;
                NavigationService.Navigate(new Uri("/Views/DetailedPersonPage.xaml?id=" + (selectedItem.id).ToString() + "&title=" + selectedItem.name, UriKind.Relative));
            }
        }

        private void Crew_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                MovieCrew selectedItem = (MovieCrew)e.AddedItems[0];
                // reset selection of ListBox 
                ((ListBox)sender).SelectedIndex = -1;
                NavigationService.Navigate(new Uri("/Views/DetailedPersonPage.xaml?id=" + (selectedItem.id).ToString() + "&title=" + selectedItem.name, UriKind.Relative));
            }
        }

    }
}