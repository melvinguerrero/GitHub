using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.DAL;

namespace CMS.BLL
{
    public class DashboardBLL
    {
        public void GetDashboard(out int validContractsCount, out int nearRenewalContractCount, out int companyWithoutContracts, out double totalValidContractsAmount)
        {
            DashboardDAL.GetDashboard(out validContractsCount, out nearRenewalContractCount, out companyWithoutContracts, out totalValidContractsAmount);
        }
    }
}
