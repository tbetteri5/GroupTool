using System;
using System.Globalization;
using System.IO;
using System.Management.Instrumentation;
using System.Windows.Forms;
using GroupTool.Func;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms.VisualStyles;
using System.Text;
using System.ComponentModel;
using Outlook = Microsoft.Office.Interop.Outlook;
using OutlookApp = Microsoft.Office.Interop.Outlook.Application;
using OutlookTask = Microsoft.Office.Interop.Outlook.Application;

using Word = Microsoft.Office.Interop.Word;
using WordApp = Microsoft.Office.Interop.Word.Application;

using System.Reflection.Emit;
using Microsoft.Office.Interop.Word;

namespace GroupTool
{


    public partial class ScratchPad : Form
    {
        DateTime FormOpenedtimeStamp = DateTime.Now;
        string txtToFile; //String field written to file
        string txtToRecord;  //String field written to record file
        string StoreRecordFilePath = MyFunc.FilePath() + "DataBase.txt";
        bool FormBeingCleared = false;
        string sptString; //SPT data pulled from file SPTList

        //Path Strings
        //string M_Path_Excel = @"M:\Desktop\GroupTool\Excel\";
        string M_Path_Excel = @"N:\Brenda\Admin\Agent Tool\Excel Templates\";

        //string M_Path_Email = @"M:\Desktop\Outlook\email templates\";
        string M_Path_Email = @"N:\Brenda\Admin\Agent Tool\Email Templates\";

        string M_Path_AgentTool = @"N:\Brenda\Admin\Agent Tool\";

        string N_Path_Letters = @"N:\Brenda\Admin\Agent Tool\Letter Templates\";



        public bool Visible { get; set; }
        //public string SelectedRecord;


        //Dim the ucTodo Form and assign it to todo1


        public ScratchPad()
        {
            InitializeComponent();

        }

        private void btPastePolicy_Click(object sender, EventArgs e)
        {
            string tmp = MyFunc.SelectedTextOveride(tbCopyWindow.SelectedText.Trim());
            string ou;


            if (tmp.IndexOf("C") > 0 || tmp.IndexOf("c") > 0)
            {
                if (tmp.IndexOf("C") > 0)
                {
                    ou = tmp.Replace("C", "-");
                }
                else
                {
                    ou = tmp.Replace("c", "-");
                }
            }
            else
            {
                ou = tmp;
                // tbPolicyD.Text = tmp.Replace("-", "C");
            }
            tbPolicy.Text = MyFunc.RemoveLeadingZero(ou);
            Clipboard.SetText(tbPolicy.Text);

            UpdateSPT();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void btOutput_Click(object sender, EventArgs e)
        {
            if (tbOutput.Text != "")
            {
                Clipboard.SetText(tbOutput.Text);
            }
        }

        private void btClear_Click(object sender, EventArgs e)
        {
            //tbName.Text = "";

            this.Width = 382;
            btCopyToggle.Text = "Open -->";

            SaveRecord();
            ClearForm();




        }

        private void ClearForm()
        {

            ClearOtherControls();
            clearCheckBoxSection();

            try
            {
                ScratchPad.ActiveForm.Text = "ScratchPad Tool";
            }
            catch
            {

            }

            cbRequestType.Text = string.Empty;
        }
        private void clearCheckBoxSection()
        {
            tbCheck1.Text = string.Empty;
            tbCheck2.Text = string.Empty;
            tbCheck3.Text = string.Empty;
            tbCheck4.Text = string.Empty;
            tbCheck5.Text = string.Empty;
            tbCheck6.Text = string.Empty;
            tbCheck7.Text = string.Empty;
            tbCheck8.Text = string.Empty;
            tbCheck9.Text = string.Empty;
            tbCheck10.Text = string.Empty;

            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
            checkBox8.Checked = false;
            checkBox9.Checked = false;
            checkBox10.Checked = false;


        }
        private void ClearOtherControls()
        {
            tbOutput.Text = string.Empty;
            tbName.Text = string.Empty;
            tbPhone.Text = string.Empty;
            tbPolicy.Text = string.Empty;
            tbPolicyD.Text = string.Empty;
            tbNotes.Text = string.Empty;
            tbOutput.Text = string.Empty;
            tbAddress.Text = string.Empty;
            tbAgeOut.Text = string.Empty;
            tbYOB.Text = string.Empty;
            tbCheckListNotes.Text = string.Empty;
            tbAccountNotes.Text = string.Empty;

            tb10.Text = string.Empty;
            tb2.Text = string.Empty;

            tbCopyWindow.Text = string.Empty;
            lbCharCount.Text = "Addr Char Count:";
            ClearCalc();
            lbSPT.Text = "";

            tbDocHandle.Text = "";
            tbCalc_Add.Text = "";
            tbCalc_Div.Text = "";
            tbCalc_Mult.Text = "";
            tbCalc_Sub.Text = "";
            tbCalc_In.Text = "";
            tbCalc_Out.Text = "";

            tbFormCode.Text = "";


            rdFemale.Checked = false;
            rdMale.Checked = false;
            tbTName.Text = "";
            tbTSIN.Text = "";
            tbTDOB.Text = "";
            tbTID.Text = "";
            tbTDOD.Text = "";
            tbNameOut.Text = "";
            tbSpouseID.Text = "";
            tbAnnOut.Text = "";

            //
            tbEmailTo.Text = "";
            tbEmailSubject.Text = "";
            tbEmailBody.Text = "";
            cbLetterSelected.Text = "...Select Letter";


            tbSpokeWith.Text = "";


            tbExecutionDate.Text = "";
            tbCoAnnName.Text = "";
            tbLetterDate.Text = "";

        }


        private void btClose_Click(object sender, EventArgs e)
        {
            SaveRecord();
            this.Close();
        }

        private void btCopyName_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tbName.Text);
        }

