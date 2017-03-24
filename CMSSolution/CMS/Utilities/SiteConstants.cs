using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Utilities
{
	public enum ContractType
	{
		Master = 1,
		Standard = 2
	}

	public class SiteConstants
	{
		public static string AppConnectionString
		{
			get
			{
				return ConfigurationManager.ConnectionStrings["AppConnectionString"].ToString();
			}
		}

		public static int PageSize
		{
			get
			{
				return int.Parse(ConfigurationManager.AppSettings["PageSize"].ToString());
			}
		}
	}
}
