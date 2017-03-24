using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.DAL;
using CMS.Model;

namespace CMS.BLL
{
	public class ContactBLL
	{
		public int _recordCount = 0;

		public List<ContactModel> GetContactList()
		{
			return ContactDAL.GetContactList();
		}

		public List<ContactModel> GetContacts(string fullName, int? companyID, int? contractTypeID, int startRowIndex, int maximumRows, string sortField, string sortOrder)
		{
			return ContactDAL.GetContacts( fullName, companyID, contractTypeID, startRowIndex, maximumRows, sortField, sortOrder, ref _recordCount);
		}

		public int GetContactsCount()
		{
			return _recordCount;
		}

		public ContactModel GetContact(int companyID)
		{
			return ContactDAL.GetContact(companyID);
		}

        public int SaveContact(ContactModel model)
        {
            if (model.ContactID > 0)
            {
                return UpdateContact(model);
            }
            else
            {
                return InsertContact(model);
            }
        }

		public int InsertContact(ContactModel model)
		{
			return ContactDAL.InsertContact(model);
		}

		public int UpdateContact(ContactModel model)
		{
			return ContactDAL.UpdateContact(model);
		}

		public int DeleteContact(int companyID)
		{
			return ContactDAL.DeleteContact(companyID);
		}
	}
}