        private void btCopyPhone_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tbPhone.Text);
            tbPhone.Select(0, 0);
        }

        private void btPastePhone_Click(object sender, EventArgs e)
        {
            string tmp = MyFunc.SelectedTextOveride(tbCopyWindow.SelectedText);
            tbPhone.Text = MyFunc.CleanStringOfNonDigits_V5(tmp);
            tbPhone.Select();
        }

        private void btCopyPolicyD_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tbPolicyD.Text);
        }

        private void btCopyPolicy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tbPolicy.Text);
        }

        private void tbOutput_TextChanged(object sender, EventArgs e)
        {

        }

        private void btCopyAddress_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tbAddress.Text);
        }

        private void btAddressPaste_Click(object sender, EventArgs e)
        {
            string tmp = MyFunc.SelectedTextOveride(tbCopyWindow.SelectedText.Trim());

            tbAddress.Text = MyFunc.CleanAddress(tmp);
        }

        private void UpdateOutput()
        {
            //Form f = this;
            string sep = tbPhone.Text == "" ? tbPhone.Text : tbPhone.Text + ":";
            tbOutput.Text = tbPolicy.Text + ">" + sep + tbAddress.Text;

            tbEmailTo.Text = lbSPT.Text;
            tbEmailSubject.Text = GetSubjectAll();

            //======================================= handle word letters
            lbLetterClientName.Text = tbName.Text;
            lbLetterGroup.Text = MyFunc.SplitFunc(tbPolicy.Text, '-', 0);
            lbLetterCert.Text = MyFunc.SplitFunc(tbPolicy.Text, '-', 1).Trim();

            //If Address field contains 2 commas, then process
            if (MyFunc.CountChar(tbAddress.Text, ',') == 2)
            {

                lbLetterAddr1.Text = MyFunc.SplitIt(tbAddress.Text, ',')[0].Trim();
                lbLetterAddr2.Text = MyFunc.SplitIt(tbAddress.Text, ',')[1].Trim() + "  " +
                    MyFunc.SplitIt(tbAddress.Text, ',')[2].Trim();
            }

            lbTaxSubject.Text = tbPolicy.Text + " " + tbName.Text;
            tbTaxAddress.Text = tbAddress.Text;


        }
        private void UpdateTitle()
        {
            if (FormBeingCleared == true) { return; }

            string request = "";
            string name;
            if (tbName.Text.Contains(" "))
            {
                string[] words = tbName.Text.Split(' ');
                name = words[0] + " " + words[1];
            }
            else
            {
                name = tbName.Text;
            }

            request = cbRequestType.Text == "Request..." ? "" : " - " + cbRequestType.Text;

            try
            {
                ActiveForm.Text = name + request;
            }
            catch
            {

            }
        }
        private void tbPhone_TextChanged(object sender, EventArgs e)
        {
            UpdateOutput();

        }

        private void tbPolicy_TextChanged(object sender, EventArgs e)
        {
            UpdateTitle(); //Updates the form title

            string[] words = tbPolicy.Text.Split('-');
            string[] wordsS = tbPolicy.Text.Split('/');
            UpdateOutput();
            UpdateSPT();

            if (words.Length > 1)
            {
                tbPolicyD.Text = words[0] + 'C' + words[1];
            }
            else
            {
                if (wordsS.Length == 3)
                {
                    tbPolicy.Text = wordsS[0] + "-" + wordsS[2];
                    tbPolicyD.Text = wordsS[0] + 'C' + wordsS[2];
                }

            }
        }

        private void tbAddress_TextChanged(object sender, EventArgs e)
        {
            UpdateOutput();
            UpdateCharCount();
        }



        private void tmTimer_Tick(object sender, EventArgs e)
        {
            lbTime.Text = DateTime.Now.ToString("ddd") + " " + DateTime.Now.ToLongDateString() + "  " + DateTime.Now.ToShortTimeString();
            tmTimer.Start();
        }

        private void btSaveRecord_Click(object sender, EventArgs e)
        {
            StoreRecord frm = new StoreRecord();

            EmptyRecordToTextFile();

        }




        private void SaveRecord()
        {

            //string filePath = @"C:\Users\" + Environment.UserName + @"\Desktop\ScratchPad Log.txt";
            // string filePath = @"C:\Users\ll67305\source\repos\GroupTool\InternalLog.txt";
            string filePath = MyFunc.FilePath() + "Scratch Pad Log.txt";

            string line = File.ReadAllText(filePath);

            string txt = string.Empty;

            GetAllDataPoints();


            File.WriteAllText(filePath, txtToFile + MyFunc.nl() + line);
        }

        private void GetAllDataPoints()
        {

            txtToFile = "";
            txtToFile = DateTime.Now.ToString("MMM d.yy") + " " + DateTime.Now.ToLongTimeString() + "--->";
            txtToFile = MyFunc.StringOut(txtToFile, "DH:", tbDocHandle.Text, "");
            txtToFile = MyFunc.StringOut(txtToFile, "Request Type", cbRequestType.Text, "Request...");
            txtToFile = MyFunc.StringOut(txtToFile, "Subject", tbPolicy.Text + " " + tbName.Text, "");
            txtToFile = MyFunc.StringOut(txtToFile, "Phone", tbPhone.Text, "");
            txtToFile = MyFunc.StringOut(txtToFile, "Addr", tbAddress.Text, "");

            txtToFile = MyFunc.StringOut(txtToFile, "", "Checks: ", "");
            txtToFile = MyFunc.StringOut(txtToFile, checkBox1.Checked == true ? "[x]" : "[ ]", tbCheck2.Text, "");
            txtToFile = MyFunc.StringOut(txtToFile, checkBox2.Checked == true ? "[x]" : "[ ]", tbCheck3.Text, "");
            txtToFile = MyFunc.StringOut(txtToFile, checkBox3.Checked == true ? "[x]" : "[ ]", tbCheck4.Text, "");
            txtToFile = MyFunc.StringOut(txtToFile, checkBox4.Checked == true ? "[x]" : "[ ]", tbCheck5.Text, "");
            txtToFile = MyFunc.StringOut(txtToFile, checkBox5.Checked == true ? "[x]" : "[ ]", tbCheck6.Text, "");
            txtToFile = MyFunc.StringOut(txtToFile, checkBox6.Checked == true ? "[x]" : "[ ]", tbCheck7.Text, "");
            txtToFile = MyFunc.StringOut(txtToFile, checkBox7.Checked == true ? "[x]" : "[ ]", tbCheck7.Text, "");


            txtToFile = MyFunc.StringOut(txtToFile, "Sticky Note", tbCheckListNotes.Text, "");
            txtToFile = MyFunc.StringOut(txtToFile, "Accnt Notes", tbAccountNotes.Text, "");
            txtToFile = MyFunc.StringOut(txtToFile, "A1", cbMars1.Text + " " + tbAccnt1.Text, " ");
            txtToFile = MyFunc.StringOut(txtToFile, "A2", cbMars2.Text + " " + tbAccnt2.Text, "SUB ");
            txtToFile = MyFunc.StringOut(txtToFile, "A3", cbMars3.Text + " " + tbAccnt3.Text, "SUB ");
            txtToFile = MyFunc.StringOut(txtToFile, "A9", tbAccnt9.Text, "0.00");

            txtToFile = MyFunc.StringOut(txtToFile, "Accnt Text Boxes", tbFormCode.Text + "." + tb10.Text + "." + tb2.Text, "..");
            txtToFile = MyFunc.StringOut(txtToFile, "Notepad", tbNotes.Text, "");

            txtToFile = txtToFile.Replace("\r\n", ";");
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {

            if (FormBeingCleared == false)
            {
                UpdateOutput();
                UpdateTitle();
            }



        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(GetSubject());
        }

        private string GetSubject()
        {
            return tbPolicy.Text + " " + tbName.Text;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process p = System.Diagnostics.Process.Start("calc.exe");
        }

        private void btAllCaps_Click(object sender, EventArgs e)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            if (tbName.Text.ToUpper() == tbName.Text)
            {
                tbName.Text = textInfo.ToTitleCase(tbName.Text.ToLower());
                tbAddress.Text = textInfo.ToTitleCase(tbAddress.Text.ToLower());
            }
            else
            {
                tbName.Text = tbName.Text.ToUpper();
                tbAddress.Text = tbAddress.Text.ToUpper();
            }

        }

        private void btPasteName_Click(object sender, EventArgs e)
        {
            string tmp = MyFunc.SelectedTextOveride(tbCopyWindow.SelectedText.Trim());
            tbName.Text = tmp.Replace(",", "");
        }

        private void tbInfo_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbRequestType_SelectedIndexChanged(object sender, EventArgs e)
        {
            RequestItems("FillItems", cbRequestType.Text);
            UpdateTitle();
            UpdateOutput();
        }

        private void cbRequestType_TextChanged(object sender, EventArgs e)
        {
            UpdateTitle();
        }

        private void tbAccnt1_TextChanged(object sender, EventArgs e)
        {
            UpdateAccounting();
        }

        private void tbAccnt2_TextChanged(object sender, EventArgs e)
        {
            UpdateAccounting();
        }




        private void UpdateAccounting()
        {
            string temp, result;

            // Mars1 and Mars2 detect subtract
            if (cbMars2.Text.Substring(0, 2) == "SU" || cbMars2.Text.Substring(0, 1) == "2")
            {
                //Hold the result in temp if subtract
                temp = MyFunc.TextMath("+", tbAccnt1.Text, "-" + tbAccnt2.Text);

            }
            else
            {
                //Hold result if add
                temp = MyFunc.TextMath("+", tbAccnt1.Text, tbAccnt2.Text);
            }

            result = temp;


            //Mars 3 detect. Skip if null
            if (cbMars3.Text != "")
            {

                if (cbMars3.Text.Substring(0, 2) == "SU" || cbMars3.Text.Substring(0, 2) == "24")
                {

                    result = MyFunc.TextMath("-", temp, tbAccnt3.Text);
                }
                else if (cbMars3.Text.Substring(0, 2) != "")
                {
                    result = MyFunc.TextMath("-", temp, tbAccnt3.Text);
                }
            }


            tbAccnt9.Text = result;
            tbAccnt9.Text = UpdateFormat(tbAccnt9.Text);

        }

        private void tbAccnt3_TextChanged(object sender, EventArgs e)
        {
            UpdateAccounting();
        }

        private void ScratchPad_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'groupTooldbDataSet.Clients' table. You can move, or remove it, as needed.
            cbMars2.Text = "SUB";
            cbMars3.Text = "SUB";
            RequestItems("Load", "");
            EmailItems("Load", "");
            btOutputToggle.PerformClick();

            tsLetters.SelectTab("Home");

            tbEmailSignature.Text = "Tim Betteridge" + "\r\n" +
                                    "Client Service Co-ordinator" + "\r\n" +
                                    "Wealth Operations, Group Annuities T424 (4B)";

            //Clear out letters header
            lbLetterClientName.Text = "";
            lbLetterGroup.Text = "";
            lbLetterCert.Text = "";
            lbLetterAddr1.Text = "";
            lbLetterAddr2.Text = "";
        }


        private void RequestItems(string inst, string heading)
        {
            //Load; FillItems; Update;

            //Set output
            string filePath = MyFunc.FilePath() + "CheckList.txt";

            //Read Checklist Data
            string[] lines;
            if (File.Exists(filePath))
            {
                lines = File.ReadAllLines(filePath); //{ "", "" };
            }
            else
            {
                return;
            }

            //New String out
            string NewCheckString = "";

            //HEADINGS - Pull from text file, Fill drop down and check boxes
            if (inst == "Load")
            {
                //Clear Combo List
                cbRequestType.Items.Clear();

                foreach (string line in lines)
                {
                    //Load Headings
                    string[] splitline = MyFunc.SplitIt(line, '~');


                    //Add each Heading to the request combo list
                    cbRequestType.Items.Add(splitline[0]);

                }

            }
            else if (inst == "FillItems")
            {

                foreach (string line in lines)
                {
                    //Load Headings
                    string[] splitline = MyFunc.SplitIt(line, '~');

                    //Does heading match
                    if (splitline[0] == heading)
                    {
                        string[] items = MyFunc.SplitIt(splitline[1], '@');

                        tbCheck1.Text = items[0];
                        tbCheck2.Text = items[1];
                        tbCheck3.Text = items[2];
                        tbCheck4.Text = items[3];
                        tbCheck5.Text = items[4];
                        tbCheck6.Text = items[5];
                        tbCheck7.Text = items[6];
                        tbCheck8.Text = items[7];
                        tbCheck9.Text = items[8];
                        tbCheck10.Text = items[9];

                        //Fill the checklist edit
                        string txt = "";
                        foreach (string item in items)
                        {
                            txt = txt + item + "\r\n";
                        }
                        tbCheckEdit.Text = txt;
                    }
                }
            }
            else if (inst == "Append")
            {
                //Read the checklist file ; txt - All the current file
                string txt = File.ReadAllText(filePath);


                //str - Text from tbCheckEdit
                //crCount - carriage return count
                string str = tbCheckEdit.Text;

                //Prepare a bunch of commas
                string atString = "@@@@@@@@";

                //Prepare the appended checklist lastline
                string lastline = str.Replace("\r\n", "@") + atString;

                //Tack on the last line to the existing text file txt
                txt = txt + "\r\n" + heading + "~" + lastline;

                //Replace any double cr in txt
                txt = txt.Replace("\r\n\r\n", "\r\n");

                File.WriteAllText(filePath, txt);
            }
            else if (inst == "Remove")
            {
                //txtOut - line by line append of original checklist
                string txtOut = "";

                //loop through each line until removal headind found
                foreach (string line in lines)
                {

                    //Load Headings
                    string[] splitline = MyFunc.SplitIt(line, '~');

                    //Append line except match
                    if (splitline[0] != heading)
                    {
                        txtOut = txtOut + line + "\r\n";
                    }
                }
                File.WriteAllText(filePath, txtOut);
            }
            else if (inst == "Update")
            {


                //Build new string until target is found
                foreach (string line in lines)
                {
                    //Load Headings
                    string[] splitline = MyFunc.SplitIt(line, '~');

                    //Does heading match
                    if (splitline[0] == heading)
                    {
                        //Replace string with single char to use splitit
                        string temp = tbCheckEdit.Text.Replace("\r\n", "@");
                        string[] lineElems = MyFunc.SplitIt(temp, '@');


                        //Fill the request line with the new checks
                        string txt = "";
                        foreach (string elem in lineElems)
                        {
                            if (txt == "")
                            {
                                txt = elem;
                            }
                            else
                            {
                                txt = txt + "@" + elem;
                            }

                        }

                        //Handle first element
                        if (NewCheckString == "")
                        {
                            NewCheckString = heading + "~" + txt;
                        }
                        else
                        {
                            NewCheckString = NewCheckString + "\r\n" + heading + "~" + txt;
                        }
                    }
                    else
                    {
                        if (NewCheckString == "")
                        {
                            //Savestring
                            NewCheckString = line;
                        }
                        else
                        {

                            NewCheckString = NewCheckString + "\r\n" + line;
                        }

                    }

                }
                File.WriteAllText(filePath, NewCheckString);
            }


        }

        private void EmailItems(string inst, string heading)
        {
            string NewCheckString = "";

            //Set output
            string filePath = MyFunc.FilePath() + "EmailItems.txt";

            //Read Email list 
            string[] lines;
            if (File.Exists(filePath))
            {
                lines = File.ReadAllLines(filePath); //{ "", "" };
            }
            else
            {
                return;
            }


            //HEADINGS - Pull from text file, Fill drop down and check boxes
            if (inst == "Load")
            {
                //Clear Current List
                cbEmailItems.Items.Clear();

                foreach (string line in lines)
                {
                    //Load Headings
                    string[] splitline = MyFunc.SplitIt(line, '~');


                    //Add each Heading to the request combo list
                    cbEmailItems.Items.Add(splitline[0]);

                }

            }
            else if (inst == "FillItem")
            {

                foreach (string line in lines)
                {
                    //Load Headings
                    string[] splitline = MyFunc.SplitIt(line, '~');

                    //Does heading match
                    if (splitline[0] == heading)
                    {
                        tbEmailBody.Text = MyFunc.ReplaceWord(splitline[1], "@", "\r\n");


                    }
                }
            }
            else if (inst == "Update")
            {
                //Build new string until target is found
                foreach (string line in lines)
                {
                    //Load Headings
                    string[] splitline = MyFunc.SplitIt(line, '~');

                    //Does heading match
                    if (splitline[0] == heading)
                    {
                        //Replace string with single char to use splitit
                        string temp = tbEmailBody.Text.Replace("\r\n", "@");
                        string[] lineElems = MyFunc.SplitIt(temp, '@');


                        //Fill the request line with the new checks
                        string txt = "";
                        foreach (string elem in lineElems)
                        {
                            if (txt == "")
                            {
                                txt = elem;
                            }
                            else
                            {
                                txt = txt + "@" + elem;
                            }

                        }

                        //Handle first element
                        if (NewCheckString == "")
                        {
                            NewCheckString = heading + "~" + txt;
                        }
                        else
                        {
                            NewCheckString = NewCheckString + "\r\n" + heading + "~" + txt;
                        }
                    }
                    else
                    {
                        if (NewCheckString == "")
                        {
                            //Savestring
                            NewCheckString = line;
                        }
                        else
                        {

                            NewCheckString = NewCheckString + "\r\n" + line;
                        }

                    }

                }
                File.WriteAllText(filePath, NewCheckString);

            }
            else if (inst == "Append")
            {
                //Read the checklist file ; txt - All the current file
                string txt = File.ReadAllText(filePath);


                //str - Text from tbCheckEdit
                //crCount - carriage return count
                string str = tbEmailBody.Text;

                //Prepare a bunch of commas
                string atString = "@@@@@@@@";

                //Prepare the appended checklist lastline
                string lastline = str.Replace("\r\n", "@") + atString;

                //Tack on the last line to the existing text file txt
                txt = txt + "\r\n" + heading + "~" + lastline;

                //Replace any double cr in txt
                txt = txt.Replace("\r\n\r\n", "\r\n");

                File.WriteAllText(filePath, txt);


            }
        }
        private void cbMars2_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateAccounting();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            ClearCalc();
        }

        private void ClearCalc()
        {
            tbAccnt1.Text = string.Empty;
            tbAccnt2.Text = string.Empty;
            tbAccnt3.Text = string.Empty;
            tbAccnt9.Text = string.Empty;
            cbMars1.Text = string.Empty;
            cbMars2.Text = "SUB";
            cbMars3.Text = "SUB";

        }

        private void cbMars3_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateAccounting();
        }

        private void cbMars1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateAccounting();

        }



        // Formatting
        private void tbAccnt1_Leave(object sender, EventArgs e)
        {
            tbAccnt1.Text = UpdateFormat(tbAccnt1.Text);
            tbAccnt9.Text = UpdateFormat(tbAccnt9.Text);
        }

        private void tbAccnt2_Leave(object sender, EventArgs e)
        {
            tbAccnt2.Text = UpdateFormat(tbAccnt2.Text);
            tbAccnt9.Text = UpdateFormat(tbAccnt9.Text);
        }

        private void tbAccnt3_Leave(object sender, EventArgs e)
        {
            tbAccnt3.Text = UpdateFormat(tbAccnt3.Text);
            tbAccnt9.Text = UpdateFormat(tbAccnt9.Text);
        }

        private string UpdateFormat(string val)
        {
            if (val == "") { val = "0"; }
            return Convert.ToDouble(val).ToString("0.00", CultureInfo.InvariantCulture);
        }



        private void tbYOB_TextChanged(object sender, EventArgs e)
        {

            String year = DateTime.Now.Year.ToString();
            if (tbYOB.Text == String.Empty)
            {
                tbYOB.Text = "";
                tbAgeOut.Text = "";
            }
            else
            {
                double age = Convert.ToDouble(year) - Convert.ToDouble(tbYOB.Text);
                tbAgeOut.Text = age.ToString();
            }
        }

        private void tbAgeOut_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbSwapName_Click(object sender, EventArgs e)
        {
            string[] words = MyFunc.SplitIt(tbName.Text, ' ');
            tbName.Text = (words[1] + " " + words[0]).Trim();
        }



        private void tmOpenDuration_Tick(object sender, EventArgs e)
        {
            long elapsedTicks = DateTime.Now.Ticks - FormOpenedtimeStamp.Ticks;
            TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);



            //lbOpenDuration.Text = elapsedSpan.TotalMinutes.ToString();
            // lbOpenDuration.Text = Convert.ToDateTime(elapsedSpan.TotalMinutes).ToShortTimeString();
            tmOpenDuration.Start();
        }

        private void lbOpenDuration_Click(object sender, EventArgs e)
        {

        }

        private void btUpdateChecks_Click(object sender, EventArgs e)
        {
            RequestItems("Update", cbRequestType.Text);
            RequestItems("FillItems", cbRequestType.Text);
        }

        private void btNewRequest_Click(object sender, EventArgs e)
        {
            tbAddRequestName.Show();
            lbAddRequestName.Show();
            tbCheckEdit.Text = "";
            btAddRequest.Show();
            btRequestReset.Show();
        }

        private void tbAddRequest_Click(object sender, EventArgs e)
        {

            RequestItems("Append", tbAddRequestName.Text);
            RequestItems("FillItems", cbRequestType.Text);
            CheckEditReset();

            //Re-load the menu of items. Clear cbRequesttype first
            cbRequestType.Items.Clear();
            RequestItems("Load", "");

            //Leave this last
            tbAddRequestName.Text = "";
        }

        private void btRequestReset_Click(object sender, EventArgs e)
        {
            CheckEditReset();
            tbCheckEdit.Text = "";
        }

        private void btRemoveCheck_Click(object sender, EventArgs e)
        {
            CheckEditReset();
            RequestItems("Remove", cbRequestType.Text);

            //Re-load the menu of items. Clear cbRequesttype first
            cbRequestType.Items.Clear();
            cbRequestType.Text = "";
            tbCheckEdit.Text = "";
            RequestItems("Load", "");

        }
        private void CheckEditReset()
        {
            tbAddRequestName.Hide();
            tbAddRequestName.Text = "";
            lbAddRequestName.Hide();
            btAddRequest.Hide();
            btRequestReset.Hide();
            tbCheckEdit.Text = "";
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void lkSchedules_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.google.com.au/search?q=" + tbName.Text + " obit");
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", @"\\Llfsp2\irisadmin\Brenda\At Home\Schedules");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", @"\\Llfsp2\irisadmin\LL Scripting\LL Manual Payments");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", @"N:\Letters\Certificate of existence\London Life\COE 2020\IMS");
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", @"\\Llfsp2\irisadmin\Brenda\Admin");
        }
        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", @"C:\Users\ll67305\OneDrive - Enterprise 365\++Client Files OD\Templates\PDF Templates");
        }

        private void btOpenTodoList_Click(object sender, EventArgs e)
        {
            Form1 todo = new Form1();
            todo.ShowDialog();
        }

        private void ckMinimizeBehaviour_CheckedChanged(object sender, EventArgs e)
        {
            if (ckMinimizeBehaviour.Checked) //Minimize to Taskbar
            {
                this.ShowInTaskbar = true;
            }
            else
            {
                this.ShowInTaskbar = false;
            }
        }

        private void btOutputToggle_Click(object sender, EventArgs e)
        {


            if (tbOutput.Visible == true)
            {
                tbOutput.Hide(); //Text box
                lbOutput.Hide(); // Label
                                 //btCopyOutput.Hide(); //Button
                btOutputToggle.Text = "...";

                int offset = 45; //Offset the controls collapse/expand


                btSaveRecord.Top = btSaveRecord.Top - offset;
                btClear.Top = btClear.Top - offset;
                btClose.Top = btClose.Top - offset;
                lbTime.Top = lbTime.Top - offset;
                btLoadRecord.Top = btLoadRecord.Top - offset;

                //ckMinimizeBehaviour.Top = ckMinimizeBehaviour.Top - offset;
                btOutputToggle.Top = btOutputToggle.Top - offset;
                this.Height = this.Height - offset;


            }
            else
            {
                tbOutput.Show(); //Text box
                lbOutput.Show(); // Label
                                 //btCopyOutput.Show(); //Button
                btOutputToggle.Text = "^";

                int offset = 45; //Offset the controls collapse/expand


                btSaveRecord.Top = btSaveRecord.Top + offset;
                btClear.Top = btClear.Top + offset;
                btClose.Top = btClose.Top + offset;
                lbTime.Top = lbTime.Top + offset;
                btLoadRecord.Top = btLoadRecord.Top + offset;

                //ckMinimizeBehaviour.Top = ckMinimizeBehaviour.Top + offset;
                btOutputToggle.Top = btOutputToggle.Top + offset;
                this.Height = this.Height + offset;
            }


        }



        private void lbTime_Click(object sender, EventArgs e)
        {

        }


        private void label1_Click(object sender, EventArgs e)
        {

        }



        private void btDocHandlePaste_Click(object sender, EventArgs e)
        {

            tbName.Text = Clipboard.GetText();
        }

        private void btDocHandlePaste_Click_1(object sender, EventArgs e)
        {
            tbDocHandle.Text = Clipboard.GetText();
        }

        private void btDocHandleCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tbDocHandle.Text);
        }

        private void tbPhone_Leave(object sender, EventArgs e)
        {
            tbPhone.Text = MyFunc.FormatPhone(tbPhone.Text);
        }

        private void btCopyAccnt1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tbAccnt1.Text);
        }

        private void btCopyAccnt2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tbAccnt2.Text);
        }

        private void btCopyAccnt3_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tbAccnt3.Text);
        }

        private void btCopyAccnt9_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tbAccnt9.Text);
        }

        private void tbCalc_In_TextChanged(object sender, EventArgs e)
        {
            UpdateCalculator();
        }

        private void tbAccnt9_TextChanged(object sender, EventArgs e)
        {

        }

        private void UpdateCalculator()
        {
            if (tbCalc_Add.Text != "")
            {
                tbCalc_Out.Text = MyFunc.TextMath("+", tbCalc_In.Text, tbCalc_Add.Text);
            }
            else if (tbCalc_Sub.Text != "")
            {
                tbCalc_Out.Text = MyFunc.TextMath("-", tbCalc_In.Text, tbCalc_Sub.Text);
            }
            else if (tbCalc_Mult.Text != "")
            {
                tbCalc_Out.Text = MyFunc.TextMath("x", tbCalc_In.Text, tbCalc_Mult.Text);
            }
            else if (tbCalc_Div.Text != "")
            {
                tbCalc_Out.Text = MyFunc.TextMath("/", tbCalc_In.Text, tbCalc_Div.Text);
            }


            tbCalc_Out.Text = UpdateFormat(tbCalc_Out.Text);
            //tbCalc_In.Text = UpdateFormat(tbCalc_In.Text);
        }

        private void tbCalc_Add_TextChanged(object sender, EventArgs e)
        {
            UpdateCalculator();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            tbCalc_Out.Text = "";
            tbCalc_Add.Text = "";
            tbCalc_Mult.Text = "";
            tbCalc_Sub.Text = "";
            tbCalc_Div.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tbCalc_In.Text = "";
        }

        private void tbCalc_Sub_TextChanged(object sender, EventArgs e)
        {
            UpdateCalculator();
        }

        private void tbCalc_Mult_TextChanged(object sender, EventArgs e)
        {
            UpdateCalculator();
        }

        private void tbCalc_Div_TextChanged(object sender, EventArgs e)
        {
            UpdateCalculator();
        }

        private void btOpenTelLog_Click(object sender, EventArgs e)
        {

            if (tbSpokeWith.Text == "")
            {
                MessageBox.Show("Please fill in 'Spoke With' field!");
                return;
            }

            Excel excel = new Excel(M_Path_Excel + "TelephoneLog.xlsm", 1);
            //MessageBox.Show(excel.ReadCell(3, 1));

            excel.WriteToRange(tbPhone.Text, "Phone");
            excel.WriteToRange(tbName.Text, "ClientName");
            excel.WriteToRange(tbSpokeWith.Text, "SpokeWith");
            excel.WriteToRange(MyFunc.SplitIt(tbPolicy.Text, '-')[0], "Group");
            excel.WriteToRange(MyFunc.SplitIt(tbPolicy.Text, '-')[1], "Cert");
            excel.WriteToRange(tbNotes.Text, "Notes");
            excel.SelectRange("G1");
        }

        private void btCopyToggle_Click(object sender, EventArgs e)
        {
            CopyWindowToggle();

        }

        public void CopyWindowToggle()
        {
            if (this.Width == 784)
            {
                this.Width = 382;
                btCopyToggle.Text = "Open -->";
            }
            else
            {
                this.Width = 784;
                btCopyToggle.Text = "<-- Close";
            }

        }


        private void btTest_Click(object sender, EventArgs e)
        {
            string a = tbCopyWindow.SelectedText.Replace("\r\n", " ");
            Clipboard.SetText(a);
        }

        private void btClearOut_Click(object sender, EventArgs e)
        {

            tbCopyWindow.Text = "";
        }

        private void btPasteAndSend_Click(object sender, EventArgs e)
        {
            tbCopyWindow.Text = Clipboard.GetText();

            if (tbName.Text == "" && tbPhone.Text == "" && tbPolicy.Text == "")
            {
                tbName.Text = MyFunc.PullData("Owner:", tbCopyWindow.Text);
                tbPhone.Text = MyFunc.CleanStringOfNonDigits_V5(MyFunc.PullData("home phone:", tbCopyWindow.Text));
                tbPolicy.Text = MyFunc.PullData("policy number:", tbCopyWindow.Text);

                btAllCaps.PerformClick();
            }

            UpdateSPT();
        }

        private void btCopySubjectAll_Click(object sender, EventArgs e)
        {

            Clipboard.SetText(GetSubjectAll());
        }

        private string GetSubjectAll()
        {
            string sep = "";
            string request = cbRequestType.Text;

            if (cbEmailItems.Text == "")
            {
                request = "";
            }

            else if (request == "Request...")
            {
                request = cbEmailItems.Text;
                sep = ", ";
            }

            return tbPolicy.Text + " " + tbName.Text + sep + request;
        }
        private void btTransferPaste_Click(object sender, EventArgs e)
        {
            tbCopyWindow.Text = Clipboard.GetText();
        }

        private void btTPasteName_Click(object sender, EventArgs e)
        {
            string tmp;
            tmp = MyFunc.SelectedTextOveride(tbCopyWindow.SelectedText.Trim());
            tmp = tmp.Replace(" ", ".");


            string[] words = MyFunc.SplitIt(tmp, '.');

            if (words.Length == 2)
            {
                tbTName.Text = (words[1] + "." + words[0]).Trim() + ".";
            }
            else
            {
                tbTName.Text = (words[2] + "." + words[0] + "." + words[1]).Trim();
            }
            UpdatetransferOut();


        }

        private void UpdateCharCount()
        {
            string[] words;
            string tmp = tbAddress.Text;
            if (tmp.Contains(","))
            {
                words = MyFunc.SplitIt(tmp, ',');
                if (words.Length == 3)
                {
                    lbCharCount.Text = words[0].Length + "," + words[1].Length + "," + words[2].Length;
                }
                else if (words.Length == 2)
                {
                    lbCharCount.Text = words[0].Length + "," + words[1].Length;
                }

            }
            else
            {
                lbCharCount.Text = tmp.Length.ToString();
            }



        }

        private void btTPasteSIN_Click(object sender, EventArgs e)
        {
            string tmp = MyFunc.SelectedTextOveride(tbCopyWindow.SelectedText.Trim());
            tmp = tmp.Replace("-", ".");
            tmp = tmp.Replace(" ", ".");

            if (tmp.Length == 9) //no dash or decimal
            {
                tmp = tmp.Substring(0, 3) + "." + tmp.Substring(3, 3) + "." + tmp.Substring(6, 3);
            }
            tbTSIN.Text = tmp;
            UpdatetransferOut();
        }

        private void btTPasteDOB_Click(object sender, EventArgs e)
        {
            string tmp = MyFunc.SelectedTextOveride(tbCopyWindow.SelectedText.Trim());
            tbTDOB.Text = MyFunc.DateConvert(tmp);
            UpdatetransferOut();
        }

        private void btTPasteID_Click(object sender, EventArgs e)
        {
            tbTID.Text = MyFunc.SelectedTextOveride(tbCopyWindow.SelectedText.Trim());
            UpdatetransferOut();
        }

        private void btTPasteDOD_Click(object sender, EventArgs e)
        {
            string tmp = MyFunc.SelectedTextOveride(tbCopyWindow.SelectedText.Trim());
            tbTDOD.Text = MyFunc.DateConvert(tmp);
            UpdatetransferOut();
        }

        private void rdFemale_CheckedChanged(object sender, EventArgs e)
        {
            tbTName.Text = tbTName.Text + ".F";
            UpdatetransferOut();
        }

        private void rdMale_CheckedChanged(object sender, EventArgs e)
        {
            tbTName.Text = tbTName.Text + ".M";
            UpdatetransferOut();
        }

        private void tbNameOut_TextChanged(object sender, EventArgs e)
        {

        }
        private void UpdatetransferOut()
        {
            tbNameOut.Text = tbTName.Text + "." + tbTSIN.Text + "." +
                                tbTDOB.Text;

            tbAnnOut.Text = tbTID.Text + "." + tbTDOD.Text;
        }

        private void tbTName_TextChanged(object sender, EventArgs e)
        {
            UpdatetransferOut();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tbAnnOut.Text);
        }

        private void btCopyNameOut_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tbNameOut.Text);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            tbSpouseID.Text = Clipboard.GetText();
        }

        private void tbTSIN_TextChanged(object sender, EventArgs e)
        {
            UpdatetransferOut();
        }

        private void tbTDOB_TextChanged(object sender, EventArgs e)
        {
            UpdatetransferOut();
        }

        private void tbTID_TextChanged(object sender, EventArgs e)
        {
            UpdatetransferOut();
        }

        private void tbTDOD_TextChanged(object sender, EventArgs e)
        {
            UpdatetransferOut();
        }

        private void btClearSpouse_Click(object sender, EventArgs e)
        {
            tbTName.Text = "";
            tbTSIN.Text = "";
            tbTDOB.Text = "";
        }

        private void btClearAnn_Click(object sender, EventArgs e)
        {
            tbTID.Text = "";
            tbTDOD.Text = "";
        }

        private void btCopyData_Click(object sender, EventArgs e)
        {
            GetAllDataPoints();
            Clipboard.SetText(txtToFile);


        }
        private void lbSPT_Click(object sender, EventArgs e)
        {
            UpdateSPT();
        }

        private void UpdateSPT()
        {
            if (tbPolicy.Text == "")
            {
                lbSPT.Text = "None";
            }
            else
            {
                lbSPT.Text = SearchSPT(0);
                lbSalutation.Text = "Hello " + SearchSPT(1) + ",";
                tbEmailTo.Text = SearchSPT(3) + " (" + SearchSPT(2) + ")";

            }

        }
        private string SearchSPT(int n)
        {
            //This opens the text file and searches the SPT based on the 
            //Policy number.

            string filepath = M_Path_AgentTool + "SPT List.txt";
            //string filepath = @"C:\Users\ll67305\Desktop\SPT List.txt";
            string ou = "";
            string cnt = "";

            //Array to hold the spt names
            string[] spt = { "" };

            //string to hold running spt Init
            string sptInit = "";
            string sptInitStack = "";

            //Group match
            int grpCount = 0;

            string targetGrp = "";
            string targetCrt = "";

            //Break down the Policy
            if (tbPolicy.Text.Contains("-"))
            {
                targetGrp = MyFunc.SplitIt(tbPolicy.Text, '-')[0];
                targetCrt = MyFunc.SplitIt(tbPolicy.Text, '-')[1];

            }
            else
            {
                targetGrp = tbPolicy.Text;
                targetCrt = "";
            }

            //Trim the above
            targetGrp = targetGrp.Trim();
            targetCrt = targetCrt.Trim();


            int i = 0;
            string lineGrp = "";
            string lineCrt = "";


            List<string> mylist = new List<string>();

            //Start a line by line search
            foreach (string line in File.ReadLines(filepath))
            {
                if (line.Substring(0, 1) == "@")
                {
                    sptInitStack = line.Substring(1, line.Length - 1); //.Substring(1, 30); //zzz'

                }

                if (line.Contains("-"))
                {
                    lineGrp = MyFunc.SplitIt(line, '-')[0];
                    lineGrp = lineGrp.Trim();

                    lineCrt = MyFunc.SplitIt(line, '-')[1];

                    //Find and save all matches for group
                    if (targetGrp == lineGrp)
                    {

                        mylist.Add(line + "," + sptInitStack);
                    }
                }
            }

            //Isolate search in mylist
            if (mylist.Count > 1)
            {
                //Multiple occurance. More than one group
                if (targetCrt == "")
                {
                    cnt = mylist.Count.ToString();
                }
                else
                {
                    for (int j = 0; j < mylist.Count; j++)
                    {
                        //If cert has 5 digits
                        if (targetCrt.Length > 4)
                        {
                            //check 5 digits
                            if (targetCrt.Substring(0, 5) == MyFunc.SplitTwice(mylist.ElementAt(j), "-", ","))
                            {
                                ou = MyFunc.SplitIt(mylist.ElementAt(j), ',')[1];
                                return MyFunc.SplitIt(ou, '.')[n];
                            }
                            else
                            {
                                ou = "NF";
                            }
                        }
                        //Cert less than 5 digits
                        else
                        {
                            ou = "NF";
                        }
                    }


                }



            }
            else
            {
                //Unique single occurrance

                if (mylist.Count == 0)
                {
                    ou = "NF";
                }
                else
                {

                    ou = MyFunc.SplitIt(mylist.ElementAt(0), ',')[1];
                }


            }

            if (ou.Length < 3)
            {
                return ou;
            }
            else
            {
                return MyFunc.SplitIt(ou, '.')[n];
            }

        }


        private void btEmailOpen_Click(object sender, EventArgs e)
        {
            OutlookApp _App = new OutlookApp();
            Outlook.MailItem mail = (Outlook.MailItem)_App.CreateItem(Outlook.OlItemType.olMailItem);
            mail.To = MyFunc.SplitIt(tbEmailTo.Text, '(')[0];
            mail.Subject = tbEmailSubject.Text; // GetSubjectAll();
            mail.Body = lbSalutation.Text + MyFunc.nl() + MyFunc.nl() + tbEmailBody.Text;
            mail.Body = mail.Body + MyFunc.nl() + MyFunc.nl() + tbEmailSignature.Text;

            mail.Display();
        }


        private void btCloseSide_Click(object sender, EventArgs e)
        {
            CopyWindowToggle();
        }

        private void cbEmailItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            EmailItems("FillItem", cbEmailItems.Text);
            tbEmailSubject.Text = GetSubjectAll();
        }

        private void btUpdateEmailItem_Click(object sender, EventArgs e)
        {
            EmailItems("Update", cbEmailItems.Text);

        }



        private void tbEmailBody_TextChanged(object sender, EventArgs e)
        {

        }

        private void btEmailNewItem_Click(object sender, EventArgs e)
        {
            EmailItems("Append", cbEmailItems.Text);
            EmailItems("Load", cbEmailItems.Text);
        }

        private void btClearChecks_Click(object sender, EventArgs e)
        {
            clearCheckBoxSection();
        }

        private void btOpenTemplate_Click(object sender, EventArgs e)
        {
            Outlook.Application application = new Outlook.Application();
            Outlook.MailItem mail = application.CreateItemFromTemplate(
                @"M:\Desktop\Outlook\email templates\FileTransfer.oft"
                 ) as Outlook.MailItem;
            mail.Subject = "HA .. IT IS WORKING !";
            mail.To = "tim.betteridge@canadalife.com";
            mail.Display();

            mail.Body = MyFunc.ReplaceWord(mail.Body, "dirxxx", "We did it!");
            int a = 5;
            //mail.SaveAs(
            // AppDomain.CurrentDomain.BaseDirectory + "\\asdf.oft");

        }


        private void btOpenLetter_Click(object sender, EventArgs e)
        {
            WordApp app = new WordApp();
            Document doc = new Document();



            string LetterPath = "";
            string selectedLetter = "";
            string docType = "";

            if (cbLetterSelected.Text == "Tax Increase Letter")
            {
                selectedLetter = "Tax";
                LetterPath = N_Path_Letters + "Additional Tax Letter Custom.docm";
                docType = "word";
            }
            else if (cbLetterSelected.Text == "CoAnn Letter")
            {
                selectedLetter = "CoAnn";
                LetterPath = N_Path_Letters + "CoAnnuitant Letter Auto Template.docm";
                docType = "word";
            }
            else if (cbLetterSelected.Text == "Equifax Letter")
            {
                selectedLetter = "Equifax";
                LetterPath = N_Path_Letters + "Equifax Letter Auto Template.docx";
                docType = "word";
            }
            else if (cbLetterSelected.Text == "POA Confirmation Letter")
            {
                selectedLetter = "POA";
                LetterPath = N_Path_Letters + "POA Confirmation.docx";
                docType = "word";
            }
            else if (cbLetterSelected.Text == "Direct Deposit - Eng")
            {
                selectedLetter = "DirDep";
                LetterPath = @"C:\Users\ll67305\OneDrive - Enterprise 365\++Client Files OD\Templates\PDF Templates\Direct Deposit Form - Template.pdf";
                docType = "pdf";
            }
            else
            {
                MessageBox.Show("Please Select a Letter");
                return;
            }

            if (docType == "word")
            {
                doc = app.Documents.Open(LetterPath);
                doc.Application.Visible = true;
            }
            else if (docType == "pdf")
            {
                System.Diagnostics.Process.Start("explorer.exe", LetterPath);
            }


            if (selectedLetter == "Tax")
            {
                doc.FormFields["ClientName"].Result = lbLetterClientName.Text.ToUpper();
                doc.FormFields["Group"].Result = lbLetterGroup.Text;
                doc.FormFields["Cert"].Result = lbLetterCert.Text;
                doc.FormFields["Addr1"].Result = lbLetterAddr1.Text.ToUpper();
                doc.FormFields["Addr2"].Result = lbLetterAddr2.Text.ToUpper();
                doc.FormFields["Signature"].Result = lbLetterClientName.Text;
                doc.FormFields["Salutation"].Result = lbLetterClientName.Text;
            }
            else if (selectedLetter == "Equifax")
            {
                string name = lbLetterClientName.Text;
                doc.FormFields["ClientName"].Result = name.ToUpper();
                doc.FormFields["ClientName2"].Result = name;
                doc.FormFields["ClientName3"].Result = name;
                doc.FormFields["ClientName4"].Result = name;

                doc.FormFields["ClientAddr1"].Result = lbLetterAddr1.Text.ToUpper();
                doc.FormFields["ClientAddr2"].Result = lbLetterAddr2.Text.ToUpper();

            }
            else if (selectedLetter == "POA")
            {
                doc.FormFields["Date"].Result = DateTime.Now.ToString("MMMM d, yyyy");
                doc.FormFields["ClientName"].Result = lbLetterClientName.Text.ToUpper();
                doc.FormFields["Addr1"].Result = lbLetterAddr1.Text.ToUpper();
                doc.FormFields["Addr2"].Result = lbLetterAddr2.Text.ToUpper();
                doc.FormFields["Salutation"].Result = lbLetterClientName.Text;
                doc.FormFields["ExecutionDate"].Result = tbExecutionDate.Text;


            }
            else if (selectedLetter == "CoAnn")
            {
                doc.FormFields["Date"].Result = DateTime.Now.ToString("MMMM d, yyyy");
                doc.FormFields["ClientName"].Result = lbLetterClientName.Text.ToUpper();
                doc.FormFields["Salutation"].Result = lbLetterClientName.Text;

                doc.FormFields["Addr1"].Result = lbLetterAddr1.Text.ToUpper();
                doc.FormFields["Addr2"].Result = lbLetterAddr2.Text.ToUpper();

                doc.FormFields["Policy"].Result = lbLetterGroup.Text + "-" + lbLetterCert.Text;
                doc.FormFields["Policy2"].Result = lbLetterGroup.Text + "-" + lbLetterCert.Text;

                doc.FormFields["LetterDate"].Result = tbLetterDate.Text;
                doc.FormFields["CoAnnName"].Result = tbCoAnnName.Text;
                doc.FormFields["CoAnnName2"].Result = tbCoAnnName.Text;
                doc.FormFields["CoAnnName3"].Result = tbCoAnnName.Text;

            }
            else if (selectedLetter == "DirDep")
            {
                SendKeys.Send("{TAB}");
                SendKeys.Send("{TAB}");
                SendKeys.Send("{TAB}");
                SendKeys.Send("{TAB}");
                SendKeys.Send("{TAB}");
                SendKeys.Send("{TAB}");

                /*
                SendKeys.Send(tbPolicy.Text);
                SendKeys.Send("{TAB}");
                SendKeys.Send(tbName.Text);
                */
            }
        }

        private void lbLetterAddr1_Click(object sender, EventArgs e)
        {

        }

        private void cbLetterSelected_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLetterSelected.Text == "Bank Letter")
            {
                tsLetters.SelectTab("Banking");
            }

            if (cbLetterSelected.Text == "POA Confirmation Letter")
            {
                tsLetters.SelectTab("POA");
            }
            if (cbLetterSelected.Text == "CoAnn Letter")
            {
                tsLetters.SelectTab("CoAnn");
            }
        }

        private void btBankStringPaste_Click(object sender, EventArgs e)
        {

            tbBankString.Text = Clipboard.GetText();

        }

        private void btCreateTask_Click(object sender, EventArgs e)
        {
            FUDays frm = new FUDays();
            frm.ShowDialog();

            if (frm.m_cancelled) { return; }


            if (GetActiveOutlookApplication() != null)
            {
                Outlook.Application app = GetActiveOutlookApplication();
                Outlook.TaskItem tsk = (Outlook.TaskItem)app.CreateItem(Outlook.OlItemType.olTaskItem);

                tsk.Subject = GetSubject() + ":" + frm.reason;
                tsk.DueDate = Convert.ToDateTime(frm.fuDate);

                //Fill in body with phone
                string sep = (tbPhone.Text == "") ? "" : "Phone: " + tbPhone.Text;
                tsk.Body = frm.nextsteps + "\r\n" + "\r\n" + sep;

                this.WindowState = FormWindowState.Minimized;
                tsk.Display();
            }
            else
            {
                MessageBox.Show("Check that outlook app is open");
            }


        }
        public static Microsoft.Office.Interop.Outlook.Application GetActiveOutlookApplication()
        {
            Microsoft.Office.Interop.Outlook.Application app = null;
            try
            {
                app = (Microsoft.Office.Interop.Outlook.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Outlook.Application");
            }
            catch (Exception e)
            {

                return null;
            }
            return app;
        }

        private void btBankAddressPaste_Click(object sender, EventArgs e)
        {
            tbBankAddressString.Text = Clipboard.GetText();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            BankEdit editbank = new BankEdit();
            editbank.ShowDialog();
        }

        private void btClearEmail_Click(object sender, EventArgs e)
        {
            tbEmailBody.Text = "";

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void btPasteSubject_Click(object sender, EventArgs e)
        {
            string tmp = Clipboard.GetText();

            //Validate. Make sure spaces > 1
            if (MyFunc.CountChar(tmp, ' ') < 1) { return; }

            tbName.Text = MyFunc.SplitFunc(tmp, ' ', 1) + " " + MyFunc.SplitFunc(MyFunc.SplitFunc(tmp, ';', 0), ' ', 2);
            tbPolicy.Text = MyFunc.SplitFunc(tmp, ' ', 0);

            UpdateSPT();
        }
        private void btLoadRecord_Click(object sender, EventArgs e)
        {

            LoadRecord frm = new LoadRecord();

            frm.records = File.ReadAllLines(StoreRecordFilePath);

            frm.ShowDialog();
            if (frm.m_cancelled) { return; }

            FillRecordFromTextfile(frm.SelectedRecord);


        }
        private void EmptyRecordToTextFile()
        {
            //Show form to input desc
            StoreRecord frm = new StoreRecord();
            frm.subject = GetSubject();

            frm.ShowDialog();
            if (frm.m_cancelled) { return; }


            //Copies all data points to DataBase.txt for retrieval
            txtToRecord = "";
            txtToRecord = MyFunc.RecordOut(txtToRecord, "", frm.desc, 0);
            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", tbPolicy.Text, 1);
            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", tbName.Text, 2);
            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", tbPhone.Text, 3);
            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", tbAddress.Text, 4);

            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", tbYOB.Text, 5);
            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", tbAgeOut.Text, 6);
            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", cbRequestType.Text, 7);
            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", checkBox1.Checked.ToString(), 8);
            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", tbCheck1.Text, 9);
            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", checkBox2.Checked.ToString(), 10);
            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", tbCheck2.Text, 11);
            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", checkBox3.Checked.ToString(), 12);
            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", tbCheck3.Text, 13);
            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", checkBox4.Checked.ToString(), 14);
            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", tbCheck4.Text, 15);
            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", checkBox5.Checked.ToString(), 16);
            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", tbCheck5.Text, 17);
            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", checkBox6.Checked.ToString(), 18);
            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", tbCheck6.Text, 19);
            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", checkBox7.Checked.ToString(), 20);
            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", tbCheck7.Text, 21);
            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", checkBox8.Checked.ToString(), 22);
            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", tbCheck8.Text, 23);
            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", checkBox9.Checked.ToString(), 24);
            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", tbCheck9.Text, 25);
            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", checkBox10.Checked.ToString(), 26);
            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", tbCheck10.Text, 27);
            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", tbCheckListNotes.Text, 28);
            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", tbDocHandle.Text, 29);
            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", tbAccountNotes.Text, 30);
            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", tbCalc_In.Text, 31);
            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", tbCalc_Add.Text, 32);
            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", tbCalc_Mult.Text, 33);
            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", tbCalc_Sub.Text, 34);
            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", tbCalc_Div.Text, 35);
            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", tbAccnt1.Text, 36);
            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", tbAccnt2.Text, 37);
            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", tbAccnt3.Text, 38);
            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", tbAccnt9.Text, 39);
            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", tbFormCode.Text, 40);
            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", tb10.Text, 41);
            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", tb2.Text, 42);
            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", tbSpokeWith.Text, 43);
            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", MyFunc.ReplaceWord(tbNotes.Text, "\r\n", ""), 44);
            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", cbMars1.Text, 45);
            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", cbMars2.Text, 46);
            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", cbMars3.Text, 47);


            txtToRecord = MyFunc.RecordOut(txtToRecord, "@", "", 50);

            //Append record to file
            string line = File.ReadAllText(StoreRecordFilePath);
            File.WriteAllText(StoreRecordFilePath, txtToRecord + MyFunc.nl() + line);

            //Clear Form
            ClearForm();

        }


        private void FillRecordFromTextfile(string selRec)
        {
            if (MyFunc.CountChar(selRec, '@') == 0) { return; }

            string[] arr = MyFunc.SplitIt(selRec, '@');
            tbPolicy.Text = arr[1];
            tbName.Text = arr[2];
            tbPhone.Text = arr[3];
            tbAddress.Text = arr[4];
            tbYOB.Text = arr[5];
            tbAgeOut.Text = arr[6];
            cbRequestType.Text = arr[7];

            //Check boxes
            checkBox1.Checked = arr[8] == "True" ? true : false;
            tbCheck1.Text = arr[9];
            checkBox2.Checked = arr[10] == "True" ? true : false;
            tbCheck2.Text = arr[11];
            checkBox3.Checked = arr[12] == "True" ? true : false;
            tbCheck3.Text = arr[13];
            checkBox4.Checked = arr[14] == "True" ? true : false;
            tbCheck4.Text = arr[15];
            checkBox5.Checked = arr[16] == "True" ? true : false;
            tbCheck5.Text = arr[17];
            checkBox6.Checked = arr[18] == "True" ? true : false;
            tbCheck6.Text = arr[19];
            checkBox7.Checked = arr[20] == "True" ? true : false;
            tbCheck7.Text = arr[21];
            checkBox8.Checked = arr[22] == "True" ? true : false;
            tbCheck8.Text = arr[23];
            checkBox9.Checked = arr[24] == "True" ? true : false;
            tbCheck9.Text = arr[25];
            checkBox10.Checked = arr[26] == "True" ? true : false;

            tbCheck10.Text = arr[27];
            tbCheckListNotes.Text = arr[28];
            tbDocHandle.Text = arr[29];

            tbAccountNotes.Text = arr[30];
            tbCalc_In.Text = arr[31];
            tbCalc_Add.Text = arr[32];
            tbCalc_Mult.Text = arr[33];
            tbCalc_Sub.Text = arr[34];
            tbCalc_Div.Text = arr[35];
            tbAccnt1.Text = arr[36];
            tbAccnt2.Text = arr[37];
            tbAccnt3.Text = arr[38];
            tbAccnt9.Text = arr[39];
            tbFormCode.Text = arr[40];
            tb10.Text = arr[41];
            tb2.Text = arr[42];
            tbSpokeWith.Text = arr[43];
            tbNotes.Text = arr[44];

            cbMars1.Text = arr[45];
            cbMars2.Text = arr[46];
            cbMars3.Text = arr[47];

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void tbFormCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void btCopyTaxFields_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tbPolicy.Text + ":" + tbName.Text + ":" + tbAddress.Text);
        }

        private void btLaunchTaxemail_Click(object sender, EventArgs e)
        {
            string fnm;
            string lnm;
            string crt;
            string grp;

            fnm = MyFunc.SplitFunc(tbName.Text, ' ', 0);
            lnm = MyFunc.SplitFunc(tbName.Text, ' ', 1);

            grp = MyFunc.SplitFunc(tbPolicy.Text, '-', 0);
            crt = MyFunc.SplitFunc(tbPolicy.Text, '-', 1);

            Outlook.Application application = new Outlook.Application();
            Outlook.MailItem mail = application.CreateItemFromTemplate(
                M_Path_Email + "TaxRequest.oft"
                 ) as Outlook.MailItem;
            mail.Subject = lnm + "," + fnm + "," + grp; //Last,First,Group;
                                                        //mail.To = "tim.betteridge@canadalife.com";


            mail.HTMLBody = MyFunc.ReplaceWord(mail.HTMLBody, "reasonxxx", cbTaxReason.Text);
            mail.HTMLBody = MyFunc.ReplaceWord(mail.HTMLBody, "taxyearxxx", cbTaxYear.Text);
            mail.HTMLBody = MyFunc.ReplaceWord(mail.HTMLBody, "addressxxx", tbTaxAddress.Text);
            mail.HTMLBody = MyFunc.ReplaceWord(mail.HTMLBody, "specialinstructionsxxx", cbTaxSpecialInstruction.Text);

            mail.HTMLBody = MyFunc.ReplaceWord(mail.HTMLBody, "certxxx", crt);
            //string a = mail.HTMLBody;
            mail.Display();
        }

        private void btClearPartial_Click(object sender, EventArgs e)
        {
            tbName.Text = "";
            tbPolicy.Text = "";
            tbPhone.Text = "";
            tbAddress.Text = "";
            tbPolicyD.Text = "";
            lbSPT.Text = "";
        }

        private void btTaxUpdateShortcut_Click(object sender, EventArgs e)
        {
            CopyWindowToggle();
            tsMain.SelectTab(3);

        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", @"file:///\\file-s3\irisdocs\Reference\POA_PGT_ADMIN_TRACKING_London_LIFE.xlsx");
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void lbClientPolicy_Click(object sender, EventArgs e)
        {

        }

        private void lbLetterClientName_Click(object sender, EventArgs e)
        {

        }

        private void lbLetterCert_Click(object sender, EventArgs e)
        {

        }

        private void tbLetterDate_TextChanged(object sender, EventArgs e)
        {

        }

        private void btLaunchICS_Click(object sender, EventArgs e)
        {
            Excel excel = new Excel(@"https://769372677-my.sharepoint.com/personal/tim_betteridge_canadalife_com/Documents/Manual%20Cheques/ICS%20Annuity%20Worksheet.xlsm?web=1", 1);
            //MessageBox.Show(excel.ReadCell(3, 1));

            
            excel.WriteToRange(tbPhone.Text, "Phone");
            excel.WriteToRange(tbName.Text, "ClientName");
            excel.WriteToRange(tbSpokeWith.Text, "SpokeWith");
        }
    }
        
}


