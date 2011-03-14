using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FindGoodRandWikiPage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void FindIt_Click(object sender, EventArgs e)
        {
            int rating = 0;
            Article art = null;
            while (rating < Int32.Parse(ThresholdBox.Text))
            {
                art = new Article();
                rating = art.rating;
                PrintArticleStats(art);
                ArtStats.Refresh();
            }
        }

        private void PrintArticleStats(Article art)
        {
            ArtStats.Text += art.title + ": " + art.rating + "\r\n\t" + art.url + "\r\n";
        }

        private void ArtStats_TextChanged(object sender, EventArgs e)
        {
            ArtStats.Select(ArtStats.Text.Length, 0);
            ArtStats.ScrollToCaret();
        }
    }
}
