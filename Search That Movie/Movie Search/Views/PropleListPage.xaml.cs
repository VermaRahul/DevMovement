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
using Coding4Fun.Toolkit.Controls;

namespace Movie_Search.Views
{
    public partial class PropleListPage : PhoneApplicationPage
    {
        public PropleListPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.NavigationMode == NavigationMode.Forward || e.NavigationMode == NavigationMode.New)
            {
                PeopleViewModel peopleViewModel = new PeopleViewModel();
                peopleViewModel.GetResults();
                this.DataContext = peopleViewModel;
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

    }
}