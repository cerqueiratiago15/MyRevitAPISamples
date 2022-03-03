using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB.Architecture;
using System.IO;
using Autodesk.Revit.DB.Structure;

namespace ElementsInRoom
{
    public class Utills
    {
        public static bool IsInRoom(Room room, Element e,Document doc, View3D view)
        {
            XYZ point = null;
            if ((e is Room)==false)
            {
                Location location = null;
                location = e.Location;
                if (location !=null)
                {
                    if ((location is LocationPoint))
                    {
                        point = (location as LocationPoint).Point;
                    }
                    else if (location is LocationCurve)
                    {
                        Curve curve = (location as LocationCurve).Curve;
                        
                        //double x = (curve.GetEndPoint(1).X + curve.GetEndPoint(0).X) / 2;
                        //double y = (curve.GetEndPoint(1).Y + curve.GetEndPoint(0).Y) / 2;
                        //double z = (curve.GetEndPoint(1).Z + curve.GetEndPoint(0).Z) / 2;
                        //XYZ p = new XYZ(x, y, z);
                        //var middle = (curve.GetEndPoint(1) + curve.GetEndPoint(0)) / 2;
                        var nwmiddle = curve.Evaluate(0.5, false);
                        point =nwmiddle;
                    }
                    else
                    {
                        if (e is Rebar)
                        {
                           var curve = (e as Rebar).GetCenterlineCurves(false,false,false,MultiplanarOption.IncludeAllMultiplanarCurves,0).OrderByDescending(x=>x.Length).FirstOrDefault();
                            point = (curve.GetEndPoint(1) + curve.GetEndPoint(0)) / 2;
                        }
                        else
                        {
                            Options opt = new Options();
                            opt.View = view;
                            //opt.DetailLevel = ViewDetailLevel.Fine;
                            //opt.View.PartsVisibility = PartsVisibility.ShowPartsAndOriginal;

                            try
                            {
                                point = e.get_Geometry(opt).Where(x => x is GeometryObject).OfType<Solid>().OrderBy(k => k.SurfaceArea).LastOrDefault().ComputeCentroid();

                            }
                            catch
                            {


                            } 
                        }
                    }
                }
                else
                {
                    Options opt = new Options();
                    opt.View = view;
                    //opt.DetailLevel = ViewDetailLevel.Fine;
                    opt.View.PartsVisibility = PartsVisibility.ShowPartsAndOriginal;
                    try
                    {
                        point = e.get_Geometry(opt).Where(x => x is GeometryObject).OfType<Solid>().OrderBy(k => k.SurfaceArea).LastOrDefault().ComputeCentroid();

                    }
                    catch 
                    {

                        
                    }                }
                if (point!=null)
                {
                   
                    if (room.IsPointInRoom(point))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }
        public static List<Parameter>ReadParameters(Room room)
        {
            List<Parameter> existing = new List<Parameter>();
            var read = File.ReadAllLines(GetPath());


            if (read.Count() > 0)
            {
                foreach (string line in read)
                {
                    string id = line;
                    Parameter p = null;
                    try
                    {
                        if (int.TryParse(id, out int intId))
                        {
                            p = room.Parameters.OfType<Parameter>().Where(x => x.Id.IntegerValue == intId).FirstOrDefault();
                        }
                        else
                        {
                            
                            p = room.Parameters.OfType<Parameter>().Where(x => x.IsShared).Where(x=>x.GUID!=null).Where(k => k.GUID.ToString() == id).FirstOrDefault();
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
                    catch 
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
        public static void Set5D(Element element)
        {
            if (element.Parameters.OfType<Parameter>().Where(x => x.Definition.Name.Contains("5D")).Count() > 0)
            {
                List<Parameter> parameters = new List<Parameter>();
                parameters = element.Parameters.OfType<Parameter>().Where(x =>
                x.Definition.Name.Contains("Codigo EAP") || x.Definition.Name.Contains("Insumo")).ToList();

                if (parameters.Count > 0)
                {
                    StringBuilder builder = new StringBuilder();
                    var insumos = parameters.Where(x => x.Definition.Name.Contains("Insumo")).OrderBy(x => x.Definition.Name).ToList();
                    var codigos = parameters.Where(x => x.Definition.Name.Contains("Codigo EAP")).OrderBy(x => x.Definition.Name).ToList();

                    int cont = 0;
                    int length = codigos.Count;
                    try
                    {
                        foreach (Parameter parameter in codigos)
                        {
                            if (parameter.HasValue)
                            {
                                builder.Append(parameter.AsString() + "-");
                                builder.Append(insumos[cont].AsString() + ";");
                            }
                            cont += 1;
                        }

                        int k = builder.ToString().Length - 1;
                        string value = builder.ToString();
                        value = value.Remove(k);
                        element.LookupParameter("5D").Set(value);
                    }
                    catch
                    {

                     
                    }

                }
            }
            element.Dispose();
        }
    }
}
