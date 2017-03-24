using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Model
{
    public class ContractModel
    {
        public int ContractID { get; set; }
        public int CompanyID { get; set; }
        public string Company { get; set; }
        public int ContractTypeID { get; set; }

        [Display(Name = "Contract Type")]
        public string ContractType { get; set; }

        //[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Signed Date")]
        public DateTime? SignedDate { get; set; }

        //[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        //[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Renewal Date")]
        public DateTime? RenewalDate { get; set; }

        //[DisplayFormat(DataFormatString = "{0:#,##0.00}", ApplyFormatInEditMode = true)]
        public double Price { get; set; }

        [Display(Name = "Is Valid")]
        public bool IsValid { get; set; }

        public string SignedDateText { get { return SignedDate.HasValue ? SignedDate.Value.ToShortDateString() : ""; } }
        public string EndDateText { get { return EndDate.HasValue ? EndDate.Value.ToShortDateString() : ""; } }
        public string RenewalDateText { get { return RenewalDate.HasValue ? RenewalDate.Value.ToShortDateString() : ""; } }
        public string PriceText { get { return Price.ToString("#,##0.00"); } }
        public string IsValidText { get { return IsValid?"Yes": "No"; } }
    }
}
