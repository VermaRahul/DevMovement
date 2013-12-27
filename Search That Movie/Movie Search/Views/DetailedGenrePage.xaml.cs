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
using System.Windows.Data;

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

                DetailedGenreViewModel detailedGenreViewModel = new DetailedGenreViewModel();
                detailedGenreViewModel.GetGenres(genre_id);
                this.DataContext = detailedGenreViewModel;

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

        public static readonly DependencyProperty ListVerticalOffsetProperty = DependencyProperty.Register("ListVerticalOffset", typeof(double), typeof(DetailedGenrePage), new PropertyMetadata(new PropertyChangedCallback(OnListVerticalOffsetChanged)));
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
        private static void OnListVerticalOffsetChanged( DependencyObject obj, DependencyPropertyChangedEventArgs e )
        {
            DetailedGenrePage page = obj as DetailedGenrePage;
            ScrollViewer viewer = page._listScrollViewer;

            if( viewer != null )
              {
                if( page._lastFetch < viewer.ScrollableHeight )
                {
                  // Trigger within 1/4 the viewport.
                  if( viewer.VerticalOffset >= viewer.ScrollableHeight - viewer.ViewportHeight )
                  {
                    page._lastFetch = viewer.ScrollableHeight;
                    (page.DataContext as DetailedGenreViewModel).FetchNextPage();
                  }
                }
              }
            }
    }
}