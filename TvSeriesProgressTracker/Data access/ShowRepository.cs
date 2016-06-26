using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows;
using TvSeriesProgressTracker.Models;

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

        /// <summary>
        /// Add new show to the programme
        /// </summary>
        /// <param name="show">A show to add</param>
        /// <returns>Returns true if the show was added successfully, else false</returns>
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

        /// <summary>
        /// Removes show from the programme
        /// </summary>
        /// <param name="show">A show to remove</param>
        public void removeShow (ShowRecord show)
        {
            var id = getIdOfExistingShow(show.Title);
            removeEpisodes(id);
            _db.removeEntry(show.Title);
        }

        /// <summary>
        /// Gets all episodes of existing show
        /// </summary>
        /// <param name="title">A name of show</param>
        /// <returns>A list of show's episodes</returns>
        public List<EpisodeRecord> getAllEpisodesInShow (string title)
        {
            List<EpisodeRecord> records =_db.getAllEpisodeEntriesForShow(getIdOfExistingShow(title));
            return records;
        }

        /// <summary>
        /// Removes all episodes from show, required for show delition
        /// </summary>
        /// <param name="id">id of the show</param>
        private void removeEpisodes (int id)
        {
            _db.removeEpisodeEntries(id);
        }

        /// <summary>
        /// Gets imdb id of added show
        /// </summary>
        /// <param name="name">Name of show</param>
        /// <returns>A string containing imdbid of the show</returns>
        public string findImdbId (string name)
        {
            return _db.getTheImdbIdOfShow(name);
        }


        /// <summary>
        /// Gets a database id of the record, required for episodes manipulation
        /// </summary>
        /// <param name="title">Name of the show</param>
        /// <returns>A database record's id</returns>
        public int getIdOfExistingShow (string title)
        {
            return _db.getIdOfShow(title);
        }

        /// <summary>
        /// Gets all shows
        /// </summary>
        /// <returns>A list of all shows currently entered by the user</returns>
        public List<ShowRecord> getAllShows ()
        {
            return _db.getAllEntries();
        }

        /// <summary>
        /// Gets all shows with the given name
        /// </summary>
        /// <param name="name">Name of the show</param>
        /// <returns>A list of shows with the name similar to the one given by user</returns>
        public List<ShowRecord> getAllShows (string name)
        {
            return _db.getAllEntriesWithName(name);
        }

        /// <summary>
        /// Gets all unfinished shows 
        /// </summary>
        /// <returns>A list of currently unfinished shows</returns>
        public List<ShowRecord> getAllUnfinishedShows ()
        {
            var shows = _db.getAllUnfinishedEntries();
            return shows;
        }

        /// <summary>
        /// Changed details of the show to the new user inputs
        /// </summary>
        /// <param name="show">Shot to change</param>
        /// <param name="oldName">An old name of the show, required for track keeping</param>
        public void editShow (ShowRecord show, string oldName)
        {
            _db.editEntry(show, oldName);
        }

        /// <summary>
        /// Changed the current episode and season that are being watched by the user
        /// </summary>
        /// <param name="episode">A new episode to change</param>
        public void changeCurrentEpisode (EpisodeRecord episode)
        {
            _db.changeCurrentEpisode(episode.ShowId, episode.Episode, episode.Season);
        }


        /// <summary>
        /// Checks if particular show already exists in the database
        /// </summary>
        /// <param name="show"></param>
        /// <returns></returns>
        public bool checkIfShowAlreadyExists (ShowRecord show)
        {
            return _db.checkForExistingEntry(show.Title);
        }

        /// <summary>
        /// Obtains all episodes for particular show
        /// </summary>
        /// <param name="movieId">AN imdb id of the show</param>
        /// <returns>A dictionary of season and episodes</returns>
        public Dictionary<int, List<EpisodeRecord>> getEpisodesInSeasons(string movieId)
        {
            int seasonCounter = 1;
            string url = "";
            Dictionary<int, List<EpisodeRecord>> result = new Dictionary<int,List<EpisodeRecord>>();
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

        /// <summary>
        /// Adds actual show from the api
        /// </summary>
        /// <param name="movieId">An imdb id of the show</param>
        /// <returns>A show that was added</returns>
        public ShowRecord addFromImdb (string movieId)
        {
            string seasonUrl = "http://www.omdbapi.com/?i=" + movieId + "&type=series&r=json&Season=1";
            //required since seasonUrl does not give all necessary info such as genre
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

        /// <summary>
        /// Aquires list shows depending on the given name
        /// </summary>
        /// <param name="name">Name of the show provided by the user</param>
        /// <returns>A list of all shows with the similar name</returns>
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

        /// <summary>
        /// Adds episodes to show
        /// </summary>
        /// <param name="id">Id of the show entry in the database</param>
        /// <param name="records">A dictionary of episodes</param>
        public void addEpisodesToShow (int id, Dictionary<int, List<EpisodeRecord>> records)
        {
            _db.addEpisodeEntries(id, records);
        }


        /// <summary>
        /// Opens IMDB page of the show
        /// </summary>
        /// <param name="id">Id at imdb to pass to the browser</param>
        public void openTheimdbPage (string id)
        {
            System.Diagnostics.Process.Start("http://www.imdb.com/title/" + id);
        }


        /// <summary>
        /// Processes the aquired Json string and ensures that it is valid
        /// </summary>
        /// <param name="query">A json string</param>
        /// <returns>A valid json string</returns>
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

        /// <summary>
        /// Checks if internet connection is available, required for onlien functionality
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Finds the show for the given episode
        /// </summary>
        /// <param name="title">Title of the episode</param>
        /// <returns>Show id of the episode</returns>
        public int findEpisodesShowId (string title)
        {
            return _db.getShowIdOfEpisode(title);
        }
    }
}
