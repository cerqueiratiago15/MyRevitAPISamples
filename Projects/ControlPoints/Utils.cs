using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Author: Iago Pinto de Cerqueira
namespace ControlPoints
{
    public class Utils
    {
        public static Transform GetProjectLocationTransform(Document doc)
        {
            // Retrieve the active project location position.

            ProjectPosition projectPosition
              = doc.ActiveProjectLocation.GetProjectPosition(
                XYZ.Zero);

            // Create a translation vector for the offsets

            XYZ translationVector = new XYZ(
              projectPosition.EastWest,
              projectPosition.NorthSouth,
              projectPosition.Elevation);

            Transform translationTransform
              = Transform.CreateTranslation(
                translationVector);

            // Create a rotation for the angle about true north

            Transform rotationTransform
              = Transform.CreateRotationAtPoint(XYZ.BasisZ, projectPosition.Angle,
                XYZ.Zero);

            // Combine the transforms 

            Transform finalTransform
              = translationTransform.Multiply(
                rotationTransform);

            return finalTransform;
        }

        public static DataTable GetInternalDataFromInstances(List<Parameter> parameters, List<FamilyInstance> instances, bool NoZ)
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add(new DataColumn("X COORDINATE"));
            tbl.Columns.Add(new DataColumn("Y COORDINATE"));
            if (NoZ == false)
            {
                tbl.Columns.Add(new DataColumn("Z COORDINATE"));
            }

            if (parameters.Count() > 0)
            {
                foreach (var p in parameters)
                {
                    try
                    {
                        tbl.Columns.Add(new DataColumn(p.Definition.Name));
                    }
                    catch
                    {

                        continue;
                    }
                }
            }
            foreach (FamilyInstance instance in instances)
            {
                XYZ point = (instance.Location as LocationPoint).Point;
                var rw = tbl.NewRow();

                foreach (DataColumn column in tbl.Columns)
                {
                    if (column.ColumnName == "X COORDINATE")
                    {
                        rw[column] = point.X;
                    }
                    else if (column.ColumnName == "Y COORDINATE")
                    {
                        rw[column] = point.Y;
                    }
                    else if (column.ColumnName == "Z COORDINATE")
                    {
                        rw[column] = point.Z;
                    }
                    else
                    {
                        string parameterName = column.ColumnName;
                        Parameter elementParameter = null;
                        elementParameter = instance.Parameters.OfType<Parameter>().Where(x => x.Definition.Name == parameterName).ToList().FirstOrDefault();
                        if (elementParameter == null)
                        {
                            elementParameter = instance.Symbol.Parameters.OfType<Parameter>().Where(x => x.Definition.Name == parameterName).ToList().FirstOrDefault();
                        }
                        if (elementParameter != null)
                        {
                            if (elementParameter.StorageType != StorageType.String)
                            {
                                rw[column] = elementParameter.AsValueString();
                            }
                            else
                            {
                                rw[column] = elementParameter.AsString();

                            }
                        }
                        else
                        {
                            continue;
                        }
                    }

                }
                tbl.Rows.Add(rw);
            }

            return tbl;
        }

        public static DataTable GetSurveyDataFromInstances(List<Parameter> parameters, List<FamilyInstance> instances, Document doc, bool NoZ)
        {
            Transform tf = GetProjectLocationTransform(doc);
            DataTable tbl = new DataTable();
            tbl.Columns.Add(new DataColumn("X COORDINATE"));
            tbl.Columns.Add(new DataColumn("Y COORDINATE"));
            if (NoZ == false)
            {
                tbl.Columns.Add(new DataColumn("Z COORDINATE"));
            }

            if (parameters.Count() > 0)
            {
                foreach (var p in parameters)
                {
                    try
                    {
                        tbl.Columns.Add(new DataColumn(p.Definition.Name));
                    }
                    catch
                    {

                        continue;
                    }
                }
            }
            foreach (FamilyInstance instance in instances)
            {
                XYZ point = (instance.Location as LocationPoint).Point;
                var rw = tbl.NewRow();
                point = tf.OfPoint(point);

                foreach (DataColumn column in tbl.Columns)
                {
                    if (column.ColumnName == "X COORDINATE")
                    {
                        rw[column] = point.X;
                    }
                    else if (column.ColumnName == "Y COORDINATE")
                    {
                        rw[column] = point.Y;
                    }
                    else if (column.ColumnName == "Z COORDINATE")
                    {
                        rw[column] = point.Z;
                    }
                    else
                    {
                        string parameterName = column.ColumnName;
                        Parameter elementParameter = null;
                        elementParameter = instance.Parameters.OfType<Parameter>().Where(x => x.Definition.Name == parameterName).ToList().FirstOrDefault();
                        if (elementParameter == null)
                        {
                            elementParameter = instance.Symbol.Parameters.OfType<Parameter>().Where(x => x.Definition.Name == parameterName).ToList().FirstOrDefault();
                        }
                        if (elementParameter != null)
                        {
                            if (elementParameter.StorageType != StorageType.String)
                            {
                                rw[column] = elementParameter.AsValueString();
                            }
                            else
                            {
                                rw[column] = elementParameter.AsString();

                            }
                        }
                        else
                        {
                            continue;
                        }
                    }

                }
                tbl.Rows.Add(rw);
            }

            return tbl;
        }

