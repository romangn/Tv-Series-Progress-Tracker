using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TvSeriesProgressTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ShowRepository _repo;

        public MainWindow()
        {
            InitializeComponent();
            _repo = new ShowRepository();
            unfinishedShows.ItemsSource = _repo.getAllUnfinishedShows();
        }

        private void newEntry_Click(object sender, RoutedEventArgs e)
        {
            NewEntry entry = new NewEntry();
            entry.Closed += ChildWindowClosed;
            entry.Show();  
        }

        private void ChildWindowClosed(object sender, EventArgs e)
        {
            ((Window)sender).Closed -= ChildWindowClosed;
            unfinishedShows.ItemsSource = _repo.getAllUnfinishedShows();
        }

        private void allShows_Click(object sender, RoutedEventArgs e)
        {
            ViewAllShows shows = new ViewAllShows();
            shows.Closed += ChildWindowClosed;
            shows.Show();
        }

        private void findShows_Click(object sender, RoutedEventArgs e)
        {
                SearchExistingShows shows = new SearchExistingShows();
                shows.Closed += ChildWindowClosed;
                shows.Show();
        }

        private void about_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This application has been developed for the purpose of " +
                "tracking the shows which can be added either manually or online. The online functionality" +
                " uses omdbapi to access the show data therefore the credit for the api's functionality" +
                " goes to its creator.");
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void entryOnline_Click(object sender, RoutedEventArgs e)
        {
            if (_repo.checkIfInternetConnectionExists())
            {
                if (_repo.CheckIfApiIsOnline("http://www.omdbapi.com/", 5000))
                {
                    DisplayAllImdbShows shows = new DisplayAllImdbShows();
                    shows.Closed += ChildWindowClosed;
                    shows.Show();
                }
                else
                {
                    MessageBox.Show("Resource is not available, please try again later");
                }
            }
            else
            {
                MessageBox.Show("No internet connection available");
            }
        }
    }
}
