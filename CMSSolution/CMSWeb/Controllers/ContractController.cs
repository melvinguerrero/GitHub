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
    public class ContractController : Controller
    {
        // GET: Contract
        public ActionResult Contracts(bool? isNearRenewal)
        {
            ContractModel model = new ContractModel();

            ViewBag.CompanyList = new CompanyBLL().GetCompanyList();
            ViewBag.ContractTypeList = new ContractTypeBLL().GetContractTypeList();

            ViewBag.IsNearRenewalText = isNearRenewal.HasValue && isNearRenewal.Value ? "selected" : "";


            return View(model);
        }

        public ActionResult GetContracts(int? companyID, int? contractTypeID, bool? isValid, bool? isNearRenewal, int? pageIndex)
        {
            ContractBLL bll = new ContractBLL();
            
            int startRowIndex = pageIndex.HasValue ? (pageIndex.Value - 1) * SiteConstants.PageSize : 0;

            List<ContractModel> contractList = bll.GetContracts(companyID, contractTypeID, isValid, isNearRenewal, startRowIndex, SiteConstants.PageSize, null, null);

            return Json(new { PageSize= SiteConstants.PageSize, PageIndex = pageIndex.HasValue ? pageIndex.Value : 0, RecordCount = bll.GetContractsCount()
                , ItemList = contractList }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ContractSave(ContractModel model)
        {
            string errMsg = "";
            int errCode = new ContractBLL().SaveContract(model);

            if (errCode == -1)
            {
                errMsg = "Save failed. This contract will overlap the current valid contract.";
            }

            return Json(new { ErrMsg = errMsg }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetContract(int id)
        {
            ContractModel model = new ContractBLL().GetContract(id);

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteContract(int id)
        {
            string errMsg = "";
            int errCode = new ContractBLL().DeleteContract(id);
            if (errCode == -1)
            {
                errMsg = "Cannot delete current valid contract.";
            }

            return Json(new { ErrMsg = errMsg }, JsonRequestBehavior.AllowGet);
        }
    }
}