using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.BLL;

namespace CMSWeb.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
            int validContractsCount = 0;
            int nearRenewalContractCount = 0;
            int companyWithoutContracts = 0;
            double totalValidContractsAmount = 0;

            new DashboardBLL().GetDashboard(out validContractsCount, out nearRenewalContractCount, out companyWithoutContracts, out totalValidContractsAmount);

            ViewBag.ValidContractsCount = validContractsCount;
            ViewBag.NearRenewalContractCount = nearRenewalContractCount;
            ViewBag.CompanyWithoutContracts = companyWithoutContracts;
            ViewBag.TotalValidContractsAmount = totalValidContractsAmount.ToString("#,##0.00");

            return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}