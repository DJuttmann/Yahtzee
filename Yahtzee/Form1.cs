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
  public partial class Form1: Form
  {
    private YahtzeeGame Game = new YahtzeeGame ();
    private Label [] CategoryNames = new Label [13];
    private Label [] Player1Scores = new Label [13];
    private Label [] Player2Scores = new Label [13];
    private DiceContainer [] Player1Categories = new DiceContainer [13];
    private DiceContainer [] Player2Categories = new DiceContainer [13];
    private Button [] SelectCategoryButtons = new Button [13];
    public Image [] DiceImages = new Image [7];
    public Image [] DiceImagesSelected = new Image [7];
    private bool [] SelectedDice = new bool [5];

    private Label LabelPlayer1 = new Label ();
    private Label LabelPlayer2 = new Label ();
    private Label LabelTotalScore = new Label ();
    private Label LabelPlayer1Score = new Label ();
    private Label LabelPlayer2Score = new Label ();
    private Label LabelStatus;
    private DiceContainer CurrentDice;
    private Button Roll = new Button ();
    private Button Restart = new Button ();

//========================================================================================
// Initialization

    public Form1 ()
    {
      InitializeComponent ();

      AddMainControls ();
      LoadDiceImages ();
      AddButtons ();
      AddDiceResults ();
      AddLabels ();
      NewGame ();
    }


    private void LoadDiceImages ()
    {
      for (int i = 1; i <= 6; i++)
      {
        DiceImages [i] = Image.FromFile ("Die" + i.ToString () + ".png");
        DiceImagesSelected [i] = Image.FromFile ("Die" + i.ToString () + "Select.png");
      }
    }


    private void AddMainControls ()
    {
      LabelStatus = new Label ();
      LabelStatus.Left = 25;
      LabelStatus.Top = 50;
      LabelStatus.Width = 200;
      LabelStatus.Text = "Player 1";

      CurrentDice = new DiceContainer ();
      CurrentDice.Left = 25;
      CurrentDice.Top = 100;
      CurrentDice.Size = 50;
      CurrentDice.AddClickHandler (SelectDieClick);

      Roll.Left = 25;
      Roll.Top = 180;
      Roll.Width = 100;
      Roll.Height = 30;
      Roll.Text = "Roll";
      Roll.Click += RollClick;

      Restart.Left = 25;
      Restart.Top = 380;
      Restart.Width = 100;
      Restart.Height = 30;
      Restart.Text = "Restart";
      Restart.Click += RestartClick;

      Controls.Add (LabelStatus);
      CurrentDice.AddToForm (this);
      Controls.Add (Roll);
      Controls.Add (Restart);
    }


    private void AddDiceResults ()
    {
      for (int i = 0; i < 13; i++)
      {
        Player1Categories [i] = new DiceContainer ();
        Player1Categories [i].Left = 400;
        Player1Categories [i].Top = i * 25 + 47;
        Player1Categories [i].Size = 20;
        Player1Categories [i].AddToForm (this);

        Player2Categories [i] = new DiceContainer ();
        Player2Categories [i].Left = 550;
        Player2Categories [i].Top = i * 25 + 47;
        Player2Categories [i].Size = 20;
        Player2Categories [i].AddToForm (this);
      }
    }


    private void AddButtons ()
    {
      for (int i = 0; i < 13; i++)
      {
        SelectCategoryButtons [i] = new Button ();
        SelectCategoryButtons [i].Top = i * 25 + 45;
        SelectCategoryButtons [i].Width = 100;
        SelectCategoryButtons [i].Text = "Submit";
        SelectCategoryButtons [i].Click += new System.EventHandler (SelectCategoryClick);
        Controls.Add (SelectCategoryButtons [i]);
      }
    }


    private void AddLabels ()
    {
      for (int i = 0; i < 13; i++)
      {
        CategoryNames [i] = new Label ();
        CategoryNames [i].Left = 300;
        CategoryNames [i].Top = i * 25 + 50;
        CategoryNames [i].Text = Rules.CategoryNames [i];

        Player1Scores [i] = new Label ();
        Player1Scores [i].Left = 505;
        Player1Scores [i].Top = i * 25 + 50;
        Player1Scores [i].Text = "0";

        Player2Scores [i] = new Label ();
        Player2Scores [i].Left = 655;
        Player2Scores [i].Top = i * 25 + 50;
        Player2Scores [i].Text = "0";

        Controls.Add (CategoryNames [i]);
        Controls.Add (Player1Scores [i]);
        Controls.Add (Player2Scores [i]);
      }
      LabelPlayer1.Left = 400;
      LabelPlayer1.Top = 20;
      LabelPlayer1.Text = "Player 1";
      Controls.Add (LabelPlayer1);

      LabelPlayer2.Left = 550;
      LabelPlayer2.Top = 20;
      LabelPlayer2.Text = "Player 2";
      Controls.Add (LabelPlayer2);

      LabelTotalScore.Left = 300;
      LabelTotalScore.Top = 380;
      LabelTotalScore.Text = "Total Score";
      Controls.Add (LabelTotalScore);

      LabelPlayer1Score.Left = 505;
      LabelPlayer1Score.Top = 380;
      LabelPlayer1Score.Text = "0";
      Controls.Add (LabelPlayer1Score);

      LabelPlayer2Score.Left = 655;
      LabelPlayer2Score.Top = 380;
      LabelPlayer2Score.Text = "0";
      Controls.Add (LabelPlayer2Score);
    }

//========================================================================================
// Updating

    // Update status text.
    private void UpdateStatus ()
    {
      if (Game.Round >= 13)
      {
        string text = "Game over. ";
        if (Game.ScorePlayer1 > Game.ScorePlayer2)
          LabelStatus.Text = text + "Player 1 wins!";
        else if (Game.ScorePlayer1 < Game.ScorePlayer2)
          LabelStatus.Text = text + "Player 2 wins!";
        else
          LabelStatus.Text = text + "It's a draw!";
      }
      else
        LabelStatus.Text = "Round " + Game.Round.ToString () +
                           " - Player " + 
                           ((Game.ActivePlayer == YahtzeeGame.Player.Player1) ?
                           "1" : "2") +
                           " - Rolls used: " + Game.RollsUsed.ToString ();
    }


    // Move submit buttons to correct position, show the ones for available categories.
    private void SetButtonPositions ()
    {
      int x;
      if (Game.ActivePlayer == YahtzeeGame.Player.Player1)
        x = 400;
      else
        x = 550;
      for (int i = 0; i < 13; i++)
      {
        SelectCategoryButtons [i].Left = x;
        SelectCategoryButtons [i].Visible = Game.GetPlayerScore (Game.ActivePlayer,
                                                           (Rules.Category) i) == -1;
      }
    }


    // Enable submit buttons.
    private void EnableButtons ()
    {
      for (int i = 0; i < 13; i++)
        SelectCategoryButtons [i].Enabled = true;
    }


    // Disable submit buttons.
    private void DisableButtons ()
    {
      for (int i = 0; i < 13; i++)
        SelectCategoryButtons [i].Enabled = false;
    }


    // Convert score to strin, or "-" when unscored (-1).
    private string ScoreToString (int score)
    {
      if (score == -1)
        return "\x2014";
      return score.ToString ();
    }


    // Update the score display.
    private void UpdateScores ()
    {
      for (int i = 0; i < 13; i++)
      {
        Player1Scores [i].Text = ScoreToString (
          Game.GetPlayerScore (YahtzeeGame.Player.Player1, (Rules.Category) i));
        Player2Scores [i].Text = ScoreToString (
          Game.GetPlayerScore (YahtzeeGame.Player.Player2, (Rules.Category) i));
        Player1Scores [i].ForeColor = Color.Black;
        Player2Scores [i].ForeColor = Color.Black;
      }
      LabelPlayer1Score.Text = Game.ScorePlayer1.ToString ();
      LabelPlayer2Score.Text = Game.ScorePlayer2.ToString ();
    }


    // Show what current dice would score for available categories.
    private void ShowPotentialScores ()
    {
      Label [] scoreList = (Game.ActivePlayer == YahtzeeGame.Player.Player1) ?
                           Player1Scores : Player2Scores;
      int score;
      for (int i = 0; i < 13; i++)
      {
        score = Game.ScoreDice ((Rules.Category) i);
        if (Game.GetPlayerScore (Game.ActivePlayer, (Rules.Category) i) == -1)
        {
          scoreList [i].Text = score.ToString ();
          scoreList [i].ForeColor = Color.Red;
        }
      }
    }


    // Update Current Dice
    private void UpdateCurrentDice ()
    {
      for (int i = 0; i < 5; i++)
      {
        if (SelectedDice [i])
          CurrentDice.SetDie (i, DiceImagesSelected [Game.Dice [i]]);
        else
          CurrentDice.SetDie (i, DiceImages [Game.Dice [i]]);
      }
    }


    // Get the selected dice indices as an array.
    private int [] GetSelectedDice ()
    {
      List <int> select = new List <int> ();
      for (int i = 0; i < 5; i++)
        if (SelectedDice [i])
          select.Add (i);
      return select.ToArray ();
    }


    // Submit score
    private void SubmitScore (int index)
    {
      YahtzeeGame.Player player = Game.ActivePlayer;
      Rules.Category category = (Rules.Category) index;
      Label scoreLabel;
      DiceContainer container;
      int [] finalDice = new int [5];
      for (int i = 0; i < 5; i++)
        finalDice [i] = Game.Dice [i];
      if (Game.SubmitScore (category))
      {
        SelectCategoryButtons [index].Visible = false;
        if (player == YahtzeeGame.Player.Player1)
        {
          scoreLabel = Player1Scores [index];
          container = Player1Categories [index];
        }
        else
        {
          scoreLabel = Player2Scores [index];
          container = Player2Categories [index];
        }
        for (int i = 0; i < 5; i++)
          container.SetDie (i, DiceImages [finalDice [i]]);
        UpdateScores ();
        DisableButtons ();
        SetButtonPositions ();
        CurrentDice.ClearDice ();
        UpdateStatus ();
        Roll.Enabled = true;
      }
    }


    // Clear the displayed results for all categories.
    private void ClearCategories ()
    {
      for (int i = 0; i < 13; i++)
      {
        Player1Categories [i].ClearDice ();
        Player2Categories [i].ClearDice ();
      }
    }


    // Start a new game.
    private void NewGame ()
    {
      Game.NewGame ();
      UpdateStatus ();
      DisableButtons ();
      SetButtonPositions ();
      ClearCategories ();
      UpdateScores ();
      CurrentDice.ClearDice ();
      Roll.Enabled = true;
    }

//========================================================================================
// Event Handlers

    // Handler for score submit buttons.
    private void SelectCategoryClick (object sender, EventArgs e)
    {
      for (int i = 0; i < 13; i++)
      {
        if (SelectCategoryButtons [i] == sender)
        {
          SubmitScore (i);
          break;
        }
      }
    }


    // Handler for roll button.
    private void RollClick (object sender, EventArgs e)
    {
      bool success;
      if (Game.RollsUsed == 0)
        success = Game.FirstRoll ();
      else
        success = Game.Reroll (GetSelectedDice ());
      if (success)
      {
        for (int i = 0; i < 5; i++)
          SelectedDice [i] = false;
        UpdateCurrentDice ();
        ShowPotentialScores ();
        EnableButtons ();
        UpdateStatus ();
        if (Game.RollsUsed == 3)
          Roll.Enabled = false;
      }
    }


    // Handler for dice selecting.
    private void SelectDieClick (object sender, EventArgs e)
    {
      int index = CurrentDice.GetImageIndex ((PictureBox) sender);
      if (index == -1)
        return;
      SelectedDice [index] = !SelectedDice [index];
      if (SelectedDice [index])
        CurrentDice.SetDie (index, DiceImagesSelected [Game.Dice [index]]);
      else
        CurrentDice.SetDie (index, DiceImages [Game.Dice [index]]);
    }


    // Handler for restarting the game.
    private void RestartClick (object sender, EventArgs e)
    {
      NewGame ();
    }
  }

