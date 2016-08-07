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

namespace TvSeriesProgressTracker
{
    /// <summary>
    /// Interaction logic for NewEpisode.xaml
    /// </summary>
    public partial class NewEpisode : Window
    {

        private ShowRepository _repo;
        private int _errors = 0;

        public NewEpisode()
        {
            InitializeComponent();
            _repo = new ShowRepository();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are the details correct?" +
                "\nTitle: " + titlebox.Text +
                "\nSeason: " + Int32.Parse(seasonBox.Text) +
                "\nEpisode: " + Int32.Parse(episodeBox.Text),
                "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                var episodes = new List<EpisodeRecord>()
                {
                    new EpisodeRecord
                    {
                        Title = titlebox.Text,
                        Season = Int32.Parse(seasonBox.Text),
                        Episode = Int32.Parse(episodeBox.Text),
                        ShowId = Int32.Parse(idElem.Text)
                    }
                };
                if (_repo.checkIfEpisodeAlreadyExists(episodes[0]))
                {
                    MessageBox.Show("Episode with the same name already exists for this show");
                }
                else {
                    _repo.addEpisodesToShow(Int32.Parse(idElem.Text), episodes);
                    MessageBox.Show("The new episode has been added");
                    this.Close();
                }
            }
        }

        private void Validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                _errors++;
            else
                _errors--;
        }

        private void Confirm_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _errors == 0;
            e.Handled = true;
        }

        private void Confirm_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
        }
    }
}
