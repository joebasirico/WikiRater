namespace WikiParser
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
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.WordCount = new System.Windows.Forms.Label();
			this.FleschScore = new System.Windows.Forms.Label();
			this.ReadingLevel = new System.Windows.Forms.Label();
			this.ParagraphCount = new System.Windows.Forms.Label();
			this.HeaderCount = new System.Windows.Forms.Label();
			this.ImageCount = new System.Windows.Forms.Label();
			this.ExternalLinks = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.textBox1.Location = new System.Drawing.Point(13, 13);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox1.Size = new System.Drawing.Size(400, 406);
			this.textBox1.TabIndex = 0;
			this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// textBox2
			// 
			this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox2.Location = new System.Drawing.Point(423, 13);
			this.textBox2.Multiline = true;
			this.textBox2.Name = "textBox2";
			this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox2.Size = new System.Drawing.Size(400, 406);
			this.textBox2.TabIndex = 1;
			// 
			// WordCount
			// 
			this.WordCount.AutoSize = true;
			this.WordCount.Location = new System.Drawing.Point(13, 426);
			this.WordCount.Name = "WordCount";
			this.WordCount.Size = new System.Drawing.Size(67, 13);
			this.WordCount.TabIndex = 2;
			this.WordCount.Text = "Word Count:";
			// 
			// FleschScore
			// 
			this.FleschScore.AutoSize = true;
			this.FleschScore.Location = new System.Drawing.Point(13, 439);
			this.FleschScore.Name = "FleschScore";
			this.FleschScore.Size = new System.Drawing.Size(75, 13);
			this.FleschScore.TabIndex = 3;
			this.FleschScore.Text = "Flesch Score: ";
			// 
			// ReadingLevel
			// 
			this.ReadingLevel.AutoSize = true;
			this.ReadingLevel.Location = new System.Drawing.Point(13, 452);
			this.ReadingLevel.Name = "ReadingLevel";
			this.ReadingLevel.Size = new System.Drawing.Size(82, 13);
			this.ReadingLevel.TabIndex = 4;
			this.ReadingLevel.Text = "Reading Level: ";
			// 
			// ParagraphCount
			// 
			this.ParagraphCount.AutoSize = true;
			this.ParagraphCount.Location = new System.Drawing.Point(13, 465);
			this.ParagraphCount.Name = "ParagraphCount";
			this.ParagraphCount.Size = new System.Drawing.Size(93, 13);
			this.ParagraphCount.TabIndex = 5;
			this.ParagraphCount.Text = "Paragraph Count: ";
			// 
			// HeaderCount
			// 
			this.HeaderCount.AutoSize = true;
			this.HeaderCount.Location = new System.Drawing.Point(13, 478);
			this.HeaderCount.Name = "HeaderCount";
			this.HeaderCount.Size = new System.Drawing.Size(79, 13);
			this.HeaderCount.TabIndex = 6;
			this.HeaderCount.Text = "Header Count: ";
			// 
			// ImageCount
			// 
			this.ImageCount.AutoSize = true;
			this.ImageCount.Location = new System.Drawing.Point(228, 426);
			this.ImageCount.Name = "ImageCount";
			this.ImageCount.Size = new System.Drawing.Size(73, 13);
			this.ImageCount.TabIndex = 7;
			this.ImageCount.Text = "Image Count: ";
			// 
			// ExternalLinks
			// 
			this.ExternalLinks.AutoSize = true;
			this.ExternalLinks.Location = new System.Drawing.Point(228, 439);
			this.ExternalLinks.Name = "ExternalLinks";
			this.ExternalLinks.Size = new System.Drawing.Size(105, 13);
			this.ExternalLinks.TabIndex = 8;
			this.ExternalLinks.Text = "External Link Count: ";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(834, 511);
			this.Controls.Add(this.ExternalLinks);
			this.Controls.Add(this.ImageCount);
			this.Controls.Add(this.HeaderCount);
			this.Controls.Add(this.ParagraphCount);
			this.Controls.Add(this.ReadingLevel);
			this.Controls.Add(this.FleschScore);
			this.Controls.Add(this.WordCount);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.textBox1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Label WordCount;
		private System.Windows.Forms.Label FleschScore;
		private System.Windows.Forms.Label ReadingLevel;
		private System.Windows.Forms.Label ParagraphCount;
		private System.Windows.Forms.Label HeaderCount;
		private System.Windows.Forms.Label ImageCount;
		private System.Windows.Forms.Label ExternalLinks;
	}
}

