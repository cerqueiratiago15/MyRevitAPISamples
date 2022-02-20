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
    public partial class CategoryForm : System.Windows.Forms.Form
    {
        public Document Doc { get; set; }
        public List<ElementId> listOfCategories = new List<ElementId>();

        public CategoryForm(Document doc)
        {
            InitializeComponent();
            this.Doc = doc;

            int y = 10;
            foreach (Category category in doc.Settings.Categories.OfType<Category>().OrderBy(x=>x.Name).Where(x=>x.CategoryType==CategoryType.Model).ToList())
            {
                CheckBox cb = new CheckBox();
                cb.Text = category.Name;
                cb.Tag = category.Id;
                panel1.Controls.Add(cb);
                cb.Location = new System.Drawing.Point(10, y);
                cb.Size = new Size(300, 30);
                y += 30;
            }


        }

        private void CategoryForm_Load(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.listOfCategories = new List<ElementId>();
            int number = panel1.Controls.OfType<CheckBox>().Where(x => x.Checked).Count();

            if (number>0)
            {
                foreach (CheckBox chb in panel1.Controls.OfType<CheckBox>().Where(x => x.Checked))
                {
                    ElementId id = null;
                    id = chb.Tag as ElementId;
                    this.listOfCategories.Add(id);
                   
                }
                this.Hide();

            }
        }
    }
}
