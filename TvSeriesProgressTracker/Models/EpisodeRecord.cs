using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TvSeriesProgressTracker.Models
{
    public class EpisodeRecord: INotifyPropertyChanged, IDataErrorInfo
    {
        private string _title;
        private string _released;
        private int _episode;
        private string _imdbRating;
        private string _imdbId;
        private int _season;
        private int _showId;

        public event PropertyChangedEventHandler PropertyChanged; 

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }

        public string Released
        {
            get { return _released; }
            set
            {
                _released = value;
                RaisePropertyChanged();
            }
        }

        public int Episode
        {
            get { return _episode; }
            set
            {
                _episode = value;
                RaisePropertyChanged();
            }
        }

        public string imdbRating
        {
            get { return _imdbRating; }
            set
            {
                _imdbRating = value;
                RaisePropertyChanged();
            }
        }
        public string imdbId
        {
            get { return _imdbId; }
            set
            {
                _imdbId = value;
                RaisePropertyChanged();
            }
        }

        public int Season
        {
            get { return _season; }
            set
            {
                _season = value;
                RaisePropertyChanged();
            }
        }
        public int ShowId
        {
            get { return _showId; }
            set
            {
                _showId = value;
                RaisePropertyChanged();
            }
        }

        public string Error
        {
            get
            {
                return null;
            }
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == "Title")
                {
                    if (string.IsNullOrEmpty(Title))
                        result = "Title cannot be empty";
                }
                if (columnName == "Season")
                {
                    if (Season < 0)
                        result = "Season cannot be lower than zero";
                }
                if (columnName == "Episode")
                {
                    if (Episode < 0)
                        result = "Episode cannot be lower than 0";
                }
                return result;
            }
        }

        private void RaisePropertyChanged([CallerMemberName] string caller="")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }
    }
}
