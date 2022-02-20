using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.DB.Mechanical;

namespace ElementsInRoom
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document doc = uiDoc.Document;

            FilteredElementCollector filterOfRooms = new FilteredElementCollector(doc);
            List<Room> rooms = null;
            rooms = filterOfRooms.OfCategory(BuiltInCategory.OST_Rooms).OfType<Room>().ToList();

            var viewType = (new FilteredElementCollector(doc)).OfClass(typeof(ViewFamilyType)).OfType<ViewFamilyType>().ToList().
                Where(x => x.ViewFamily == ViewFamily.ThreeDimensional).FirstOrDefault();

            List<RoomAndBoundingBoxElementsClass> roomsAndElements = new List<RoomAndBoundingBoxElementsClass>();
            if (rooms!=null)
            {
                if (rooms.Count>0)
                {
                    using (Transaction viewTransaction = new Transaction(doc,"Temporary view"))
                    {
                        viewTransaction.Start();
                        View3D view = View3D.CreateIsometric(doc,viewType.Id);
                        view.PartsVisibility = PartsVisibility.ShowPartsAndOriginal;
                        view.DetailLevel = ViewDetailLevel.Fine;
                        view.CropBox.Enabled = true;

                        foreach (Room room in rooms)
                            {
                                try
                                {
                                    BoundingBoxXYZ box = room.get_BoundingBox(view);
                                    view.SetSectionBox(box);
                                    IList<Element> elementsBoudingBox = (new FilteredElementCollector(doc, view.Id)).OfType<Element>().ToList();
                                    RoomAndBoundingBoxElementsClass roomAndBoundingBoxElements = new RoomAndBoundingBoxElementsClass();
                                    roomAndBoundingBoxElements.GetElements = new List<Element>();
                                    roomAndBoundingBoxElements.GetRoom = room;
                                    if (elementsBoudingBox != null)
                                    {
                                        if (elementsBoudingBox.Count > 0)
                                        {
                                            foreach (Element element in elementsBoudingBox)
                                            {
                                            try
                                            {
                                                if (Utills.IsInRoom(room, element, doc, view))
                                                {
                                                    roomAndBoundingBoxElements.GetElements.Add(element);
                                                }
                                                else
                                                {
                                                    continue;
                                                }
                                                element.Dispose();
                                            }
                                            catch 
                                            {

                                                continue;
                                            }
                                            }
                                        }
                                        roomsAndElements.Add(roomAndBoundingBoxElements);
                                    }
                                }
                                catch
                                {
                                    continue;
                                }
                                room.Dispose();
                            }
                        

                        viewTransaction.RollBack();
                    }
                }
            }
            
            if (roomsAndElements.Count>0)
            {

                using (Transaction t = new Transaction(doc,"Set values from Room"))
                {
                   var parameters= Utills.ReadParameters(roomsAndElements.FirstOrDefault().GetRoom);
                    if (parameters.Count>0)
                    {
                        t.Start();
                        foreach (var item in roomsAndElements)
                        {
                            Room room = item.GetRoom;
                            string imprimir = string.Empty;
                            object value = string.Empty;
                           
                            foreach (Parameter parameter in parameters)
                            {
                                if (parameter.StorageType == StorageType.Double)
                                {
                                    value = room.Parameters.OfType<Parameter>().Where(x => x.Id == parameter.Id).FirstOrDefault().AsDouble();
                                }
                                else if (parameter.StorageType == StorageType.Integer)
                                {
                                    value = room.Parameters.OfType<Parameter>().Where(x => x.Id == parameter.Id).FirstOrDefault().AsInteger();
                                }
                                else if (parameter.StorageType == StorageType.ElementId)
                                {
                                    value = room.Parameters.OfType<Parameter>().Where(x => x.Id == parameter.Id).FirstOrDefault().AsElementId();
                                }
                                else if (parameter.StorageType == StorageType.String)
                                {
                                    value = room.Parameters.OfType<Parameter>().Where(x => x.Id == parameter.Id).FirstOrDefault().AsString();
                                }
                                else if (parameter.StorageType == StorageType.None)
                                {
                                    value = room.Parameters.OfType<Parameter>().Where(x => x.Id == parameter.Id).FirstOrDefault().AsValueString();
                                }
                                foreach (var element in item.GetElements)
                                {
                                    try
                                    {
                                        if (element.Parameters.OfType<Parameter>().Where(x=>x.Id==parameter.Id).Count()>0)
                                        {
                                            if (parameter.StorageType == StorageType.Double)
                                            {
                                                element.Parameters.OfType<Parameter>().Where(x => x.Id == parameter.Id).FirstOrDefault().Set((double)value);
                                            }
                                            else if (parameter.StorageType == StorageType.Integer)
                                            {
                                                element.Parameters.OfType<Parameter>().Where(x => x.Id == parameter.Id).FirstOrDefault().Set((int)value);
                                            }
                                            else if (parameter.StorageType == StorageType.ElementId)
                                            {
                                                element.Parameters.OfType<Parameter>().Where(x => x.Id == parameter.Id).FirstOrDefault().Set((ElementId)value);
                                            }
                                            else if (parameter.StorageType == StorageType.String)
                                            {
                                                element.Parameters.OfType<Parameter>().Where(x => x.Id == parameter.Id).FirstOrDefault().Set((string)value);
                                            }
                                            else if (parameter.StorageType == StorageType.None)
                                            {
                                                element.Parameters.OfType<Parameter>().Where(x => x.Id == parameter.Id).FirstOrDefault().Set((string)value);
                                            }
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                        //if (element.Parameters.OfType<Parameter>().Where(x => x.Definition.Name.Contains("EAP")).Count() > 0 &&
                                        //    element.Parameters.OfType<Parameter>().Where(x => x.Definition.Name.Contains("Codigo")).Count() > 0
                                        //    && element.Parameters.OfType<Parameter>().Where(x => x.Definition.Name.Contains("Localização") && x.HasValue).Count() > 0)
                                        //{
                                        //    ConcatenatedValue.GetValue(element, out string codigo);
                                        //}
                                    }
                                    catch
                                    {
                                        continue;
                                    }
                                }
                            }

                        }
                        t.Commit(); 
                    }
                }

            }
            return Result.Succeeded;

        }
        
    }
}
