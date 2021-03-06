﻿using System;
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
  public partial class FormMainWindow: Form
  {
    private YahtzeeGame Game = new YahtzeeGame ();
    private Label [] CategoryNames = new Label [Rules.CategoryCount];
    private Label [] Player1Scores = new Label [Rules.CategoryCount];
    private Label [] Player2Scores = new Label [Rules.CategoryCount];
    private DiceContainer [] Player1Categories = new DiceContainer [Rules.CategoryCount];
    private DiceContainer [] Player2Categories = new DiceContainer [Rules.CategoryCount];
    private Button [] SelectCategoryButtons = new Button [Rules.CategoryCount];
    private Image [] DiceImages = new Image [Rules.DiceSides + 1];
    private Image [] DiceImagesSelected = new Image [Rules.DiceSides + 1];
    private bool [] SelectedDice = new bool [DiceSet.DiceCount];

    private Label LabelPlayer1 = new Label ();
    private Label LabelPlayer2 = new Label ();
    private Label LabelBonus = new Label ();
    private Label LabelPlayer1Bonus = new Label ();
    private Label LabelPlayer2Bonus = new Label ();
    private Label LabelTotalScore = new Label ();
    private Label LabelPlayer1Score = new Label ();
    private Label LabelPlayer2Score = new Label ();
    private Label LabelStatus = new Label ();
    private Label LabelWarning = new Label ();
    private DiceContainer CurrentDice;
    private Button ButtonRoll = new Button ();
    private Button ButtonRestart = new Button ();

    private Button ButtonHighScores = new Button ();
    FormHighScores HighScores = new FormHighScores ();

//========================================================================================
// Initialization

    // Constructor.
    public FormMainWindow ()
    {
      InitializeComponent ();

      if (!LoadDiceImages ())
        return;
      AddMainControls ();
      AddButtons ();
      AddDiceResults ();
      AddLabels ();
    }


    // Load dice images from Data folder.
    private bool LoadDiceImages ()
    {
      try
      {
        for (int i = 1; i <= 6; i++)
        {
          DiceImages [i] = Image.FromFile ("Data/Die" + i.ToString () + ".png");
          DiceImagesSelected [i] = Image.FromFile ("Data/Die" + i.ToString () +
                                                   "Select.png");
        }
      }
      catch (System.IO.FileNotFoundException ex)
      {
        MessageBox.Show (@"
Error: image file not found.
Make sure the images Die#.png and Die#Selected.png exist in the Data folder."
          );
        return false;
      }
      catch (Exception ex)
      {
        MessageBox.Show ("Error: Could not open files");
        return false;
      }
      return true;
    }


    // Initialize and add main game controls to the main window.
    private void AddMainControls ()
    {
      CurrentDice = new DiceContainer ();
      CurrentDice.Left = 25;
      CurrentDice.Top = 110;
      CurrentDice.Size = 50;
      CurrentDice.AddClickHandler (SelectDieClick);
      CurrentDice.AddToForm (this);

      ButtonRoll.Left = 25;
      ButtonRoll.Top = 180;
      ButtonRoll.Width = 100;
      ButtonRoll.Height = 30;
      ButtonRoll.Text = "Roll";
      ButtonRoll.Click += RollClick;
      ButtonRoll.Enabled = false;
      Controls.Add (ButtonRoll);

      ButtonRestart.Left = 25;
      ButtonRestart.Top = 370;
      ButtonRestart.Width = 100;
      ButtonRestart.Height = 30;
      ButtonRestart.Text = "New Game";
      ButtonRestart.Click += RestartClick;
      Controls.Add (ButtonRestart);

      ButtonHighScores.Left = 150;
      ButtonHighScores.Top = 370;
      ButtonHighScores.Width = 100;
      ButtonHighScores.Height = 30;
      ButtonHighScores.Text = "High Scores";
      ButtonHighScores.Click += HighScoresClick;
      Controls.Add (ButtonHighScores);

      LabelStatus.Left = 25;
      LabelStatus.Top = 50;
      LabelStatus.Width = 200;
      LabelStatus.Height = 60;
      LabelStatus.Text = "Player 1";
      Controls.Add (LabelStatus);
    }


    // Add the the players' dice rolls for all categories to the main window.
    private void AddDiceResults ()
    {
      for (int i = 0; i < Rules.CategoryCount; i++)
      {
        Player1Categories [i] = new DiceContainer ();
        Player1Categories [i].Left = 400;
        Player1Categories [i].Top = i * 25 + 47;
        Player1Categories [i].Size = 20;
        Player1Categories [i].AddToForm (this);

        Player2Categories [i] = new DiceContainer ();
        Player2Categories [i].Left = 560;
        Player2Categories [i].Top = i * 25 + 47;
        Player2Categories [i].Size = 20;
        Player2Categories [i].AddToForm (this);
      }
    }


    // Add the submit score buttons to the window.
    private void AddButtons ()
    {
      for (int i = 0; i < Rules.CategoryCount; i++)
      {
        SelectCategoryButtons [i] = new Button ();
        SelectCategoryButtons [i].Top = i * 25 + 45;
        SelectCategoryButtons [i].Width = 100;
        SelectCategoryButtons [i].Text = "Submit";
        SelectCategoryButtons [i].Click += new System.EventHandler (SelectCategoryClick);
        Controls.Add (SelectCategoryButtons [i]);
      }
      SetButtonPositions ();
    }


    // Add a text label to the main window.
    private void AddLabel (Label l, int left, int top, string text)
    {
      l.Left = left;
      l.Top = top;
      l.Text = text;
      Controls.Add (l);
    }


    // Add the score sheet text labels to the main window.
    private void AddLabels ()
    {
      for (int i = 0; i < Rules.CategoryCount; i++)
      {
        CategoryNames [i] = new Label ();
        Player1Scores [i] = new Label ();
        Player2Scores [i] = new Label ();
        AddLabel (CategoryNames [i], 300, i * 25 + 50, Rules.CategoryNames [i]);
        AddLabel (Player1Scores [i], 505, i * 25 + 50, "0");
        AddLabel (Player2Scores [i], 665, i * 25 + 50, "0");
      }
      AddLabel (LabelPlayer1, 400, 20, "Player 1");
      AddLabel (LabelPlayer2, 560, 20, "Player 2");
      AddLabel (LabelBonus, 300, 380, "Bonus");
      AddLabel (LabelPlayer1Bonus, 505, 380, "0");
      AddLabel (LabelPlayer2Bonus, 665, 380, "0");
      AddLabel (LabelTotalScore, 300, 410, "Total Score");
      AddLabel (LabelPlayer1Score, 505, 410, "0");
      AddLabel (LabelPlayer2Score, 665, 410, "0");
      AddLabel (LabelWarning, 150, 187, "");
      LabelWarning.ForeColor = Color.Red;
      LabelWarning.Width = 200;
    }

//========================================================================================
// Updating UI

    // Update status text.
    private void UpdateStatus ()
    {
      if (Game.Round >= Rules.TotalRounds)
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
        LabelStatus.Text = "Round: " + (Game.Round + 1).ToString () +
                           "\nPlayer: " +
                           ((Game.ActivePlayer == YahtzeeGame.Player.Player1) ?
                           "1" : "2") +
                           "\nRolls used: " + Game.RollsUsed.ToString ();
    }


    // Move submit buttons to correct position, show the ones for available categories.
    private void SetButtonPositions ()
    {
      int x;
      if (Game.ActivePlayer == YahtzeeGame.Player.Player1)
        x = 400;
      else
        x = 560;
      for (int i = 0; i < Rules.CategoryCount; i++)
      {
        SelectCategoryButtons [i].Left = x;
        SelectCategoryButtons [i].Visible = Game.GetPlayerScore (Game.ActivePlayer,
                                                           (Rules.Category) i) == -1;
      }
    }


    // Enable submit buttons.
    private void EnableButtons ()
    {
      for (int i = 0; i < Rules.CategoryCount; i++)
        SelectCategoryButtons [i].Enabled = true;
    }


    // Disable submit buttons.
    private void DisableButtons ()
    {
      for (int i = 0; i < Rules.CategoryCount; i++)
        SelectCategoryButtons [i].Enabled = false;
    }


    // Convert score to string, or "-" when unscored (score = -1).
    private string ScoreToString (int score)
    {
      if (score == -1)
        return "\x2014";
      return score.ToString ();
    }


    // Update the score display.
    private void UpdateScores ()
    {
      for (int i = 0; i < Rules.CategoryCount; i++)
      {
        Player1Scores [i].Text = ScoreToString (
          Game.GetPlayerScore (YahtzeeGame.Player.Player1, (Rules.Category) i));
        Player2Scores [i].Text = ScoreToString (
          Game.GetPlayerScore (YahtzeeGame.Player.Player2, (Rules.Category) i));
        Player1Scores [i].ForeColor = Color.Black;
        Player2Scores [i].ForeColor = Color.Black;
      }
      LabelPlayer1Bonus.Text = Game.BonusPlayer1.ToString ();
      LabelPlayer2Bonus.Text = Game.BonusPlayer2.ToString ();
      LabelPlayer1Score.Text = Game.ScorePlayer1.ToString ();
      LabelPlayer2Score.Text = Game.ScorePlayer2.ToString ();
    }


    // Show what current dice would score for available categories.
    private void ShowPotentialScores ()
    {
      Label [] scoreList = (Game.ActivePlayer == YahtzeeGame.Player.Player1) ?
                           Player1Scores : Player2Scores;
      Tuple <int, int> score;
      string bonus;
      for (int i = 0; i < Rules.CategoryCount; i++)
      {
        score = Game.ScoreDice ((Rules.Category) i);
        bonus = score.Item2 > 0 ? " + " + score.Item2.ToString () : "";
        if (Game.GetPlayerScore (Game.ActivePlayer, (Rules.Category) i) == -1)
        {
          scoreList [i].Text = score.Item1.ToString () + bonus;
          scoreList [i].ForeColor = Color.Red;
        }
      }
    }


    // Update Current Dice
    private void UpdateCurrentDice ()
    {
      for (int i = 0; i < DiceSet.DiceCount; i++)
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
      for (int i = 0; i < DiceSet.DiceCount; i++)
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
      int [] finalDice = new int [DiceSet.DiceCount];
      for (int i = 0; i < DiceSet.DiceCount; i++)
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
        for (int i = 0; i < DiceSet.DiceCount; i++)
          container.SetDie (i, DiceImages [finalDice [i]]);
        UpdateScores ();
        DisableButtons ();
        SetButtonPositions ();
        CurrentDice.ClearDice ();
        UpdateStatus ();
        LabelWarning.Text = "";
        if (Game.Round < Rules.TotalRounds)
          ButtonRoll.Enabled = true;
        else
        {
          ButtonRoll.Enabled = false;
          string date = System.DateTime.Now.ToShortDateString () + " " +
                        System.DateTime.Now.ToShortTimeString ();
          HighScores.SubmitScore (FormInputPlayerNames.Player1Name,
                                  Game.ScorePlayer1, date);
          HighScores.SubmitScore (FormInputPlayerNames.Player2Name,
                                  Game.ScorePlayer2, date);
        }
      }
    }


    // Clear the displayed results for all categories.
    private void ClearCategories ()
    {
      for (int i = 0; i < Rules.CategoryCount; i++)
      {
        Player1Categories [i].ClearDice ();
        Player2Categories [i].ClearDice ();
      }
    }


    // Start a new game.
    private void NewGame ()
    {
      FormInputPlayerNames inputNames = new FormInputPlayerNames ();
      if (inputNames.ShowDialog (this) != DialogResult.OK)
        return;
      LabelPlayer1.Text = FormInputPlayerNames.Player1Name;
      LabelPlayer2.Text = FormInputPlayerNames.Player2Name;
      Game.NewGame ();
      UpdateStatus ();
      DisableButtons ();
      SetButtonPositions ();
      ClearCategories ();
      UpdateScores ();
      CurrentDice.ClearDice ();
      ButtonRoll.Enabled = true;
    }


    // Asks user if they want to quit game. Returns false to continue game.
    private bool GameQuitConfirmation ()
    {
      if ((Game.Round == 0 && Game.ActivePlayer == YahtzeeGame.Player.Player1 &&
           Game.RollsUsed == 0) || Game.Round == Rules.TotalRounds || 
           Game.Round == Rules.GameNotActive)
        return true;
      FormGameQuitWarning warning = new FormGameQuitWarning ();
      DialogResult result = warning.ShowDialog (this);
      if (result == DialogResult.OK)
        return true;
      return false;
    }

