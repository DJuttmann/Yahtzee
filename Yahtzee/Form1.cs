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
    private YahtzeeGame Game;
    private DiceContainer [] Player1Categories = new DiceContainer [13];
    private DiceContainer [] Player2Categories = new DiceContainer [13];
    public static Image test = Image.FromFile ("Die6.png");
    public Image [] DiceImages = new Image [6];

    public Form1 ()
    {
      InitializeComponent ();
      for (int i = 0; i < 13; i++)
      {
        Player1Categories [i] = new DiceContainer ();
        Player2Categories [i] = new DiceContainer ();
      }
      for (int i = 1; i <= 6; i++)
      {
        DiceImages [i - 1] = Image.FromFile ("Die" + i.ToString () + ".png");
      }
      Player1Categories [0].Size = 25;
      Player1Categories [0].AddToForm (this);
      Player1Categories [0].SetDie (2, DiceImages [4]);
      Player1Categories [0].Left = 400;
      Player1Categories [0].Top = 70;
    }
  }



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
      Box.Width = (Size + Size / 10) * 5;
      int i = 0;
      foreach (PictureBox picture in Box.Controls)
      {
        picture.Width = size;
        picture.Height = size;
        picture.Top = 0;
        picture.Left = (Size + Size / 10) * i;
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


    public DiceContainer ()
    {
      Box = new Panel ();
//      Box.
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
        DiceImages [i].Image = Form1.test;
        Box.Controls.Add (DiceImages [i]);
      }
    }


    public void AddToForm (Form1 parentForm)
    {
      parentForm.Controls.Add (Box);
    }
  }
}
