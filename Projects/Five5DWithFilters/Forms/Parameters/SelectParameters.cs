using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;

namespace Five5DWithFilters
{
    public partial class SelectParameters : System.Windows.Forms.Form
    {
        public List<Parameter> Parameters = new List<Parameter>();
        public SelectParameters(Document doc, List<ElementId> categories, List<Parameter> parameters)
        {
            InitializeComponent();
           ;
            listBox1.SelectionMode = SelectionMode.MultiExtended;
            var filterOfElements = new FilteredElementCollector(doc).WhereElementIsNotElementType().ToElements();

            var newParameters = GetParameters(categories, doc);
          
            if (newParameters.Count>0)
            {
                foreach (var p in newParameters)
                {
                    if (!parameters.Exists(x=>x.Definition.Name==p.Definition.Name))
                    {
                        ParametersListBox pa = new ParametersListBox();
                        pa.GetParameter = p;
                        listBox1.Items.Add(pa);
                        listBox1.DisplayMember = "Name";
                        listBox1.ValueMember = "GetParameter"; 
                    }
                    else
                    {
                         continue;
                    }
                } 
            }
        }
        public struct ParametersListBox
        {
            public Parameter GetParameter { get; set; }
            public string Name { get { return GetParameter.Definition.Name; } }
        }
        public List<Parameter> GetParameters(List<ElementId> listOfCategories, Document doc)
        {
            List<Parameter> listOfDiferences = new List<Parameter>();
           var parametersFounded= FilterUtilities.GetCommonParameters(listOfCategories,doc);

            if (parametersFounded.Count>0)
            {
                listOfDiferences = parametersFounded.ToList();
                return listOfDiferences;
            }
            return listOfDiferences;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count>0)
            {
                foreach (var parameters in listBox1.SelectedItems.OfType<ParametersListBox>())
                {
                    this.Parameters.Add(parameters.GetParameter);
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Você deve selecionar um ou mais parâmetros !","Aviso",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

    }
}
