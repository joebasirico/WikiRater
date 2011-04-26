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
using System.Net;

namespace WikiParser
{
	public partial class Form1 : Form
	{
		double numSentences = 0;
		double numWords = 0;
		double numSyllables = 0;
		int numParagraphs = 0;
		double flesch = 0;
		double readingLevel = 0;

		public Form1()
		{
			InitializeComponent();
			if (File.Exists("wikiText.txt"))
				textBox1.Text = File.ReadAllText("wikiText.txt", Encoding.ASCII);
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			numSentences = 0;
			numWords = 0;
			numSyllables = 0;
			numParagraphs = 0;
			
			string input = textBox1.Text;
			if (input.Length > 2 && 
				input[input.Length - 1] != ' ' &&
				input[input.Length - 2] == '.')
				input += " ";

			if (input.Length > 1 && 
				input[input.Length - 1] == '.')
				input += " ";

			if (input.Length > 2 && 
				input[input.Length - 1] != '.' &&
				input[input.Length - 2] != '.')
				input += ". ";


			string textOnly = ParseWiki(input);



			//
			GetFlesch(input);
			FleschScore.Text = "Flesch Score: " + flesch;
			ReadingLevel.Text = "Reading Level: " + readingLevel;
			numParagraphs = CountParagraphs(input);
			WordCount.Text = "Word Count: " + numWords;
			SentenceCount.Text = "Sentence Count: " + numSentences;
			SyllableCount.Text = "Syllable Count: " + numSyllables;
			SyllablesPerWord.Text = "Syllables Per Word: " + numSyllables / numWords;
			WordsPerSentence.Text = "Words Per Sentence: " + numWords / numSentences;
			ParagraphCount.Text = "Paragraph Count: " + numParagraphs;
			WordsPerParagraph.Text = String.Format("{0:0.00}", ((double)numWords / (double)numParagraphs));


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

		private void GetFlesch(string input)
		{
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

			flesch = 206.835 - 1.015 * (numWords / numSentences) - 84.6 * (numSyllables / numWords);
			readingLevel = 0.39 * (numWords / numSentences) + 11.8 * (numSyllables / numWords) - 15.59;
		}

		/// <summary>
		/// How I am Finding Syllables:
		///Count the number of vowels (a, e, i, o, u, and sometimes y) in the word.
		///Subtract any silent vowels (like the silent 'e' at the end of a word).
		///Subtract 1 vowel from every diphthong.
		///A diphthong is when two volwels make only 1 sound (oi, oy, ou, ow, au, aw, oo, ...).
		///The number you are left with should be the number of vowels in the word.
		///http://www.howmanysyllables.com/howtocountsyllables.html
		///
		/// This method is inaccurate, but pretty close. It screws up on challenging 
		/// words like "hyphenation" and "invisible"
		/// 
		/// I'd like to return to this and implement something more accurate as was discussed in
		/// this 1983 NLP thesis: http://www.tug.org/docs/liang/
		/// 
		/// </summary>
		/// <param name="word"></param>
		/// <returns></returns>
		private int CountSyllables(string word)
		{
			char[] vowels = { 'a', 'e', 'i', 'o', 'u', 'y' };
			string currentWord = word;
			int numVowels = 0;
			bool lastWasVowel = false;
			foreach (char wc in currentWord)
			{
				bool foundVowel = false;
				foreach (char v in vowels)
				{
					//don't count diphthongs
					if (v == wc && lastWasVowel)
					{
						foundVowel = true;
						lastWasVowel = true;
						break;
					}
					else if (v == wc && !lastWasVowel)
					{
						numVowels++;
						foundVowel = true;
						lastWasVowel = true;
						break;
					}
				}

				//if full cycle and no vowel found, set lastWasVowel to false;
				if (!foundVowel)
					lastWasVowel = false;
			}
			//remove es, it's _usually? silent
			if (currentWord.Length > 2 && 
				currentWord.Substring(currentWord.Length - 2) == "es")
				numVowels--;
			// remove silent e
			else if (currentWord.Length > 1 &&
				currentWord.Substring(currentWord.Length - 1) == "e")
				numVowels--;

			return numVowels;
		}

		private int hardCodedWords(string word)
		{
			return 0;
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

		private void random_Click(object sender, EventArgs e)
		{
			string url = "";
			string body = "";
			try
			{
				HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
				request.Method = "GET";
				request.UserAgent = "IE";

				request.AllowAutoRedirect = true;
				WebResponse response = request.GetResponse();
				StreamReader inStream = new StreamReader(response.GetResponseStream());
				body = inStream.ReadToEnd();
			}
			catch (WebException)
			{
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
