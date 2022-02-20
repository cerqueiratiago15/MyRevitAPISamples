using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

//Author: Iago Pinto de Cerqueira

namespace ControlPoints
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document doc = uiDoc.Document;

            FilteredElementCollector collector = new FilteredElementCollector(doc);
            List<FamilyInstance> familyInstances = collector.OfClass(typeof(FamilyInstance)).OfType<FamilyInstance>().ToList();



            if (familyInstances.Count()>0)
            {
                ViewModel vm = new ViewModel(doc);
                MainWindow mw = new MainWindow(vm);
                mw.DataContext = vm;
                List<Family> families = familyInstances.Select(x => x.Symbol.Family).GroupBy(x=>x.Id).Select(x=>x.FirstOrDefault()).ToList();

                vm.Families = new System.Collections.ObjectModel.ObservableCollection<Family>(families.OrderBy(x=>x.Name));
                mw.ShowDialog();
                mw.Close();
            }
            

           return Result.Succeeded;
        }
    }
}