        public static void ToCSV(DataTable dtDataTable, string strFilePath)
        {
            StreamWriter sw = new StreamWriter(strFilePath, false);
            //headers  
            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                sw.Write(dtDataTable.Columns[i]);
                if (i < dtDataTable.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in dtDataTable.Rows)
            {
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }
                    if (i < dtDataTable.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }

        public static List<Parameter> GetFamilyParameters(Document doc, FamilySymbol symbol)
        {
            List<Parameter> parameters = new List<Parameter>();

            FilteredElementCollector collector = new FilteredElementCollector(doc);
            FamilyInstance instances = collector.OfClass(typeof(FamilyInstance)).OfType<FamilyInstance>().Where(x => x.Symbol.Id == symbol.Id).ToList().FirstOrDefault();
            
            parameters.AddRange(instances.Parameters.OfType<Parameter>());
            parameters.AddRange(symbol.Parameters.OfType<Parameter>());


            return parameters;

        }

        public static List<FamilySymbol> GetFamilyTypes(Document doc, Family family)
        {
            List<FamilySymbol> symbols = new List<FamilySymbol>();

            FilteredElementCollector collector = new FilteredElementCollector(doc);
            List<FamilyInstance> instances = collector.OfClass(typeof(FamilyInstance)).OfType<FamilyInstance>().Where(x => x.Symbol.Family.Id == family.Id).ToList();

            symbols = instances.Select(x => x.Symbol).GroupBy(x => x.Id).Select(x => x.FirstOrDefault()).ToList();

            return symbols;
        }

        public static List<ViewModel.MyParameters> CleanList(List<ViewModel.MyParameters> main, List<ViewModel.MyParameters> list2 )
        {
            foreach (var item in list2)
            {
                main.RemoveAll(x => x.GetParameter.Id == item.GetParameter.Id);
            }
            return main;
        }

        public static List<ViewModel.MyParameters> OrderListUp(List<ViewModel.MyParameters> main, List<ViewModel.MyParameters> list2)
        {

            foreach (var item in list2)
            {
               int index= main.FindIndex(x => x.GetParameter.Id == item.GetParameter.Id);
                if (index==0)
                {
                    continue;
                }
                else
                {
                    var p = main[index - 1];
                    main[index - 1] = item;
                    main[index] = p;
                }
            }
            return main;
        }

        public static List<ViewModel.MyParameters> OrderListDown(List<ViewModel.MyParameters> main, List<ViewModel.MyParameters> list2)
        {

            foreach (var item in list2)
            {
                int index = main.FindIndex(x => x.GetParameter.Id == item.GetParameter.Id);
                if (index == (main.Count-1))
                {
                    continue;
                }
                else
                {
                    var p = main[index + 1];
                    main[index + 1] = item;
                    main[index] = p;
                }
            }
            return main;
        }

        public static List<FamilyInstance> GetInstances(Document doc,FamilySymbol symbol, bool currentView)
        {
            List<FamilyInstance> instances = new List<FamilyInstance>();


            if (currentView)
            {
                FilteredElementCollector collector = new FilteredElementCollector(doc,doc.ActiveView.Id);
                instances = collector.OfClass(typeof(FamilyInstance)).OfType<FamilyInstance>().ToList();
            }
            else
            {
                FilteredElementCollector collector = new FilteredElementCollector(doc);
                instances = collector.OfClass(typeof(FamilyInstance)).OfType<FamilyInstance>().ToList();
            }

            return instances.Where(x => x.Symbol.Id == symbol.Id).ToList();
        }

        public static void Export(Document doc, FamilySymbol symbol, List<Parameter> parameters, bool currentView, bool NoZ)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();

            if (folder.ShowDialog()== DialogResult.OK)
            {
               var instances = GetInstances(doc, symbol, currentView);

                var tblInternal = GetInternalDataFromInstances(parameters, instances, NoZ);
                var tblSurvey = GetSurveyDataFromInstances(parameters, instances, doc, NoZ);
                string path = folder.SelectedPath + $@"/{symbol.FamilyName}_{symbol.Name}_ExportedPoints";
                ToCSV(tblInternal, path + @"_Internal.csv");
                ToCSV(tblSurvey, path + @"_Survey.csv");
            }

        }

    }
}
