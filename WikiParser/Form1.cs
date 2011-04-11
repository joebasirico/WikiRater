using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

namespace WikiParser
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			if (File.Exists("wikiText.txt"))
				textBox1.Text = File.ReadAllText("wikiText.txt");
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			string input = textBox1.Text;
			string textOnly = ParseWiki(input);

			int words = CountWords(textOnly);
			int paragraphs = CountParagraphs(input);
			WordCount.Text = "Word Count: " + words;
			ParagraphCount.Text = "Paragraph Count: " + paragraphs;
			WordsPerParagraph.Text = String.Format("{0:0.00}", ((double)words / (double)paragraphs));
			FleschScore.Text = "Flesch Score: " + GetFlesch(input);
			ReadingLevel.Text = "Flesch Kincade Reading Level: " + GetReadingLevel(input);
			
			textBox2.Text = textOnly;
		}

		private int CountParagraphs(string input)
		{
			int count = 0;
			foreach (String s in Regex.Split(input, "\r\n"))
			{
				if (CountWords(s) > 2)
					count++;
			}
			return count;
		}

		private string GetReadingLevel(string input)
		{
			return "Not Implemented";
		}

		private double GetFlesch(string input)
		{
			double numSentences = 0;
			double numWords = 0;
			double numSyllables = 0;
			double flesch = 0;

			Regex sentences = new Regex(@".+?\. ");
			foreach (Match s in sentences.Matches(input))
			{
				numSentences++;
				Regex words = new Regex(".+? ");
				foreach (Match w in words.Matches(s.Value))
				{
					numWords++;
					string word = Regex.Match(w.Value, @"\w+").Value;
					numSyllables += CountSyllables(word);
				}
			}

			return 206.835 - 1.015 * (numWords / numSentences) - 84.6 * (numSyllables / numWords);
		}

		/// <summary>
		/// How To Find Syllables:
		///Count the number of vowels (a, e, i, o, u, and sometimes y) in the word.
		///Subtract any silent vowels (like the silent 'e' at the end of a word).
		///Subtract 1 vowel from every diphthong.
		///A diphthong is when two volwels make only 1 sound (oi, oy, ou, ow, au, aw, oo, ...).
		///The number you are left with should be the number of vowels in the word.
		///http://www.howmanysyllables.com/howtocountsyllables.html
		/// </summary>
		/// <param name="word"></param>
		/// <returns></returns>
		private int CountSyllables(string word)
		{
			char[] vowels = { 'a', 'e', 'i', 'o', 'u' };

			//get rid of spaces etc.
			string currentWord = word;
			//get rid of silent e
			if(currentWord != "e" && currentWord[currentWord.Length-1] == 'e')
				currentWord = currentWord.Substring(0, currentWord.Length-2);
			//count vowels
			int numVowels = 0;
			foreach (char v in vowels)
			{
				bool lastWasVowel = false;
				foreach (char wc in currentWord)
				{
					//don't add it if we're in a diphthong
					if(v==wc && !lastWasVowel)
					{
						numVowels++;
						lastWasVowel = true;
					}
					else
						lastWasVowel = false;
				}
			}
			return numVowels;
		}

		private int CountWords(string input)
		{
			Regex r = new Regex(".+? ");
			return r.Matches(input).Count;
		}

		private string ParseWiki(string input)
		{
			string output = input;
			string[] regexs = {	
								@"""\*""", //beginning star
								@"\{\{.*?\}\}", //stach
								@"'''(.*?)'''", //bold
								@"\[\[Image:(.*?\||)?([\w\s=\.\-/'%&?+,:;@]*?)\]\]", //Images
								@"\[\[(.*?\|?)?([\w\s=\.\-/%&?+,:;'@]*?)\]\]",//Internal Links
								@"<ref.*?>.*?<\/ref>", //references
								@"''(.*?)''", //italic
								@"===(.*?)===", //H3
								@"==(.*?)==", //H2
								@"=(.*?)=", //H1
								@"<source.*?>.*?<\/source>", //Remove source lines, count them later
								@"[ ]+\*", //bullets
								@"[ ]+\d",//numbered lists, 
								@"\[http[s]?://[A-Z0-9.-]+\.[A-Z]{2,6}.+?\]", //External Links
								@"http[s]?://[a-z0-9=\.\-/%&?+,:';@]+" //External Links without brackets
							  };

			foreach (string regex in regexs)
			{
				Regex r = new Regex(regex, RegexOptions.IgnoreCase);

				int zerocount = 0;
				int onecount = 0;
				int twocount = 0;
				foreach (Match m in r.Matches(output))
				{
					if (m.Groups.Count == 3)
					{
						output = output.Replace(m.Groups[0].Value, m.Groups[2].Value);
						twocount++;
					}
					else if (m.Groups.Count == 2)
					{
						output = output.Replace(m.Groups[0].Value, m.Groups[1].Value);
						onecount++;
					}
					else
					{
						output = r.Replace(output, "");
						zerocount++;
					}
				}
				//Count stuff using regex
				if (regex == @"==(.*?)==")
					HeaderCount.Text = "Header Count: " + onecount;
				else if (regex == @"\[\[Image:(.*?\||)?([\w\s=\.\-/'%&?+,:;@]*?)\]\]")
					ImageCount.Text = "Image Count: " + twocount;
			}
			output = output.Replace("\\r", "\r\n");
			
			return output;
		}

		private void RefreshWikiText_Click(object sender, EventArgs e)
		{
			if (File.Exists("wikiText.txt"))
				textBox1.Text = File.ReadAllText("wikiText.txt");
			else
				MessageBox.Show("Couldn't find 'wikiText.txt'", "Couldn't find 'wikiText.txt'", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}
	}
}
