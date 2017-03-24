using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Utilities
{
    public class SqlHelper
	{
		public static readonly string AppConnectionString = GetConnectionString();

		private static string GetConnectionString()
		{
			return SiteConstants.AppConnectionString;
		}

		public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
		{
			try
			{
				DateTime start = DateTime.Now;
				SqlCommand cmd = new SqlCommand();

				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
					int val = cmd.ExecuteNonQuery();
					return val;
				}
			}
			catch (SqlException ex)
			{
				throw ex;
			}
		}

		public static SqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
		{
			DateTime start = DateTime.Now;
			SqlCommand cmd = new SqlCommand();
			SqlConnection conn = new SqlConnection(connectionString);

			try
			{
				PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
				SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
				return rdr;
			}
			catch (SqlException ex)
			{
				conn.Close();
				throw ex;
			}
		}

		private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
		{
			if (conn.State != ConnectionState.Open)
				conn.Open();

			cmd.Connection = conn;
			cmd.CommandText = cmdText;
			cmd.CommandTimeout = 300;

			if (trans != null)
			{
				cmd.Transaction = trans;
			}

			cmd.CommandType = cmdType;

			if (cmdParms != null)
			{
				foreach (SqlParameter parm in cmdParms)
				{
					cmd.Parameters.Add(parm);
				}
			}
		}

		public static SqlParameter CreateOutParameter(string parameterName, DbType dbType)
		{
			SqlParameter param = new SqlParameter();
			param.ParameterName = parameterName;
			param.DbType = dbType;
			param.IsNullable = true;
			param.Direction = ParameterDirection.Output;
			return param;
		}
	}
}
