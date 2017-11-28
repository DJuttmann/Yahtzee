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
  public partial class FormInputPlayerNames: Form
  {
    public static string Player1Name = "";
    public static string Player2Name = "";


    // Constructor.
    public FormInputPlayerNames ()
    {
      InitializeComponent ();
      textBoxName1.Text = Player1Name;
      textBoxName2.Text = Player2Name;
      labelWarning.Text = "";
    }


    // Handler for Start button.
    private void buttonStart_Click (object sender, EventArgs e)
    {
      if (textBoxName1.Text.Length > 0 && textBoxName2.Text.Length > 0)
      {
        Player1Name = textBoxName1.Text;
        Player2Name = textBoxName2.Text;
        DialogResult = DialogResult.OK;
        Dispose ();
      }
      else
        labelWarning.Text = "Names may not be blank";
    }


    // Handler for Cancel button.
    private void buttonCancel_Click (object sender, EventArgs e)
    {
      DialogResult = DialogResult.Cancel;
      Dispose ();
    }


    // Handlers for Enter key press on text fields.
    private void textBoxName1_KeyDown (object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
        buttonStart_Click (sender, null);
    }

    private void textBoxName2_KeyDown (object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
        buttonStart_Click (sender, null);
    }
  }
}
