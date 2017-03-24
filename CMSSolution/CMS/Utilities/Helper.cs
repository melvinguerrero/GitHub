using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Utilities
{
    public class Helper
    {
        public static DateTime ObjToDate(object objToConvert)
        {
            return Convert.ToDateTime(objToConvert);
        }

        public static DateTime? ObjToNullableDate(object objToConvert)
        {
            DateTime? tempVal;

            if (objToConvert == null || objToConvert.ToString().Length == 0)
            {
                tempVal = null;
            }
            else
            {
                tempVal = Convert.ToDateTime(objToConvert);
            }
            return tempVal;
        }

        public static bool ObjToBool(object objToConvert)
        {
            bool tempVal = false;

            if (objToConvert != null &&
                    ((objToConvert.ToString() == "1") || (objToConvert.ToString().ToUpper() == bool.TrueString.ToUpper()) || (objToConvert.ToString().ToLower() == "yes")))
            {
                tempVal = true;
            }

            return tempVal;
        }

        public static Nullable<int> ObjToNullableInt(object objToConvert)
        {
            Nullable<int> tempVal = null;
            if (objToConvert != null)
            {
                try
                {
                    tempVal = int.Parse(objToConvert.ToString());
                }
                catch
                {
                }
            }
            return tempVal;
        }
    }
}
