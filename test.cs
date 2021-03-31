using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace WpfControlLibrary1
{
    [Transaction(TransactionMode.ReadOnly)]

    class test : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            message = "Please note the highlighted walls";
            var uidoc = commandData.Application.ActiveUIDocument;
            var doc = uidoc.Document;

            var colector = new FilteredElementCollector(doc);
            ICollection<Element> collection = colector.OfClass(typeof(Wall))
                .ToElements();
            

            foreach (var element in collection)
            {
                elements.Insert(element);

            }






            return Result.Succeeded;
        }
    }
}
