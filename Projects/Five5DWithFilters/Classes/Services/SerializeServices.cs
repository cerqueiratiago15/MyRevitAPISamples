using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Five5DWithFilters
{
    public class SerializeServices
    {
        public static void Serialize(string path, List<ServiceFiltersClass> list)
        {

            try
            {

                string content = JsonConvert.SerializeObject(list);
                File.WriteAllText(path, content);

            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }

        }

        public static List<ServiceFiltersClass> DeserializeList(string path)
        {
            List<ServiceFiltersClass> services = new List<ServiceFiltersClass>();
            if (File.Exists(path))
            {
                

                try
                {
                    using (StreamReader reader = new StreamReader(path))
                    {
                        string text = reader.ReadToEnd();
                        var jsonList = JsonConvert.DeserializeObject<List<ServiceFiltersClass>>(text);

                        if (jsonList!=null && jsonList.Count()>0)
                        {
                            services.AddRange(jsonList);
                        }
                      
                    }
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show($"Error: {e.Message}\n{e.StackTrace}",
                        "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show($"Arquivo náo encontrado",
                  "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return services;
        }

    
        public static void AddService(string path, ServiceFiltersClass service)
        {
            List<ServiceFiltersClass> services = new List<ServiceFiltersClass>();

            if (File.Exists(path))
            {

                services.AddRange(DeserializeList(path));
                services.Add(service);

            }
            try
            {

                string content = JsonConvert.SerializeObject(services);
                File.WriteAllText(path, content);

            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
        }
    }
}
