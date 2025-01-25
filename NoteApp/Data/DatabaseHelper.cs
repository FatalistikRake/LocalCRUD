using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace NoteApp
{
    public class DatabaseHelper
    {
        private string connectionString = "Data Source=NotesDatabase.db;Version=3;";

        public void CreateTable()
        {
            try
            {
                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string query = @"CREATE TABLE IF NOT EXISTS Notes (
                                        Id INTEGER PRIMARY KEY,
                                        Title TEXT NOT NULL,
                                        Content TEXT NOT NULL,
                                        CreatedAt DATETIME NOT NULL
                                    );";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Error creating table: {ex.Message}");
            }
        }

        public void SaveNote(Note note)
        {
            try
            {
                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO Notes (Title, Content, CreatedAt) VALUES (@Title, @Content, @CreatedAt);";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Title", note.Title);
                        cmd.Parameters.AddWithValue("@Content", note.Content);
                        cmd.Parameters.AddWithValue("@CreatedAt", note.CreatedAt);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Error saving note: {ex.Message}");
            }
        }

        public List<Note> GetAllNotes()
        {
            var notes = new List<Note>();
            try
            {
                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM Notes;";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                notes.Add(new Note
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Title = reader["Title"].ToString()!,
                                    Content = reader["Content"].ToString(),
                                    CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                                });
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Error fetching notes: {ex.Message}");
            }
            return notes;
        }

        public void UpdateNote(Note note)
        {
            try
            {
                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE Notes SET Title = @Title, Content = @Content WHERE Id = @Id;";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", note.Id);
                        cmd.Parameters.AddWithValue("@Title", note.Title);
                        cmd.Parameters.AddWithValue("@Content", note.Content);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Error updating note: {ex.Message}");
            }
        }

        public void DeleteNote(int id)
        {
            try
            {
                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM Notes WHERE Id = @Id;";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Error deleting note: {ex.Message}");
            }
        }
    }
}
