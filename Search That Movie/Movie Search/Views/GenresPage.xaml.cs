using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Movie_Search.ViewModel;
using Movie_Search.Model;

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
    }
}