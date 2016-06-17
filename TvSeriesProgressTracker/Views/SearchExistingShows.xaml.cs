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
            EditEntry entry = new EditEntry();
            entry.DataContext = _show;
            entry.Closed += ChildWindowClosed;
            entry.Show();
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
