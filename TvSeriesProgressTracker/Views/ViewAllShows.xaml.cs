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
            if (_repo.checkIfInternetConnectionExists())
            {
                if (_repo.CheckIfApiIsOnline("http://www.omdbapi.com/", 5000))
                {
                    _show = (ShowRecord)shows.SelectedItem;
                    var currentEpisodes = _repo.getAllEpisodesInShow(_show.Title);
                    var currentEpisodesOnline = _repo.getEpisodesInSeasons(_repo.findImdbId(_show.Title));
                    var listCurrentEpisodesOnline = _repo.convertEpisodes(currentEpisodesOnline);
                    for (int i = listCurrentEpisodesOnline.Count - 1; i >= 0; i--)
                    {
                        for (int j = 0; j < currentEpisodes.Count; j++)
                        {
                            if (listCurrentEpisodesOnline[i].Title == currentEpisodes[j].Title)
                            {
                                listCurrentEpisodesOnline.RemoveAt(i);
                                break;
                            }
                        }
                    }
                    if (listCurrentEpisodesOnline.Count == 0)
                    {
                        MessageBox.Show("No new episodes were found");
                    }
                    else
                    {
                        int id = _repo.getIdOfExistingShow(_show.Title);
                        _repo.addEpisodesToShow(id, listCurrentEpisodesOnline);
                        MessageBox.Show(String.Format("{0} new episode(s) were added!", listCurrentEpisodesOnline.Count));
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
}
