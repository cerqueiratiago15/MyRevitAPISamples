using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace Five5DWithFilters
{
    public partial class SelectTask : Form
    {
        string path = string.Empty;
        public string ServiceCode { get; set; }
        public string FullServiceName { get; set; }
        DataTable TBL = null;
       
        public SelectTask(DataTable tbl)
        {
            InitializeComponent();
            this.TBL = tbl;
            this.ServiceCode = string.Empty;
            this.FullServiceName = string.Empty;

            //this.path= filepath;
            //dataGridView1.DataSource = ExcelUtilities.ReadExcelServices(this.path);
            dataGridView1.DataSource = tbl;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.RowTemplate = null;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.White;
            dataGridView1.RowsDefaultCellStyle.BackColor = System.Drawing.Color.Silver;

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count>0)
            {
                var row = dataGridView1.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
                this.ServiceCode= row.Cells[0].Value.ToString();
              
                this.FullServiceName = $"{this.ServiceCode}-{row.Cells[1].Value.ToString()}";
                this.Hide();
            }
            else
            {
                MessageBox.Show("Você deve selecionar um serviço","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        public void Search()
        {
            StringBuilder filterBuilder = new StringBuilder();
            try
            {
                if (textBox2.Text.Length>0)
                {
                    filterBuilder.Append("Convert(serviceCode,'System.String') LIKE '%" + textBox2.Text + "%'");
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = filterBuilder.ToString();
                }
                if (textBox1.Text.Length > 0)
                {
                    if (textBox2.Text.Length>0)
                    {
                        filterBuilder.Append(" AND ");
                    }
                    filterBuilder.Append("Convert(serviceDescription,'System.String') LIKE '%" + textBox1.Text + "%'");

                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = filterBuilder.ToString();
                }
                if(textBox1.Text.Length == 0 && textBox2.Text.Length == 0)
                {
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = null;
                }
            }
            catch
            {
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Search();
        }
    }
}
