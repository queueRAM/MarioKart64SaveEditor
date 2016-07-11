namespace MarioKart64SaveEditor
{
   partial class TimeControl
   {
      /// <summary> 
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary> 
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Component Designer generated code

      /// <summary> 
      /// Required method for Designer support - do not modify 
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.numericMinute = new System.Windows.Forms.NumericUpDown();
         this.numericSecond = new System.Windows.Forms.NumericUpDown();
         this.numericCentisecond = new System.Windows.Forms.NumericUpDown();
         this.labelMinute = new System.Windows.Forms.Label();
         this.labelSecond = new System.Windows.Forms.Label();
         this.comboCharacter = new System.Windows.Forms.ComboBox();
         this.labelTitle = new System.Windows.Forms.Label();
         ((System.ComponentModel.ISupportInitialize)(this.numericMinute)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.numericSecond)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.numericCentisecond)).BeginInit();
         this.SuspendLayout();
         // 
         // numericMinute
         // 
         this.numericMinute.Location = new System.Drawing.Point(28, 4);
         this.numericMinute.Name = "numericMinute";
         this.numericMinute.Size = new System.Drawing.Size(40, 20);
         this.numericMinute.TabIndex = 0;
         // 
         // numericSecond
         // 
         this.numericSecond.Location = new System.Drawing.Point(79, 3);
         this.numericSecond.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
         this.numericSecond.Name = "numericSecond";
         this.numericSecond.Size = new System.Drawing.Size(40, 20);
         this.numericSecond.TabIndex = 1;
         // 
         // numericCentisecond
         // 
         this.numericCentisecond.Location = new System.Drawing.Point(130, 3);
         this.numericCentisecond.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
         this.numericCentisecond.Name = "numericCentisecond";
         this.numericCentisecond.Size = new System.Drawing.Size(40, 20);
         this.numericCentisecond.TabIndex = 2;
         // 
         // labelMinute
         // 
         this.labelMinute.AutoSize = true;
         this.labelMinute.Location = new System.Drawing.Point(69, 5);
         this.labelMinute.Name = "labelMinute";
         this.labelMinute.Size = new System.Drawing.Size(9, 13);
         this.labelMinute.TabIndex = 3;
         this.labelMinute.Text = "\'";
         // 
         // labelSecond
         // 
         this.labelSecond.AutoSize = true;
         this.labelSecond.Location = new System.Drawing.Point(118, 5);
         this.labelSecond.Name = "labelSecond";
         this.labelSecond.Size = new System.Drawing.Size(12, 13);
         this.labelSecond.TabIndex = 4;
         this.labelSecond.Text = "\"";
         // 
         // comboCharacter
         // 
         this.comboCharacter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboCharacter.FormattingEnabled = true;
         this.comboCharacter.Items.AddRange(new object[] {
            "Mario",
            "Luigi",
            "Yoshi",
            "Toad",
            "DK",
            "Wario",
            "Peach",
            "Bowser"});
         this.comboCharacter.Location = new System.Drawing.Point(177, 3);
         this.comboCharacter.Name = "comboCharacter";
         this.comboCharacter.Size = new System.Drawing.Size(80, 21);
         this.comboCharacter.TabIndex = 5;
         this.comboCharacter.SelectedIndexChanged += new System.EventHandler(this.comboCharacter_SelectedIndexChanged);
         // 
         // labelTitle
         // 
         this.labelTitle.AutoSize = true;
         this.labelTitle.Location = new System.Drawing.Point(0, 7);
         this.labelTitle.Name = "labelTitle";
         this.labelTitle.Size = new System.Drawing.Size(24, 13);
         this.labelTitle.TabIndex = 6;
         this.labelTitle.Text = "1st:";
         // 
         // TimeControl
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.labelTitle);
         this.Controls.Add(this.comboCharacter);
         this.Controls.Add(this.numericCentisecond);
         this.Controls.Add(this.numericSecond);
         this.Controls.Add(this.numericMinute);
         this.Controls.Add(this.labelSecond);
         this.Controls.Add(this.labelMinute);
         this.Name = "TimeControl";
         this.Size = new System.Drawing.Size(260, 28);
         ((System.ComponentModel.ISupportInitialize)(this.numericMinute)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.numericSecond)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.numericCentisecond)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.NumericUpDown numericMinute;
      private System.Windows.Forms.NumericUpDown numericSecond;
      private System.Windows.Forms.NumericUpDown numericCentisecond;
      private System.Windows.Forms.Label labelMinute;
      private System.Windows.Forms.Label labelSecond;
      private System.Windows.Forms.ComboBox comboCharacter;
      private System.Windows.Forms.Label labelTitle;
   }
}
