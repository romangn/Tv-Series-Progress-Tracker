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
    /// Interaction logic for DisplayAllImdbShows.xaml
    /// </summary>
    public partial class DisplayAllImdbShows : Window
    {
        private ShowRepository _repo;
        private ShowRecord _show = new ShowRecord();

        public DisplayAllImdbShows()
        {
            InitializeComponent();
            _repo = new ShowRepository();
        }

        private void onClick_Add(object sender, RoutedEventArgs e)
        {
            ShowRecord record = (ShowRecord)imdbShows.SelectedItem;
            ShowRecord newRecord = _repo.addFromImdb(record.imdbID);

            _show.Title = newRecord.Title;
            _show.Genre = newRecord.Genre;
            _show.IsFinished = false;
            _show.totalSeasons = newRecord.totalSeasons;
            _show.imdbID = newRecord.imdbID;
            MessageBoxResult result = MessageBox.Show("Are you sure you want to add the following show?" +
                "\nName of the show: " + _show.Title +
                "\nGenre of the show: " + _show.Genre +
                "\nTotal seasons in the show: " + newRecord.totalSeasons, "Confirmation",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                if (!_repo.addNewShow(_show))
                {
                    MessageBox.Show("The show with the same name already exists");
                }
                else
                {
                    var episodes = _repo.getEpisodesInSeasons(_show.imdbID);
                    var id = _repo.getIdOfExistingShow(_show.Title);
                    _repo.addEpisodesToShow(id, episodes);
                    MessageBox.Show("The new show has been added");
                    this.Close();
                }
            }
        }

        private void searchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                imdbShows.ItemsSource = _repo.getAllShowsFromImdb(searchBox.Text);
            }
        }

        private void searchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= searchBox_GotFocus;
        }

        private void onClick_Imdb(object sender, RoutedEventArgs e)
        {
            ShowRecord record = (ShowRecord)imdbShows.SelectedItem;
            if (!string.IsNullOrEmpty(record.imdbID) && !string.IsNullOrWhiteSpace(record.imdbID))
                _repo.openTheimdbPage(record.imdbID);
            else
                MessageBox.Show("Could not find show");
        }
    }
}
