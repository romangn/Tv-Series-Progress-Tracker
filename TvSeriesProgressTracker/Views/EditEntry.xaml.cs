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

namespace TvSeriesProgressTracker
{
    /// <summary>
    /// Interaction logic for EditEntry.xaml
    /// </summary>
    public partial class EditEntry : Window
    {
        private ShowRecord _show = new ShowRecord();
        private ShowRepository _repo;
        private int _errors = 0;

        public EditEntry()
        {
            InitializeComponent();
            _repo = new ShowRepository();
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
            _show = new ShowRecord();
            e.Handled = true;
        }

        private void button_Save(object sender, RoutedEventArgs e)
        {
            string oldName = oldNameBox.Text;
            MessageBoxResult result = MessageBox.Show("Are the new details correct?" +
                "\nName of the show: " + namebox.Text +
                "\nGenre of the show: " + genreBox.Text +
                "\nTotal seasons in the show: " + Int32.Parse(seasonsCountBox.Text) +
                "\nCurrent season: " + Int32.Parse(currentSeasonBox.Text) +
                "\nCurrent episode: " + Int32.Parse(currentEpBox.Text) +
                "\nHave you finished watching the show? " + isFinishedBox.IsChecked.Value , "Confirmation",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _show = new ShowRecord();
                _show.Title = namebox.Text;
                _show.Genre = genreBox.Text;
                _show.CurrentEpisode = Int32.Parse(currentEpBox.Text);
                _show.CurrentSeason = Int32.Parse(currentSeasonBox.Text);
                _show.IsFinished = isFinishedBox.IsChecked.Value;
                _show.totalSeasons = Int32.Parse(seasonsCountBox.Text);
                _repo.editShow(_show, oldName);
                this.Close();
            }

        }
    }
}
