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
    private Button [] SelectCategory = new Button [13];
    public Image [] DiceImages = new Image [6];
    private bool [] SelectedDice = new bool [5];

    private Label LabelPlayer;
    private DiceContainer CurrentDice;
    private Button Roll;


    public Form1 ()
    {
      InitializeComponent ();

      AddMainControls ();
      LoadDiceImages ();
      AddButtons ();
      AddLabels ();
      AddDiceResults ();
    }


    private void LoadDiceImages ()
    {
      for (int i = 1; i <= 6; i++)
      {
        DiceImages [i - 1] = Image.FromFile ("Die" + i.ToString () + ".png");
      }
    }


    private void AddMainControls ()
    {
      LabelPlayer = new Label ();
      LabelPlayer.Left = 25;
      LabelPlayer.Top = 50;
      LabelPlayer.Text = "Player 1";

      CurrentDice = new DiceContainer ();
      CurrentDice.Left = 25;
      CurrentDice.Top = 100;
      CurrentDice.Size = 50;

      Roll = new Button ();
      Roll.Left = 25;
      Roll.Top = 180;
      Roll.Width = 100;
      Roll.Height = 30;
      Roll.Text = "Roll";
      Roll.Click += RollClick;

      Controls.Add (LabelPlayer);
      CurrentDice.AddToForm (this);
      Controls.Add (Roll);
    }


    private void AddDiceResults ()
    {
      for (int i = 0; i < 13; i++)
      {
        Player1Categories [i] = new DiceContainer ();
        Player1Categories [i].Left = 400;
        Player1Categories [i].Top = i * 25;
        Player1Categories [i].AddToForm (this);

        Player2Categories [i] = new DiceContainer ();
        Player2Categories [i].Left = 500;
        Player2Categories [i].Top = i * 25;
        Player2Categories [i].AddToForm (this);
      }
    }


    private void AddButtons ()
    {
      for (int i = 0; i < 13; i++)
      {
        SelectCategory [i] = new Button ();
        SelectCategory [i].Click += new System.EventHandler (SelectCategoryClick);
        Controls.Add (SelectCategory [i]);
      }
      SetButtonPositions ();
    }


    private void AddLabels ()
    {
      for (int i = 0; i < 13; i++)
      {
        CategoryNames [i] = new Label ();
        CategoryNames [i].Left = 300;
        CategoryNames [i].Top = i * 25;
        CategoryNames [i].Text = Rules.CategoryNames [i];

        Player1Scores [i] = new Label ();
        Player1Scores [i].Left = 480;
        Player1Scores [i].Top = i * 25;
        Player1Scores [i].Text = "0";

        Player2Scores [i] = new Label ();
        Player2Scores [i].Left = 580;
        Player2Scores [i].Top = i * 25;
        Player2Scores [i].Text = "0";

        Controls.Add (CategoryNames [i]);
        Controls.Add (Player1Scores [i]);
        Controls.Add (Player2Scores [i]);
      }
    }


    private void SetButtonPositions () {
      int x;
      if (Game.ActivePlayer == YahtzeeGame.Player.Player1)
        x = 400;
      else
        x = 500;
      for (int i = 0; i < 13; i++)
      {
        SelectCategory [i].Left = x;
        SelectCategory [i].Top = i * 25;
      }
    }



    // Update Current Dice
    private void UpdateCurrentDice ()
    {
      for (int i = 0; i < 5; i++)
        CurrentDice.SetDie (i, DiceImages [Game.Dice [i]]);
    }



    // Handler for score submit buttons.
    private void SelectCategoryClick (object sender, EventArgs e)
    {
      for (int i = 0; i < 13; i++)
      {
        if (SelectCategory [i] == sender)
        {
          Game.SubmitScore ((Rules.Category) i);
          break;
        }
      }
    }


    // Handler for roll button.
    private void RollClick (object sender, EventArgs e)
    {
      if (Game.RollsUsed == 0)
      {
        Game.FirstRoll ();
      }
      else
      {
        Game.Reroll (new [] {1,2,3});
      }
      UpdateCurrentDice ();
    }
  }

﻿//========================================================================================
// Class DiceContainer
﻿//========================================================================================

  public class DiceContainer
  {
    public Panel Box;
    public PictureBox [] DiceImages;
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


    public DiceContainer ()
    {
      Box = new Panel ();
      Box.Width = 300;
      Box.Height = 50;
      DiceImages = new PictureBox [5];
      for (int i = 0; i < 5; i++)
      {
        DiceImages [i] = new PictureBox ();
        DiceImages [i].SizeMode = PictureBoxSizeMode.Zoom;
        DiceImages [i].Left = (Size + Size / 10) * i;
        DiceImages [i].Width = Size;
        DiceImages [i].Top = 0;
        DiceImages [i].Height = Size;
        DiceImages [i].Image = null;
        Box.Controls.Add (DiceImages [i]);
      }
    }


    public void AddToForm (Form1 parentForm)
    {
      parentForm.Controls.Add (Box);
    }
  }
}
