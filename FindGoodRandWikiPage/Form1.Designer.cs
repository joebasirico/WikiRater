namespace FindGoodRandWikiPage
{
    partial class Form1
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
			this.ThresholdBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.FindIt = new System.Windows.Forms.Button();
			this.ArtStats = new System.Windows.Forms.TextBox();
			this.ArticleURL = new System.Windows.Forms.TextBox();
			this.Rate = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// ThresholdBox
			// 
			this.ThresholdBox.Location = new System.Drawing.Point(73, 15);
			this.ThresholdBox.Name = "ThresholdBox";
			this.ThresholdBox.Size = new System.Drawing.Size(100, 20);
			this.ThresholdBox.TabIndex = 0;
			this.ThresholdBox.Text = "6";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(54, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Threshold";
			// 
			// FindIt
			// 
			this.FindIt.Location = new System.Drawing.Point(179, 13);
			this.FindIt.Name = "FindIt";
			this.FindIt.Size = new System.Drawing.Size(75, 23);
			this.FindIt.TabIndex = 2;
			this.FindIt.Text = "Find It!";
			this.FindIt.UseVisualStyleBackColor = true;
			this.FindIt.Click += new System.EventHandler(this.FindIt_Click);
			// 
			// ArtStats
			// 
			this.ArtStats.Location = new System.Drawing.Point(16, 84);
			this.ArtStats.Multiline = true;
			this.ArtStats.Name = "ArtStats";
			this.ArtStats.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.ArtStats.Size = new System.Drawing.Size(487, 218);
			this.ArtStats.TabIndex = 3;
			this.ArtStats.TextChanged += new System.EventHandler(this.ArtStats_TextChanged);
			// 
			// ArticleURL
			// 
			this.ArticleURL.Location = new System.Drawing.Point(16, 48);
			this.ArticleURL.Name = "ArticleURL";
			this.ArticleURL.Size = new System.Drawing.Size(335, 20);
			this.ArticleURL.TabIndex = 4;
			// 
			// Rate
			// 
			this.Rate.Location = new System.Drawing.Point(357, 44);
			this.Rate.Name = "Rate";
			this.Rate.Size = new System.Drawing.Size(145, 23);
			this.Rate.TabIndex = 5;
			this.Rate.Text = "Rate Specific Article";
			this.Rate.UseVisualStyleBackColor = true;
			this.Rate.Click += new System.EventHandler(this.Rate_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(515, 314);
			this.Controls.Add(this.Rate);
			this.Controls.Add(this.ArticleURL);
			this.Controls.Add(this.ArtStats);
			this.Controls.Add(this.FindIt);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.ThresholdBox);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ThresholdBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button FindIt;
        private System.Windows.Forms.TextBox ArtStats;
		private System.Windows.Forms.TextBox ArticleURL;
		private System.Windows.Forms.Button Rate;
    }
}

