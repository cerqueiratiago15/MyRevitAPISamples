using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Autodesk Namespaces
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

#endregion


namespace Five5DWithFilters
{
    public class FilterUtilities
    {

        public static FilterRule GetFilterRule(string logic, ParametersValueProperties x, ElementId parameterID)
        {
            if (x.Value is double)
            {
                switch (logic)
                {
                    case ">": return ParameterFilterRuleFactory.CreateGreaterRule(parameterID, double.Parse(x.Value.ToString()), 0.001);
                    case "<": return ParameterFilterRuleFactory.CreateLessRule(parameterID, double.Parse(x.Value.ToString()), 0.001);
                    case "==": return ParameterFilterRuleFactory.CreateEqualsRule(parameterID, double.Parse(x.Value.ToString()), 0.001);
                    case "!=": return ParameterFilterRuleFactory.CreateNotEqualsRule(parameterID, double.Parse(x.Value.ToString()), 0.001);
                    case ">=": return ParameterFilterRuleFactory.CreateGreaterOrEqualRule(parameterID, double.Parse(x.Value.ToString()), 0.001);
                    case "<=": return ParameterFilterRuleFactory.CreateLessOrEqualRule(parameterID, double.Parse(x.Value.ToString()), 0.001);
                    default: throw new Exception("invalid logic");

                }
            }
            else if (x.Value is int)
            {
                switch (logic)
                {
                    case ">": return ParameterFilterRuleFactory.CreateGreaterRule(parameterID, int.Parse(x.Value.ToString()));
                    case "<": return ParameterFilterRuleFactory.CreateLessRule(parameterID, int.Parse(x.Value.ToString()));
                    case "==": return ParameterFilterRuleFactory.CreateEqualsRule(parameterID, int.Parse(x.Value.ToString()));
                    case "!=": return ParameterFilterRuleFactory.CreateNotEqualsRule(parameterID, int.Parse(x.Value.ToString()));
                    case ">=": return ParameterFilterRuleFactory.CreateGreaterOrEqualRule(parameterID, int.Parse(x.Value.ToString()));
                    case "<=": return ParameterFilterRuleFactory.CreateLessOrEqualRule(parameterID, int.Parse(x.Value.ToString()));
                    default: throw new Exception("invalid logic");
                }

            }
            else if (x.Value is Element)
            {
                switch (logic)
                {
                    case ">": return ParameterFilterRuleFactory.CreateGreaterRule(parameterID, (x.Value as Element).Id);
                    case "<": return ParameterFilterRuleFactory.CreateLessRule(parameterID, (x.Value as Element).Id);
                    case "==": return ParameterFilterRuleFactory.CreateEqualsRule(parameterID, (x.Value as Element).Id);
                    case "!=": return ParameterFilterRuleFactory.CreateNotEqualsRule(parameterID, (x.Value as Element).Id);
                    case ">=": return ParameterFilterRuleFactory.CreateGreaterOrEqualRule(parameterID, (x.Value as Element).Id);
                    case "<=": return ParameterFilterRuleFactory.CreateLessOrEqualRule(parameterID, (x.Value as Element).Id);
                    default: throw new Exception("invalid logic");
                }
            }
            else//string
            {
                switch (logic)
                {
                    case ">": return ParameterFilterRuleFactory.CreateGreaterRule(parameterID, x.Value.ToString(), true);
                    case "<": return ParameterFilterRuleFactory.CreateLessRule(parameterID, x.Value.ToString(), true);
                    case "==": return ParameterFilterRuleFactory.CreateEqualsRule(parameterID, x.Value.ToString(), true);
                    case "!=": return ParameterFilterRuleFactory.CreateNotEqualsRule(parameterID, x.Value.ToString(), true);
                    case ">=": return ParameterFilterRuleFactory.CreateGreaterOrEqualRule(parameterID, x.Value.ToString(), true);
                    case "<=": return ParameterFilterRuleFactory.CreateLessOrEqualRule(parameterID, x.Value.ToString(), true);
                    case "Contain": return ParameterFilterRuleFactory.CreateContainsRule(parameterID, x.Value.ToString(), true);
                    case "Not Contain": return ParameterFilterRuleFactory.CreateNotContainsRule(parameterID, x.Value.ToString(), true);
                    default: throw new Exception("invalid logic");
                }
            }
        }

