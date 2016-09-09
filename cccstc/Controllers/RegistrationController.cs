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

		/// <summary>
		/// Writes the worker to the database
		/// </summary>
		/// <returns>The worker</returns>
		public string WriteWorker()
		{
			var name = Request ["name"];
			var email = Request ["email"];

			STCFactory.WriteWorker (name, email, "Content/stcdb.sqlite");

			return "";
		}

		/// <summary>
		/// Unregister a user from the database
		/// </summary>
		public void Unregister(string userName)
		{
			
			STCFactory.Unregister (userName, "Content/stcdb.sqlite");
			this.RedirectToActionPermanent ("Index", "Home");
		}
    }
}
