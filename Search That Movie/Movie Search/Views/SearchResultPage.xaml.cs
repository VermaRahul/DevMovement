using Coding4Fun.Toolkit.Controls;
using Microsoft.Phone.Controls;
using Movie_Search.ViewModel;
using PortableClassLibrary.Model;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Navigation;

namespace Movie_Search.Views
{
    public partial class SearchResultPage : PhoneApplicationPage
    {
        string que;
        public SearchResultPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.NavigationMode == NavigationMode.Forward || e.NavigationMode == NavigationMode.New)
            {
                que = NavigationContext.QueryString["query"];

                SearchResultPageViewModel searchResultPageViewModel = new SearchResultPageViewModel();
                pivotItem.Title = que;
                searchResultPageViewModel.GetResults(que);
                this.DataContext = searchResultPageViewModel;

            }
        }

        private void MovieListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Movie selectedItem = (Movie)e.AddedItems[0];
                // reset selection of ListBox 
                ((ListBox)sender).SelectedIndex = -1;
                NavigationService.Navigate(new Uri("/Views/DetailedMoviePage.xaml?id=" + (selectedItem.id).ToString() + "&title=" + selectedItem.title, UriKind.Relative));
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

        public static readonly DependencyProperty ListVerticalOffsetProperty = DependencyProperty.Register("ListVerticalOffset", typeof(double), typeof(SearchResultPage), new PropertyMetadata(new PropertyChangedCallback(OnListVerticalOffsetChanged)));
        public double ListVerticalOffset
        {
            get { return (double)this.GetValue(ListVerticalOffsetProperty); }
            set { this.SetValue(ListVerticalOffsetProperty, value); }
        }

        private ScrollViewer _listScrollViewer;
        private void ScrollViewer_Loaded(object sender, RoutedEventArgs e)
        {
            _listScrollViewer = sender as ScrollViewer;

            Binding binding = new Binding();
            binding.Source = _listScrollViewer;
            binding.Path = new PropertyPath("VerticalOffset");
            binding.Mode = BindingMode.OneWay;
            this.SetBinding(ListVerticalOffsetProperty, binding);
        }

        private double _lastFetch;
        private static void OnListVerticalOffsetChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            SearchResultPage page = obj as SearchResultPage;
            ScrollViewer viewer = page._listScrollViewer;

            if (viewer != null)
            {
                if (page._lastFetch < viewer.ScrollableHeight)
                {
                    // Trigger within 1/4 the viewport.
                    if (viewer.VerticalOffset >= viewer.ScrollableHeight - viewer.ViewportHeight)
                    {
                        page._lastFetch = viewer.ScrollableHeight;
                        (page.DataContext as SearchResultPageViewModel).FetchNextMoviePage();
                    }
                }
            }
        }

        public static readonly DependencyProperty PeopleListVerticalOffsetProperty = DependencyProperty.Register("PeopleListVerticalOffset", typeof(double), typeof(SearchResultPage), new PropertyMetadata(new PropertyChangedCallback(OnPeopleListVerticalOffsetChanged)));
        public double PeopleListVerticalOffset
        {
            get { return (double)this.GetValue(PeopleListVerticalOffsetProperty); }
            set { this.SetValue(PeopleListVerticalOffsetProperty, value); }
        }

        private ScrollViewer _PeoplelistScrollViewer;

        private void peoplescrollViewer_Loaded(object sender, RoutedEventArgs e)
        {
            _PeoplelistScrollViewer = sender as ScrollViewer;

            Binding binding = new Binding();
            binding.Source = _PeoplelistScrollViewer;
            binding.Path = new PropertyPath("VerticalOffset");
            binding.Mode = BindingMode.OneWay;
            this.SetBinding(PeopleListVerticalOffsetProperty, binding);
        }

        private double _PeoplelastFetch;
        private static void OnPeopleListVerticalOffsetChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            SearchResultPage page = obj as SearchResultPage;
            ScrollViewer viewer = page._PeoplelistScrollViewer;

            if (viewer != null)
            {
                if (page._PeoplelastFetch < viewer.ScrollableHeight)
                {
                    // Trigger within 1/4 the viewport.
                    if (viewer.VerticalOffset >= viewer.ScrollableHeight - viewer.ViewportHeight)
                    {
                        page._PeoplelastFetch = viewer.ScrollableHeight;
                        (page.DataContext as SearchResultPageViewModel).FetchNextPeoplePage();
                    }
                }
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