using System;
using System.Globalization;
using System.IO;
using System.Management.Instrumentation;
using System.Windows.Forms;
using GroupTool.Func;
using System.Collections.Generic;

namespace GroupTool
{

   
    public partial class ScratchPad : Form
    {
        DateTime FormOpenedtimeStamp = DateTime.Now;

        public bool Visible { get; set; }

       

        //Dim the ucTodo Form and assign it to todo1


        public ScratchPad()
        {
            InitializeComponent();
            
        }

        private void btCopy_Click(object sender, EventArgs e)
        {
            string tmp = Clipboard.GetText();
            if (tmp.IndexOf("C") > 0 || tmp.IndexOf("c") > 0)
            {
                if (tmp.IndexOf("C") > 0)
                {
                    tbPolicy.Text = tmp.Replace("C", "-");
                }
                else
                {
                    tbPolicy.Text = tmp.Replace("c", "-");
                }


            }
            else
            {
                tbPolicy.Text = tmp;
                // tbPolicyD.Text = tmp.Replace("-", "C");
            }

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
        }

        private void btPastePhone_Click(object sender, EventArgs e)
        {
            tbPhone.Text = Clipboard.GetText();
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
            tbAddress.Text = Clipboard.GetText();
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
                append = tbPolicy.Text;
            }
            string name;
            if (tbName.Text.Contains(" "))
            {
                string[] words = tbName.Text.Split(' ');
                name = words[0] + " " + words[1] + append;
            }
            else
            {
                name = tbName.Text + append;
            }

            ScratchPad.ActiveForm.Text = name;
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
            string filePath = @"C:\Users\ll67305\Desktop\ScratchPad\Scratch Pad Log.txt";
            

            string line = File.ReadAllText(filePath);

            string txt = string.Empty;

            txt = DateTime.Now.ToString("MMM d.yy") + " " + DateTime.Now.ToLongTimeString() + "--->" + ",";
            txt = MyFunc.StringOut(txt, "Request Type", cbRequestType.Text,"Request...");
            txt = MyFunc.StringOut(txt, "Policy", tbPolicy.Text,"");
            txt = MyFunc.StringOut(txt, "Name", tbName.Text,"");
            txt = MyFunc.StringOut(txt, "Phone", tbPhone.Text,"");
            txt = MyFunc.StringOut(txt, "Addr", tbAddress.Text,"");
            t

            txt = MyFunc.StringOut(txt, "Checks", tbCheck1.Text,"");
            txt = MyFunc.StringOut(txt, ".", tbCheck2.Text,"");
            txt = MyFunc.StringOut(txt, ".", tbCheck3.Text,"");
            txt = MyFunc.StringOut(txt, ".", tbCheck4.Text,"");
            txt = MyFunc.StringOut(txt, ".", tbCheck5.Text,"");
            txt = MyFunc.StringOut(txt, ".", tbCheck6.Text,"");
            txt = MyFunc.StringOut(txt, ".", tbCheck7.Text,"");

            txt = MyFunc.StringOut(txt, "Sticky Note", tbCheckListNotes.Text,"");
            txt = MyFunc.StringOut(txt, "Accnt Notes", tbAccountNotes.Text,"");
            txt = MyFunc.StringOut(txt, "A1", cbMars1.Text + " " + tbAccnt1.Text," ");
            txt = MyFunc.StringOut(txt, "A2", cbMars2.Text + " " + tbAccnt2.Text,"SUB ");
            txt = MyFunc.StringOut(txt, "A3", cbMars3.Text + " " + tbAccnt3.Text,"SUB ");
            txt = MyFunc.StringOut(txt, "A9", tbAccnt9.Text,"0.00");

            txt = MyFunc.StringOut(txt, "Accnt Text Boxes", textBox9.Text + "." + textBox10.Text + "." + textBox2.Text,"..");
            txt = MyFunc.StringOut(txt, "Notepad", tbNotes.Text,"");

            File.WriteAllText(filePath, txt.Replace("\r\n",";") + MyFunc.nl() + line);
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
            tbName.Text = Clipboard.GetText();
        }

        private void tbInfo_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbRequestType_SelectedIndexChanged(object sender, EventArgs e)
        {
            RequestItems("FillItems", cbRequestType.Text);

        }


        private void tbAccnt1_TextChanged(object sender, EventArgs e)
        {
            UpdateAccounting();
        }

        private void tbAccnt2_TextChanged(object sender, EventArgs e)
        {
            UpdateAccounting();
        }


        private string TextAdd(string val1, string val2)
        {
            double operand1, operand2, result;

            if (val1 == "" || val1 == "-") { val1 = "0"; }
            if (val2 == "" || val2 == "-") { val2 = "0"; }

            try
            {
                operand1 = Convert.ToDouble(val1);
                operand2 = Convert.ToDouble(val2);

                result = operand1 + operand2;
                return Convert.ToString(result);
            }
            catch (InvalidCastException e)
            {
                return "error";
            }

        }

        private void UpdateAccounting()
        {
            string temp, result;

            // Mars1 and Mars2 detect subtract
            if (cbMars2.Text.Substring(0, 2) == "SU" || cbMars2.Text.Substring(0, 2) == "24")
            {
                //Hold the result in temp if subtract
                temp = TextAdd(tbAccnt1.Text, "-" + tbAccnt2.Text);
                
            }
            else
            {
                //Hold result if add
                temp = TextAdd(tbAccnt1.Text, tbAccnt2.Text);
            }

            result = temp;


            //Mars 3 detect. Skip if null
            if (cbMars3.Text != "")
            {
        
                if (cbMars3.Text.Substring(0, 2) == "SU" || cbMars3.Text.Substring(0, 2) == "24")
                {

                    result = TextAdd(temp, "-" + tbAccnt3.Text);
                }
                else if (cbMars3.Text.Substring(0, 2) != "")
                {
                    result = TextAdd(temp, tbAccnt3.Text);
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
            RequestItems("Load","");
            btOutputToggle.PerformClick();
        }

        private void RequestItems(string inst, string heading)
        {
            //Load; FillItems; Update;

            //Set output
            string filePath = @"C:\Users\ll67305\Desktop\ScratchPad\CheckList.txt";
        
            //Read Checklist Data
            string[] lines = File.ReadAllLines(filePath);

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
                string commaString = ",,,,,,,,";

                //Prepare the appended checklist lastline
                string lastline = str.Replace("\r\n", "@") + commaString;

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
            string[] words = MyFunc.SplitIt(tbName.Text,' ');
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
            RequestItems("FillItems",cbRequestType.Text);
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
            

            if(tbOutput.Visible == true)
            {
                tbOutput.Hide(); //Text box
                lbOutput.Hide(); // Label
                btCopyOutput.Hide(); //Button
                btOutputToggle.Text = "...";

                int offset = 45; //Offset the controls collapse/expand

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

        
        
    }

}


