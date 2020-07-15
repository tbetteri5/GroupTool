using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GroupTool.Func
{
    public class MyFunc
    {
        public static string FilePath()
        {
            return @"M:\Desktop\GroupTool\ScratchPad\";
        }

        public static string nl()
        {
            return System.Environment.NewLine;
        }
        public static string StringOut(string StrToAppend,string id, string data, string mask)
        {
            string ou;
            //This formats a value for output
            //'[Name:] John Smith,'  
            //StrToAppend - The text string to append to with new data
            //id - The description of the dat
            //data - the data. If the data is null then append null to StrToAppend and return
            //mask - default string to be ignored if value is not equal to null

            if(data == string.Empty || data == mask)
            {
                ou = StrToAppend;
            }
            else
            {
                ou = StrToAppend + "  " + id + ":" + data;
            }
            return ou;
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
