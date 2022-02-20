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


namespace Five5DWithFilters
{
    public partial class AddRemoveFilters : System.Windows.Forms.Form
    {
        public List<FilterClass> listOfFilters { get; set; }
        public Document Doc { get; set; }
        
        public AddRemoveFilters(Document doc, List<FilterClass> filters)
        {
            InitializeComponent();
            this.Doc = doc;
            this.listOfFilters = filters.Distinct().ToList();
            foreach (var f in this.listOfFilters)
            {
                lboxAppliedFilters.Items.Add(f);
                lboxAppliedFilters.DisplayMember = "FilterName";
            }

        }

        private void btnCreateNew_Click(object sender, EventArgs e)
        {

            SelectFiltersForm select = new SelectFiltersForm(this.Doc);
            select.ShowDialog();
            List<FilterClass> filters = new List<FilterClass>();
            filters = select.SelectedFilters;

            if (filters.Count>0)
            {
                this.listOfFilters.AddRange(filters.ToArray());
                lboxAppliedFilters.Items.Clear();
                lboxAppliedFilters.DataSource = this.listOfFilters;
                lboxAppliedFilters.DisplayMember = "FilterName";
            }

        }

        private void btnOK_Click(object sender, EventArgs e)
        {

        }

        private void btnRmvFilter_Click(object sender, EventArgs e)
        {

        }
    }
}