//========================================================================================
// Event Handlers

    // Handler for score submit buttons.
    private void SelectCategoryClick (object sender, EventArgs e)
    {
      for (int i = 0; i < Rules.CategoryCount; i++)
      {
        if (SelectCategoryButtons [i] == sender)
        {
          SubmitScore (i);
          break;
        }
      }
    }


    // Check if at least one die selected.
    bool DiceSelected ()
    {
      foreach (bool b in SelectedDice)
        if (b)
          return true;
      return false;
    }


    // Handler for roll button.
    private void RollClick (object sender, EventArgs e)
    {
      bool success;
      if (Game.RollsUsed == 0)
        success = Game.FirstRoll ();
      else
      {
        if (DiceSelected ())
        {
          success = Game.Reroll (GetSelectedDice ());
          LabelWarning.Text = "";
        }
        else
        {
          success = false;
          LabelWarning.Text = "Select at least one die.";
        }
      }
      if (success)
      {
        for (int i = 0; i < DiceSet.DiceCount; i++)
          SelectedDice [i] = false;
        UpdateCurrentDice ();
        ShowPotentialScores ();
        EnableButtons ();
        UpdateStatus ();
        if (Game.RollsUsed == Rules.MaxRolls)
          ButtonRoll.Enabled = false;
      }
    }


    // Handler for dice selecting.
    private void SelectDieClick (object sender, EventArgs e)
    {
      int index = CurrentDice.GetImageIndex ((PictureBox) sender);
      if (index == -1 || Game.RollsUsed >= Rules.MaxRolls)
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
      if (GameQuitConfirmation ())
        NewGame ();
    }


    // Program exit event handler. Gives warning if game still ongoing.
    protected override void OnFormClosing (FormClosingEventArgs e)
    {
      base.OnFormClosing (e);
      if (!GameQuitConfirmation ())
        e.Cancel = true;
    }


    // Handler for button to show high scores.
    private void HighScoresClick (object sender, EventArgs e)
    {
      HighScores.LoadDatabase ();
      HighScores.ShowDialog ();
    }
  }


