using Microsoft.Phone.Controls;
using Movie_Search.ViewModel;
using System;
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

        }

        private void crew_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}