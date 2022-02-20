using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
namespace ElementsInRoom
{
    public static class ConcatenatedValue
    {
        public static void GetValue(Element element, out string codigoEap)
        {
            string codi = string.Empty;
            string e = string.Empty;

            List<Parameter> eapParameters = new List<Parameter>();
            eapParameters = element.Parameters.OfType<Parameter>().Where(x => x.Definition.Name.Contains("EAP") && x.Definition.Name.Contains("Codigo") == false).OrderBy(x=>x.Definition.Name).ToList();
           

            List<Parameter> codigosParameters = new List<Parameter>();
            codigosParameters = element.Parameters.OfType<Parameter>().Where(x => x.Definition.Name.Contains("EAP") && x.Definition.Name.Contains("Codigo") == true).OrderBy(x => x.Definition.Name).ToList();

            if (codigosParameters.Count>0 && eapParameters.Count>0)
            {
                int cont = 0;
                foreach (Parameter parameter in eapParameters)
                {
                    try
                    {
                        if (parameter.HasValue)
                        {
                            codi = element.LookupParameter("Localização Código").AsString() + "." + parameter.AsString();
                            Parameter p = codigosParameters[cont];
                            p.Set(codi);
                            cont += 1;
                        }
                    }
                    catch
                    {

                        continue;
                    }
                }
            }
            codigoEap = codi;
            
        }
    }
}
