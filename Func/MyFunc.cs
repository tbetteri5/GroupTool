using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        public static string CleanStringOfNonDigits_V5(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            string cleaned = new string(s.Where(c => c - '0' < 10).ToArray());
            return cleaned;
        }
        public static string FormatPhone(string s)
        {
            string ou = "";
            //Replace characters with a letter that will be removed
            s = s.Replace('-', 'a');
            s = s.Replace(')', 'a');
            s = s.Replace('(', 'a');
            s = s.Replace(' ', 'a'); 
            s = s.Replace('.', 'a');

            string s1 =  MyFunc.CleanStringOfNonDigits_V5(s);
            if(s1.Length == 10)
            {
                ou = s1.Substring(0, 3) + "-" + s1.Substring(3,3) + "-" + s1.Substring(6, 4);
                    
            }
            else
            {
                ou = s1;
            }
            return ou;

            
        }

        public static string TextMath(string oper,string val1, string val2)
        {
            //oper {+,-,/,x}  val1 - text value 1
            double operand1, operand2, result;

            if (val1 == "" || val1 == "-") { val1 = "0"; }
            if (val2 == "" || val2 == "-") { val2 = "0"; }

            try
            {
                operand1 = Convert.ToDouble(val1);
                operand2 = Convert.ToDouble(val2);

                result = 0;
                if (oper == "+")
                {
                    result = operand1 + operand2;
                }
                
                else if(oper == "-")
                {
                    result = operand1 - operand2;
                }
                else if (oper == "x")
                {
                    result = operand1 * operand2;
                }
                else if (oper == "/")
                {
                    result = operand1 / operand2;
                }

                return Convert.ToString(result);
            }
            catch (InvalidCastException e)
            {
                return "error";
            }

        }
       
    }

    //HELP
    //tbPhone.SelectionStart = tbPhone.Text.Length;
     //tbPhone.SelectionLength = 0;
}
