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

namespace Movie_Search.Views
{
    public partial class GenresPage : PhoneApplicationPage
    {
        public GenresPage()
        {
            InitializeComponent();

            GenreViewModel genreViewModel = new GenreViewModel();
            genreViewModel.GetGenres();
            this.DataContext = genreViewModel;
        }
    }
}