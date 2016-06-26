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

        public ViewAllEpisodes()
        {
            InitializeComponent();
            _repo = new ShowRepository();
        }

        private void makeCurrent_Click(object sender, RoutedEventArgs e)
        {
            EpisodeRecord episode = (EpisodeRecord)episodes.SelectedItem;
            episode.ShowId = _repo.findEpisodesShowId(episode.Title);
            _repo.changeCurrentEpisode(episode);
        }
    }
}
