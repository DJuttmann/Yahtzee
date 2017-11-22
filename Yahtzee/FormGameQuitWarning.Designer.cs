namespace Yahtzee
{
  partial class FormGameQuitWarning
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
      this.labelWarning = new System.Windows.Forms.Label();
      this.buttonNo = new System.Windows.Forms.Button();
      this.buttonYes = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // labelWarning
      // 
      this.labelWarning.AutoSize = true;
      this.labelWarning.Location = new System.Drawing.Point(25, 35);
      this.labelWarning.Name = "labelWarning";
      this.labelWarning.Size = new System.Drawing.Size(265, 17);
      this.labelWarning.TabIndex = 0;
      this.labelWarning.Text = "Are you sure you want to quit this game?";
      // 
      // buttonNo
      // 
      this.buttonNo.Location = new System.Drawing.Point(64, 85);
      this.buttonNo.Name = "buttonNo";
      this.buttonNo.Size = new System.Drawing.Size(75, 29);
      this.buttonNo.TabIndex = 1;
      this.buttonNo.Text = "No";
      this.buttonNo.UseVisualStyleBackColor = true;
      this.buttonNo.Click += new System.EventHandler(this.buttonNo_Click);
      // 
      // buttonYes
      // 
      this.buttonYes.Location = new System.Drawing.Point(176, 85);
      this.buttonYes.Name = "buttonYes";
      this.buttonYes.Size = new System.Drawing.Size(75, 29);
      this.buttonYes.TabIndex = 2;
      this.buttonYes.Text = "Yes";
      this.buttonYes.UseVisualStyleBackColor = true;
      this.buttonYes.Click += new System.EventHandler(this.buttonYes_Click);
      // 
      // FormGameQuitWarning
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(315, 144);
      this.Controls.Add(this.buttonYes);
      this.Controls.Add(this.buttonNo);
      this.Controls.Add(this.labelWarning);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FormGameQuitWarning";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Warning";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label labelWarning;
    private System.Windows.Forms.Button buttonNo;
    private System.Windows.Forms.Button buttonYes;
  }
}