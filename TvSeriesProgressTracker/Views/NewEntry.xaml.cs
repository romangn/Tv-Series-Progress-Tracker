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
    /// Interaction logic for NewEntry.xaml
    /// </summary>
    public partial class NewEntry : Window
    {
        private ShowRecord _show = new ShowRecord();
        private ShowRepository _repo;
        private int _errors = 0;

        public NewEntry()
        {
            InitializeComponent();
            _repo = new ShowRepository();
            grid_showData.DataContext = _show;
        }

        private void button_Save(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are these details correct?" + 
                "\nName of the show: " + namebox.Text +
                "\nGenre of the show: " + genreBox.Text +
                "\nCurrent episode: " + Int32.Parse(currentEpBox.Text) +
                "\nCurrent season: " + Int32.Parse(currentSeasonBox.Text) +
                "\nHave you finished watching the show? " + isFinishedBox.IsChecked.Value +
                "\nTotal seasons: " + Int32.Parse(overallSeasonsBox.Text), "Confirmation", 
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _show.Title = namebox.Text;
                _show.Genre = genreBox.Text;
                _show.CurrentEpisode = Int32.Parse(currentEpBox.Text);
                _show.CurrentSeason = Int32.Parse(currentSeasonBox.Text);
                _show.totalSeasons = Int32.Parse(overallSeasonsBox.Text);
                _show.IsFinished = isFinishedBox.IsChecked.Value;
                if (!_repo.addNewShow(_show))
                {
                    MessageBox.Show("The show with the same name already exists");
                }
                else
                {
                    MessageBox.Show("The new show has been added");
                    this.Close();
                }
            }
        }

        private void Validation_Error (object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                _errors++;
            else
                _errors--;
        }

        private void Confirm_CanExecute (object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _errors == 0;
            e.Handled = true;
        }

        private void Confirm_Executed (object sender, ExecutedRoutedEventArgs e)
        {
            _show = new ShowRecord();
            e.Handled = true;
        }
    }
}
