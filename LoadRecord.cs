using GroupTool.Func;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GroupTool
{
    public partial class LoadRecord : Form
    {
        public bool m_cancelled = false;
        public string[] records;
        string StoreRecordFilePath = MyFunc.FilePath() + "DataBase.txt";
        public string SelectedRecord;

        public LoadRecord()
        {
            InitializeComponent();
        }

        private void lsLoadRecList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            m_cancelled = true;
            this.Close();
        }

        private void LoadRecord_Load(object sender, EventArgs e)
        {
            FillListView();
        }

        private void FillListView()
        {
            //fill the listview box with all records
            listView.Items.Clear();
            
            foreach (string rec in records)
            {
                //Breakdown sample ***Callback@35000-554885484@Bill Bixter@***
                listView.Items.Add(FillListBox(rec));
            }
            /*
            for (int i = 0; i < listView.Items.Count; i++)
            {
                if (listView.Items[i].Text == "")
                {
                    listView.Items[i].Remove();
                }
            }
            */
        }
        private ListViewItem FillListBox(string line)
        {
            ListViewItem item = null;
            string[] arr = MyFunc.SplitIt(line, '@');

            item = new ListViewItem(arr[0]);
            for (int i = 1;i < arr.Length; i++)
            {
                item.SubItems.Add(arr[i]);
            }

            return item;
        }


        private void btLoad_Click(object sender, EventArgs e)
        {
            //FillRecored then remove
            ScratchPad frm = new ScratchPad();
            int recToRemove;

            //From Remove  button
            recToRemove = listView.Items.IndexOf(listView.SelectedItems[0]);

            SelectedRecord = records[recToRemove];
            RemoveRecord(recToRemove);

            //From Update Button
            List<string> list = new List<string>(records);
            File.WriteAllLines(StoreRecordFilePath, list);

            this.Close();

            //RemoveRecord(listView.Items.IndexOf(listView.SelectedItems[0]));
        }

       
        private void btRemove_Click(object sender, EventArgs e)
        {
            //Remove record
            RemoveRecord(listView.Items.IndexOf(listView.SelectedItems[0]));
            
        }

        private void RemoveRecord(int delRec)
        {
            List<string> list = new List<string>(records);
            list.RemoveAt(delRec);//remove item from index.

            records = list.ToArray();
            //Refresh listview

            //Fill remaining items
            FillListView();
        }

        private void btUpdate_Click(object sender, EventArgs e)
        {
            //write records to the text file
            List<string> list = new List<string>(records);

            File.WriteAllLines(StoreRecordFilePath, list);
            
        }
    }
}
