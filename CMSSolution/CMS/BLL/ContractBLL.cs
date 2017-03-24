using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.DAL;
using CMS.Model;

namespace CMS.BLL
{
	public class ContractBLL
	{
		public int _recordCount = 0;

		public List<ContractModel> GetContracts(int? companyID, int? contractTypeID, bool? isValid, bool? isNearRenewal,
            int startRowIndex, int maximumRows, string sortField, string sortOrder)
		{
			return ContractDAL.GetContracts(companyID, contractTypeID, isValid, isNearRenewal, 
                startRowIndex, maximumRows, sortField, sortOrder, ref _recordCount);
		}

		public int GetContractsCount()
		{
			return _recordCount;
		}

		public ContractModel GetContract(int companyID)
		{
			return ContractDAL.GetContract(companyID);
		}

        public int SaveContract(ContractModel model)
        {
            if (model.ContractID > 0)
            {
                return UpdateContract(model);
            }
            else
            {
                return InsertContract(model);
            }
        }

		public int InsertContract(ContractModel model)
		{
			return ContractDAL.InsertContract(model);
		}

		public int UpdateContract(ContractModel model)
		{
			return ContractDAL.UpdateContract(model);
		}

		public int DeleteContract(int companyID)
		{
			return ContractDAL.DeleteContract(companyID);
		}
	}
}
