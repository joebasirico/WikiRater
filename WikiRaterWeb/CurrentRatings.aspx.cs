﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WikiRaterWeb
{
	public partial class CurrentRatings : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			DataClassesDataContext dc = new DataClassesDataContext();
			var query1 = from rating in dc.Ratings 
						 where rating.IsLatest == true
						 group rating by rating.Article into result
						 select new
						 {
							 Article = result.Key,
							 Average = result.Average(i => (Double)i.Value)
						 };

			DataTable dt = new DataTable();
			dt.Columns.Add("Article");
			dt.Columns.Add("Rating", System.Type.GetType("System.Double"));

			foreach (var ratingValue in query1)
			{
				DataRow dr = dt.NewRow();
				//encode
				dr["Article"] = Server.HtmlEncode(ratingValue.Article); 
				dr["Rating"] = ratingValue.Average;
				
				dt.Rows.Add(dr);
			}
			dt.DefaultView.Sort = "Rating DESC";
			RatingsListView.DataSource = dt.DefaultView;
			RatingsListView.DataBind();
		}
	}
}