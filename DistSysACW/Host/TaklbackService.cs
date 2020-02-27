using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Host
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TaklbackService" in both code and config file together.
    public class TaklbackService : ITalkbackService
    {
        //public string GetData(int value)
        //{
        //    return string.Format("You entered: {0}", value);
        //}
        public string server_reply()
        {
            
            return "Hello world";
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
