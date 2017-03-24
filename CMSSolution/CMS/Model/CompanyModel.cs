using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Model
{
	public class CompanyModel
	{
		public int CompanyID { get; set; }

		[Required]
		[Display(Name = "Company Name")]
		public string Name { get; set; }

		[Required]
        [Display(Name = "ABN/CAN")]
        public string ABN { get; set; }
		
		public string Description { get; set; }

		[Required]
		public string Website { get; set; }
	}
}
