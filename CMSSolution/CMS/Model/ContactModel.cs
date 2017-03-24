using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Model
{
    public class ContactModel
    {
        public int ContactID { get; set; }
        public int CompanyID { get; set; }
        public string Company { get; set; }
        public int TitleID { get; set; }
        public string Title { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public int? ContractTypeID { get; set; }

        [Display(Name = "Contract Type")]
        public string ContractType { get; set; }

        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public string Department { get; set; }

        [Display(Name ="Full Name")]
        public string FullName
        {
            get
            {
                return string.Format("{0} {1} {2}", Title, FirstName, LastName);
            }
        }
    }
}
