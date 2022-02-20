using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Xml;

namespace Five5DWithFilters
{
    public class SerializeServices
    {
        public static void Serialize(string path, ListOfServices Services)
        {
            StreamWriter writer = new StreamWriter(path);
            try
            {
                XmlSerializer sw = new XmlSerializer(typeof(ListOfServices));
                sw.Serialize(writer, Services);
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

        public static ListOfServices UnSerialize(string path)
        {
            if (File.Exists(path))
            {
                StreamReader reader = new StreamReader(path);
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ListOfServices));
                    ListOfServices services = (ListOfServices)serializer.Deserialize(reader);
                    return services;
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show($"Error: {e.Message}\n{e.StackTrace}",
                        "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                finally
                {
                    reader.Close();
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show($"Arquivo náo encontrado",
                  "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return null;
        }

        public static ListOfServices ListarServicos(string path)
        {
            ListOfServices services = new ListOfServices();
            return services;
        }

        public static void AddService(string path, ServiceFiltersClass service)
        {
            XElement x = new XElement("ServiceFilter");
            x.Add(new XAttribute("Nome_Do_Servico",service.ServiceName));

            XElement filterTo = new XElement("FiltersToExport");
            foreach (FilterToExport filterToExport in service.Filters)
            {
                var f= SerializeFilters.BuildXFilterElement(filterToExport);
                filterTo.Add(f);
            }
            x.Add(filterTo);
            XElement fourD = new XElement("FourDParameters");
            foreach (ParameterToExport parameterTo in service.Parameters)
            {
                XElement xp = new XElement("Parameters");
                xp.Add(new XText(parameterTo.ParameterName));
                xp.Add(new XAttribute("ParameterID", parameterTo.ParameterID));
                fourD.Add(xp);
            }
            x.Add(fourD);

            try
            {
                XElement xml = XElement.Load(path);
                xml.Add(x);
                xml.Save(path);
                System.Windows.Forms.MessageBox.Show("Serviço adicionado com sucesso","Erro",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
        }

        public static void ReadAllServices()
        {

        }
    }
}
