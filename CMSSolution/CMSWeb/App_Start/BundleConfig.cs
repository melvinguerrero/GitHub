﻿using System.Web;
using System.Web.Optimization;

namespace CMSWeb
{
	public class BundleConfig
	{
		// For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/jquery-{version}.js",
						"~/Scripts/jquery-ui.js",
                        "~/Scripts/jquery.twbsPagination.js"));



            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
						"~/Scripts/jquery.validate*"));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/modernizr-*"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
					  "~/Scripts/bootstrap.js",
					  "~/Scripts/respond.js")); 


            bundles.Add(new StyleBundle("~/Content/css").Include(
					  "~/Content/jquery-ui.css",
					  "~/Content/bootstrap.css",
					  "~/Content/sb-admin.css",
					  "~/Content/plugins/morris.css"));

			bundles.Add(new StyleBundle("~/Content/fontcss").Include(
					  "~/font-awesome/css/font-awesome.css"));
		}
	}
}
