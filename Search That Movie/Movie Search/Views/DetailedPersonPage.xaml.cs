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
    public partial class DetailedPersonPage : PhoneApplicationPage
    {
        private int person_id;
        private string person_name;

        public DetailedPersonPage()
        {
            InitializeComponent();
            DetailedPersonViewModel detailedPersonViewModel = new DetailedPersonViewModel();
            this.DataContext = detailedPersonViewModel;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.NavigationMode == NavigationMode.Forward || e.NavigationMode == NavigationMode.New)
            {
                person_id = Convert.ToInt32(NavigationContext.QueryString["id"]);
                person_name = NavigationContext.QueryString["title"];
                pivotItem.Title = person_name;

                (this.DataContext as DetailedPersonViewModel).GetPerson(person_id);

            }
        }

        private void cast_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                PersonCast selectedItem = (PersonCast)e.AddedItems[0];
                // reset selection of ListBox 
                ((ListBox)sender).SelectedIndex = -1;
                NavigationService.Navigate(new Uri("/Views/DetailedMoviePage.xaml?id=" + (selectedItem.id).ToString() + "&title=" + selectedItem.title, UriKind.Relative));
            }
        }

        private void crew_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                PersonCrew selectedItem = (PersonCrew)e.AddedItems[0];
                // reset selection of ListBox 
                ((ListBox)sender).SelectedIndex = -1;
                NavigationService.Navigate(new Uri("/Views/DetailedMoviePage.xaml?id=" + (selectedItem.id).ToString() + "&title=" + selectedItem.title, UriKind.Relative));
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