using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class translator:MarshalByRefObject
    {
        public string Translate(string English_string)
        {
            string[] words = English_string.Split(' ');
            string result = "";
            foreach(string word in words)
            {
                result = word.Substring(1);
                result += word.Substring(0, 1) + "ay";
            }
            return result;
        }
        
    }
}
