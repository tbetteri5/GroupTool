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

namespace GroupTool
{


    public partial class ScratchPad : Form
    {
        DateTime FormOpenedtimeStamp = DateTime.Now;
        string txtToFile; //String field written to file

        public bool Visible { get; set; }



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
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void btOutput_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tbOutput.Text);
        }

        private void btClear_Click(object sender, EventArgs e)
        {
            SaveRecord();
            ClearOtherControls();
            clearCheckBoxSection();


            ScratchPad.ActiveForm.Text = "ScratchPad Tool";

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

            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;



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

            textBox10.Text = string.Empty;
            textBox2.Text = string.Empty;

            tbCopyWindow.Text = string.Empty;
            lbCharCount.Text = "Addr Char Count:";
            ClearCalc();
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

            //ScratchPad.ActiveForm.Text = tbPolicy.Text + "  " + tbName.Text;
        }
        private void UpdateTitle()
        {
            bool ShowDuration = true;
            string append = "";

            if (ShowDuration)
            {
                //Policy to show
                //append = " " + "5:02"; 
            }
            else
            {
                //Duration of time to show
                //append; //currently not used
            }
            string name;
            if (tbName.Text.Contains(" "))
            {
                string[] words = tbName.Text.Split(' ');
                name = words[0] + " " + words[1];
            }
            else
            {
                name = tbName.Text + append;
            }

            string request = cbRequestType.Text == "Request..." ? "" : " - " + cbRequestType.Text;

            ScratchPad.ActiveForm.Text = name + request;
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
            SaveRecord();
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

            txtToFile = MyFunc.StringOut(txtToFile, "Accnt Text Boxes", textBox9.Text + "." + textBox10.Text + "." + textBox2.Text, "..");
            txtToFile = MyFunc.StringOut(txtToFile, "Notepad", tbNotes.Text, "");

            txtToFile = txtToFile.Replace("\r\n", ";");
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {

            UpdateOutput();
            UpdateTitle();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tbPolicy.Text + " " + tbName.Text);
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
            string tmp =  MyFunc.SelectedTextOveride(tbCopyWindow.SelectedText.Trim());
            tbName.Text = tmp.Replace(",", "");
        }

        private void tbInfo_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbRequestType_SelectedIndexChanged(object sender, EventArgs e)
        {
            RequestItems("FillItems", cbRequestType.Text);
            UpdateTitle();
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
            this.clientsTableAdapter.Fill(this.groupTooldbDataSet.Clients);
            cbMars2.Text = "SUB";
            cbMars3.Text = "SUB";
            RequestItems("Load", "");
            btOutputToggle.PerformClick();
        }

        private void RequestItems(string inst, string heading)
        {
            //Load; FillItems; Update;

            //Set output
            string filePath = MyFunc.FilePath() + "CheckList.txt";

            //Read Checklist Data
            string[] lines;
            if (File.Exists(filePath)) { 
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
                    string[] splitline = MyFunc.SplitIt(line, ':');


                    //Add each Heading to the request combo list
                    cbRequestType.Items.Add(splitline[0]);

                }

            }
            else if (inst == "FillItems")
            {

                foreach (string line in lines)
                {
                    //Load Headings
                    string[] splitline = MyFunc.SplitIt(line, ':');

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
                txt = txt + "\r\n" + heading + ":" + lastline;

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
                    string[] splitline = MyFunc.SplitIt(line, ':');

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
                    string[] splitline = MyFunc.SplitIt(line, ':');

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
                            NewCheckString = heading + ":" + txt;
                        }
                        else
                        {
                            NewCheckString = NewCheckString + "\r\n" + heading + ":" + txt;
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
            System.Diagnostics.Process.Start("http://google.com");
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
            System.Diagnostics.Process.Start("explorer.exe", @"M:\Desktop\M_PDF");
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
                btCopyOutput.Hide(); //Button
                btOutputToggle.Text = "...";

                int offset = 45; //Offset the controls collapse/expand

                btCopyData.Top = btCopyData.Top - offset;
                btSaveRecord.Top = btSaveRecord.Top - offset;
                btClear.Top = btClear.Top - offset;
                btClose.Top = btClose.Top - offset;
                lbTime.Top = lbTime.Top - offset;

                //ckMinimizeBehaviour.Top = ckMinimizeBehaviour.Top - offset;
                btOutputToggle.Top = btOutputToggle.Top - offset;
                this.Height = this.Height - offset;


            }
            else
            {
                tbOutput.Show(); //Text box
                lbOutput.Show(); // Label
                btCopyOutput.Show(); //Button
                btOutputToggle.Text = "^";

                int offset = 45; //Offset the controls collapse/expand

                btCopyData.Top = btCopyData.Top + offset;
                btSaveRecord.Top = btSaveRecord.Top + offset;
                btClear.Top = btClear.Top + offset;
                btClose.Top = btClose.Top + offset;
                lbTime.Top = lbTime.Top + offset;

                //ckMinimizeBehaviour.Top = ckMinimizeBehaviour.Top + offset;
                btOutputToggle.Top = btOutputToggle.Top + offset;
                this.Height = this.Height + offset;
            }


        }



        private void lbTime_Click(object sender, EventArgs e)
        {

        }

        private void clientsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.clientsBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.groupTooldbDataSet);

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
            Excel excel = new Excel(@"M:\Desktop\GroupTool\Excel\TelephoneLog.xlsm", 1);
            //MessageBox.Show(excel.ReadCell(3, 1));

            excel.WriteToRange(tbPhone.Text, "Phone");
            excel.WriteToRange(tbName.Text, "ClientName");
            excel.WriteToRange(tbSpokeWith.Text,"SpokeWith");
            excel.WriteToRange(MyFunc.SplitIt(tbPolicy.Text,'-')[0],"Group");
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
                this.Width = 382;
            else
                this.Width = 784;
        }

       
        

        private void btTest_Click(object sender, EventArgs e)
        {
            string a = tbCopyWindow.SelectedText.Replace("\r\n"," ");
            Clipboard.SetText(a);
        }

        private void btClearOut_Click(object sender, EventArgs e)
        {
           
            tbCopyWindow.Text = "";
        }

        private void btPasteAndSend_Click(object sender, EventArgs e)
        {
            tbCopyWindow.Text = Clipboard.GetText();

            if (tbName.Text == "" && tbPhone.Text == "" && tbPolicy.Text == "") {
                tbName.Text = MyFunc.PullData("Owner:", tbCopyWindow.Text);
                tbPhone.Text = MyFunc.CleanStringOfNonDigits_V5(MyFunc.PullData("home phone:", tbCopyWindow.Text));
                tbPolicy.Text = MyFunc.PullData("policy number:", tbCopyWindow.Text);

                btAllCaps.PerformClick();
            }
        }

        private void btCopySubjectAll_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tbPolicy.Text + " " + tbName.Text + " " + cbRequestType.Text);
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
           
            if(words.Length == 2)
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
                else if(words.Length == 2)
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
    }

}