﻿//========================================================================================
// Class DiceContainer
﻿//========================================================================================

  public class DiceContainer
  {
    private Panel Box;
    private PictureBox [] DicePictureBoxes;
    private int size = 50;
    public int Size {get {return size;} set {SetSize (value);}}

    public int Top
    {
      get {return Box.Top;}
      set {Box.Top = value;}
    }
    public int Left
    {
      get {return Box.Left;}
      set {Box.Left = value;}
    }


    public DiceContainer ()
    {
      Box = new Panel ();
      Box.Width = 300;
      Box.Height = 50;
      DicePictureBoxes = new PictureBox [5];
      for (int i = 0; i < 5; i++)
      {
        DicePictureBoxes [i] = new PictureBox ();
        DicePictureBoxes [i].SizeMode = PictureBoxSizeMode.Zoom;
        DicePictureBoxes [i].Left = (Size + Size / 10) * i;
        DicePictureBoxes [i].Width = Size;
        DicePictureBoxes [i].Top = 0;
        DicePictureBoxes [i].Height = Size;
        DicePictureBoxes [i].Image = null;
        Box.Controls.Add (DicePictureBoxes [i]);
      }
    }


    private void SetSize (int size)
    {
      this.size = size;
      Box.Height = size;
      Box.Width = Size * 5;
      int i = 0;
      foreach (PictureBox picture in Box.Controls)
      {
        picture.Width = size;
        picture.Height = size;
        picture.Top = 0;
        picture.Left = Size * i;
        i++;
      }
    }


    public int GetImageIndex (PictureBox img)
    {
      for (int i = 0; i < 5; i++)
        if (DicePictureBoxes [i] == img)
          return i;
      return -1;
    }


    public void SetDie (int index, Image img)
    {
      if (index >= 0 && index < 5)
      {
        ((PictureBox) Box.Controls [index]).Image = img;
      }
    }


    public void ClearDice ()
    {
      for (int i = 0; i < 5; i++)
        ((PictureBox) Box.Controls [i]).Image = null;
    }


    public void AddClickHandler (System.EventHandler handler)
    {
      for (int i = 0; i < 5; i++)
        DicePictureBoxes [i].Click += handler;
    }


    public void AddToForm (Form1 parentForm)
    {
      parentForm.Controls.Add (Box);
    }
  }
}
