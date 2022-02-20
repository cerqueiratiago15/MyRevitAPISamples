using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB.Architecture;
using System.IO;

namespace SetRoomsParameter
{
    public partial class RoomForm : System.Windows.Forms.Form
    {
        Room R = null;
        List<Parameter> existing = new List<Parameter>();

        string Path = string.Empty;
        public RoomForm(Room room)
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.R = room;
            DataTable tbl = new DataTable();
            tbl.Columns.Add(new DataColumn("ID"));
            tbl.Columns.Add(new DataColumn("Type"));
            tbl.Columns.Add(new DataColumn("Name"));
            tbl.Columns.Add(new DataColumn("IsShared"));
            dataGridView1.DataSource = tbl;
            dataGridView1.AutoGenerateColumns = false;
            existing = Utills.ReadParameters(room);
            this.Path = Utills.GetPath();

            if (existing.Count>0)
            {
                foreach (Parameter parameter in existing)
                {
                    ParameterProperties p = new ParameterProperties(parameter);
                    DataRow row = tbl.NewRow();
                    row["ID"] = p.ID;
                    row["Type"] = p.Type;
                    row["Name"] = p.Name;
                    row["IsShared"] = p.IsShared;
                    tbl.Rows.Add(row);
                    dataGridView1.DataSource = tbl;
                   
                }
            }

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            List<string> lines = new List<string>();

            if (dataGridView1.Rows.Count>0)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    string value = row.Cells[ParameterIDColumn.Name].Value.ToString();
                    lines.Add(value);
                }            
            }
            File.WriteAllLines(this.Path, lines.ToArray());
            this.Close();
        }

        private void btn_AddNewParameter_Click(object sender, EventArgs e)
        {
            ParametersForm pForm = new ParametersForm(this.R.Parameters, this.existing);
            pForm.ShowDialog();
            if (pForm.Parameters.Count>0)
            {
                var tbl = dataGridView1.DataSource as DataTable;
           
                foreach (ParameterProperties parameterProperties in pForm.Parameters)
                {
                    DataRow row = tbl.NewRow();
                    row["ID"] = parameterProperties.ID;
                    row["Type"] = parameterProperties.Type;
                    row["Name"] = parameterProperties.Name;
                    row["IsShared"] = parameterProperties.IsShared;
                    tbl.Rows.Add(row);
                    dataGridView1.DataSource = tbl;
                    this.existing.Add(parameterProperties.P);
                }
            }
            pForm.Close();
        }

        private void RoomForm_Load(object sender, EventArgs e)
        {

        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            var tbl = dataGridView1.DataSource as DataTable;
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                if (row.Index>=0)
                {
                    string value = row.Cells[ParameterIDColumn.Name].Value.ToString();
                    tbl.Rows[row.Index].Delete();
                    try
                    {
                        if ((bool)row.Cells[IsSharedColumn.Name].Value == true)
                        {
                            this.existing.Remove(this.existing.Where(x => x.IsShared).Where(y => y.GUID.ToString() == value).FirstOrDefault());
                        }
                        else
                        {
                            this.existing.Remove(this.existing.Where(y => y.Id.IntegerValue.ToString() == value).FirstOrDefault());
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
                else
                {
                    continue;
                }
            }
            dataGridView1.DataSource = tbl;
        }
    }
}
