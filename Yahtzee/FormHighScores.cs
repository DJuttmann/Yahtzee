using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yahtzee
{
  public partial class FormHighScores: Form
  {
    private Label LabelPlace = new Label ();
    private Label LabelScore = new Label ();
    private Label LabelPlayerName = new Label ();
    private Label LabelDate = new Label ();

    private DatabaseManager HighScoreDatabase = new DatabaseManager ();


    public FormHighScores ()
    {
      InitializeComponent ();
      LabelPlace.Text = "#";
      LabelScore.Text = "Score";
      LabelPlayerName.Text = "Player";
      LabelDate.Text = "Date";

    }


    public void SubmitScore (string playerName, int score, string date)
    {
      HighScoreDatabase.AddScore (playerName, score, date);
    }


    public void LoadDatabase ()
    {
      List <HighScore> highScores = HighScoreDatabase.GetTopScores (10);
      tableHighScores.Controls.Clear ();
      tableHighScores.RowCount = (highScores.Count + 1) * 4;

      tableHighScores.Controls.Add (LabelPlace);
      tableHighScores.Controls.Add (LabelScore);
      tableHighScores.Controls.Add (LabelPlayerName);
      tableHighScores.Controls.Add (LabelDate);
      foreach (HighScore newScore in highScores)
      {
        Label rank = new Label ();
        rank.Text = newScore.Rank.ToString ();
        tableHighScores.Controls.Add (rank);
        
        Label score = new Label ();
        score.Text = newScore.Score.ToString ();
        tableHighScores.Controls.Add (score);

        Label playerName = new Label ();
        playerName.Text = newScore.PlayerName;
        tableHighScores.Controls.Add (playerName);

        Label date = new Label ();
        date.Text = newScore.Date;
        tableHighScores.Controls.Add (date);
      }
    }
  }
}
