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
using System.IO;

namespace Five5DWithFilters
{
    public partial class SelectFiltersForm : System.Windows.Forms.Form
    {
        public Document Doc { get; set; }
        string filterPath { get; set; }
        public List<FilterClass> SelectedFilters = new List<FilterClass>();
        List<FilterClass> filterClasses = new List<FilterClass>();

        public SelectFiltersForm(Document doc)
        {
            InitializeComponent();

            this.Doc = doc;
            this.filterPath = null;
            string porjectFolder = FolderOganization.GetProjectFolderPath(doc);
            this.filterPath = FolderOganization.GetFilterXML(porjectFolder);
            var filtersToExport = SerializeFilters.ReadAllFilters(filterPath);
            if (filtersToExport.Count > 0)
            {
                filterClasses = SerializeFilters.GetFilterClasses(filtersToExport, this.Doc);
                if (filterClasses.Count > 0)
                {

                    populateListBox(ref listBox1, filterClasses);
                }
            }
            else
            {
                MessageBox.Show("Não há nenhum filtro configurado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void populateListBox(ref ListBox listbox, List<FilterClass> dataSource)
        {
            listbox.DataSource = null;
            listbox.DataSource = dataSource.OrderBy(x => x.FilterName).ToList();
            listbox.DisplayMember = "FilterName";
        }

        private void btnCreateNew_Click(object sender, EventArgs e)
        {
            CategoryForm categoryForm = new CategoryForm(this.Doc);
            categoryForm.ShowDialog();
            var selectedCategories = categoryForm.listOfCategories;

            if (selectedCategories.Count > 0)
            {
                FiltersForm filters = new FiltersForm(this.Doc, selectedCategories, false, string.Empty);
                filters.ShowDialog();
                var filterToExport = filters.filterToExport;

                if (filterToExport != null)
                {

                    SerializeFilters.AddFilter(filterToExport, this.filterPath);
                    var filtersToExport = SerializeFilters.ReadAllFilters(filterPath);
                    filterClasses = SerializeFilters.GetFilterClasses(filtersToExport, this.Doc);
                    if (filterClasses.Count > 0)
                    {
                        populateListBox(ref listBox1, filterClasses);
                    }
                    //  SerializeFilters.Serialize(this.filterPath,filterToExport,this.Doc); 
                }
                filters.Close();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count > 0)
            {
                foreach (var selected in listBox1.SelectedItems.OfType<FilterClass>())
                {
                    this.SelectedFilters.Add(selected);
                }
                this.Hide();
            }
            else
            {
                MessageBox.Show("Não há nenhum filtro selecionado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count == 1)
            {
                FilterClass filterElement = listBox1.SelectedItem as FilterClass;

                FiltersForm filtersForm = new FiltersForm(this.Doc, filterElement.ListOfCategories, true, filterElement.FilterName);

                int cont = 0;

                if (filterElement.ToExport.Rules.Count > 1)
                {
                    foreach (var rule in filterElement.ToExport.Rules)
                    {
                        GroupBox box = filtersForm.AddNewGroup();
                    }
                }

                foreach (var rule in filterElement.ToExport.Rules)
                {
                    var panel = filtersForm.Controls.OfType<System.Windows.Forms.Panel>().Where(x => x.Name == "panel1").FirstOrDefault();
                    GroupBox box = panel.Controls.OfType<GroupBox>().ToList()[cont];
                    filtersForm.AddControlsInGroup(box);
                    filtersForm.populateOperators(box);
                    filtersForm.populateParameters(box, filterElement.ListOfCategories, this.Doc);
                    filtersForm.populateValuesComboBox(box);
                    System.Windows.Forms.ComboBox cmbParameter = box.Controls.OfType<System.Windows.Forms.ComboBox>().
                        Where(x => x.Name.Contains("Parameter")).FirstOrDefault();
                    System.Windows.Forms.ComboBox cmbOperator = box.Controls.OfType<System.Windows.Forms.ComboBox>().
                       Where(x => x.Name.Contains("Operator")).FirstOrDefault();
                    System.Windows.Forms.ComboBox cmbValue = box.Controls.OfType<System.Windows.Forms.ComboBox>().
                       Where(x => x.Name.Contains("Value")).FirstOrDefault();

                    var operatorRule = cmbParameter.Items.OfType<Dictionary<string, string>>().
                         Where(x => x.ContainsKey(rule.Operator)).FirstOrDefault();
                    if (operatorRule != null)
                    {
                        cmbParameter.SelectedItem = operatorRule;
                    }
                    ParametersProperties parameter = null;
                    if (!rule.IsShared)
                    {
                        parameter = cmbParameter.Items.OfType<ParametersProperties>().
                        Where(x => x.Parameter.Id.IntegerValue.ToString() == rule.ParameterID).FirstOrDefault();
                    }
                    else
                    {
                        parameter = cmbParameter.Items.OfType<ParametersProperties>().
                        Where(x => x.Parameter.GUID.ToString() == rule.ParameterID).FirstOrDefault();
                    }
                    cmbParameter.SelectedItem = parameter;
                    if (parameter != null)
                    {
                        ParametersValueProperties valueRule = null;

                        if (parameter.Parameter.StorageType == StorageType.ElementId)
                        {

                            foreach (var element in cmbValue.Items.OfType<ParametersValueProperties>())
                            {
                                if (element.Value is Element)
                                {
                                    if ((element.Value as Element).Id.IntegerValue.ToString() == rule.Value)
                                    {
                                        valueRule = new ParametersValueProperties();
                                        valueRule.Value = element.Value;
                                        valueRule.AsValueString = element.AsValueString;
                                    }
                                }
                            }
                        }

                        else
                        {
                            foreach (var val in cmbValue.Items.OfType<ParametersValueProperties>())
                            {
                                try
                                {
                                    if (val.AsValueString == rule.Value || val.Value.ToString() == rule.Value)
                                    {
                                        valueRule = val;
                                    }
                                }
                                catch
                                {
                                }
                            }
                        }
                        if (valueRule != null)
                        {
                            cmbValue.SelectedItem = cmbValue.Items.
                                OfType<ParametersValueProperties>().
                                Where(x => x.Value == valueRule.Value).FirstOrDefault();
                        }
                    }

                    cont += 1;
                }
                FilterToExport filterToExport = null;
                filtersForm.Controls.OfType<System.Windows.Forms.TextBox>().Where(x => x.Name == "txtFilterName").FirstOrDefault().Text = filterElement.FilterName;
                filtersForm.ShowDialog();
                filterToExport = filtersForm.filterToExport;

                if (filterToExport != null)
                {
                    SerializeFilters.AddFilter(filterToExport, this.filterPath);
                    var filtersToExport = SerializeFilters.ReadAllFilters(filterPath);
                    filterClasses = SerializeFilters.GetFilterClasses(filtersToExport, this.Doc);
                    if (filterClasses.Count > 0)
                    {
                        populateListBox(ref listBox1, filterClasses);
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count > 0)
            {
                foreach (FilterClass filter in listBox1.SelectedItems.OfType<FilterClass>())
                {
                    SerializeFilters.DeleteFilter(this.filterPath, filter.FilterName);
                }

                var filtersToExport = SerializeFilters.ReadAllFilters(filterPath);
                filterClasses = SerializeFilters.GetFilterClasses(filtersToExport, this.Doc);
                if (filterClasses.Count > 0)
                {
                    populateListBox(ref listBox1, filterClasses);
                }
            }
        }
    }
}
