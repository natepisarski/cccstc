using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cccstc.Controllers
{
	/*
	 * <table class="table table-striped">
<tr>
<th>Email</th><th>Start</th><th>End</th><th>Length</th>
</tr>
@foreach(Session s in Model)
{
<tr>
	<td>@s.Email</td>
	<td>@s.Start.ToString()</td>
	<td>@s.End.ToString()</td>
	<td>@s.ElapsedTime().ToString()</td>
</tr>
}
</table>
*/
    public class SessionController : Controller
    {
        public ActionResult Index()
        {
            return View ();
        }

		public static string HTML_MakeTableRow(params string[] info)
		{
			string row = "<tr>";

			foreach (string line in info)
				row += String.Format ("<td>{0}</td>", line);

			row += "</tr>";

			return row;
		}

		public string GetSessions()
		{
			DateTime from = Convert.ToDateTime (Request.Params ["fromFormat"]);
			DateTime to = Convert.ToDateTime (Request.Params ["toFormat"]);

			string email = Request.Params ["email"];

			string responseHTML = "";
			var sessions = STCFactory.SessionsOf (email, from, to, "Content/stcdb.sqlite");

			// Initialize the table
			responseHTML += "<table class=\"table table-striped\">"; 

			// Set up the table headings, etc
			responseHTML += "<tr><th>Email</th><th>Start</th><th>End</th><th>Length</th></tr>";

			// Add all of the sessions to the table
			foreach (Session s in sessions) 
				responseHTML += HTML_MakeTableRow (s.Email, s.Start.ToString (), s.End.ToString (), s.ElapsedTime().ToString ());

			TimeSpan totalHours = new TimeSpan ();

			foreach (Session s in sessions.Where(x => x.Email.Equals(email)))
				totalHours = totalHours.Add (s.ElapsedTime());
			
			responseHTML += "</table><h3 class=\"center\">Total hours: " + totalHours + "</h3>";

			return responseHTML;
		}
    }
}
