using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TvSeriesProgressTracker.Models;

namespace TvSeriesProgressTracker
{
    public class ShowRecord: INotifyPropertyChanged, IDataErrorInfo
    {
        private string _title;
        private int _totalSeasons;
        private string _genre;
        private int _currentEpisode;
        private int _currentSeason;
        private string _year;
        private string _imdbId;
        private string _oldName;
        private bool _isFinished;
        private string _error;

        public List<EpisodeRecord> Episodes = new List<EpisodeRecord>();

        public event PropertyChangedEventHandler PropertyChanged;

        public string Title
        {
            get { return _title; }
            set { _title = value;
                _oldName = _title;
                RaisePropertyChanged();
            }
        }

        public int totalSeasons
        {
            get { return _totalSeasons; }
            set { _totalSeasons = value;
                RaisePropertyChanged();
            }
        }

        public string Year
        {
            get { return _year; }
            set
            {
                _year = value;
                RaisePropertyChanged();
            }
        }

        public string Genre
        {
            get { return _genre; }
            set
            {
                _genre = value;
                RaisePropertyChanged();
            }
        }

        public string imdbID
        {
            get { return _imdbId; }
            set
            {
                _imdbId = value;
                RaisePropertyChanged();
            }
        }

        public string JsonError
        {
            get { return _error; }
            set
            {
                _error = value;
                RaisePropertyChanged();
            }
        }

        public int CurrentSeason
        {
            get { return _currentSeason; }
            set { _currentSeason = value;
                RaisePropertyChanged();
            }
        }

        public int CurrentEpisode
        {
            get { return _currentEpisode; }
            set
            {
                _currentEpisode = value;
                RaisePropertyChanged();
            }
        }

        public bool IsFinished
        {
            get { return _isFinished; }
            set
            {
                _isFinished = value;
                RaisePropertyChanged();
            }
        }

        public string OldName
        {
            get { return _oldName; }
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
                        result = "Title of the show is required";
                }
                if (columnName == "Genre")
                {
                    if (string.IsNullOrEmpty(Genre))
                        result = "Genre of the show is required";
                }
                if (columnName == "CurrentEpisode")
                {
                    if (CurrentEpisode < 0)
                        result = "Cannot be less than 0";
                }
                if (columnName == "CurrentSeason")
                {
                    if (CurrentSeason < 0)
                        result = "Cannot be less than 0";
                    if (CurrentSeason > totalSeasons)
                        result = "Current season cannot be higher than the overall number of seasosn";
                }
                if (columnName == "totalSeasons")
                {
                    if (totalSeasons < 0)
                        result = "Cannot be less than 0";
                }
                return result;
            }
        }

        private void RaisePropertyChanged(
            [CallerMemberName] string caller="")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }

    }

}
