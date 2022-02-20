using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace Five5DWithFilters
{
    public class FolderOganization
    {
        public static string GetProjectFolderPath(Document doc)
        {
            string documentFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string projectName = @"\QuantiFiltrar\Projetos\" + doc.ProjectInformation.Name;
            string projectFolder = documentFolder+projectName;
            return projectFolder;
        }

        public static string GetServicesFolder(string projectFolder)
        {
            string serviceFolderName = @"\ServicosConfigurados";
            string completePath = projectFolder + serviceFolderName;
            if (Directory.Exists(projectFolder))
            {
                if (!Directory.Exists(completePath))
                {
                    Directory.CreateDirectory(completePath);
                }
            }
            else
            {
                Directory.CreateDirectory(completePath);
            }
            return completePath;
        }
        public static string GetFilterFolder(string projectFolder)
        {
            string filterFolder = @"\FiltrosExistentes";
            string completePath = projectFolder + filterFolder;
            if (Directory.Exists(projectFolder))
            {
                if (!Directory.Exists(completePath))
                {
                   Directory.CreateDirectory(completePath);
                }
            }
            else
            {
                Directory.CreateDirectory(completePath);
            }
            return completePath;
        }
        public static string GetFilterXML(string projectFolder)
        {
            FileStream filterFile = null;
            string filterFileName = @"\Filters.xml";
            string filterFilePath = GetFilterFolder(projectFolder)+filterFileName;

            if (!File.Exists(filterFilePath))
            {
                try
                {
                    filterFile = File.Create(filterFilePath);
                }
                catch
                {
                   
                }
                finally
                {
                    filterFile.Close();
                    SerializeFilters.Serialize(filterFilePath, new FilterToExport());
                }
            }
            return filterFilePath;
           
        }
        public static string GetServiceXML(string projectFolder)
        {
            FileStream serviceFile = null;
            string serviceFileName = @"\Servicos.xml";
            string serviceFilePath = GetServicesFolder(projectFolder) + serviceFileName;

            if (!File.Exists(serviceFilePath))
            {
                try
                {
                    serviceFile = File.Create(serviceFilePath);
                }
                catch
                {

                }
                finally
                {
                    serviceFile.Close();
                    SerializeServices.Serialize(serviceFilePath, new ListOfServices());


                }
            }
            return serviceFilePath;

        }
    }
}
