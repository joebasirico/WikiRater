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
			textBox2.Text = ParseWiki(textBox1.Text);
		}

		private string ParseWiki(string input)
		{
			string output = input;
			string[] regexs = {	@"""\*""", //beginning star
								@"\{\{.*?\}\}", //stach
								@"'''(.*?)'''", //bold
 								@"\[\[[.*?|]?(.*?)\]\]",//Internal Links
								@"<ref>.*?<\\/ref>", //references
								@"''(.*?)''", //italic
								@"=(.*?)=", //H1
								@"==(.*?)==", //H2
								@"===(.*?)===", //H3
								@"<source.*?>.*?<\\/source>", //Remove source lines, count them later
								@"[ ]+\*", //bullets
								@"[ ]+\d",//numbered lists
							  };

			foreach (string regex in regexs)
			{
				Regex r = new Regex(regex);
				if (r.Match(output).Groups.Count > 1)
					output = r.Replace(output, r.Match(output).Groups[1].Value);
				else
					output = r.Replace(output, "");
			}
			output = output.Replace("\\r", "\r\n");
			
			return output;
		}
	}
}
