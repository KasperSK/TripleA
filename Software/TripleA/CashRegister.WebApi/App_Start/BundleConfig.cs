﻿using System.Web;
using System.Web.Optimization;

namespace CashRegister.WebApi
{
    /// <summary>
    /// To setup the webpart of the project
    /// </summary>
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        /// <summary>
        /// To register javascript html and css pages
        /// </summary>
        /// <param name="bundles"></param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/Scripts/knockout-{version}.js", 
                "~/Scripts/app.js"));
            bundles.Add(new ScriptBundle("~/bundles/Statistik").Include(
                 "~/Scripts/knockout-{version}.js",
                 "~/Scripts/kendo.all.min.js",
                 "~/Scripts/knockout-kendo.min.js",
                 "~/Scripts/Statistik.js"));
        }
    }
}
