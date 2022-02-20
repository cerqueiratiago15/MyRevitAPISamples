using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Xml;
using System.IO;

namespace Five5DWithFilters
{
    public class SerializeFilters
    {

        public static void Serialize(string file, FilterToExport filtersToExport)
        {
            //ListOfFilters Existingfilters = new ListOfFilters();
            //StreamReader reader = new StreamReader(file);
            //try
            //{
            //    XmlSerializer serializer = new XmlSerializer(typeof(ListOfFilters));
            //    Existingfilters = (ListOfFilters)serializer.Deserialize(reader);
            //}
            //catch
            //{
            //}
            //finally
            //{
            //    reader.Close();
            //}

            StreamWriter writer = new StreamWriter(file);
            var list = new ListOfFilters { filtersToExport };

            //if (Existingfilters.Count>0)
            //{
            //    list.AddRange(Existingfilters.ToArray());
            //}
            try
            {

                XmlSerializer sw = new XmlSerializer(typeof(ListOfFilters));
                sw.Serialize(writer,list);
                writer.Close();
            }
            catch (Exception e)
            {

                System.Windows.Forms.MessageBox.Show($"Error: {e.Message}",
                    "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                writer.Close();
            }


        }

        public static List<FilterClass> UnSerialize(string path, Document doc)
        {
            FilterClass filterC = null;
            List<FilterClass> classes = new List<FilterClass>();

            if (path!=string.Empty)
            {
                StreamReader reader = new StreamReader(path);
                try
                {

                    XmlSerializer serializer = new XmlSerializer(typeof(ListOfFilters));
                    var listOfilters = (ListOfFilters)serializer.Deserialize(reader);
                    
                    ElementParameterFilter elementParameterFilter = null;

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
                }
                catch (Exception e)
                {
                    //System.Windows.Forms.MessageBox.Show($"Error:{e.Message}\n {e.StackTrace}",
                    //    "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                finally
                {
                    reader.Close();
                }

                return classes ;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show($"Arquivo náo encontrado",
                  "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return classes;
        }

        public static List<XElement> GetXFiltersToExport(XElement serviceFilter)
        {
            var filtersToExport = serviceFilter.Elements("FiltersToExport").ToList();
            return filtersToExport;
        }

        public static void GetFilterData(XElement filter, out List<XElement> listOfRules, out List<XElement> categories, out string filterName)
        {
            filterName = filter.Attribute("FilterName").Value;
            listOfRules = filter.Elements("ListFilterRules").Elements("FilterRules").ToList();
            categories = filter.Elements("Categories").ToList();
        }

        public static FilterRulesToExport GetRuleData(XElement filterRule)
        {
           
            string parameterID = filterRule.Attribute("ParameterID").Value;
            
            string storageType = filterRule.Attribute("StorageType").Value;
            string parameterName = filterRule.Element("ParameterName").Value;
            string operatorName = filterRule.Element("Operator").Value;
            string parameterValue = filterRule.Element("Value").Value;
            string shared = filterRule.Element("Shared").Value;
            FilterRulesToExport toExport = new FilterRulesToExport();
            toExport.IsShared = bool.Parse(shared);
            toExport.Operator = operatorName;
            toExport.ParameterID = parameterID;
            toExport.ParameterName = parameterName;
            toExport.ParameterStorageType = Enum.GetValues(typeof(Autodesk.Revit.DB.StorageType)).
                Cast<Autodesk.Revit.DB.StorageType>().Where(x => x.ToString() == storageType).FirstOrDefault();
            toExport.Value = parameterValue;
            return toExport;
        }

        public static List<string> GetCategoriesList(List<XElement> categories)
        {
            List<string> listOfCategories = new List<string>();
            if (categories != null)
            {
                if (categories.Count() > 0)
                {
                    foreach (var category in categories)
                    {
                        string value = category.Value;
                        listOfCategories.Add(value);
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Categoria vazia");
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Categoria nula");
            }

            return listOfCategories;
        }

        public static List<FilterRulesToExport> GetListOfRules(List<XElement> listOfRules)
        {
            List<FilterRulesToExport> filterRulesToExports = new List<FilterRulesToExport>();
            foreach (var rules in listOfRules)
            {
                var ruleToExport = GetRuleData(rules);
                if (ruleToExport != null)
                {
                    filterRulesToExports.Add(ruleToExport);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("nulo");
                }
            }
            return filterRulesToExports;
        }

        public static List<FilterToExport> GetFiltersToExport(List<XElement> xFiltersToExport)
        {
            List<FilterToExport> filterToExports = new List<FilterToExport>();
            foreach (var item in xFiltersToExport)
            {
                try
                {
                    FilterToExport toExport = new FilterToExport();
                    GetFilterData(item, out List<XElement> listOfRules, out List<XElement> categories, out string filterName);
                    var cat = GetCategoriesList(categories);
                    if (cat.Count > 0)
                    {
                        
                        var listOfRulesToExport = GetListOfRules(listOfRules);
                        toExport.CategoriesName = cat;
                        toExport.FilterName = filterName;
                        toExport.Rules = listOfRulesToExport;
                        filterToExports.Add(toExport);
                    }
                }
                catch 
                {

                    continue;
                }

            }
         
            return filterToExports;
        }

        public static List<FilterToExport> ReadAllFilters(string path)
        {
            XElement xml = XElement.Load(path);
            var serviceFilters = xml.Elements("Filter");

            List<FilterToExport> filterToExport = new List<FilterToExport>();

            filterToExport = GetFiltersToExport(serviceFilters.ToList());
           
            return filterToExport;
        }

        public static XElement BuildXFilterElement(FilterToExport toExport)
        {
            XElement f = new XElement("Filter");
            f.Add(new XAttribute("FilterName", toExport.FilterName));

            XElement xRules = new XElement("ListFilterRules");//list

            #region FilterRules
            foreach (FilterRulesToExport rule in toExport.Rules)
            {
                XElement xRule = new XElement("FilterRules");//list
                xRule.Add(new XAttribute("StorageType", rule.ParameterStorageType));
                xRule.Add(new XAttribute("ParameterID", rule.ParameterID));
                xRule.Add(new XElement("ParameterName", rule.ParameterName));
                xRule.Add(new XElement("Operator", rule.Operator));
                xRule.Add(new XElement("Value", rule.Value));
                xRule.Add(new XElement("Shared", rule.IsShared));
                xRules.Add(xRule);
            }
            #endregion

            f.Add(xRules);
            XElement xCategories = new XElement("Categories");
            foreach (var categoriesID in toExport.CategoriesName)
            {
                xCategories.Add(new XElement("Category"), categoriesID);
            }
            f.Add(xCategories);
            return f;
        }

        public static void AddFilter(FilterToExport toExport, string path)
        {
            XElement x = BuildXFilterElement(toExport);
            try
            {
                XElement xml = XElement.Load(path);
                xml.Add(x);
                xml.Save(path);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
        }

        public static List<FilterClass> GetFilterClasses(List<FilterToExport> listOfilters, Document doc)
        {
            FilterClass filterC = null;
            List<FilterClass> classes = new List<FilterClass>();
            ElementParameterFilter elementParameterFilter = null;

            try
            {
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
            }
            catch
            {
            }
            return classes;
        }

        public static void DeleteFilter(string path, string filterName)
        {
            XElement xml = XElement.Load(path);
            var filters= xml.Elements("Filter");

            foreach (var item in filters)
            {
                if (item.Attribute("FilterName").Value==filterName)
                {
                    var nodeToRemove = item.DescendantNodesAndSelf();
                    nodeToRemove.Remove();
                }
            }
            xml.Save(path);
        }

        public static bool ExistFilter(string path, string filterName)
        {
            XElement xml = XElement.Load(path);
            var filters = xml.Elements("Filter");

            foreach (var item in filters)
            {
                if (item.Attribute("FilterName").Value == filterName)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
