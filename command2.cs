using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Linq;
using System.Windows;

namespace WpfControlLibrary1
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    class command2 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uidoc = commandData.Application.ActiveUIDocument;
            var doc = uidoc.Document;
            var filter = new filter();
            var ele = new FilteredElementCollector(doc).OfClass(typeof(FamilySymbol))
                .Cast<FamilySymbol>()
                .Where(familysymbol => familysymbol.Name == "600 x 600 x 900mm").FirstOrDefault();
            uidoc.PostRequestForElementTypePlacement(ele);
            return Result.Succeeded;
        }
    }

    class filter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            if (elem.Category.Name == "Structural Foundations")
                return true;
            else
                return false;
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return false;
        }
    }
}
