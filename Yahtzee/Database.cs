using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;


namespace Yahtzee
{
  struct HighScore
  {
    public int Rank;
    public int Score;
    public string PlayerName;
    public string Date;
  }

//========================================================================================
// class DatabaseManager
//========================================================================================

  class DatabaseManager
  {
    public static string FileName = "YahtzeeDatabase.sqlite";
    private SQLiteConnection Connection;

//========================================================================================
// Initialization, open and close

    // Constructor. Check if database file exists, creat if it doesn't.
    public DatabaseManager ()
    {
      if (!File.Exists (FileName))
        if (!CreateDatabase ())
          MessageBox.Show ("Error: could not create highscore database.");
    }


    // Create a new database file and add a highscore table.
    private bool CreateDatabase ()
    {
      try
      {
        SQLiteConnection.CreateFile (FileName);
      }
      catch (SQLiteException ex)
      {
        MessageBox.Show (ex.Message);
        return false;
      }
      Connection = new SQLiteConnection ("Data Source=" + FileName + ";Version=3;");
      if (Open ())
      {
        SQLiteCommand command = new SQLiteCommand (Connection);

        command.CommandText = @"
CREATE TABLE highscores (id INTEGER PRIMARY KEY, playername VARCHAR(32), score INT, date VARCHAR(32))";
        command.ExecuteNonQuery ();

        command.CommandText = @"
INSERT INTO highscores (playername, score, date) values ('Max Score', 1575, '2000-01-01 00:00')";
        command.ExecuteNonQuery ();

        command.CommandText = @"
INSERT INTO highscores (playername, score, date) values ('Min Score', 12, '2000-01-01 00:00')";
        command.ExecuteNonQuery ();

        command.CommandText = @"
INSERT INTO highscores (playername, score, date) values ('Average', 254, '2000-01-01 00:00')";
        command.ExecuteNonQuery ();

        command.CommandText = @"
INSERT INTO highscores (playername, score, date) values ('Random', 350, '2000-01-01 00:00')";
        command.ExecuteNonQuery ();

        Close ();
        return true;
      }
      return false;
    }


    // Open a connection to the database.
    private bool Open ()
    {
      Connection = new SQLiteConnection ("Data Source=" + FileName + ";Version=3;");
      try
      {
        Connection.Open ();
        return true;
      }
      catch
      {
        return false;
      }
    }


    // Close a connection to the database.
    private void Close ()
    {
      Connection.Close ();
    }

//========================================================================================
// Reading and Writing

    // Return the top n scores from the database.
    public List <HighScore> GetTopScores (int n)
    {
      List <HighScore> HighScores = new List <HighScore> ();
      if (Open ())
      {
        SQLiteCommand command = new SQLiteCommand (Connection);
        command.CommandText = "SELECT * FROM highscores ORDER BY score DESC LIMIT " +
                              n.ToString ();
        SQLiteDataReader reader = command.ExecuteReader ();
        int i = 1;
        while (reader.Read ())
        {
          HighScore score = new HighScore
          {
            Rank = i,
            Score = (int) reader ["score"],
            PlayerName = (string) reader ["playername"],
            Date = (string) reader ["date"]
          };
          HighScores.Add (score);
          i++;
        }
        Close ();
      }
      return HighScores;
    }


    // Add a score to the database.
    public void AddScore (string playerName, int score, string date)
    {
      if (Open ())
      {
        SQLiteCommand command = new SQLiteCommand (Connection)
        {
          CommandText = 
            "INSERT INTO highscores (playername, score, date) values (" +
            playerName + ", " + score.ToString () + ", " + date + ")"
        };
        command.ExecuteNonQuery ();
        Close ();
      }
    }
  }
}