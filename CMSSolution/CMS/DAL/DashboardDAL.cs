using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Utilities;

namespace CMS.DAL
{
    public class DashboardDAL
    {
        public static void GetDashboard(out int validContractsCount, out int nearRenewalContractCount, out int companyWithoutContracts, out double totalValidContractsAmount)
        {
            SqlParameter[] param = new SqlParameter[] {
                SqlHelper.CreateOutParameter("@ValidContractsCount", DbType.Int32),
                SqlHelper.CreateOutParameter("@NearRenewalContractCount", DbType.Int32),
                SqlHelper.CreateOutParameter("@CompanyWithoutContracts", DbType.Int32),
                SqlHelper.CreateOutParameter("@TotalValidContractsAmount", DbType.Int32)                
             };

            SqlHelper.ExecuteNonQuery(SqlHelper.AppConnectionString, CommandType.StoredProcedure, "usp_GetDashboard", param);

            validContractsCount = int.Parse(param[0].Value.ToString());
            nearRenewalContractCount = int.Parse(param[1].Value.ToString());
            companyWithoutContracts = int.Parse(param[2].Value.ToString());
            totalValidContractsAmount = double.Parse(param[3].Value.ToString());
        }
    }
}