        public static List<ParametersValueProperties> GetListOfValues(Parameter parameter, Document doc, List<ElementId> categoriesId)
        {
            FilteredElementCollector elementsCollector1 = new FilteredElementCollector(doc).WhereElementIsElementType();
            FilteredElementCollector elementsCollector2 = new FilteredElementCollector(doc).WhereElementIsNotElementType();

            var list1 = elementsCollector1.ToElements();
            var list2 = elementsCollector2.ToElements();
            List<Element> elements = new List<Element>();

            elements.AddRange(list1.ToArray());
            elements.AddRange(list2.ToArray());
            int cont = 0;
            List<ParametersValueProperties> listOfValues = new List<ParametersValueProperties>();
            foreach (Element element in elements)
            {
                if (element.Category != null)
                {
                    if (categoriesId.Contains(element.Category.Id))
                    {
                        ParameterSet set = element.Parameters;
                        IList<Parameter> setList = set.OfType<Parameter>().ToList();
                        int count = 0;
                        count = setList.Where(x => x.Definition.Name == parameter.Definition.Name).Count();
                        if (count > 0 && parameter.Id.IntegerValue!= -1002002 && parameter.Id.IntegerValue != -1002001)
                        {
                            cont += 1;
                            double parameterDouble = 0;
                            int parameterInt = 0;
                            string parameterAsValueString = string.Empty;
                            ElementId parameterElementId = null;
                            string parameterAsString = string.Empty;
                            var parameterElement = element.LookupParameter(parameter.Definition.Name);
                            parameterDouble = element.LookupParameter(parameter.Definition.Name).AsDouble();
                            parameterInt = element.LookupParameter(parameter.Definition.Name).AsInteger();
                            parameterAsValueString = element.LookupParameter(parameter.Definition.Name).AsValueString();
                            parameterElementId = element.LookupParameter(parameter.Definition.Name).AsElementId();
                            parameterAsString = element.LookupParameter(parameter.Definition.Name).AsString();

                            if (parameterElement.StorageType == StorageType.Double)
                            {
                                ParametersValueProperties parameterValue = new ParametersValueProperties()
                                { Value = parameterDouble, AsValueString = parameterAsValueString };
                                listOfValues.Add(parameterValue);
                            }
                            else if (parameterElement.StorageType == StorageType.Integer)
                            {
                                ParametersValueProperties parameterValue = new ParametersValueProperties()
                                { Value = parameterInt, AsValueString = parameterAsValueString };

                                listOfValues.Add(parameterValue);
                            }
                            else if (parameterElement.StorageType == StorageType.ElementId)
                            {
                                Element elementValue = doc.GetElement(parameterElementId);

                                ParametersValueProperties parameterValue = new ParametersValueProperties();
                                if (parameterAsValueString != string.Empty)
                                {
                                    parameterValue.Value = elementValue;
                                    parameterValue.AsValueString = parameterAsValueString;
                                }
                                else
                                {
                                    parameterValue.Value = elementValue;
                                    parameterValue.AsValueString = parameterAsString;
                                }

                                if (parameterValue.AsValueString != string.Empty || listOfValues.Where(x => x.AsValueString == parameterValue.AsValueString).Count() == 0)
                                {
                                    listOfValues.Add(parameterValue);
                                }
                            }
                            else if (parameterElement.StorageType == StorageType.String)
                            {
                                ParametersValueProperties parameterValue = new ParametersValueProperties()
                                { Value = parameterAsString, AsValueString = parameterAsString };

                                listOfValues.Add(parameterValue);
                            }
                            else if (parameterAsValueString != string.Empty)
                            {
                                ParametersValueProperties parameterValue = new ParametersValueProperties()
                                { Value = parameterAsValueString, AsValueString = parameterAsValueString };
                                listOfValues.Add(parameterValue);
                            }

                        }
                        else if (parameter.Id.IntegerValue == -1002002 || parameter.Id.IntegerValue == -1002001)
                        {
                            if (parameter.Id.IntegerValue == -1002001)
                            {
                                if (element is ElementType)
                                {
                                    ParametersValueProperties parameterValue = new ParametersValueProperties()
                                    { Value = element.Name, AsValueString = element.Name};

                                    listOfValues.Add(parameterValue); 
                                }
                               
                            }
                            else if (parameter.Id.IntegerValue == -1002002)
                            {
                                if (element is ElementType)
                                {
                                    ParametersValueProperties parameterValue = new ParametersValueProperties()
                                    { Value = (element as ElementType).FamilyName, AsValueString = (element as ElementType).FamilyName};

                                    listOfValues.Add(parameterValue);
                                }
                            }
                        }
                    }
                }
            }
            List<ParametersValueProperties> l = new List<ParametersValueProperties>();
            foreach (var item in listOfValues)
            {
                if (!l.Exists(x=>x.AsValueString==item.AsValueString))
                {
                    l.Add(item);
                }
                else
                {
                    continue;
                }
            }
            return l;
        }

