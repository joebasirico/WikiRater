<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
	CodeBehind="About.aspx.cs" Inherits="WikiRaterWeb.About" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="content">
		<h1>
			About</h1>
		<div class="paragraph">
			I've started a project to create an algorithm to rate articles on Wikipedia, and
			you can help! My hope is to take user supplied rating and content and create an
			algorithm that can reliably rate how interesting an article is.
		</div>
		<div class="paragraph">
			I realize "interesting" is a very ambiguous word, and that's mostly on purpose.
			I want to find the articles on Wikipedia that you are most likely to enjoy reading,
			if presented randomly to you. If you're curious I suggest you read the "history"
			section to learn how I came up with this idea.</div>
		<div class="paragraph">
			The WikiRater comes in two parts. The Algorithm and User generated content. If you're
			interested in the algorithm, please check it out below. My Algorithm can automatically
			create a best guess rating on each Wikipedia article, it may be close, or it may
			be way off, it all depends on how I ultimately tune and tweak it. I'm not trying
			to use any fancy Artificial Intelligence here, just trying to find correlations
			between certain pieces of data that I can easily collect and how interesting the
			article is to most people.</div>
		<div class="paragraph">
			<strong>This is where you come in.</strong>
		</div>
		<div class="paragraph">
			If I have enough ratings from real people I can start to see how the different pieces
			of data correlate to real ratings. Does it matter more if an article is very long?
			What about if it is edited often? What about the total number of links to or from
			the article?
		</div>
		<div class="paragraph">
			Once I have a reasonable amount of data I'll use your votes to tune The Algorithm
			to be able to get in the same ballpark as your average votes, this way you can see
			how interesting an article might be before you read it!</div>
		<h2>
			History</h2>
		<div class="paragraph">
			For a long time I had my browser's homepage set to the random Wikipedia article:
			http://en.wikipedia.org/wiki/Special:Random. After a while I got pretty disappointed
			by the quality of the article that came up. (For example, I just clicked it now
			and got: http://en.wikipedia.org/wiki/List_of_Newark_Bears_(AFL)_players currently
			rates a 2). I was complaining about this to a friend and he said "why don't you
			fix it?" After a bit of thinking "why don't I fix it" I set out to create an algorithm.</div>
		<div class="paragraph">
			I quickly discovered collecting the data was the easy part, but I had seriously
			no idea what to do with it or how it correlated to what was actually interesting
			I decided to enlist my the "crowd" for feedback. I appreciate you spending time
			with this, you're helping to make the world a better place.</div>
		<h2>
			The Algorithm</h2>
		<div class="paragraph">
			For all you nerds out there, here's the current state of the algorithm:</div>
		<div class="paragraph">
			Sorry, it's late, so I don't have much time to explain what this means if you don't
			read source, but suffice it to say, it's rough, and is in need of some serious tuning.
			<br /><br />
			Last updated 3/16/2011
			</div>
	<pre>
	<code>
//is disambiguation page, automatically a 1
if (body.Contains(&quot;This &lt;a href=\&quot;/wiki/Help:Disambiguation\&quot; 
        title=\&quot;Help:Disambiguation\&quot;&gt;disambiguation&lt;/a&gt;&quot;))
{
   rating = 1;
}
else
{
   int linksToWeight = 50;
   int linksFromWeight = 25;
   int minutesSinceLastEditWeight = 0;
   int totalEditsWeight = 5;
   int totalMinorEditsWeight = 2;
   //if something is featured it's probably pretty good, this will
   // push it up into the 7 Range, plus the other pieces it's close
   // to being a 10
   int isFeaturedWeight = 10000;
   // reduce the total length weight by a decent amount
   double totalLengthWeight = 1.0 / 200.0;
   int viewsInLast30DaysWeight = 2;

   int CurLinksTo = GetLinksTo();
   int CurLinksFrom = GetLinksFrom();
   int CurMinutesSinceLastEdit = GetMinSinceLastEdit();
   int CurTotalEdits = GetTotalEdits();
   int CurTotalMinorEdits = GetTotalMinEdits();
   int CurIsFeatured = GetIsFeatured();
   //25000 bytes is about what the wiki cruft is
   int CurTotalLength = GetTotalLength() - 25000;
   int CurViewsInLast30Days = GetViewsInLast30Days();


   //Here's where the magic happens!
   int internalRating = (linksToWeight * CurLinksTo) +
            (linksFromWeight * CurLinksFrom) +
            (minutesSinceLastEditWeight * CurMinutesSinceLastEdit) +
            (totalEditsWeight * CurTotalEdits) +
            (totalMinorEditsWeight * CurTotalMinorEdits) +
            (isFeaturedWeight * CurIsFeatured) +
            (int)(totalLengthWeight * (double)CurTotalLength) +
            (viewsInLast30DaysWeight * CurViewsInLast30Days);

   if (internalRating &lt;= 300)
      rating = 1;
   else if (internalRating &gt; 300 &amp;&amp; internalRating &lt;= 1000)
      rating = 2;
   else if (internalRating &gt; 1000 &amp;&amp; internalRating &lt;= 2500)
      rating = 3;
   else if (internalRating &gt; 3500 &amp;&amp; internalRating &lt;= 6000)
      rating = 4;
   else if (internalRating &gt; 6000 &amp;&amp; internalRating &lt;= 9000)
      rating = 5;
   else if (internalRating &gt; 9000 &amp;&amp; internalRating &lt;= 12000)
      rating = 6;
   else if (internalRating &gt; 12000 &amp;&amp; internalRating &lt;= 20000)
      rating = 7;
   else if (internalRating &gt; 20000 &amp;&amp; internalRating &lt;= 30000)
      rating = 8;
   else if (internalRating &gt; 30000 &amp;&amp; internalRating &lt;= 40000)
      rating = 9;
   else if (internalRating &gt; 40000)
      rating = 10;
}
</code>
</pre>
	</div>
</asp:Content>
