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
using System.Windows.Shapes;
using TvSeriesProgressTracker.Views;

namespace TvSeriesProgressTracker
{
    /// <summary>
    /// Interaction logic for SearchExistingShows.xaml
    /// </summary>
    public partial class SearchExistingShows : Window
    {

        private ShowRepository _repo;
        private ShowRecord _show = new ShowRecord();

        public SearchExistingShows()
        {
            InitializeComponent();
            _repo = new ShowRepository();
        }

        private void onClick_Remove(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirmation",
            MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _show = (ShowRecord)searchShows.SelectedItem;
                _repo.removeShow(_show);
            }
            searchShows.ItemsSource = _repo.getAllShows(searchBox.Text);
        }

        private void onClick_Edit(object sender, RoutedEventArgs e)
        {
            _show = (ShowRecord)searchShows.SelectedItem;
            EditEntry entry = new EditEntry(_show.Title);
            entry.DataContext = _show;
            entry.Closed += ChildWindowClosed;
            entry.Show();
        }

        private void imdbView_Click(object sender, RoutedEventArgs e)
        {
            ShowRecord record = (ShowRecord)searchShows.SelectedItem;
            _repo.openTheimdbPage(record);
        }

        private void viewEpisodes_Click(object sender, RoutedEventArgs e)
        {
            _show = (ShowRecord)searchShows.SelectedItem;
            ViewAllEpisodes allEpisodes = new ViewAllEpisodes();
            allEpisodes.episodes.ItemsSource = _repo.getAllEpisodesInShow(_show.Title);
            allEpisodes.Closed += ChildWindowClosed;
            allEpisodes.Show();
        }

        private void checkForNewEpisodes_Click(object sender, RoutedEventArgs e)
        {
            _show = (ShowRecord)searchShows.SelectedItem;
            if (_repo.findImdbId(_show.Title) == "Manual")
            {
                MessageBox.Show("Cannot perform the operation for manually added shows");
            }
            else {
                if (_repo.checkIfInternetConnectionExists())
                {
                    if (_repo.CheckIfApiIsOnline("http://www.omdbapi.com/", 5000))
                    {
                        if (_repo.checkForNewEpisodes(_show.Title))
                        {
                            ViewAllEpisodes allEpisodes = new ViewAllEpisodes();
                            allEpisodes.episodes.ItemsSource = _repo.getAllEpisodesInShow(_show.Title);
                            allEpisodes.Show();
                        }
                    }
                    else
                    {
                        MessageBox.Show("The online resource is currently unavailable. Please try again later");
                    }
                }
                else
                {
                    MessageBox.Show("Please check your internet connection");
                }
            }
        }

        private void searchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                searchShows.ItemsSource = _repo.getAllShows(searchBox.Text);
            }
        }

        private void searchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= searchBox_GotFocus;
        }

        private void ChildWindowClosed(object sender, EventArgs e)
        {
            ((Window)sender).Closed -= ChildWindowClosed;
            searchShows.ItemsSource = _repo.getAllShows();
        }
    }
}
