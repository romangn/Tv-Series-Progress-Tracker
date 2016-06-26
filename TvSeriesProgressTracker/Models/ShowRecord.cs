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
        private string _rated;
        private string _released;
        private string _runtime;
        private string _director;
        private string _writer;
        private string _actors;
        private string _plot;
        private string _language;
        private string _country;
        private string _awards;
        private string _poster;
        private string _metascore;
        private string _imdbRating;
        private string _imdbVotes;
        private string _imdbId;
        private string _type;
        private string _response;
        private string _error;
        private string _oldName;
        private bool _isFinished;

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

        public string Rated
        {
            get { return _rated; }
            set
            {
                _rated = value;
                RaisePropertyChanged();
            }
        }

        public string Released
        {
            get { return _released; }
            set { _released = value;
                RaisePropertyChanged();
            }
        }

        public string Runtime
        {
            get { return _runtime; }
            set { _runtime = value;
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

        public string Director
        {
            get { return _director; }
            set
            {
                _director = value;
                RaisePropertyChanged();
            }
        }

        public string Writer
        {
            get { return _writer; }
            set
            {
                _writer = value;
                RaisePropertyChanged();
            }
        }

        public string Actors
        {
            get { return _actors; }
            set
            {
                _actors = value;
                RaisePropertyChanged();
            }
        }

        public string Plot
        {
            get { return _plot; }
            set
            {
                _plot = value;
                RaisePropertyChanged();
            }
        }

        public string Language
        {
            get { return _language; }
            set
            {
                _language = value;
                RaisePropertyChanged();
            }
        }

        public string Country
        {
            get { return _country; }
            set
            {
                _country = value;
                RaisePropertyChanged();
            }
        }

        public string Awards
        {
            get { return _awards; }
            set
            {
                _awards = value;
                RaisePropertyChanged();
            }
        }

        public string Poster
        {
            get { return _poster; }
            set
            {
                _poster = value;
                RaisePropertyChanged();
            }
        }

        public string Metascore
        {
            get { return _metascore; }
            set
            {
                _metascore = value;
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

        public string imdbVotes
        {
            get { return _imdbVotes; }
            set
            {
                _imdbVotes = value;
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

        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
                RaisePropertyChanged();
            }
        }

        public string Response
        {
            get { return _response; }
            set
            {
                _response = value;
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
