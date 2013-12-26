using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Movie_Search.Views
{
    public partial class DetailedGenrePage : PhoneApplicationPage
    {
        string genre_name;
        int genre_id;

        public DetailedGenrePage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.NavigationMode == NavigationMode.Forward || e.NavigationMode == NavigationMode.New)
            {
                genre_id = Convert.ToInt32(NavigationContext.QueryString["id"]);
                genre_name = NavigationContext.QueryString["name"];
                genre.Text = genre_name;
            }
        }
    }
}