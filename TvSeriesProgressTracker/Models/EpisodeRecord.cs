using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TvSeriesProgressTracker.Models
{
    public class EpisodeRecord
    {
        private string _title;
        private string _released;
        private int _episode;
        private string _imdbRating;
        private string _imdbId;
        private int _season;
        private int _showId;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
            }
        }

        public string Released
        {
            get { return _released; }
            set
            {
                _released = value;
            }
        }

        public int Episode
        {
            get { return _episode; }
            set
            {
                _episode = value;
            }
        }

        public string imdbRating
        {
            get { return _imdbRating; }
            set
            {
                _imdbRating = value;
            }
        }
        public string imdbId
        {
            get { return _imdbId; }
            set
            {
                _imdbId = value;
            }
        }

        public int Season
        {
            get { return _season; }
            set
            {
                _season = value;
            }
        }
        public int ShowId
        {
            get { return _showId; }
            set
            {
                _showId = value;
            }
        }
    }
}
