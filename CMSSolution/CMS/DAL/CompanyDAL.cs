using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Model;
using CMS.Utilities;

namespace CMS.DAL
{
	public class CompanyDAL
	{
		public static List<CompanyModel> GetCompanyList()
		{
			List<CompanyModel> list = new List<CompanyModel>();
			using (SqlDataReader reader = SqlHelper.ExecuteReader(
				SqlHelper.AppConnectionString,
				CommandType.StoredProcedure,
				"usp_GetCompanyList"))
			{
				while (reader.Read())
				{
					CompanyModel model = new CompanyModel()
					{
						CompanyID = int.Parse(reader["CompanyID"].ToString()),
						Name = reader["Name"].ToString(),
						ABN = reader["ABN"].ToString(),
						Description = reader["Description"].ToString(),
						Website = reader["Website"].ToString()
					};
					list.Add(model);
				}
			}
			return list;
		}

		public static List<CompanyModel> GetCompanies(string name, int startRowIndex, int maximumRows, string sortField, string sortOrder, ref int recordCount)
		{
			SqlParameter[] parms = new SqlParameter[]
			{
				new SqlParameter("@Name",String.IsNullOrEmpty(name) ? null : name),
				new SqlParameter("@StartRowIndex",startRowIndex),
				new SqlParameter("@MaxRowCount",maximumRows),
				new SqlParameter("@SortField",sortField),
				new SqlParameter("@SortOrder",sortOrder),
				SqlHelper.CreateOutParameter("@ActualCount",DbType.Int32)
			};

			List<CompanyModel> list = new List<CompanyModel>();
			using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AppConnectionString, CommandType.StoredProcedure, "usp_GetCompanies", parms))
			{
				while (reader.Read())
				{
					CompanyModel model = new CompanyModel()
					{
						CompanyID = int.Parse(reader["CompanyID"].ToString()),
						Name = reader["Name"].ToString(),
						ABN = reader["ABN"].ToString(),
						Description = reader["Description"].ToString(),
						Website = reader["Website"].ToString()
					};
					list.Add(model);
				}
			}

			recordCount = int.Parse(parms[parms.Length - 1].Value.ToString());
			return list;
		}

		public static CompanyModel GetCompany(int companyID)
		{
			CompanyModel model = null;
			SqlParameter[] param = new SqlParameter[] {
				 new SqlParameter("@CompanyID", companyID)
			};

			using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AppConnectionString, CommandType.StoredProcedure, "usp_GetCompany", param))
			{
				while (reader.Read())
				{
					model = new CompanyModel()
					{
						CompanyID = int.Parse(reader["CompanyID"].ToString()),
						Name = reader["Name"].ToString(),
						ABN = reader["ABN"].ToString(),
						Description = reader["Description"].ToString(),
						Website = reader["Website"].ToString()
					};
				}
			}
			return model;
		}

		public static int InsertCompany(CompanyModel model)
		{
			SqlParameter[] param = new SqlParameter[] {
				new SqlParameter("@Name",model.Name),
				new SqlParameter("@ABN",model.ABN),
				new SqlParameter("@Description",model.Description),
				new SqlParameter("@Website",model.Website),
				SqlHelper.CreateOutParameter("@CompanyID", DbType.Int32)
			 };

			SqlHelper.ExecuteNonQuery(SqlHelper.AppConnectionString, CommandType.StoredProcedure, "usp_InsertCompany", param);

			return int.Parse(param[param.Length - 1].Value.ToString());
		}

		public static int UpdateCompany(CompanyModel model)
		{
			SqlParameter[] param = new SqlParameter[] {
				new SqlParameter("@CompanyID",model.CompanyID),
				new SqlParameter("@Name",model.Name),
				new SqlParameter("@ABN",model.ABN),
				new SqlParameter("@Description",model.Description),
				new SqlParameter("@Website",model.Website),
                SqlHelper.CreateOutParameter("@ErrCode", DbType.Int32)
            };

			SqlHelper.ExecuteNonQuery(SqlHelper.AppConnectionString, CommandType.StoredProcedure, "usp_UpdateCompany", param);

            return int.Parse(param[param.Length - 1].Value.ToString());
        }

		public static int DeleteCompany(int companyID)
		{
			SqlParameter[] param = new SqlParameter[] {
				 new SqlParameter("@CompanyID", companyID),
                SqlHelper.CreateOutParameter("@ErrCode", DbType.Int32)
            };

			SqlHelper.ExecuteNonQuery(SqlHelper.AppConnectionString, CommandType.StoredProcedure, "usp_DeleteCompany", param);

            return int.Parse(param[param.Length - 1].Value.ToString());
        }
	}
}
