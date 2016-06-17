using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows;

namespace TvSeriesProgressTracker
{
    class ShowRepository
    {
        private DatabaseManipulation _db;

        public ShowRepository ()
        {
            _db = new DatabaseManipulation();
            _db.setUpDatabase();
        }

        public bool addNewShow (ShowRecord show)
        {
            bool success = false;
            if (!checkIfShowAlreadyExists(show))
            {
                _db.createNewEntry(show);
                success = true;
            }
            return success;
        }

        public void removeShow (ShowRecord show)
        {
            var id = getIdOfExistingShow(show.Title);
            removeEpisodes(id);
            _db.removeEntry(show.Title);
        }

        public List<episodeRecord> getAllEpisodesInShow (string title)
        {
            List<episodeRecord> records =_db.getAllEpisodeEntriesForShow(getIdOfExistingShow(title));
            return records;
        }

        private void removeEpisodes (int id)
        {
            _db.removeEpisodeEntries(id);
        }

        public string findImdbId (string name)
        {
            return _db.getTheImdbIdOfShow(name);
        }

        public int getIdOfExistingShow (string title)
        {
            return _db.getIdOfShow(title);
        }

        public List<ShowRecord> getAllShows ()
        {
            return _db.getAllEntries();
        }

        public List<ShowRecord> getAllShows (string name)
        {
            return _db.getAllEntriesWithName(name);
        }

        public List<ShowRecord> getAllUnfinishedShows ()
        {
            var shows = _db.getAllUnfinishedEntries();
            return shows;
        }

        public void editShow (ShowRecord show, string oldName)
        {
            _db.editEntry(show, oldName);
        }

        public bool checkIfShowAlreadyExists (ShowRecord show)
        {
            return _db.checkForExistingEntry(show.Title);
        }

        public Dictionary<int, List<episodeRecord>> getEpisodesInSeasons(string movieId)
        {
            int seasonCounter = 1;
            string url = "";
            Dictionary<int, List<episodeRecord>> result = new Dictionary<int,List<episodeRecord>>();
            ShowRecord record = new ShowRecord();
            int totalSeasons = addFromImdb(movieId).totalSeasons;
            var webClient = new WebClient();
            while (seasonCounter <= totalSeasons)
            {
                url = "http://www.omdbapi.com/?i=" + movieId + "&type=series&r=json&Season=" + seasonCounter;
                var json = string.Empty;
                try
                    {
                        json = webClient.DownloadString(url);
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                    }
                    record = JsonConvert.DeserializeObject<ShowRecord>(json);
                    result.Add(seasonCounter, record.Episodes);
                    seasonCounter++;
            }
            return result;
        }

        public ShowRecord addFromImdb (string movieId)
        {
            string seasonUrl = "http://www.omdbapi.com/?i=" + movieId + "&type=series&r=json&Season=1";

            string url = "http://www.omdbapi.com/?i=" + movieId + "&r=json";
            ShowRecord record;
            ShowRecord seasonRecord;
            using (var webClient = new WebClient())
            {
                var json = string.Empty;
                var seasonJson = string.Empty;
                try
                {
                    json = webClient.DownloadString(url);
                    seasonJson = webClient.DownloadString(seasonUrl);
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
                seasonRecord = JsonConvert.DeserializeObject<ShowRecord>(seasonJson);
                record = JsonConvert.DeserializeObject<ShowRecord>(json);
                record.totalSeasons = seasonRecord.totalSeasons;
            }
            return record;
        }

        public List<ShowRecord> getAllShowsFromImdb (string name)
        {
            string url = "http://www.omdbapi.com/?s=" + name + "&type=series&r=json";
            var result = new List<ShowRecord>();
            using (var webClient = new WebClient())
            {
                var json = string.Empty;
                try
                {
                    json = webClient.DownloadString(url);
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
                json = processJsonString(json);
                if (json.Length > 1)
                    result = JsonConvert.DeserializeObject<List<ShowRecord>>(json);
                return result;
            }
        }

        public void addEpisodesToShow (int id, Dictionary<int, List<episodeRecord>> records)
        {
            _db.addEpisodeEntries(id, records);
        }

        public void openTheimdbPage (string id)
        {
            System.Diagnostics.Process.Start("http://www.imdb.com/title/" + id);
        }

        private string processJsonString (string query)
        {
            StringBuilder sb = new StringBuilder();
            int counter = 0;
            foreach (var i in query)
            {
                if (i == '[' || i == ']')
                    counter++;
                if (counter == 1)
                    sb.Append(i);
            }
            sb.Append("]");
            return sb.ToString();
        }

        public bool checkIfInternetConnectionExists ()
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    using (var stream = webClient.OpenRead("http://www.google.com"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
