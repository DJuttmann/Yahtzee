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
  public partial class FormGameQuitWarning: Form
  {
    public FormGameQuitWarning ()
    {
      InitializeComponent ();
    }

    private void buttonNo_Click (object sender, EventArgs e)
    {
      DialogResult = DialogResult.Cancel;
      Dispose ();
    }

    private void buttonYes_Click (object sender, EventArgs e)
    {
      DialogResult = DialogResult.OK;
      Dispose ();
    }
  }
}
