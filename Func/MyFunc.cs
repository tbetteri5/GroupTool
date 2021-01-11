using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml;
using Microsoft.Office.Interop.Outlook;
using Outlook = Microsoft.Office.Interop.Outlook;
using OutlookApp = Microsoft.Office.Interop.Outlook.Application;

namespace GroupTool.Func
{
    public class MyFunc
    {
        public static string FilePath()
        {
            return @"M:\Agent Tool Folder\ScratchPad\";
        }
        public static string LLNumber()
        {
            return Environment.UserName;
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

        public static string RecordOut(string StrToAppend, string sep, string data,int dummycount)
        {
            string ou = "";
            if (data == "")
            {
                ou = StrToAppend + sep;
            }
            else
            {
                ou = StrToAppend + sep + data;
            }
            
            return ou;
        }

        public static string[] SplitIt(string val, char sep)
        {
            string[] words = { };

            try
            {
                if (val.Contains(sep.ToString()))
                {
                    words = val.Split((char)sep);
                }
                else
                {
                    //Return a 1 dim array with val
                    words = (val + "," + " ").Split(',');
                }
            }
            catch (System.Exception ex)
            {

                MessageBox.Show("SplitIt Error: " + ex.Message);
            }
            
            return words;

        }
        public static string SplitTwice(string val, string sep1, string sep2)
        {
            string tmp;
            string[] words;
            val = val.ToUpper();
            sep1 = sep1.ToUpper();
            

            
            tmp = val.Replace(sep1, "~");
            words = tmp.Split('~');

            try
            {
                //This exception is ignored
                tmp = words[1].Replace(sep2, "~");
                return tmp.Split('~')[0].Trim();
            }
            catch{
                
                return "";
            }
            
           

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
            if (val1 == ".") { val1 = "0"; }
            if (val2 == ".") { val2 = "0"; }
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
        public static string PullData(string srch, string input)
        {
            //srch - Type of data to filter: Address, Phone, Name;
            //input - input text string to filter for srch

            string temp;

            return MyFunc.SplitTwice(input, srch, "\r\n");
        }

        public static string SelectedTextOveride(string selText)
        {
            /*This function checks to see if any text is selectedin
             * copyWindow. If so, it will paste if not then the 
             * contents of clipboard are pasted.
             */

            if (selText == "")
            {
                return Clipboard.GetText();
            }
            else
            {
                return selText;
            }
        }
        public static string CleanAddress(string addr)
        {
            addr = addr.Replace("\r\n", ",");
            addr = addr.Replace("Drive", "Dr ");
            addr = addr.Replace(".", "");
            addr = addr.Replace("Avenue", "Ave");
            addr = addr.Replace("Place", "Pl");
            addr = addr.Replace("Street", "St");
            addr = addr.Replace("Court", "Crt");
            
            return addr;
        }
        public static string DateConvert(string dt)
        {
            dt = dt.Replace(",", "");
            dt = dt.Replace(" ", ".");

            string[] words = MyFunc.SplitIt(dt, '.');
            if (words[1].Length == 1) { words[1] = "0" + words[1]; }


            return words[0] + "." + words[1] + "." + words[2];
        }
        public static string RemoveLeadingZero(string pol)
        {
            string tmp = pol;

            
                if (pol.Substring(0,2) == "00")
                {
                    tmp = pol.Substring(2, pol.Length - 2);
                   
                }
                else if(pol.Substring(0,1) == "0")
                {
                    tmp = pol.Substring(2, pol.Length - 1);
                }

            return tmp;
            
        }

        public static string ReplaceWord(string input,string oldword, string newword)
        {
            return  Regex.Replace(input, oldword, newword);

           
        }

        public static int CountChar(string input,char character)
        {
            if(input == null) { return 0; }
            return input.ToCharArray().Count(c => c == character);
        }


        public static string SplitFunc(string input, char splitchar, int elem)
        {
            string ou = "";

           

            if (MyFunc.CountChar(input, splitchar) > 0)
            {
                ou = MyFunc.SplitIt(input, splitchar)[elem];
            }

            return ou;
        }
    }
    
    //HELP
    //tbPhone.SelectionStart = tbPhone.Text.Length;
     //tbPhone.SelectionLength = 0;
}
