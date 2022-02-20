using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#region Autodesk Namespaces
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

#endregion

namespace Five5DWithFilters
{
    public partial class FiltersForm : System.Windows.Forms.Form
    {
        public Document Doc { get; set; }
        public Dictionary<string, string> Operators = new Dictionary<string, string>();
        List<ElementId> ListOfCategoryIds;
        static int numberOfGroups = 0;
        //public List<FilterRule> Rules = new List<FilterRule>();
        public string FilterName { get; set; }
        public FilterToExport filterToExport { get; set; }
        public bool Editing { get; set; }
        public string OldName { get; set; }
        public FiltersForm(Document doc, List<ElementId> listOfCategoryIds, bool editing, string oldFilterName)
        {
            InitializeComponent();
            this.Doc = doc;
            filterToExport = null;
            this.OldName = oldFilterName;
            this.ListOfCategoryIds = listOfCategoryIds;
            this.Editing = editing;
            Operators.Add("Igual a", "==");
            Operators.Add("Maior que", ">");
            Operators.Add("Menor que", "<");
            Operators.Add("Maior ou igual a", ">=");
            Operators.Add("Menor ou igual a", "<=");
            Operators.Add("Contém", "Contain");
            Operators.Add("Não Contém", "Not Contain");
            Operators.Add("Diferente de", "!=");

            numberOfGroups = this.panel1.Controls.OfType<GroupBox>().Count();

            populateOperators(groupBox1);
            populateParameters(groupBox1, this.ListOfCategoryIds, this.Doc);
            populateValuesComboBox(groupBox1);

        }




        public void populateParameters(GroupBox gr, List<ElementId> listOfCategories, Document doc)
        {
            System.Windows.Forms.ComboBox cmb = null;
            cmb = gr.Controls.OfType<System.Windows.Forms.ComboBox>().ToList().Where(x => x.Name.Contains("Parameter")).FirstOrDefault();
            if (cmb != null)
            {
                IList<Parameter> parameters = FilterUtilities.GetCommonParameters(listOfCategories, doc);
                if (parameters != null)
                {
                    if (parameters.Count > 0)
                    {
                        foreach (var parameter in parameters)
                        {
                            ParametersProperties pr = new ParametersProperties() { Parameter = parameter };
                            cmb.Items.Add(pr);
                            cmb.DisplayMember = "Name";
                            cmb.ValueMember = "Parameter";
                        }
                    }
                }
            }
        }

        public void populateOperators(GroupBox gr)
        {
            System.Windows.Forms.ComboBox cmbOperator = null;
            cmbOperator = gr.Controls.OfType<System.Windows.Forms.ComboBox>().ToList().Where(x => x.Name.Contains("Operator")).FirstOrDefault();
            if (cmbOperator != null)
            {
                cmbOperator.DataSource = Operators.ToList();
                cmbOperator.DisplayMember = "KEY";
                cmbOperator.ValueMember = "VALUE";

            }

        }

        public void populateValuesComboBox(GroupBox gr)
        {
            System.Windows.Forms.ComboBox cmb = null;
            cmb = gr.Controls.OfType<System.Windows.Forms.ComboBox>().ToList().Where(x => x.Name.Contains("Parameter")).FirstOrDefault();

            if (cmb != null)
            {
                cmb.SelectedValueChanged += OnSelectedParameterSelected;
            }

        }

        public void OnSelectedParameterSelected(object sender, EventArgs eventArgs)
        {

            Parameter selectedValue = ((ParametersProperties)(sender as System.Windows.Forms.ComboBox).SelectedItem).Parameter;

            var listOfVal = FilterUtilities.GetListOfValues(selectedValue, this.Doc, this.ListOfCategoryIds).Distinct().OrderBy(x => x.AsValueString).ToList();
            System.Windows.Forms.ComboBox cmbValue = ((sender as System.Windows.Forms.ComboBox).Parent as GroupBox).Controls.OfType<System.Windows.Forms.ComboBox>().Where(x => x.Name.Contains("Value")).FirstOrDefault();

            cmbValue.DataSource = listOfVal;
            cmbValue.DisplayMember = "AsValueString";

        }

        public bool ValidadeGroupBox(GroupBox box)
        {
            int number = box.Controls.OfType<System.Windows.Forms.ComboBox>().Where(x => x.SelectedIndex == null).Count();
            if (number == 0)
            {
                return true;
            }
            return false;
        }

