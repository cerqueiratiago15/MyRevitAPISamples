using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using Data = System.Data;
using System.Runtime.InteropServices;

namespace Five5DWithFilters
{
    public class ExcelUtilities
    {
        public static Data.DataTable ReadExcelServices(string filePath)
        {
            Excel.Application xlApp = null;
            Excel.Workbook xlWorkBook = null;
            Excel.Worksheet xlWorkSheet = null;
            Excel.Range range = null;
            Data.DataTable tbl = new Data.DataTable("Table");
            
            try
            {
                int rw = 0;
                int cl = 0;

                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Open(filePath, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                range = xlWorkSheet.UsedRange;
                rw = range.Rows.Count;
                cl = range.Columns.Count;

                String serviceCode = string.Empty;
                String serviceDescription = string.Empty;
                tbl.Columns.Add("serviceCode");
                tbl.Columns.Add("serviceDescription");
                for (int rowCount = 2; rowCount <= rw; rowCount++)
                {
                    for (int columnCount = 1; columnCount <= cl; columnCount++)
                    {
                        serviceCode = Convert.ToString((range.Cells[rowCount, 1] as Excel.Range).Value2);
                        serviceDescription = Convert.ToString((range.Cells[rowCount, 2] as Excel.Range).Value2); 
                    }
                    var row = tbl.NewRow();
                    row["serviceCode"] = serviceCode;
                    row["serviceDescription"] = serviceCode;
                    tbl.Rows.Add(serviceCode,serviceDescription);
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message+"\n"+e.StackTrace);
            }
            finally
            {
                xlWorkBook.Close(true, null, null);
                xlApp.Quit();

                Marshal.ReleaseComObject(xlWorkSheet);
                Marshal.ReleaseComObject(xlWorkBook);
                Marshal.ReleaseComObject(xlApp);
            }
            return tbl;
        }
    }
}
