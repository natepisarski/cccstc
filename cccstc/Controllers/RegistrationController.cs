using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cccstc.Controllers
{
    public class RegistrationController : Controller
    {
        public ActionResult Index()
        {
            return View ();
        }

		public string WriteWorker()
		{
			var name = Request ["name"];
			var email = Request ["email"];

			STCFactory.WriteWorker (name, email, "Content/stcdb.sqlite");

			return "";
		}
    }
}
