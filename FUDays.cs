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
    public partial class FUDays : Form
    {
        public string fuDate;
        public bool m_cancelled = false;
        public string reason = "";
        public string nextsteps = "";
        

        public FUDays()
        {
            InitializeComponent();
            
        }

        private void btFU1Week_Click(object sender, EventArgs e)
        {
            mcalFU.SetDate(DateTime.Today.AddDays(7));
            
        }

        private void btFU2Week_Click(object sender, EventArgs e)
        {
            mcalFU.SetDate(DateTime.Today.AddDays(14));

        }

        private void btFU6Week_Click(object sender, EventArgs e)
        {
            mcalFU.SetDate(DateTime.Today.AddDays(42));

        }

        private void button3_Click(object sender, EventArgs e)
        {
            mcalFU.SetDate(DateTime.Today.AddMonths(1));

        }

        private void btFU2Months_Click(object sender, EventArgs e)
        {
            mcalFU.SetDate(DateTime.Today.AddMonths(2));

        }

        private void btFU3Months_Click(object sender, EventArgs e)
        {
            mcalFU.SetDate(DateTime.Today.AddMonths(3));

        }

        private void button6_Click(object sender, EventArgs e)
        {
            m_cancelled = true;
            this.Close();
        }

        private void FUDays_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            reason = tbFUReason.Text;
            nextsteps = tbFUNextSteps.Text;
            fuDate = mcalFU.SelectionRange.Start.ToString();

            this.Close();
            
        }

        private void tbFUReason_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
