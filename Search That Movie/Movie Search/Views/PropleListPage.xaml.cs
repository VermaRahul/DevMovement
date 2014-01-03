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
                NavigationService.Navigate(new Uri("/Views/DetailedPersonPage.xaml?id=" + (selectedItem.id).ToString() + "&title=" + selectedItem.name, UriKind.Relative));
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