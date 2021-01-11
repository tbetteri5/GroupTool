using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GroupTool
{
    public partial class StoreRecord : Form
    {
        public bool m_cancelled = false;
        public string subject = "";
        public string desc = "";

        public StoreRecord()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Cancel Button
            m_cancelled = true;
            this.Close();
        }

        private void lbStoreRecSubject_Click(object sender, EventArgs e)
        {
            
        }

        private void StoreRecord_Load(object sender, EventArgs e)
        {
            lbStoreRecSubject.Text = subject;
        }

        private void btStorRecSave_Click(object sender, EventArgs e)
        {
            desc = tbStoreRecDescription.Text;

            this.Close();
        }
    }
}
