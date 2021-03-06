﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows;
using System.Collections.ObjectModel;
using TvSeriesProgressTracker.Models;

namespace TvSeriesProgressTracker
{
    public class DatabaseManipulation
    {
        public DatabaseManipulation () { }

        private readonly string connectionString = "Server=(localdb)\\MSSQLLocalDB;database=ShowsDB;integrated security=SSPI;";

        public void setUpDatabase ()
        {
            createDatabase();
            setUpTables();
        }

        /// <summary>
        /// Creates the new database
        /// </summary>
        private void createDatabase()
        {
            string connectionCreateString = "Server=(localdb)\\MSSQLLocalDB;database=master;integrated security=SSPI;";
            if (!checkForDbExistence(connectionCreateString, "ShowsDB"))
            {
                string query = "CREATE DATABASE ShowsDB;";
                var conn = new SqlConnection(connectionCreateString);
                var command = new SqlCommand(query, conn);
                try
                {
                    conn.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        /// <summary>
        /// Sets up all of the required tables if these are non existant
        /// </summary>
        private void setUpTables()
        {
            var conn = new SqlConnection(connectionString);
            string episodeQuery = "if not exists (select name from sysobjects where name = 'Episodes') CREATE TABLE" +
                " Episodes(EpisodeId int PRIMARY KEY IDENTITY (1, 1), Title nvarchar(100), EpisodeNumber int NOT NULL," +
                " Season int NOT NULL, IdofShow int, FOREIGN KEY (IdofShow) REFERENCES Shows(ShowId));";
            string query = "if not exists (select name from sysobjects where name = 'Shows') CREATE TABLE" +
                " Shows(ShowId int PRIMARY KEY IDENTITY (1, 1), Title nvarchar(100) NOT NULL, Genre nvarchar(50) NOT NULL,"
                + " CurrentEpisode int, CurrentSeason int, IsFinished bit, TotalSeasons int, ImdbId nvarchar(50));";
            var command = new SqlCommand(query, conn);
            var comm = new SqlCommand(episodeQuery, conn);
            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if ((conn.State == ConnectionState.Open))
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Checks if the database already exists
        /// </summary>
        /// <param name="connString">A connection to use</param>
        /// <param name="dbName">Name of the database</param>
        /// <returns>True is exists, else false</returns>
        private bool checkForDbExistence(string connString, string dbName)
        {
            bool result = false;
            var conn = new SqlConnection(connString);
            var command = new SqlCommand(string.Format(
                "SELECT db_id('{0}')", dbName), conn);
            try
            {
                conn.Open();
                result = (command.ExecuteScalar() != DBNull.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return result;
        }

        /// <summary>
        /// Adds new show entry to the database
        /// </summary>
        /// <param name="show">A show to add</param>
        public void createNewEntry (ShowRecord show)
        {
            string query = string.Format("Insert into Shows (Title, Genre, CurrentEpisode, CurrentSeason, IsFinished, TotalSeasons, ImdbId)" 
                + " values (@Title, @Genre, @CurrentEpisode, @CurrentSeason, @IsFinished, @TotalSeasons, @ImdbId);");
            var conn = new SqlConnection(connectionString);
            var command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@Title", show.Title);
            command.Parameters.AddWithValue("@Genre", show.Genre);
            command.Parameters.AddWithValue("@CurrentEpisode", show.CurrentEpisode);
            command.Parameters.AddWithValue("@CurrentSeason", show.CurrentSeason);
            command.Parameters.AddWithValue("@IsFinished", show.IsFinished);
            command.Parameters.AddWithValue("@TotalSeasons", show.totalSeasons);
            command.Parameters.AddWithValue("@ImdbId", show.imdbID);
            try
            {
                conn.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if ((conn.State == ConnectionState.Open))
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Changes details of the show in accordance to the given input
        /// </summary>
        /// <param name="record">A show to change</param>
        /// <param name="oldName">An original name, required for keeping track of the show</param>
        public void editEntry (ShowRecord record, string oldName, int id)
        {
            string query = string.Format("Update Shows set Title=@Title, Genre=@Genre, CurrentEpisode=@CurrentEpisode," +
                " CurrentSeason=@CurrentSeason, IsFinished=@IsFinished, TotalSeasons=@TotalSeasons where ShowId=@IdofShow;");
            var conn = new SqlConnection(connectionString);
            var command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@Title", record.Title);
            command.Parameters.AddWithValue("@Genre", record.Genre);
            command.Parameters.AddWithValue("@CurrentEpisode", record.CurrentEpisode);
            command.Parameters.AddWithValue("@CurrentSeason", record.CurrentSeason);
            command.Parameters.AddWithValue("@IsFinished", record.IsFinished);
            command.Parameters.AddWithValue("@TotalSeasons", record.totalSeasons);
            command.Parameters.AddWithValue("@OldTitle", oldName);
            command.Parameters.AddWithValue("@IdofShow", id);
            try
            {
                conn.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if ((conn.State == ConnectionState.Open))
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Updates an episode entry in the database
        /// </summary>
        /// <param name="record">Episode to update</param>
        /// <param name="oldName">Old name of the episode</param>
        public void editEpisodeEntry (EpisodeRecord record, int id)
        {
            string query = string.Format("Update Episodes set Title=@Title, Season=@Season, EpisodeNumber=@Episode" +
                " where EpisodeId=@IdofEpisode;");
            var conn = new SqlConnection(connectionString);
            var command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@Title", record.Title);
            command.Parameters.AddWithValue("@Season", record.Season);
            command.Parameters.AddWithValue("@Episode", record.Episode);
            command.Parameters.AddWithValue("@IdofEpisode", id);
            try
            {
                conn.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if ((conn.State == ConnectionState.Open))
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Removes show entry from the database
        /// </summary>
        /// <param name="title">A title of the show to remove</param>
        public void removeEntry (int id)
        {
            string query = string.Format("Delete from Shows where ShowId = @IdofShow;");
            var conn = new SqlConnection(connectionString);
            var command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@IdofShow", id);
            try
            {
                conn.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if ((conn.State == ConnectionState.Open))
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Removes all episodes of the given show from the database
        /// </summary>
        /// <param name="id">An id of the database show record</param>
        public void removeAllEpisodeEntries (int id)
        {
            string query = string.Format("Delete from Episodes where IdOfShow = @Id;");
            var conn = new SqlConnection(connectionString);
            var command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@Id", id);
            try
            {
                conn.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if ((conn.State == ConnectionState.Open))
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Removes an episode from a database depending on the given id and name
        /// </summary>
        /// <param name="id">Id of show</param>
        /// <param name="title">Title of the episode</param>
        public void removeSingleEpisodeEntry (int id)
        {
            string query = string.Format("Delete from episodes where EpisodeId=@IdofEpisode;");
            var conn = new SqlConnection(connectionString);
            var command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@IdofEpisode", id);
            try
            {
                conn.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Adds episodes to the database
        /// </summary>
        /// <param name="id">An id of the show</param>
        /// <param name="episodes">A collection of episodes</param>
        public void addEpisodeEntries (int id, List<EpisodeRecord> episodes)
        {
            string query = "";
            var connection = new SqlConnection(connectionString);
            connection.Open();
            foreach (var episode in episodes)
            {
                query = string.Format("Insert into Episodes(Title, EpisodeNumber, Season, IdofShow)" +
                    " values(@Title, @EpisodeNumber, @Season, @IdofShow);");
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Title", episode.Title);
                command.Parameters.AddWithValue("@EpisodeNumber", episode.Episode);
                command.Parameters.AddWithValue("@Season", episode.Season);
                command.Parameters.AddWithValue("@IdofShow", id);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }
            if ((connection.State == ConnectionState.Open))
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Changes current episode for particular show
        /// </summary>
        /// <param name="id">Id of the show</param>
        /// <param name="episode">New episode</param>
        /// <param name="season">New seasons</param>
        public void changeCurrentEpisode (int id, int episode, int season)
        {
            string query = string.Format("Update Shows set CurrentEpisode=@CurrentEpisode, CurrentSeason=@CurrentSeason" +
                " where ShowId=@ShowId;");
            var conn = new SqlConnection(connectionString);
            var command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@CurrentEpisode", episode);
            command.Parameters.AddWithValue("@CurrentSeason", season);
            command.Parameters.AddWithValue("@ShowId", id);
            try
            {
                conn.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                if ((conn.State == ConnectionState.Open))
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Check if show with the given name and id already exists in the database while editing the item
        /// </summary>
        /// <param name="title">A name of the show</param>
        /// <param name="id">A database id of the show record</param>
        /// <returns>True if exists, else false</returns>
        public bool checkForExistingShowEntry (string title)
        {
            bool result = false;
            string query = string.Format("Select count (*) from Shows where Title = @Title ;");
            var conn = new SqlConnection(connectionString);
            var command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@Title", title);
            try
            {
                conn.Open();
                int count = (int)command.ExecuteScalar();
                if (count > 0)
                    result = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if ((conn.State == ConnectionState.Open))
                {
                    conn.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// Check if episode already exists in the database
        /// </summary>
        /// <param name="title">Title of the episode</param>
        /// <param name="id">The id of show the episode belongs to</param>
        /// <returns>True if exists else false</returns>
        public bool checkForExistingEpisodeEntry (string title, int id)
        {
            bool result = false;
            string query = string.Format("Select count (*) from Episodes where Title=@Title and IdofShow=@ShowId;");
            var conn = new SqlConnection(connectionString);
            var command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@Title", title);
            command.Parameters.AddWithValue("@ShowId", id);
            try
            {
                conn.Open();
                int count = (int)command.ExecuteScalar();
                if (count > 0)
                    result = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if ((conn.State == ConnectionState.Open))
                {
                    conn.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// Gets all show entries currently existing in the database
        /// </summary>
        /// <returns>A list of shows</returns>
        public List<ShowRecord> getAllEntries ()
        {
            var shows = new List<ShowRecord>();
            var conn = new SqlConnection(connectionString);
            var command = new SqlCommand("Select * from Shows;", conn);
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ShowRecord show = new ShowRecord();
                        show.Title = reader.GetString(reader.GetOrdinal("Title"));
                        show.Genre = reader.GetString(reader.GetOrdinal("Genre"));
                        show.CurrentEpisode = reader.GetInt32(reader.GetOrdinal("CurrentEpisode"));
                        show.CurrentSeason = reader.GetInt32(reader.GetOrdinal("CurrentSeason"));
                        show.IsFinished = reader.GetBoolean(reader.GetOrdinal("IsFinished"));
                        show.totalSeasons = reader.GetInt32(reader.GetOrdinal("TotalSeasons"));
                        show.imdbID = reader.GetString(reader.GetOrdinal("ImdbId"));
                        shows.Add(show);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if ((conn.State == ConnectionState.Open))
                {
                    conn.Close();
                }
            }
            return shows;
        }

        /// <summary>
        /// Gets all episodes for particular show from the database
        /// </summary>
        /// <param name="id">Id of show</param>
        /// <returns>A list of episodes for the particular show</returns>
        public List<EpisodeRecord> getAllEpisodeEntriesForShow (int id)
        {
            var entries = new List<EpisodeRecord>();
            var conn = new SqlConnection(connectionString);
            string query = string.Format("Select * from Episodes where IdofShow = @IdofShow;");
            var command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@IdofShow", id);
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        EpisodeRecord record = new EpisodeRecord();
                        record.Title = reader.GetString(reader.GetOrdinal("Title"));
                        record.Episode = reader.GetInt32(reader.GetOrdinal("EpisodeNumber"));
                        record.Season = reader.GetInt32(reader.GetOrdinal("Season"));
                        record.ShowId = reader.GetInt32(reader.GetOrdinal("IdofShow"));
                        entries.Add(record);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                if ((conn.State == ConnectionState.Open))
                {
                    conn.Close();
                }
            }
            return entries;
        }

        /// <summary>
        /// Gets all show entries with the name that are similar to the one given by the user
        /// </summary>
        /// <param name="name">A name of the shows to search for</param>
        /// <returns>A list of found shows</returns>
        public List<ShowRecord> getAllEntriesWithName (string name)
        {
            var shows = new List<ShowRecord>();
            var conn = new SqlConnection(connectionString);
            string query = "Select * from Shows where Title like @names;";
            var command = new SqlCommand(query, conn);
            try
            {
                command.Parameters.AddWithValue("@names", name + "%");
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ShowRecord show = new ShowRecord();
                        show.Title = reader.GetString(reader.GetOrdinal("Title"));
                        show.Genre = reader.GetString(reader.GetOrdinal("Genre"));
                        show.CurrentEpisode = reader.GetInt32(reader.GetOrdinal("CurrentEpisode"));
                        show.CurrentSeason = reader.GetInt32(reader.GetOrdinal("CurrentSeason"));
                        show.IsFinished = reader.GetBoolean(reader.GetOrdinal("IsFinished"));
                        show.totalSeasons = reader.GetInt32(reader.GetOrdinal("TotalSeasons"));
                        show.imdbID = reader.GetString(reader.GetOrdinal("ImdbId"));
                        shows.Add(show);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if ((conn.State == ConnectionState.Open))
                {
                    conn.Close();
                }
            }
            return shows;
        }

        /// <summary>
        /// Gets all unfinished show entries from the database
        /// </summary>
        /// <returns>A list of unfinished shows</returns>
        public List<ShowRecord> getAllUnfinishedEntries()
        {
            var shows = new List<ShowRecord>();
            var conn = new SqlConnection(connectionString);
            var command = new SqlCommand("Select * from Shows where IsFinished = 0;", conn);
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ShowRecord show = new ShowRecord();
                        show.Title = reader.GetString(reader.GetOrdinal("Title"));
                        show.Genre = reader.GetString(reader.GetOrdinal("Genre"));
                        show.CurrentEpisode = reader.GetInt32(reader.GetOrdinal("CurrentEpisode"));
                        show.CurrentSeason = reader.GetInt32(reader.GetOrdinal("CurrentSeason"));
                        show.IsFinished = reader.GetBoolean(reader.GetOrdinal("IsFinished"));
                        show.totalSeasons = reader.GetInt32(reader.GetOrdinal("TotalSeasons"));
                        show.imdbID = reader.GetString(reader.GetOrdinal("ImdbId"));
                        shows.Add(show);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if ((conn.State == ConnectionState.Open))
                {
                    conn.Close();
                }
            }
            return shows;
        }

        /// <summary>
        /// Gets imdb if of particular shows
        /// </summary>
        /// <param name="name">A name of the show</param>
        /// <returns>Imdb if of the show</returns>
        public string getTheImdbIdOfShow (string name)
        {
            string showId = "";
            var conn = new SqlConnection(connectionString);
            string query = string.Format("Select ImdbId from Shows where Title=@Title;");
            var command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@Title", name);
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        showId = reader.GetString(reader.GetOrdinal("ImdbId"));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if ((conn.State == ConnectionState.Open))
                {
                    conn.Close();
                }
            }
            return showId;
        }

        /// <summary>
        /// Gets the id record of the show in the database
        /// </summary>
        /// <param name="name">Name of the show</param>
        /// <returns>An id record of the show</returns>
        public int getIdOfShow (string name)
        {
            int showId  = 0;
            var conn = new SqlConnection(connectionString);
            string query = string.Format("Select ShowId from Shows where Title=@Title;");
            var command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@Title", name);
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        showId = reader.GetInt32(reader.GetOrdinal("ShowId"));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if ((conn.State == ConnectionState.Open))
                {
                    conn.Close();
                }
            }
            return showId;
        }

        /// <summary>
        /// Gets the id record of the episode in the database
        /// </summary>
        /// <param name="title">Title of the episode</param>
        /// <param name="id">An id of the show the episode belongs to</param>
        /// <returns></returns>
        public int getIdOfEpisode (string title, int id)
        {
            int episodeId = 0;
            var conn = new SqlConnection(connectionString);
            string query = string.Format("Select EpisodeId from Episodes where Title=@Title and IdofShow=@ShowId;");
            var command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@Title", title);
            command.Parameters.AddWithValue("@ShowId", id);
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        episodeId = reader.GetInt32(reader.GetOrdinal("EpisodeId"));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if ((conn.State == ConnectionState.Open))
                {
                    conn.Close();
                }
            }
            return episodeId;
        }

        /// <summary>
        /// Returns title of the show depending on the given id
        /// </summary>
        /// <param name="id">Id of the show entry in the database</param>
        /// <returns>Title of the show</returns>
        public string getShowTitle (int id)
        {
            string result = "";
            string query = string.Format("Select Title from Shows where ShowId=@IdOfShow");
            var conn = new SqlConnection(connectionString);
            var command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@IdOfShow", id);
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read() && result == "")
                    {
                        result = reader.GetString(reader.GetOrdinal("Title"));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// Gets the id of the show for the given episode
        /// </summary>
        /// <param name="title">Title of the episode</param>
        /// <returns>A database id for the given episode's show</returns>
        public int getShowIdOfEpisode (string title)
        {
            int id = 0;
            var conn = new SqlConnection(connectionString);
            string query = string.Format("Select IdofShow from Episodes where Title=@Title;");
            var command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@Title", title);
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        id = reader.GetInt32(reader.GetOrdinal("IdofShow"));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if ((conn.State == ConnectionState.Open))
                {
                    conn.Close();
                }
            }
            return id;
        }
    }
}
