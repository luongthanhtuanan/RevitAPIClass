using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Linq;
using System.Windows;

namespace WpfControlLibrary1
{
    class transfer : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            var uidoc = commandData.Application.ActiveUIDocument;
            var doc = uidoc.Document;

            //Get elements of Category
            var eles = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_StructuralColumns)
                .WhereElementIsNotElementType()
                .ToElementIds();




            return Result.Succeeded;
        }
    } 
}