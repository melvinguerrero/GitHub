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
	public class ContractDAL
	{
		public static List<ContractModel> GetContracts(int? companyID, int? contractTypeID, bool? isValid, bool? isNearRenewal,
            int startRowIndex, int maximumRows, string sortField, string sortOrder, ref int recordCount)
		{
			SqlParameter[] parms = new SqlParameter[]
			{
				new SqlParameter("@CompanyID",companyID),
                new SqlParameter("@ContractTypeID",contractTypeID),
                new SqlParameter("@IsValid",isValid),
                new SqlParameter("@IsNearRenewal",isNearRenewal),
                new SqlParameter("@StartRowIndex",startRowIndex),
				new SqlParameter("@MaxRowCount",maximumRows),
				new SqlParameter("@SortField",sortField),
				new SqlParameter("@SortOrder",sortOrder),
				SqlHelper.CreateOutParameter("@ActualCount",DbType.Int32)
			};

			List<ContractModel> list = new List<ContractModel>();
			using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AppConnectionString, CommandType.StoredProcedure, "usp_GetContracts", parms))
			{
				while (reader.Read())
				{
					ContractModel model = new ContractModel()
					{
						ContractID = int.Parse(reader["ContractID"].ToString()),
                        CompanyID = int.Parse(reader["CompanyID"].ToString()),
                        Company = reader["Company"].ToString(),
                        ContractTypeID = int.Parse(reader["ContractTypeID"].ToString()),
                        ContractType = reader["ContractType"].ToString(),
						SignedDate = Helper.ObjToNullableDate(reader["SignedDate"]),
						EndDate = Helper.ObjToNullableDate(reader["EndDate"]),
                        RenewalDate = Helper.ObjToNullableDate(reader["RenewalDate"]),
                        Price = double.Parse(reader["Price"].ToString()),
                        IsValid = Helper.ObjToBool(reader["IsValid"])
                    };
					list.Add(model);
				}
			}

			recordCount = int.Parse(parms[parms.Length - 1].Value.ToString());
			return list;
		}

		public static ContractModel GetContract(int companyID)
		{
			SqlParameter[] param = new SqlParameter[] {
				 new SqlParameter("@ContractID", companyID)
			};
            ContractModel model = null;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AppConnectionString, CommandType.StoredProcedure, "usp_GetContract", param))
			{
				while (reader.Read())
				{
					model = new ContractModel()
                    {
                        ContractID = int.Parse(reader["ContractID"].ToString()),
                        CompanyID = int.Parse(reader["CompanyID"].ToString()),
                        Company = reader["Company"].ToString(),
                        ContractTypeID = int.Parse(reader["ContractTypeID"].ToString()),
                        ContractType = reader["ContractType"].ToString(),
                        SignedDate = Helper.ObjToNullableDate(reader["SignedDate"]),
                        EndDate = Helper.ObjToNullableDate(reader["EndDate"]),
                        RenewalDate = Helper.ObjToNullableDate(reader["RenewalDate"]),
                        Price = double.Parse(reader["Price"].ToString()),
                        IsValid = Helper.ObjToBool(reader["IsValid"])
                    };
				}
			}
			return model;
		}

		public static int InsertContract(ContractModel model)
		{
			SqlParameter[] param = new SqlParameter[] {
				new SqlParameter("@CompanyID",model.CompanyID),
				new SqlParameter("@ContractTypeID",model.ContractTypeID),
				new SqlParameter("@SignedDate",model.SignedDate),
				new SqlParameter("@EndDate",model.EndDate),
                new SqlParameter("@RenewalDate",model.RenewalDate),
                new SqlParameter("@Price",model.Price),
                SqlHelper.CreateOutParameter("@ContractID", DbType.Int32)
			 };

			SqlHelper.ExecuteNonQuery(SqlHelper.AppConnectionString, CommandType.StoredProcedure, "usp_InsertContract", param);

			return int.Parse(param[param.Length - 1].Value.ToString());
		}

		public static int UpdateContract(ContractModel model)
		{
			SqlParameter[] param = new SqlParameter[] {
				new SqlParameter("@ContractID",model.ContractID),
                new SqlParameter("@CompanyID",model.CompanyID),
                new SqlParameter("@ContractTypeID",model.ContractTypeID),
                new SqlParameter("@SignedDate",model.SignedDate),
                new SqlParameter("@EndDate",model.EndDate),
                new SqlParameter("@RenewalDate",model.RenewalDate),
                new SqlParameter("@Price",model.Price),
                SqlHelper.CreateOutParameter("@ErrCode", DbType.Int32)
            };

			SqlHelper.ExecuteNonQuery(SqlHelper.AppConnectionString, CommandType.StoredProcedure, "usp_UpdateContract", param);

            return int.Parse(param[param.Length - 1].Value.ToString());
        }

		public static int DeleteContract(int companyID)
		{
			SqlParameter[] param = new SqlParameter[] {
				 new SqlParameter("@ContractID", companyID),
                SqlHelper.CreateOutParameter("@ErrCode", DbType.Int32)
            };

			SqlHelper.ExecuteNonQuery(SqlHelper.AppConnectionString, CommandType.StoredProcedure, "usp_DeleteContract", param);

            return int.Parse(param[param.Length - 1].Value.ToString());
        }
	}
}