        public static List<Parameter> GetCommonParameters(List<ElementId> listOfIds, Document doc)
        {
            IList<ElementId> list = ParameterFilterUtilities.GetFilterableParametersInCommon(doc, listOfIds).ToList();
       
            List<Parameter> listOfParameters = new List<Parameter>();
            FilteredElementCollector filterOfViews = new FilteredElementCollector(doc);
            ViewFamilyType default3d = null;
            default3d = filterOfViews.OfClass(typeof(ViewFamilyType)).OfType<ViewFamilyType>().Where(x=>x.ViewFamily == ViewFamily.ThreeDimensional).ToList().FirstOrDefault();

            using (SubTransaction sub = new SubTransaction(doc))
            {
                sub.Start();
                if (default3d != null)
                {
                    View3D new3d = View3D.CreateIsometric(doc, default3d.Id);
                    if (list.Count > 0)
                    {
                        new3d.PartsVisibility = PartsVisibility.ShowPartsAndOriginal;
                        FilteredElementCollector elementCollector;
                        elementCollector = new FilteredElementCollector(doc, new3d.Id);
                        IList<Element> listOfElements = elementCollector.ToElements();

                        Element element = null;
                        ElementType elementType = null;
                      
                        foreach (ElementId categoryID in listOfIds)
                        {
                            try
                            {
                                element = listOfElements.Where(x => x.Category.Id == categoryID).FirstOrDefault();
                                if (element.GetTypeId().IntegerValue > 0)
                                {
                                    elementType = doc.GetElement(element.GetTypeId()) as ElementType;
                                }
                                ParameterSet set1 = null;
                                ParameterSet set2 = null;
                                if (element != null)
                                {

                                    set1 = element.Parameters;
                                    if (elementType != null)
                                    {
                                        set2 = elementType.Parameters;
                                    }
                                    Parameter founded1 = null;
                                    Parameter founded2 = null;

                                    foreach (ElementId parameterID in list)
                                    {
                                        founded1 = set1.OfType<Parameter>().Where(x => x.Id == parameterID).FirstOrDefault();
                                        if (set2 != null)
                                        {
                                            founded2 = set2.OfType<Parameter>().Where(x => x.Id == parameterID).FirstOrDefault();
                                        }
                                        if (founded1 != null && listOfParameters.Contains(founded1) == false)
                                        {
                                            if ((listOfParameters.Where(x => x.Id == founded1.Id).Count() == 0))
                                            {
                                                listOfParameters.Add(founded1);
                                            }
                                            else
                                            {
                                                continue;
                                            }

                                        }
                                        else if (founded2 != null && listOfParameters.Contains(founded2) == false)
                                        {
                                            if (listOfParameters.Where(x => x.Id == founded2.Id).Count() == 0)
                                            {
                                                listOfParameters.Add(founded2);
                                            }
                                            else
                                            {
                                                continue;
                                            }
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                    }
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            catch 
                            {

                                continue;
                            }
                        }
                    }
                }
                sub.RollBack();
            }
            
            return listOfParameters.Distinct().OrderBy(x => x.Definition.Name).ToList();
        }

    }

}
