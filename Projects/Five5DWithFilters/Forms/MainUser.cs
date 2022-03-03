using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Five5DWithFilters
{
    public partial class MainUser : System.Windows.Forms.Form
    {
        public Document Doc { get; set; }
        List<ParameterFilterElement> filters = new List<ParameterFilterElement>();
        List<FilterClass> filterClasses = new List<FilterClass>();
        public View3D Element3DView { get; set; }
        public UIDocument UIDoc { get; set; }
        FilteredElementCollector viewElements { get; set; }
        List<ElementId> allElements { get; set; }
        DataTable dataSource;
        public MainUser(UIDocument uiDoc, View3D view3D)
        {
            InitializeComponent();
            this.dataSource = null;
            lboxParameters.SelectionMode = SelectionMode.MultiExtended;
            this.Element3DView = view3D;
            this.FilePath = string.Empty;
            viewElements = new FilteredElementCollector(uiDoc.Document, view3D.Id);
            allElements = new FilteredElementCollector(uiDoc.Document).WhereElementIsNotElementType().ToElementIds().ToList();
            IList<ElementId> filteredElements = viewElements.ToElementIds().ToList();
            
            this.Element3DView.DetailLevel = ViewDetailLevel.Fine;
            this.Element3DView.DisplayStyle = DisplayStyle.Shading;
            this.Element3DView.PartsVisibility = PartsVisibility.ShowPartsAndOriginal;
            this.Doc = uiDoc.Document;
            this.UIDoc = uiDoc;
            this.elementHost1.Child = new PreviewControl(uiDoc.Document, view3D.Id);
        }

        private void btnFilters_Click(object sender, EventArgs e)
        {
            AddRemoveFilters addRemoveFilters = new AddRemoveFilters(this.Doc, filterClasses);
            addRemoveFilters.ShowDialog();
            filterClasses=addRemoveFilters.listOfFilters;
           
            List<ElementId> elements = new List<ElementId>();
            List<ElementId> categories = new List<ElementId>();

            if (filterClasses.Count>0)
            {
                
                foreach (var f in filterClasses)
                {
                    categories.AddRange(f.ListOfCategories);
                }
                foreach (var fc in filterClasses)
                {
                    var elementsIDs = new FilteredElementCollector(this.Doc).WherePasses(fc.GetFilterElement).ToElementIds();
                    elements.AddRange(elementsIDs.ToArray());
                }
                if (elements.Count>0)
                {
                    ResetView();
                   // MessageBox.Show("Test");
                    this.Element3DView.IsolateElementsTemporary(elements);
                    this.Element3DView.IsolateCategoriesTemporary(categories.Distinct().ToList());
                    this.Element3DView.ConvertTemporaryHideIsolateToPermanent(); 
                }
            }
        }

        private void elementHost1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
           
        }

        private void MainUser_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.elementHost1.Child = null;
        }
        private void btnAddParameter_Click_1(object sender, EventArgs e)
        {
            if (this.filterClasses.Count>0)
            {
                List<Parameter> listOfExistingParameters = new List<Parameter>();
                List<ElementId> listOfCategories = new List<ElementId>();

                foreach (var fc in this.filterClasses)
                {
                    listOfCategories.AddRange(fc.ListOfCategories.ToArray());
                }
                listOfCategories = listOfCategories.Distinct().ToList();
                int count = lboxParameters.Items.Cast<ParameterListBox>().ToList().Count;
                if (count > 0)
                {
                    foreach (var item in lboxParameters.Items.Cast<ParameterListBox>().ToList())
                    {
                        listOfExistingParameters.Add(item.GetParameter);
                    }
                }

                SelectParameters select = new SelectParameters(this.Doc,listOfCategories, listOfExistingParameters);
                select.ShowDialog();
                if (select.Parameters.Count > 0)
                {
                    foreach (Parameter parameter in select.Parameters)
                    {
                        ParameterListBox box = new ParameterListBox();
                        box.GetParameter = parameter;
                        lboxParameters.Items.Add(box);
                        lboxParameters.DisplayMember = "Name";
                        lboxParameters.ValueMember = "GetParameter";
                    }
                }
                select.Close(); 
            }
            else
            {
                MessageBox.Show("Você precisa primeiramente configurar os filtros de seleção!","Aviso",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }
        public struct ParameterListBox
        {
            public Parameter GetParameter { get; set; }
            public string Name { get { return GetParameter.Definition.Name; } }
        }

        private void btnRemoveParameter_Click(object sender, EventArgs e)
        {
            if (lboxParameters.SelectedItems.Count>0)
            {
                foreach (var item in lboxParameters.SelectedItems.OfType<ParameterListBox>().ToList())
                {
                    try
                    {
                        lboxParameters.Items.Remove(item);
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void btnResetControls_Click(object sender, EventArgs e)
        {
            ResetView();
            filterClasses = new List<FilterClass>();
        }
        public void ResetView()
        {
            
            using (SubTransaction sb = new SubTransaction(this.Doc))
            {

                sb.Start();
                this.Element3DView = View3D.CreateIsometric(this.Doc, this.Element3DView.GetTypeId());
                this.Element3DView.DetailLevel = ViewDetailLevel.Fine;
                this.Element3DView.DisplayStyle = DisplayStyle.Shading;
                this.Element3DView.PartsVisibility = PartsVisibility.ShowPartsAndOriginal;
                sb.Commit();

            }
            this.elementHost1.Dispose();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.elementHost1.Parent = groupBox1;
            this.elementHost1.Dock = DockStyle.Fill;
            this.elementHost1.Child = new PreviewControl(this.Doc, this.Element3DView.Id);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (this.FilePath!=string.Empty || this.dataSource!=null)
            {
                
                SelectTask task = new SelectTask(this.dataSource);
                task.ShowDialog();
                if (task.ServiceCode!=string.Empty)
                {
                    textBox2.Text = task.FullServiceName;
                    textBox2.Tag = task.ServiceCode;
                }
                
            }
        }

        public string FilePath { get; set; }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "File Excel|*.xlsx";
            DialogResult re = fd.ShowDialog();
            this.FilePath = fd.FileName;
            if (re==DialogResult.OK)
            {
                txtFileName.Text = this.FilePath;
                this.dataSource = ExcelUtilities.ReadExcelServices(this.FilePath);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSaveService_Click(object sender, EventArgs e)
        {
            string serviceCod = textBox2.Tag.ToString();
            List<FilterToExport> listOfToExports = new List<FilterToExport>();
            List<ParameterToExport> listOfParaToExport = new List<ParameterToExport>();

            if (lboxParameters.Items.Count>0)
            {
                if (this.filterClasses.Count > 0)
                {
                    foreach (var fc in this.filterClasses)
                    {
                        listOfToExports.Add(fc.ToExport);
                    }
                }
                foreach (ParameterListBox item in lboxParameters.Items)
                {
                    ParameterToExport p = new ParameterToExport();
                    if (item.GetParameter.IsShared)
                    {
                        p.ParameterID = item.GetParameter.GUID.ToString();
                    }
                    else if (item.GetParameter.Id.IntegerValue < 0)
                    {
                        p.ParameterID = item.GetParameter.Id.IntegerValue.ToString();
                    }
                    p.ParameterName = item.GetParameter.Definition.Name;

                    listOfParaToExport.Add(p);
                }
                string path =FolderOganization.GetProjectFolderPath(this.Doc);

                string filePath = FolderOganization.GetServiceFilePath(path);

                var listOfServices = new List<ServiceFiltersClass>();

                var ser = SerializeServices.DeserializeList(filePath);
                if (ser != null)
                {
                    listOfServices.AddRange(ser);
                }

                ServiceFiltersClass service = new ServiceFiltersClass();
                service.Filters = listOfToExports;
                service.Parameters = listOfParaToExport;
                service.ServiceName = serviceCod;
                listOfServices.Add(service);

                SerializeServices.Serialize(filePath, listOfServices);
                SerializeServices.AddService(filePath, service);
                MessageBox.Show("Serviço salvo com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                MessageBox.Show("Você precisa primeiramente selecionar os parâmetros de exportação!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


    }
}
