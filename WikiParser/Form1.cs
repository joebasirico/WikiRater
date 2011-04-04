using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace WikiParser
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			string input = textBox1.Text;
			string textOnly = ParseWiki(input);

			WordCount.Text = "Word Count: " + CountWords(textOnly);
			FleschScore.Text = "Flesch Score: " + GetFlesch(input);
			ReadingLevel.Text = "Flesch Kincade Reading Level: " + GetReadingLevel(input);
			ParagraphCount.Text = "Paragraph Count: " + CountParagraphs(input);
			ImageCount.Text = "Image Count: " + CountImages(input);
			textBox2.Text = textOnly;
		}

		private string CountImages(string input)
		{
			return "Not Implemented";
		}

		private string CountParagraphs(string input)
		{
			return "Not Implemented";
		}

		private string GetReadingLevel(string input)
		{
			return "Not Implemented";
		}

		private string GetFlesch(string input)
		{
			return "Not Implemented";
		}

		private string CountWords(string input)
		{
			Regex r = new Regex(".+? ");
			return r.Matches(input).Count.ToString();
		}

		private string ParseWiki(string input)
		{
			string output = input;
			string[] regexs = {	
								@"""\*""", //beginning star
								@"\{\{.*?\}\}", //stach
								@"'''(.*?)'''", //bold
								@"\[\[(.*?\|?)?([\w\s]*?)\]\]",//Internal Links
								@"<ref.*?>.*?<\/ref>", //references
								@"''(.*?)''", //italic
								@"[^=]=(.*?)=[^=]", //H1
								@"==(.*?)==", //H2
								@"===(.*?)===", //H3
								@"<source.*?>.*?<\/source>", //Remove source lines, count them later
								@"[ ]+\*", //bullets
								@"[ ]+\d",//numbered lists
							  };

			foreach (string regex in regexs)
			{
				Regex r = new Regex(regex);
				//Count stuff using regex
				if (regex == @"[^=]=(.*?)=[^=]")
					HeaderCount.Text = "Header Count: " + r.Matches(input).Count;


				foreach (Match m in r.Matches(output))
				{					
					if (m.Groups.Count == 3)
						output = output.Replace(m.Groups[0].Value, m.Groups[2].Value);
					else if(m.Groups.Count == 2)
						output = output.Replace(m.Groups[0].Value, m.Groups[1].Value);
					else
						output = r.Replace(output, "");
				}

			}
			output = output.Replace("\\r", "\r\n");
			
			return output;
		}
	}
}
