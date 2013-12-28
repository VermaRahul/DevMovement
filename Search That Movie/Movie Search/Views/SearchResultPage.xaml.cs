using Microsoft.Phone.Controls;
using Movie_Search.ViewModel;
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
                searchResultPageViewModel.GetResults(que);
                this.DataContext = searchResultPageViewModel;

            }
        }

        private void MovieListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (e.AddedItems.Count > 0)
            //{
            //    Genre selectedItem = (Genre)e.AddedItems[0];
            //    // reset selection of ListBox 
            //    ((ListBox)sender).SelectedIndex = -1;
            //    NavigationService.Navigate(new Uri("/Views/DetailedGenrePage.xaml?id=" + (selectedItem.id).ToString() + "&name=" + selectedItem.name, UriKind.Relative));
            //} 
        }

        private void PeopleListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (e.AddedItems.Count > 0)
            //{
            //    Genre selectedItem = (Genre)e.AddedItems[0];
            //    // reset selection of ListBox 
            //    ((ListBox)sender).SelectedIndex = -1;
            //    NavigationService.Navigate(new Uri("/Views/DetailedGenrePage.xaml?id=" + (selectedItem.id).ToString() + "&name=" + selectedItem.name, UriKind.Relative));
            //} 
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
    }
}