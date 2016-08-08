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
    /// Interaction logic for EditEpisode.xaml
    /// </summary>
    public partial class EditEpisode : Window
    {
        private EpisodeRecord _record = new EpisodeRecord();
        private ShowRepository _repo;
        private int _errors = 0;

        public EditEpisode()
        {
            InitializeComponent();
            _repo = new ShowRepository();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            string oldName = oldNameBox.Text;
            MessageBoxResult result = MessageBox.Show("Are the new details correct?" +
                 "\nTitle: " + titlebox.Text +
                "\nSeason: " + Int32.Parse(seasonBox.Text) +
                "\nEpisode: " + Int32.Parse(episodeBox.Text),
            "   Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _record.Title = titlebox.Text;
                _record.Season = Int32.Parse(seasonBox.Text);
                _record.Episode = Int32.Parse(episodeBox.Text);
                _record.ShowId = _repo.findEpisodesShowId(_record.Title);
                if (!_repo.checkIfEpisodeAlreadyExists(_record))
                {
                    _repo.editEpisode(_record, oldName);
                    this.Close();
                }
                else if (_record.Title == oldName)
                {
                    this.Close();
                }
                else
                {
                    MessageBox.Show("The episode with the same name already exists");
                }
            }
        }

        private void Validation_Error(object sener, ValidationErrorEventArgs e)
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
            _record = new EpisodeRecord();
            e.Handled = true;
        }
    }
}
