namespace Yahtzee
{
  partial class FormInputPlayerNames
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose (bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose ();
      }
      base.Dispose (disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent ()
    {
      this.labelPlayer1 = new System.Windows.Forms.Label();
      this.labelPlayer2 = new System.Windows.Forms.Label();
      this.textBoxName1 = new System.Windows.Forms.TextBox();
      this.textBoxName2 = new System.Windows.Forms.TextBox();
      this.buttonStart = new System.Windows.Forms.Button();
      this.buttonCancel = new System.Windows.Forms.Button();
      this.labelWarning = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // labelPlayer1
      // 
      this.labelPlayer1.AutoSize = true;
      this.labelPlayer1.Location = new System.Drawing.Point(29, 38);
      this.labelPlayer1.Name = "labelPlayer1";
      this.labelPlayer1.Size = new System.Drawing.Size(99, 17);
      this.labelPlayer1.TabIndex = 0;
      this.labelPlayer1.Text = "Player 1 name";
      // 
      // labelPlayer2
      // 
      this.labelPlayer2.AutoSize = true;
      this.labelPlayer2.Location = new System.Drawing.Point(29, 76);
      this.labelPlayer2.Name = "labelPlayer2";
      this.labelPlayer2.Size = new System.Drawing.Size(99, 17);
      this.labelPlayer2.TabIndex = 1;
      this.labelPlayer2.Text = "Player 2 name";
      // 
      // textBoxName1
      // 
      this.textBoxName1.Location = new System.Drawing.Point(138, 35);
      this.textBoxName1.MaxLength = 32;
      this.textBoxName1.Name = "textBoxName1";
      this.textBoxName1.Size = new System.Drawing.Size(258, 22);
      this.textBoxName1.TabIndex = 2;
      // 
      // textBoxName2
      // 
      this.textBoxName2.Location = new System.Drawing.Point(138, 73);
      this.textBoxName2.MaxLength = 32;
      this.textBoxName2.Name = "textBoxName2";
      this.textBoxName2.Size = new System.Drawing.Size(258, 22);
      this.textBoxName2.TabIndex = 3;
      // 
      // buttonStart
      // 
      this.buttonStart.Location = new System.Drawing.Point(120, 152);
      this.buttonStart.Name = "buttonStart";
      this.buttonStart.Size = new System.Drawing.Size(75, 28);
      this.buttonStart.TabIndex = 4;
      this.buttonStart.Text = "Start";
      this.buttonStart.UseVisualStyleBackColor = true;
      this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
      // 
      // buttonCancel
      // 
      this.buttonCancel.Location = new System.Drawing.Point(248, 152);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new System.Drawing.Size(75, 28);
      this.buttonCancel.TabIndex = 5;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.UseVisualStyleBackColor = true;
      this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
      // 
      // labelWarning
      // 
      this.labelWarning.AutoSize = true;
      this.labelWarning.ForeColor = System.Drawing.Color.Red;
      this.labelWarning.Location = new System.Drawing.Point(29, 116);
      this.labelWarning.Name = "labelWarning";
      this.labelWarning.Size = new System.Drawing.Size(73, 17);
      this.labelWarning.TabIndex = 6;
      this.labelWarning.Text = "<warning>";
      // 
      // FormInputPlayerNames
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(434, 213);
      this.Controls.Add(this.labelWarning);
      this.Controls.Add(this.buttonCancel);
      this.Controls.Add(this.buttonStart);
      this.Controls.Add(this.textBoxName2);
      this.Controls.Add(this.textBoxName1);
      this.Controls.Add(this.labelPlayer2);
      this.Controls.Add(this.labelPlayer1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FormInputPlayerNames";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Enter Player names";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label labelPlayer1;
    private System.Windows.Forms.Label labelPlayer2;
    private System.Windows.Forms.TextBox textBoxName1;
    private System.Windows.Forms.TextBox textBoxName2;
    private System.Windows.Forms.Button buttonStart;
    private System.Windows.Forms.Button buttonCancel;
    private System.Windows.Forms.Label labelWarning;
  }
}