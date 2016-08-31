using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cccstc.Controllers
{
    public class InfoController : Controller
    {
		/// <summary>
		/// Writes info to the database
		/// </summary>
		/// <returns>The info to write to the database</returns>
		public string WriteInfo()
		{
			var title = Request ["title"];
			var body = Request ["body"];

			STCFactory.WriteInformation (title, body, 0, "Content/stcdb.sqlite");

			return "Information submitted. Refresh to view it.";
		}

        public ActionResult Index()
        {
			var information = STCFactory.Information ("Content/stcdb.sqlite");


			List<Info> sorted = new List<Info> ();

			// The sticky posts should be added according to chronological order
			foreach (Info info in information.Where(x => x.Sticky))
				sorted.Add (info);

			// Add the rest of the information in chronilogical order
			foreach (Info info in information.Where(x => (!x.Sticky)))
				sorted.Add (info);
			
			return View (sorted);
        }
    }
}
