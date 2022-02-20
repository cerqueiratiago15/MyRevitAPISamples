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

namespace SetRoomsParameter
{
    public partial class ParametersForm : System.Windows.Forms.Form
    {
        public List<ParameterProperties> Parameters { get; set; }

        public ParametersForm(ParameterSet set, List<Parameter> existing)
        {
            InitializeComponent();
            this.Parameters = new List<ParameterProperties>();
            listBox1.SelectionMode = SelectionMode.MultiExtended;
            foreach (Parameter parameter in set.OfType<Parameter>().OrderBy(k=>k.Definition.Name))
            {
                if (existing.Where(x=>x.Id==parameter.Id).Count()==0)
                {
                    ParameterProperties p = new ParameterProperties(parameter);
                    listBox1.Items.Add(p);
                    listBox1.DisplayMember = "Name"; 
                }
                else
                {
                    continue;
                }
            }
           
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count>0)
            {
                foreach (ParameterProperties parameter in listBox1.SelectedItems.OfType<ParameterProperties>())
                {
                    this.Parameters.Add(parameter);
                }
                this.Hide();
            }
            else
            {
                MessageBox.Show("You must select some parameters","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}
