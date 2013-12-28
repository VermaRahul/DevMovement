using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using PortableClassLibrary.Model;
using System.Windows.Navigation;
using Movie_Search.ViewModel;
using Microsoft.Phone.Shell;

namespace Movie_Search.Views
{
    public partial class HomePage : PhoneApplicationPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.NavigationMode == NavigationMode.Forward || e.NavigationMode == NavigationMode.New)
            {
                HomePageViewModel homePageViewModel = new HomePageViewModel();
                homePageViewModel.LoadHomeItems();
                this.DataContext = homePageViewModel;

            }
        }

        private void Genre_OnTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/GenresPage.xaml", UriKind.Relative));
        }

        private void MovieListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Movie selectedItem = (Movie)e.AddedItems[0];
                // reset selection of ListBox 
                ((ListBox)sender).SelectedIndex = -1;
                //NavigationService.Navigate(new Uri("/Views/DetailedGenrePage.xaml?id=" + (selectedItem.id).ToString() + "&name=" + selectedItem.name, UriKind.Relative));
            } 
        }

        private void PeopleListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Person selectedItem = (Person)e.AddedItems[0];
                // reset selection of ListBox 
                ((ListBox)sender).SelectedIndex = -1;
                //NavigationService.Navigate(new Uri("/Views/DetailedGenrePage.xaml?id=" + (selectedItem.id).ToString() + "&name=" + selectedItem.name, UriKind.Relative));
            }
        }

        private void Panorama_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((Panorama)sender).SelectedIndex == 0)
            {
                for (var i = 0; i < this.ApplicationBar.Buttons.Count; i++)
                {
                    var button = this.ApplicationBar.Buttons[i] as ApplicationBarIconButton;

                    if (button != null)
                    {
                        if (button.Text == "Show All")
                        {
                            this.ApplicationBar.Buttons.RemoveAt(i);
                            return;
                        }
                    }
                }
            }
            else
            {
                for (var i = 0; i < this.ApplicationBar.Buttons.Count; i++)
                {
                    var button = this.ApplicationBar.Buttons[i] as ApplicationBarIconButton;

                    if (button != null)
                    {
                        if (button.Text == "Show All")
                        {
                            return;
                        }
                    }
                }
                
                var newButton = new ApplicationBarIconButton();
                newButton.IconUri = new Uri("/Assets/AppBar/appbar.download.rest.png", UriKind.Relative);
                newButton.Text = "Show All";
                this.ApplicationBar.Buttons.Add(newButton);
                panoramaItem.Title = "Yolopad";
            }
        }
    }
}