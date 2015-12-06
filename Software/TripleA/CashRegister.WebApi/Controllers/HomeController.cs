using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CashRegister.WebApi.Controllers
{
    /// <summary>
    /// Controller to website
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Function to get the index site
        /// </summary>
        /// <returns>Index site</returns>
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        /// <summary>
        /// Function to get the settings site
        /// </summary>
        /// <returns>Settings site</returns>
        public ActionResult Settings()
        {
            return View();
        }

        /// <summary>
        /// Function to get the statistic site
        /// </summary>
        /// <returns>Statistic site</returns>
        public ActionResult Statistic()
        {
            return View();
        }
    }
}
