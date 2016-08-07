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
using TvSeriesProgressTracker.Models;

namespace TvSeriesProgressTracker.Views
{
    /// <summary>
    /// Interaction logic for ViewAllEpisodes.xaml
    /// </summary>
    public partial class ViewAllEpisodes : Window
    {

        private ShowRepository _repo;
        private string title;

        public ViewAllEpisodes()
        {
            InitializeComponent();
            _repo = new ShowRepository();
        }

        public ViewAllEpisodes(string name)
        {
            InitializeComponent();
            _repo = new ShowRepository();
            this.title = name;
        }

        private void makeCurrent_Click(object sender, RoutedEventArgs e)
        {
            EpisodeRecord episode = (EpisodeRecord)episodes.SelectedItem;
            episode.ShowId = _repo.findEpisodesShowId(episode.Title);
            _repo.changeCurrentEpisode(episode);
        }

        private void remove_Click(object sender, RoutedEventArgs e)
        {
            EpisodeRecord episode = (EpisodeRecord)episodes.SelectedItem;
            episode.ShowId = _repo.findEpisodesShowId(episode.Title);
            _repo.removeEpisode(episode.ShowId, episode.Title);
            episodes.ItemsSource = _repo.getAllEpisodesInShow(_repo.FindShowsTitle(episode.ShowId));
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            if (_repo.checkIfInternetConnectionExists())
            {
                if (_repo.CheckIfApiIsOnline("http://www.omdbapi.com/", 5000))
                {
                    var items = episodes.Items.Cast<EpisodeRecord>().ToList();
                    var title = _repo.FindShowsTitle(items[0].ShowId);
                    if (_repo.checkForNewEpisodes(title))
                    {
                        episodes.ItemsSource = _repo.getAllEpisodesInShow(title);
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

        private void newEpisode_Click(object sender, RoutedEventArgs e)
        {
            var items = episodes.Items.Cast<EpisodeRecord>().ToList();
            var ep = new EpisodeRecord() { ShowId = _repo.getIdOfExistingShow(title) };
            NewEpisode episode = new NewEpisode();
            episode.DataContext = ep;
            episode.Closed += ChildWindowClosed;
            episode.Show();
        }

        private void ChildWindowClosed(object sender, EventArgs e)
        {
            ((Window)sender).Closed -= ChildWindowClosed;
            episodes.ItemsSource = _repo.getAllEpisodesInShow(title);
        }
    }
}
