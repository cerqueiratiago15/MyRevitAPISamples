using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace SetRoomsParameter
{
    public class ParameterProperties
    {
        public Parameter P { get;}
        public string Name { get;}
        public StorageType Type { get; }
        public string ID { get; }
        public bool IsShared { get; }

        public ParameterProperties(Parameter p)
        {
            this.P = p;
            this.Name = p.Definition.Name;
            this.Type = p.StorageType;
            this.IsShared = p.IsShared;
            if (p.IsShared)
            {
                this.ID = p.GUID.ToString();
            }
            else
            {
                this.ID = p.Id.ToString();
            }
        }
    }
}
