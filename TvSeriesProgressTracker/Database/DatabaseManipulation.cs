using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows;
using System.Collections.ObjectModel;

namespace TvSeriesProgressTracker
{
    public class DatabaseManipulation
    {
        public DatabaseManipulation () { }

        private readonly string connectionString = "Server=(localdb)\\MSSQLLocalDB;database=ShowsDB;integrated security=SSPI;";

        public void setUpDatabase ()
        {
            createDatabase();
            setUpTable();
        }

        public void createNewEntry (ShowRecord show)
        {
            string query = string.Format("Insert into Shows (Title, Genre, CurrentEpisode, CurrentSeason, IsFinished, TotalSeasons, ImdbId)" 
                + " values ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')", show.Title, show.Genre, show.CurrentEpisode, show.CurrentSeason,
                show.IsFinished, show.totalSeasons, show.imdbID);
            var conn = new SqlConnection(connectionString);
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
            finally
            {
                if ((conn.State == ConnectionState.Open))
                {
                    conn.Close();
                }
            }
        }

        public void editEntry (ShowRecord record, string oldName)
        {
            string query = string.Format("Update Shows set Title=('{0}'), Genre=('{1}'), CurrentEpisode=('{2}')," +
                " CurrentSeason=('{3}'), IsFinished=('{4}'), TotalSeasons=('{5}'), ImdbId=('{6}') where Title=('{7}')", record.Title, 
                record.Genre, record.CurrentEpisode, record.CurrentSeason, record.IsFinished, record.totalSeasons, 
                record.imdbID, oldName);
            var conn = new SqlConnection(connectionString);
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
            finally
            {
                if ((conn.State == ConnectionState.Open))
                {
                    conn.Close();
                }
            }
        }

        public void removeEntry (string title)
        {
            string query = string.Format("Delete from Shows where Title = ('{0}')", title);
            var conn = new SqlConnection(connectionString);
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
            finally
            {
                if ((conn.State == ConnectionState.Open))
                {
                    conn.Close();
                }
            }
        }

        public void removeEpisodeEntries (int id)
        {
            string query = string.Format("Delete from Episodes where IdOfShow = ('{0}')", id);
            var conn = new SqlConnection(connectionString);
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
            finally
            {
                if ((conn.State == ConnectionState.Open))
                {
                    conn.Close();
                }
            }
        }

        public void addEpisodeEntries (int id, Dictionary<int, List<episodeRecord>> records)
        {
            string query = "";
            var connection = new SqlConnection(connectionString);
            connection.Open();
            foreach (KeyValuePair<int, List<episodeRecord>> record in records)
            {
                foreach (episodeRecord episode in record.Value)
                {
                    var command = new SqlCommand(query, connection);
                    query = string.Format("Insert into Episodes(Title, EpisodeNumber, Season, IdofShow)" +
                        " values('{0}', '{1}', '{2}', '{3}');", episode.Title, episode.Episode, record.Key, id);
                    command = new SqlCommand(query, connection);
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                    }
                }
            }
            if ((connection.State == ConnectionState.Open))
            {
                connection.Close();
            }
        }

        public bool checkForExistingEntry (string title)
        {
            bool result = false;
            string query = string.Format("Select count (*) from Shows where Title = ('{0}')", title);
            var conn = new SqlConnection(connectionString);
            var command = new SqlCommand(query, conn);
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


        //Helper methods
        private void createDatabase ()
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

        private void setUpTable ()
        {
            var conn = new SqlConnection(connectionString);
            string episodeQuery = "if not exists (select name from sysobjects where name = 'Episodes') CREATE TABLE" +
                " Episodes(EpisodeId int PRIMARY KEY IDENTITY (1, 1), Title nvarchar(50), EpisodeNumber int NOT NULL," +
                " Season int NOT NULL, IdofShow int, FOREIGN KEY (IdofShow) REFERENCES Shows(ShowId))";
            string query = "if not exists (select name from sysobjects where name = 'Shows') CREATE TABLE" +
                " Shows(ShowId int PRIMARY KEY IDENTITY (1, 1), Title nvarchar(50) NOT NULL, Genre nvarchar(50) NOT NULL,"
                + " CurrentEpisode int, CurrentSeason int, IsFinished bit, TotalSeasons int, ImdbId nvarchar(50)) ";
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

        public List<ShowRecord> getAllEntries ()
        {
            var shows = new List<ShowRecord>();
            var conn = new SqlConnection(connectionString);
            var command = new SqlCommand("Select * from Shows", conn);
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

        public List<episodeRecord> getAllEpisodeEntriesForShow (int id)
        {
            var entries = new List<episodeRecord>();
            var conn = new SqlConnection(connectionString);
            string query = string.Format("Select * from Episodes where IdofShow = ('{0}')", id);
            var command = new SqlCommand(query, conn);
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        episodeRecord record = new episodeRecord();
                        record.Title = reader.GetString(reader.GetOrdinal("Title"));
                        record.Episode = reader.GetInt32(reader.GetOrdinal("EpisodeNumber"));
                        record.Season = reader.GetInt32(reader.GetOrdinal("Season"));
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

        public List<ShowRecord> getAllEntriesWithName (string name)
        {
            var shows = new List<ShowRecord>();
            var conn = new SqlConnection(connectionString);
            string query = "Select * from Shows where Title like @names";
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

        public List<ShowRecord> getAllUnfinishedEntries()
        {
            var shows = new List<ShowRecord>();
            var conn = new SqlConnection(connectionString);
            var command = new SqlCommand("Select * from Shows where IsFinished = 0", conn);
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

        public string getTheImdbIdOfShow (string name)
        {
            string showId = "";
            var conn = new SqlConnection(connectionString);
            string query = string.Format("Select ImdbId from Shows where Title=('{0}')", name);
            var command = new SqlCommand(query, conn);
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

        public int getIdOfShow (string name)
        {
            int showId  = 0;
            var conn = new SqlConnection(connectionString);
            string query = string.Format("Select ShowId from Shows where Title=('{0}')", name);
            var command = new SqlCommand(query, conn);
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

    }
}
