namespace Loader.UI
{
    partial class Main
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.controlPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // controlPanel
            // 
            this.controlPanel.BackColor = System.Drawing.Color.Transparent;
            this.controlPanel.Location = new System.Drawing.Point(2, 2);
            this.controlPanel.Name = "controlPanel";
            this.controlPanel.Size = new System.Drawing.Size(430, 21);
            this.controlPanel.TabIndex = 0;
            this.controlPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.controlPanel_MouseDown);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(38)))), ((int)(((byte)(41)))));
            this.ClientSize = new System.Drawing.Size(493, 34);
            this.Controls.Add(this.controlPanel);
            this.Name = "Main";
            this.Text = "Loader";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel controlPanel;
    }
}