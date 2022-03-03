using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Five5DWithFilters;
using System.Data;
using System.Data.Common;
using System.Xml.Linq;

namespace ReadQuantities
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document doc = uiDoc.Document;
            var folderPath = Five5DWithFilters.FolderOganization.GetProjectFolderPath(doc);
            var filePath = Five5DWithFilters.FolderOganization.GetServiceFilePath(folderPath);

           
            var readService = Five5DWithFilters.SerializeServices.DeserializeList(filePath);
           
            if (readService!=null)
            {
                
                var dtbl = GetParameterValues(doc, readService);
                if (dtbl != null)
                {
                    TableFormcs form = new TableFormcs(dtbl);
                    form.ShowDialog();
                }
            }
            return Result.Succeeded;
        }

        public DataTable GetParameterValues(Document doc,List<ServiceFiltersClass> readService)
        {
            FilteredElementCollector filterOfViews = new FilteredElementCollector(doc);
            ViewFamilyType view3D = filterOfViews.OfClass(typeof(ViewFamilyType)).OfType<ViewFamilyType>().ToList().Where(x=>x.ViewFamily== ViewFamily.ThreeDimensional).FirstOrDefault();
            View3D new3d = null;

            DataTable tbl = new DataTable("Table");
            tbl.Columns.Add("ServiceCode");
            tbl.Columns.Add("ParameterName");
            tbl.Columns.Add("ParameterValue");
            
            if (readService != null)
            {
                foreach (var service in readService)
                {
                    using (Transaction t = new Transaction(doc, "Create temporary view"))
                    {
                        t.Start();
                        new3d = View3D.CreateIsometric(doc, view3D.Id);
                        new3d.DetailLevel = ViewDetailLevel.Fine;
                        new3d.DisplayStyle = DisplayStyle.Shading;
                        new3d.PartsVisibility = PartsVisibility.ShowPartsAndOriginal;

                        var filterClasses = GetFilterClasses(service.Filters, doc);
                        List<ElementId> categories = new List<ElementId>();
                        List<ElementId> listOfFilteredElements = new List<ElementId>();
                        if (filterClasses.Count > 0)
                        {

                            foreach (var f in filterClasses)
                            {
                                categories.AddRange(f.ListOfCategories);
                            }
                            foreach (var fc in filterClasses)
                            {
                                var elementsIDs = new FilteredElementCollector(doc).WherePasses(fc.GetFilterElement).ToElementIds();
                                listOfFilteredElements.AddRange(elementsIDs.ToArray());
                            }
                            if (listOfFilteredElements.Count > 0)
                            {

                                new3d.IsolateElementsTemporary(listOfFilteredElements);
                                new3d.IsolateCategoriesTemporary(categories.Distinct().ToList());
                                new3d.ConvertTemporaryHideIsolateToPermanent();

                                var tbl0 = GetValues(doc, new3d, service.Parameters, service.ServiceName);
                                tbl.Merge(tbl0, false, MissingSchemaAction.Add);
                                
                            }
                        }

                        t.RollBack();
                    }

                }

            }
         
            return tbl;
        }

        public List<FilterClass> GetFilterClasses(List<FilterToExport> listOfilters, Document doc)
        {
            ElementParameterFilter elementParameterFilter = null;
            FilterClass filterC = null;
            List<FilterClass> classes = new List<FilterClass>();
            foreach (var filters in listOfilters)
            {
                List<FilterRule> listOfRules = new List<FilterRule>();

                if (filters.Rules.Count() > 0)
                {
                    Element valueElement = null;
                    string valueString = string.Empty;
                    ElementId parameterID = null;
                    FilterRule filterRule = null;

                    ParametersValueProperties valueProperties = new ParametersValueProperties();

                    foreach (var rule in filters.Rules)
                    {
                        if (rule.ParameterStorageType == StorageType.ElementId)
                        {
                            int value = int.Parse(rule.Value.ToString());
                            valueElement = doc.GetElement(new ElementId(value));
                            valueProperties.Value = valueElement;

                        }
                        else if (rule.ParameterStorageType == StorageType.Double)
                        {
                            double value = double.Parse(rule.Value.ToString());
                            valueProperties.Value = value;
                        }
                        else if (rule.ParameterStorageType == StorageType.Integer)
                        {
                            int value = int.Parse(rule.Value.ToString());
                            valueProperties.Value = value;
                        }
                        else
                        {
                            string value = rule.Value.ToString();
                            valueProperties.Value = value;
                        }
                        if (!rule.IsShared)
                        {
                            parameterID = new ElementId(int.Parse(rule.ParameterID));
                        }
                        else
                        {
                            parameterID = doc.GetElement(rule.ParameterID).Id;
                        }
                        filterRule = FilterUtilities.GetFilterRule(rule.Operator, valueProperties, parameterID);
                        if (filterRule != null)
                        {
                            listOfRules.Add(filterRule);
                        }
                    }
                }
                if (listOfRules.Count > 0)
                {
                    elementParameterFilter = new ElementParameterFilter(listOfRules);

                }
                if (elementParameterFilter != null)
                {
                    filterC = new FilterClass();
                    filterC.ToExport = filters;
                    filterC.GetFilterElement = elementParameterFilter;
                    filterC.ListOfCategories = new List<ElementId>();
                    filterC.FilterName = filters.FilterName;
                    foreach (var categories in filters.CategoriesName)
                    {
                        int intID = int.Parse(categories);
                        ElementId id = new ElementId(intID);
                        filterC.ListOfCategories.Add(id);
                    }
                    classes.Add(filterC);
                }
            }
            return classes;
        }

        public DataTable GetValues(Document doc,View3D view3d, List<ParameterToExport> listOfParameters,string serviceCode)
        {
            var elements = new FilteredElementCollector(doc, view3d.Id).WhereElementIsNotElementType().ToElements().ToList();
            DataTable tbl = new DataTable("Table");
            tbl.Columns.Add("ServiceCode");
            tbl.Columns.Add("ParameterName");
            tbl.Columns.Add("ParameterValue");

            List<Element> listOfTypes = new List<Element>();
            foreach (Element element in elements)
            {
                Element typeElement = null;
                typeElement = doc.GetElement(element.GetTypeId());
                if (typeElement !=null)
                {
                    listOfTypes.Add(typeElement); 
                }
                else
                {
                    continue;
                }
            }
            elements.AddRange(listOfTypes.ToArray());

            foreach (ParameterToExport parameterToExport in listOfParameters)
            {
                Parameter parameter = null;
                if (int.TryParse(parameterToExport.ParameterID,out int id))
                {
                    foreach (var element in elements)
                    {
                        var set = element.Parameters.OfType<Parameter>().ToList();
                        if (set.Exists(x=>x.Id.IntegerValue ==id))
                        {
                            //System.Windows.Forms.MessageBox.Show(element.Name);
                            parameter =element.get_Parameter((BuiltInParameter)id);
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                else
                {
                    foreach (var element in elements)
                    {
                        var set = element.Parameters.OfType<Parameter>().ToList();
                        if (set.Where(k=>k.IsShared).ToList().Exists(x => x.GUID.ToString() == parameterToExport.ParameterID))
                        {
                            parameter = element.get_Parameter(new Guid(parameterToExport.ParameterID));
                        }
                        else
                        {
                            continue;
                        }
                    }
                  
                }
                DataRow row = tbl.NewRow();
                row["ServiceCode"] = serviceCode;
                row["ParameterName"] = parameterToExport.ParameterName;
                if (parameter!=null)
                {
                    if (parameter.StorageType == StorageType.Double)
                    {

                        double sum = 0;
                        foreach (Element element in elements)
                        {
                            var set = element.Parameters.OfType<Parameter>().ToList();
                            if (set.Exists(x => x.Id == parameter.Id) && (element is ElementType)==false)
                            {
                                double value = 0;
                                value = element.get_Parameter(parameter.Definition).AsDouble();
                               
                                sum = sum + value;
                            }
                            else if (set.Exists(x => x.Id == parameter.Id) && (element is ElementType) == true)
                            {
                                double value = 0;
                                value = element.get_Parameter(parameter.Definition).AsDouble();
                                sum = value;
                            }
                            else
                            {
                                continue;
                            }
                          
                        }
                        string stringValue = ConvertToCurrentUnit(doc, parameter, sum);
                        row["ParameterValue"] = stringValue;
                    }
                    else if (parameter.StorageType == StorageType.Integer)
                    {
                        int sum = 0;
                        foreach (Element element in elements)
                        {
                            var set = element.Parameters.OfType<Parameter>().ToList();
                            if (set.Exists(x => x.Id == parameter.Id) && (element is ElementType) == false)
                            {
                                int value = 0;
                                value = element.get_Parameter(parameter.Definition).AsInteger();
                                sum = sum + value;
                            }
                            else if (set.Exists(x => x.Id == parameter.Id) && (element is ElementType) == true)
                            {
                                int value = 0;
                                value = element.get_Parameter(parameter.Definition).AsInteger();
                                sum = value;
                            }
                            else
                            {
                                continue;
                            }
                        }
                        row["ParameterValue"] = sum;
                    }
                    else if (parameter.StorageType == StorageType.String)
                    {
                        row["ParameterValue"] = elements.FirstOrDefault().LookupParameter(parameterToExport.ParameterName).AsString();
                    }
                    else
                    {
                        row["ParameterValue"] = elements.FirstOrDefault().LookupParameter(parameterToExport.ParameterName).AsValueString();

                    }
                    
                    tbl.Rows.Add(row);
                }
                else
                {
                   // System.Windows.Forms.MessageBox.Show("Test");
                    continue;
                }
            }

            return tbl;


        }
        public string ConvertToCurrentUnit(Document doc, Parameter p, double value)
        {
            string stringValue = UnitFormatUtils.Format(doc.GetUnits(), 
                p.Definition.UnitType, value, false, false);
            return stringValue;
        }
    }
    
}
