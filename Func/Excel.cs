using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;

namespace GroupTool
{
    class Excel {
        string path = "";
        _Application excel = new _Excel.Application();
        Workbook wb;
        Worksheet ws;


        public Excel(string path, int sheet)
        {
            this.path = path;
            wb = excel.Workbooks.Open(path);
            ws = wb.Worksheets[sheet];
            excel.Visible = true;
            
        }

        public void Run(string macro)
        {

            excel.Application.Run(macro);


        }

        public void Close(string status="IgnoreAlerts")
        {
            if (status == "IgnoreAlerts")
            {
                excel.DisplayAlerts = false;
            }
                excel.Quit();
        }

        public void WriteToRange(string txt,string rangename)
        {
            ws.Range[rangename].Value = txt;
        }
        public void SelectRange(string rangename)
        {
            ws.Range[rangename].Select();
        }
    }
}
