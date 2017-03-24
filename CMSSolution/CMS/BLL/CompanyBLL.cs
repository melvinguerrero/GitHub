using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.DAL;
using CMS.Model;

namespace CMS.BLL
{
	public class CompanyBLL
	{
		public int _recordCount = 0;

		public List<CompanyModel> GetCompanyList()
		{
			return CompanyDAL.GetCompanyList();
		}

		public List<CompanyModel> GetCompanies(string name, int startRowIndex, int maximumRows, string sortField, string sortOrder)
		{
			return CompanyDAL.GetCompanies(name, startRowIndex, maximumRows, sortField, sortOrder, ref _recordCount);
		}

		public int GetCompanysCount(string sortField, string sortOrder)
		{
			return _recordCount;
		}

		public CompanyModel GetCompany(int companyID)
		{
			return CompanyDAL.GetCompany(companyID);
		}

        public int SaveCompany(CompanyModel model)
        {
            if (model.CompanyID > 0)
            {
                return UpdateCompany(model);
            }
            else
            {
                return InsertCompany(model);
            }
        }

		public int InsertCompany(CompanyModel model)
		{
			return CompanyDAL.InsertCompany(model);
		}

		public int UpdateCompany(CompanyModel model)
		{
			return CompanyDAL.UpdateCompany(model);
		}

		public int DeleteCompany(int companyID)
		{
			return CompanyDAL.DeleteCompany(companyID);
		}
	}
}
