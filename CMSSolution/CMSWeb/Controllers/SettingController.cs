using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.BLL;
using CMS.Model;
using CMS.Utilities;

namespace CMSWeb.Controllers
{
    public class SettingController : Controller
    {
        #region Company Settings

        public ActionResult Company(string filterName)
        {
			List<CompanyModel> companyList = new CompanyBLL().GetCompanies(filterName, 0, SiteConstants.PageSize, null, null);

            ViewBag.FilterName = filterName;

			return View(companyList);
		}
		
		public ActionResult CompanyList()
		{
			List<CompanyModel> companyList = new CompanyBLL().GetCompanyList();
			List<string> nameList = companyList.Select(c => c.Name).ToList();

			return Json(nameList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CompanySave(CompanyModel model)
        {
            string errMsg = "";
            int errCode = new CompanyBLL().SaveCompany(model);

            if (errCode == -1)
            {
                errMsg = "Company Name already exists";
            }

            return Json(new { ErrMsg = errMsg }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCompany(int id)
        {
            CompanyModel model = new CompanyBLL().GetCompany(id);

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteCompany(int id)
        {
            string errMsg = "";
            int errCode = new CompanyBLL().DeleteCompany(id);
            if(errCode == -1)
            {
                errMsg = "This company is still being used as reference in Contracts.";
            }

            return Json(new { ErrMsg = errMsg }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Contacts Settings

        public ActionResult Contacts(string fullName, int? companyID, int? contractTypeID)
        {
            List<ContactModel> contactList = new ContactBLL().GetContacts(fullName, companyID, contractTypeID, 0, SiteConstants.PageSize, null, null);

            ViewBag.FullName = fullName;
            ViewBag.CompanyID = companyID;
            ViewBag.ContractTypeID = contractTypeID;

            return View(contactList);
        }

        public ActionResult Details(int id)
        {
            return View();
        }
        
        public ActionResult ContactCreate()
        {
            return View();
        }

        // POST: Test/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                //collection.GetValue

                //new ContactBLL().InsertContact();

                return RedirectToAction("/Setting/Contacts");
            }
            catch
            {
                return View();
            }
        }

        // GET: Test/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Test/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Test/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Test/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        #endregion
    }
}