        public bool ValidatePanel(System.Windows.Forms.Panel panel)
        {
            foreach (GroupBox group in panel.Controls.OfType<GroupBox>())
            {
                if (!ValidadeGroupBox(group))
                {
                    return false;
                }
                else
                {
                    continue;
                }
            }
            return true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidatePanel(panel1))
            {
                if (txtFilterName.Text != string.Empty)
                {
                    string projectFolder = FolderOganization.GetProjectFolderPath(this.Doc);
                    string fileFilter = FolderOganization.GetFilterXML(projectFolder);
                    if (SerializeFilters.ExistFilter(fileFilter, txtFilterName.Text) || this.Editing)
                    {
                        DialogResult result = MessageBox.Show("O filtro nomeado já existe." +
                            "Ao confirmar a operação o filtro será permanentemente modificado.\n" +
                            "Você deseja sobrescreve-lo?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            SerializeFilters.DeleteFilter(fileFilter, this.OldName);
                            BuildFilter();
                            this.Hide();
                        }
                        else
                        {
                            txtFilterName.Focus();
                        }
                    }
                    else
                    {
                        BuildFilter();
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("O filtro precisa ser nomeado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtFilterName.Focus();
                }
            }
            else
            {
                MessageBox.Show("Você deve selecionar todos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void BuildFilter()
        {
            List<FilterRulesToExport> filterRulesToExport = new List<FilterRulesToExport>();

                List<GroupBox> listOfGroupBoxes = panel1.Controls.OfType<GroupBox>().ToList();
                FilterRulesToExport toExport = null;
                foreach (var group in listOfGroupBoxes)
                {
                    System.Windows.Forms.ComboBox cmbOperator = group.Controls.OfType<System.Windows.Forms.ComboBox>().Where(x => x.Name.Contains("Operator")).FirstOrDefault();
                    System.Windows.Forms.ComboBox cmbValue = group.Controls.OfType<System.Windows.Forms.ComboBox>().Where(x => x.Name.Contains("Value")).FirstOrDefault();
                    System.Windows.Forms.ComboBox cmbParameter = group.Controls.OfType<System.Windows.Forms.ComboBox>().Where(x => x.Name.Contains("Parameter")).FirstOrDefault();

                    toExport = new FilterRulesToExport();
                    toExport.Operator = cmbOperator.SelectedValue.ToString();
                    toExport.IsShared = ((ParametersProperties)cmbParameter.SelectedItem).Parameter.IsShared;
                    if (toExport.IsShared)
                    {
                        toExport.ParameterID = ((ParametersProperties)cmbParameter.SelectedItem).Parameter.GUID.ToString();
                    }
                    else
                    {
                        toExport.ParameterID = ((ParametersProperties)cmbParameter.SelectedItem).Parameter.Id.ToString();
                    }
                    toExport.ParameterName = ((ParametersProperties)cmbParameter.SelectedItem).Name;
                    toExport.ParameterStorageType = ((ParametersProperties)cmbParameter.SelectedItem).Parameter.StorageType;
                    if (((ParametersValueProperties)cmbValue.SelectedItem).Value is Element)
                    {
                        toExport.Value = (((ParametersValueProperties)cmbValue.SelectedItem).Value as Element).Id.IntegerValue.ToString();
                    }
                    else
                    {
                        toExport.Value = ((ParametersValueProperties)cmbValue.SelectedItem).Value.ToString();

                    }

                    filterRulesToExport.Add(toExport);
                }

                this.FilterName = txtFilterName.Text;

            this.filterToExport = new FilterToExport();
            this.filterToExport.CategoriesName = new List<string>();
            this.filterToExport.Rules = new List<FilterRulesToExport>();

            foreach (var id in this.ListOfCategoryIds)
            {
                this.filterToExport.CategoriesName.Add(id.IntegerValue.ToString());
            }

            this.filterToExport.Rules = filterRulesToExport;
            this.filterToExport.FilterName = this.FilterName;
        }

        public void OnSelectedValues(object sender, EventArgs e)
        {
        }

        public GroupBox AddNewGroup()
        {
            int width = 654;
            int height = 206;
            GroupBox oldbox = GetLastAddedGroup();

            GroupBox newBox = new GroupBox() { Size = new Size(width, height) };
            numberOfGroups += 1;
            newBox.Name = $"groupbox{numberOfGroups}";
            panel1.Controls.Add(newBox);
            newBox.Location = new System.Drawing.Point(oldbox.Location.X, oldbox.Location.Y + height + 10);

            newBox.Text = $"Filtro {numberOfGroups}";

            return newBox;
        }

        public GroupBox GetLastAddedGroup()
        {
            GroupBox groupbox = this.panel1.Controls.OfType<GroupBox>().OrderBy(x => x.Name).LastOrDefault();

            return groupbox;
        }

        public void AddControlsInGroup(GroupBox newGroup)
        {
            int width = 500;
            int height = 24;

            int x = 25;

            int py = 40;
            int oy = 90;
            int vy = 140;

            System.Windows.Forms.ComboBox parameterCmb = new System.Windows.Forms.ComboBox() { Size = new Size(width, height), DropDownStyle = ComboBoxStyle.DropDownList };
            System.Windows.Forms.ComboBox operatorCmb = new System.Windows.Forms.ComboBox() { Size = new Size(width, height), DropDownStyle = ComboBoxStyle.DropDownList }; ;
            System.Windows.Forms.ComboBox valueCmb = new System.Windows.Forms.ComboBox() { Size = new Size(width, height), DropDownStyle = ComboBoxStyle.DropDownList }; ;

            newGroup.Controls.Add(parameterCmb);
            parameterCmb.Location = new System.Drawing.Point(x, py);
            parameterCmb.Name = $"Parameter{numberOfGroups}";

            newGroup.Controls.Add(operatorCmb);
            operatorCmb.Location = new System.Drawing.Point(x, oy);
            operatorCmb.Name = $"Operator{numberOfGroups}";

            newGroup.Controls.Add(valueCmb);
            valueCmb.Location = new System.Drawing.Point(x, vy);
            valueCmb.Name = $"Value{numberOfGroups}";

        }

        private void FiltersForm_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            GroupBox newbox = AddNewGroup();
            AddControlsInGroup(newbox);
            populateOperators(newbox);
            populateParameters(newbox, this.ListOfCategoryIds, this.Doc);
            populateValuesComboBox(newbox);

        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            GroupBox last = GetLastAddedGroup();

            if (numberOfGroups > 1)
            {
                this.panel1.Controls.Remove(last);
                numberOfGroups -= 1;
            }
        }
    }
    public class ParametersProperties
    {
        public Parameter Parameter { get; set; }
        public string Name { get { return Parameter.Definition.Name; } }
    }
}
