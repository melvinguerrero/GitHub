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
	public class ContactDAL
	{
		public static List<ContactModel> GetContactList()
		{
			List<ContactModel> list = new List<ContactModel>();
			using (SqlDataReader reader = SqlHelper.ExecuteReader(
				SqlHelper.AppConnectionString,
				CommandType.StoredProcedure,
				"usp_GetContactList"))
			{
				while (reader.Read())
				{
					ContactModel model = new ContactModel()
					{
						ContactID = int.Parse(reader["ContactID"].ToString()),
						Title = reader["Title"].ToString(),
						FirstName = reader["FirstName"].ToString(),
						LastName = reader["LastName"].ToString()
					};
					list.Add(model);
				}
			}
			return list;
		}

		public static List<ContactModel> GetContacts(string fullName, int? companyID, int? contractTypeID, 
            int startRowIndex, int maximumRows, string sortField, string sortOrder, ref int recordCount)
		{
			SqlParameter[] parms = new SqlParameter[]
			{
				new SqlParameter("@FullName",String.IsNullOrEmpty(fullName) ? null : fullName),
                new SqlParameter("@CompanyID",companyID),
                new SqlParameter("@ContractTypeID",contractTypeID),
                new SqlParameter("@StartRowIndex",startRowIndex),
				new SqlParameter("@MaxRowCount",maximumRows),
				new SqlParameter("@SortField",sortField),
				new SqlParameter("@SortOrder",sortOrder),
				SqlHelper.CreateOutParameter("@ActualCount",DbType.Int32)
			};

			List<ContactModel> list = new List<ContactModel>();
			using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AppConnectionString, CommandType.StoredProcedure, "usp_GetContacts", parms))
			{
				while (reader.Read())
				{
                    ContactModel model = new ContactModel()
                    {
                        ContactID = int.Parse(reader["ContactID"].ToString()),
                        TitleID = int.Parse(reader["TitleID"].ToString()),
                        Title = reader["Title"].ToString(),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        CompanyID = int.Parse(reader["CompanyID"].ToString()),
                        Company = reader["Company"].ToString(),
                        ContractTypeID = Helper.ObjToNullableInt(reader["ContractTypeID"].ToString()),
                        ContractType = reader["ContractType"].ToString(),
                        Email = reader["Email"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString(),
                        Department = reader["Department"].ToString()
                    };

                    list.Add(model);
				}
			}

			recordCount = int.Parse(parms[parms.Length - 1].Value.ToString());
			return list;
		}

		public static ContactModel GetContact(int companyID)
		{
			SqlParameter[] param = new SqlParameter[] {
				 new SqlParameter("@ContactID", companyID)
			};

            ContactModel model = null;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AppConnectionString, CommandType.StoredProcedure, "usp_GetContact", param))
			{
				while (reader.Read())
				{
					model = new ContactModel()
					{
						ContactID = int.Parse(reader["ContactID"].ToString()),
                        TitleID = int.Parse(reader["TitleID"].ToString()),
                        Title = reader["Title"].ToString(),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        CompanyID = int.Parse(reader["CompanyID"].ToString()),
                        Company = reader["Company"].ToString(),
                        ContractTypeID = Helper.ObjToNullableInt(reader["ContractTypeID"].ToString()),
                        ContractType = reader["ContractType"].ToString(),
                        Email = reader["Email"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString(),
                        Department = reader["Department"].ToString()
                    };
				}
			}
			return model;
		}

		public static int InsertContact(ContactModel model)
		{
			SqlParameter[] param = new SqlParameter[] {
				new SqlParameter("@TitleID",model.TitleID),
				new SqlParameter("@FirstName",model.FirstName),
				new SqlParameter("@LastName",model.LastName),
				new SqlParameter("@CompanyID",model.CompanyID),
                new SqlParameter("@ContractTypeID",model.ContractTypeID),
                new SqlParameter("@Email",model.Email),
                new SqlParameter("@PhoneNumber",model.PhoneNumber),
                new SqlParameter("@Department",model.Department),
                SqlHelper.CreateOutParameter("@ContactID", DbType.Int32)
			 };

			SqlHelper.ExecuteNonQuery(SqlHelper.AppConnectionString, CommandType.StoredProcedure, "usp_InsertContact", param);

			return int.Parse(param[param.Length - 1].Value.ToString());
		}

		public static int UpdateContact(ContactModel model)
		{
			SqlParameter[] param = new SqlParameter[] {
				new SqlParameter("@ContactID",model.ContactID),
                new SqlParameter("@TitleID",model.TitleID),
                new SqlParameter("@FirstName",model.FirstName),
                new SqlParameter("@LastName",model.LastName),
                new SqlParameter("@CompanyID",model.CompanyID),
                new SqlParameter("@ContractTypeID",model.ContractTypeID),
                new SqlParameter("@Email",model.Email),
                new SqlParameter("@PhoneNumber",model.PhoneNumber),
                new SqlParameter("@Department",model.Department)
            };

			SqlHelper.ExecuteNonQuery(SqlHelper.AppConnectionString, CommandType.StoredProcedure, "usp_UpdateContact", param);

            return int.Parse(param[param.Length - 1].Value.ToString());
        }

		public static int DeleteContact(int companyID)
		{
			SqlParameter[] param = new SqlParameter[] {
				 new SqlParameter("@ContactID", companyID)
            };

			return SqlHelper.ExecuteNonQuery(SqlHelper.AppConnectionString, CommandType.StoredProcedure, "usp_DeleteContact", param);            
        }
	}
}
