using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Model;
using CMS.Utilities;

namespace CMS.BLL
{
    public class ContractTypeBLL
    {
        public List<ContractTypeModel> GetContractTypeList()
        {
            List<ContractTypeModel> ctList = new List<ContractTypeModel>();
            foreach (int id in Enum.GetValues(typeof(ContractType)))
            {
                ctList.Add(new ContractTypeModel { ContractTypeID = id, Name = Enum.GetName(typeof(ContractType), id) });
            }

            return ctList;
        }
    }
}