//========================================================================================
// Class DiceContainer
//========================================================================================


  public class DiceContainer
  {
    private Panel Box; // container for dice images
    private PictureBox [] DicePictureBoxes; // picture boxes for dice images
    private int size = 50; // Height of the container
    public int Size {get {return size;} set {SetSize (value);}}

    public int Top // distance from top of parent container
    {
      get {return Box.Top;}
      set {Box.Top = value;}
    }
    public int Left // distance from left side of parent container
    {
      get {return Box.Left;}
      set {Box.Left = value;}
    }


    // Constructor.
    public DiceContainer ()
    {
      Box = new Panel ();
      Box.Width = 300;
      Box.Height = 50;
      DicePictureBoxes = new PictureBox [DiceSet.DiceCount];
      for (int i = 0; i < DiceSet.DiceCount; i++)
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


    // Resize the container to height = size and width = size * DiceSet.DiceCount.
    private void SetSize (int size)
    {
      this.size = size;
      Box.Height = size;
      Box.Width = Size * DiceSet.DiceCount;
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


    // Returns index of a PictureBox in the container, or -1 if it's not in container.
    public int GetImageIndex (PictureBox img)
    {
      for (int i = 0; i < DiceSet.DiceCount; i++)
        if (DicePictureBoxes [i] == img)
          return i;
      return -1;
    }


    // Set the image for one of the dice.
    public void SetDie (int index, Image img)
    {
      if (index >= 0 && index < DiceSet.DiceCount)
      {
        ((PictureBox) Box.Controls [index]).Image = img;
      }
    }


    // Remove the images for all dice.
    public void ClearDice ()
    {
      for (int i = 0; i < DiceSet.DiceCount; i++)
        ((PictureBox) Box.Controls [i]).Image = null;
    }


    // Add an event handler for all dice clicks.
    public void AddClickHandler (System.EventHandler handler)
    {
      for (int i = 0; i < DiceSet.DiceCount; i++)
        DicePictureBoxes [i].Click += handler;
    }


    // Add the container to a Main window form.
    public void AddToForm (FormMainWindow parentForm)
    {
      parentForm.Controls.Add (Box);
    }
  }
}
