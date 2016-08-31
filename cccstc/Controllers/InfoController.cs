using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cccstc.Controllers
{
    public class InfoController : Controller
    {
        public ActionResult Index()
        {
			return View (STCFactory.Information("Content/stcdb.sqlite"));
        }
    }
}
