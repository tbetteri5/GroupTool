using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using GroupTool.Func;


namespace GroupTool
{
    public partial class Form1 : Form
    {
        public System.Windows.Forms.CheckedListBox.ObjectCollection Items { get; }
        string filePathTD = @"C:\Users\ll67305\Desktop\ScratchPad\Todo.txt";
        string filePathLT = @"C:\Users\ll67305\Desktop\ScratchPad\LongTerm.txt";

        public Form1()
        {
            InitializeComponent();
        }

        private void cklTodo_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (tbAddCheck.Text != "")
            {
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                cklTodo.Items.Add(textInfo.ToTitleCase(tbAddCheck.Text));
                tbAddCheck.Text = "";
            }
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            string items = ""; 
            //cklTodo.Items.CopyTo(items, 0);

            //loop through and store unchecked items
            for (int i = 0; i < cklTodo.Items.Count; i++)
            {
                
                    if (!cklTodo.GetItemChecked(i))
                    {
  
                    string str = (string)cklTodo.Items[i];
                    if(items == ""){items = "u" + str;}else{items = items + "," + "u" + str;}

                    }
  
            }

            //Send to sub to load the array
            cklTodo.Items.Clear();
            items = items.Substring(0, items.Length);
            CheckListLoad(items,"TD");
            
        }

        private void btClear_Click(object sender, EventArgs e)
        {
            object[] items = new object[cklTodo.Items.Count];
            cklTodo.Items.CopyTo(items, 0);
            cklTodo.Items.Clear();
            cklTodo.Items.AddRange(items);
            
        }

        private void CheckListLoad(string str,string ListPage)
        {
            //ListPage - Identifyer of the check list. Long Term or Todo.

            string[] arr = MyFunc.SplitIt(str, '@');
            
            //Add checklist items and store state flags
            foreach (string elem in arr)
            {
                if (elem.Trim() != "")
                {

                    string chkflag = elem.Substring(0, 1);
                    bool chkstate = chkflag == "u" ? false : true;

                    if(ListPage == "TD")
                    {
                        cklTodo.Items.Add(elem.Substring(1, elem.Length - 1), chkstate);
                    }
                    else
                    {
                        cklLongTerm.Items.Add(elem.Substring(1, elem.Length - 1), chkstate);
                    }

                }
                
            }
        }

        private void CheckListSaveTD()
        {
            string txt = "";
            
            //loop through and store unchecked items
            for (int i = 0; i < cklTodo.Items.Count; i++)
            {
  
                string str = (string)cklTodo.Items[i];
                //string chkflag = (string)cklTodo.CheckedItems[i].ToString();
                string chkstate = cklTodo.GetItemCheckState(i).ToString();

                string chkflag = chkstate == "Unchecked" ? "u" : "c";
                if (txt == "") { txt = chkflag + str; } else { txt = txt + "," + chkflag + str; }

            }
            File.WriteAllText(filePathTD,txt);

        }

        private void CheckListSaveLT()
        {
            string txt = "";

            //loop through and store unchecked items
            for (int i = 0; i < cklLongTerm.Items.Count; i++)
            {

                string str = (string)cklLongTerm.Items[i];
                //string chkflag = (string)cklTodo.CheckedItems[i].ToString();
                string chkstate = cklLongTerm.GetItemCheckState(i).ToString();

                string chkflag = chkstate == "Unchecked" ? "u" : "c";
                if (txt == "") { txt = chkflag + str; } else { txt = txt + "," + chkflag + str; }

            }
            File.WriteAllText(filePathLT, txt);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Todo List";
            CheckListLoad(File.ReadAllText(filePathTD),"TD");
            CheckListLoad(File.ReadAllText(filePathLT), "LT");
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            CheckListSaveTD();
            CheckListSaveLT();
            this.Close();
        }

        private void tbAddCheck_TextChanged(object sender, EventArgs e)
        {

        }

        private void btAddLT_Click(object sender, EventArgs e)
        {
            if (tbAddLT.Text != "")
            {
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                cklLongTerm.Items.Add(textInfo.ToTitleCase(tbAddLT.Text));
                tbAddLT.Text = "";
            }
        }

        private void btRemoveLT_Click(object sender, EventArgs e)
        {
            string items = "";
            //cklTodo.Items.CopyTo(items, 0);

            //loop through and store unchecked items
            for (int i = 0; i < cklLongTerm.Items.Count; i++)
            {

                if (!cklLongTerm.GetItemChecked(i))
                {

                    string str = (string)cklLongTerm.Items[i];
                    if (items == "") { items = "u" + str; } else { items = items + "," + "u" + str; }

                }

            }

            //Send to sub to load the array
            cklLongTerm.Items.Clear();
            items = items.Substring(0, items.Length);
            CheckListLoad(items,"LT"); //Load the LongTerm Page
        }
    }
}
