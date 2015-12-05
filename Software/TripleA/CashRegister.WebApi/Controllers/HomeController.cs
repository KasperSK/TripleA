using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CashRegister.WebApi.Controllers
{
    /// <summary>
    /// Controller til website
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Funktion til at hente index siden
        /// </summary>
        /// <returns>Index siden</returns>
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        /// <summary>
        /// Funktion til at hente settings siden
        /// </summary>
        /// <returns>Settings siden</returns>
        public ActionResult Settings()
        {
            return View();
        }

        public ActionResult Statistic()
        {
            return View();
        }
    }
}
