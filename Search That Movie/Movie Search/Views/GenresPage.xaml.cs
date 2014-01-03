using Coding4Fun.Toolkit.Controls;
using Microsoft.Phone.Controls;
using Movie_Search.Model;
using Movie_Search.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Movie_Search.Views
{
    public partial class GenresPage : PhoneApplicationPage
    {
        public GenresPage()
        {
            InitializeComponent();

            AllGenresViewModel genreViewModel = new AllGenresViewModel();
            genreViewModel.GetGenres();
            this.DataContext = genreViewModel;
        }

        private void GenreListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {      
                Genre selectedItem = (Genre)e.AddedItems[0];
                // reset selection of ListBox 
                ((ListBox)sender).SelectedIndex = -1;
                NavigationService.Navigate(new Uri("/Views/DetailedGenrePage.xaml?id=" + (selectedItem.id).ToString() + "&name=" + selectedItem.name, UriKind.Relative));
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

    }
}