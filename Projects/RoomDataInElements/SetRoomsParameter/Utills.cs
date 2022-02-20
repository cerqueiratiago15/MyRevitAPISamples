using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB.Architecture;
using System.IO;

//Author: Iago Pinto de Cerqueira
namespace SetRoomsParameter
{
    class Utills
    {
        public static List<Parameter> ReadParameters(Room room)
        {
            List<Parameter> existing = new List<Parameter>();
            var read = File.ReadAllLines(GetPath());


            if (read.Count() > 0)
            {
                foreach (string line in read)
                {
                    string id = line;
                    Parameter p = null;
                    if (int.TryParse(id, out int intId))
                    {
                        p = room.Parameters.OfType<Parameter>().Where(x => x.Id.IntegerValue == intId).FirstOrDefault();
                    }
                    else
                    {
                        p = room.Parameters.OfType<Parameter>().Where(x => x.IsShared).Where(k => k.GUID.ToString() == id).FirstOrDefault();
                    }
                    if (p != null)
                    {
                        existing.Add(p);
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return existing;
        }
        public static string GetPath()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\KaedrusAddins\CopyFromRooms";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fullPath = $@"{path}\roomParameter.txt";
            if (!File.Exists(fullPath))
            {
                var file = File.Create(fullPath);
                file.Close();
            }
            return fullPath;
        }
    }
}
