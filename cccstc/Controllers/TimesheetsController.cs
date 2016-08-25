using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cccstc.Controllers
{
	/// <summary>
	/// The controller that handles the Timesheets and sessions
	/// </summary>
    public class TimesheetsController : Controller
    {
        public ActionResult Index()
        {
			IEnumerable<STC> workers = STCFactory.Workers ("Content/stcdb.sqlite");

            return View (workers);
        }
    }
}
