﻿using Coding4Fun.Toolkit.Controls;
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
            if (e.AddedItems.Count > 0)
            {
                Movie selectedItem = (Movie)e.AddedItems[0];
                // reset selection of ListBox 
                ((ListBox)sender).SelectedIndex = -1;
                NavigationService.Navigate(new Uri("/Views/DetailedMoviePage.xaml?id=" + (selectedItem.id).ToString() + "&title=" + selectedItem.title, UriKind.Relative));
            } 
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