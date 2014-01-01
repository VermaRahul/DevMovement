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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Movie_Search
{
    public partial class App : Application
    {
        //Globals
        public string SearchMoviesAPI = "https://api.themoviedb.org/3/search/movie?api_key=c9271ad7bfc80b904833986c70d34b6f&page=";
        public string SearchPeopleAPI = "https://api.themoviedb.org/3/search/person?api_key=c9271ad7bfc80b904833986c70d34b6f&page=";

        public string PopularPeopleAPI = "https://api.themoviedb.org/3/person/popular?api_key=c9271ad7bfc80b904833986c70d34b6f&page=";
        public string UpcomingAPI = "https://api.themoviedb.org/3/movie/upcoming?api_key=c9271ad7bfc80b904833986c70d34b6f&page=";
        public string NowPlayingAPI = "https://api.themoviedb.org/3/movie/now_playing?api_key=c9271ad7bfc80b904833986c70d34b6f&page=";
        public string PopularAPI = "https://api.themoviedb.org/3/movie/popular?api_key=c9271ad7bfc80b904833986c70d34b6f&page=";
        public string TopRatedAPI = "https://api.themoviedb.org/3/movie/top_rated?api_key=c9271ad7bfc80b904833986c70d34b6f&page=";
        
        public string ImagesBaseLink = "http://image.tmdb.org/t/p/";

        public string AllGenresAPI = "https://api.themoviedb.org/3/genre/list?api_key=c9271ad7bfc80b904833986c70d34b6f&language=en";

        public string DetailedGenresAPI_1 = "https://api.themoviedb.org/3/genre/";
        public string DetailedGenresAPI_2 = "/movies?api_key=c9271ad7bfc80b904833986c70d34b6f&page=";

        public string DetailedMovieAPI_1 = "https://api.themoviedb.org/3/movie/";
        public string DetailedMovieAPI_2 = "?api_key=c9271ad7bfc80b904833986c70d34b6f&append_to_response=similar_movies,alternative_titles,keywords,releases,trailers,reviews,credits,images&include_image_language=en,null";

        public string MovieTypeAPI_1 = "https://api.themoviedb.org/3/movie/";
        public string MovieTypeAPI_2 = "?api_key=c9271ad7bfc80b904833986c70d34b6f&page=";

        //Globals end
        public static new App Current
        {
            get { return Application.Current as App; }
        }

        /// <summary>
        /// Provides easy access to the root frame of the Phone Application.
        /// </summary>
        /// <returns>The root frame of the Phone Application.</returns>
        public PhoneApplicationFrame RootFrame { get; private set; }

        /// <summary>
        /// Constructor for the Application object.
        /// </summary>
        public App()
        {
            // Global handler for uncaught exceptions. 
            UnhandledException += Application_UnhandledException;

            // Standard Silverlight initialization
            InitializeComponent();

            // Phone-specific initialization
            InitializePhoneApplication();

            // Show graphics profiling information while debugging.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // Display the current frame rate counters.
                Application.Current.Host.Settings.EnableFrameRateCounter = true;

                // Show the areas of the app that are being redrawn in each frame.
                //Application.Current.Host.Settings.EnableRedrawRegions = true;

                // Enable non-production analysis visualization mode, 
                // which shows areas of a page that are handed off to GPU with a colored overlay.
                //Application.Current.Host.Settings.EnableCacheVisualization = true;

                // Disable the application idle detection by setting the UserIdleDetectionMode property of the
                // application's PhoneApplicationService object to Disabled.
                // Caution:- Use this under debug mode only. Application that disables user idle detection will continue to run
                // and consume battery power when the user is not using the phone.
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }

        }

        // Code to execute when the application is launching (eg, from Start)
        // This code will not execute when the application is reactivated
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
        }

        // Code to execute when the application is activated (brought to foreground)
        // This code will not execute when the application is first launched
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
        }

        // Code to execute when the application is deactivated (sent to background)
        // This code will not execute when the application is closing
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
        }

        // Code to execute when the application is closing (eg, user hit Back)
        // This code will not execute when the application is deactivated
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
        }

        // Code to execute if a navigation fails
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // A navigation has failed; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        // Code to execute on Unhandled Exceptions
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        #region Phone application initialization

        // Avoid double-initialization
        private bool phoneApplicationInitialized = false;

        // Do not add any additional code to this method
        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            // Create the frame but don't set it as RootVisual yet; this allows the splash
            // screen to remain active until the application is ready to render.
            RootFrame = new PhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            // Handle navigation failures
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            // Ensure we don't initialize again
            phoneApplicationInitialized = true;
        }

        // Do not add any additional code to this method
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Set the root visual to allow the application to render
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            // Remove this handler since it is no longer needed
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        #endregion
    }
}