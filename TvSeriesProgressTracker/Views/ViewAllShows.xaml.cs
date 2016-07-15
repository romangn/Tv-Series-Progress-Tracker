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
    /// Interaction logic for ViewAllShows.xaml
    /// </summary>
    public partial class ViewAllShows : Window
    {
        private ShowRepository _repo;
        private ShowRecord _show = new ShowRecord();

        public ViewAllShows()
        {
            InitializeComponent();
            _repo = new ShowRepository();
            shows.ItemsSource = _repo.getAllShows();
        }

        private void onClick_Remove (object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirmation",
            MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _show = (ShowRecord)shows.SelectedItem;
                _repo.removeShow(_show);
            }
            shows.ItemsSource = _repo.getAllShows();
        }

        private void onClick_Edit (object sender, RoutedEventArgs e)
        {
            _show = (ShowRecord)shows.SelectedItem;
            EditEntry entry = new EditEntry();
            entry.DataContext = _show;
            entry.Closed += ChildWindowClosed;
            entry.Show();
        }

        private void ChildWindowClosed(object sender, EventArgs e)
        {
            ((Window)sender).Closed -= ChildWindowClosed;
            shows.ItemsSource = _repo.getAllShows();
        }

        private void imdbView_Click(object sender, RoutedEventArgs e)
        {
            ShowRecord record = (ShowRecord)shows.SelectedItem;
            string id = _repo.findImdbId(record.Title);
            if (!string.IsNullOrEmpty(id) && !string.IsNullOrWhiteSpace(id))
                _repo.openTheimdbPage(id);
            else
                MessageBox.Show("Could not find show");
        }

        private void viewEpisodes_Click(object sender, RoutedEventArgs e)
        {
            _show = (ShowRecord)shows.SelectedItem;
            ViewAllEpisodes allEpisodes = new ViewAllEpisodes();
            allEpisodes.episodes.ItemsSource = _repo.getAllEpisodesInShow(_show.Title);
            allEpisodes.Closed += ChildWindowClosed;
            allEpisodes.Show();
        }

        private void checkForNewEpisodes_Click(object sender, RoutedEventArgs e)
        {
            _show = (ShowRecord)shows.SelectedItem;
            int currentNumberOfEps = _repo.getAllEpisodesInShow(_show.Title).Count;
            var episodesInShow = _repo.getEpisodesInSeasons(_repo.findImdbId(_show.Title));
            int currentTotalOnline = episodesInShow.Sum(x => x.Value.Count);
            if (currentTotalOnline == currentNumberOfEps)
                MessageBox.Show("No new episodes found");
            else
            {
                var id = _repo.getIdOfExistingShow(_show.Title);
                _repo.addEpisodesToShow(id, episodesInShow);
                MessageBox.Show(String.Format("{0} new episodes were added!", currentTotalOnline - currentNumberOfEps));
                ViewAllEpisodes AllEpisodes = new ViewAllEpisodes();
                AllEpisodes.episodes.ItemsSource = _repo.getAllEpisodesInShow(_show.Title);
                AllEpisodes.Show();
            }
        }
    }
}
