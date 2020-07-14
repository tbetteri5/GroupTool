using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupTool.Func
{
    public class MyFunc
    {

        public static string StringOut(string addstr,string id, string data)
        {
            //This formats a value for output
            //'[Name:] John Smith,'  

            return addstr + "[" + id + ":] " + data + ", ";
        }
        public static string[] SplitIt(string val, char sep)
        {
            string[] words;
            if (val.Contains(sep.ToString()))
            {

                words = val.Split((char)sep);
            }
            else
            {
                //Return a 1 dim array with val
                words = (val + "," + " ").Split(',');

            }

            return words;

        }
    }
}
