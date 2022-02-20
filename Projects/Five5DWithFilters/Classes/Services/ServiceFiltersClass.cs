using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Linq;

namespace Five5DWithFilters
{
    [Serializable()]
    [XmlRoot("ServiceFilter")]
    public class ServiceFiltersClass
    {

        [XmlAttribute("Nome_Do_Servico")]
        public string ServiceName { get; set; }

        [XmlElement("FiltersToExport")]
        public List<FilterToExport> Filters { get; set; }

        [XmlElement("FourDParameters")]
        public List<ParameterToExport> Parameters { get; set; }

    }
    [Serializable()]
    [XmlRoot("ServiceFilters")]
    public class ListOfServices: List<ServiceFiltersClass>
    {

    }
    [Serializable()]
    [XmlRoot("Filter")]
    public class FilterToExport
    {
        [XmlAttribute("FilterName")]
        public string FilterName { get; set; }

        [XmlElement("ListFilterRules")]
        public List<FilterRulesToExport> Rules { get; set; }

        [XmlElement("Categories")]
        public List<string> CategoriesName { get; set; }
    }

    [Serializable()]
    [XmlRoot("ListOfFilters")]
    public class ListOfFilters: List<FilterToExport>
    {

    }

    [XmlRoot("FilterRules")]
    public class FilterRulesToExport
    {
        [XmlAttribute("StorageType")]
        public StorageType ParameterStorageType { get; set; }

        [XmlAttribute("ParameterID")]
        public string ParameterID { get; set; }//if is shared parameter I will insert the GUID the rest of the parameter the ID is negative

        [XmlElement("ParameterName")]
        public string ParameterName { get; set; }

        [XmlElement("Operator")]
        public string Operator { get; set; }

        [XmlElement("Value")]
        public string Value { get; set; }

        [XmlElement("Shared")]
        public bool IsShared { get; set; }
    }

    [XmlRoot("Parameters")]
    public class ParameterToExport
    {
        [XmlText]
        public string ParameterName { get; set; }

        [XmlAttribute("ParameterID")]
        public string ParameterID { get; set; }
    }

}
