using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Linq;
using System.Windows;
using System.Collections.Generic;

namespace WpfControlLibrary1
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    class transfer : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            var uidoc = commandData.Application.ActiveUIDocument;
            var doc = uidoc.Document;

            //Get elements of Category
            var eles = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_StructuralColumns)
                .WhereElementIsNotElementType()              
                .ToElements();
            var floors = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Floors)
                .WhereElementIsNotElementType()
                .ToElements();

            using (var tran = new Transaction(doc, "Join Columns and Floors"))
            {
                tran.Start();
                foreach (var ele in eles)
                {
                    if (ele.Name == "300 x 450mm")
                    {
                        var volume = ele.LookupParameter("Volume").AsDouble();
                        volume = UnitUtils.Convert(volume, DisplayUnitType.DUT_CUBIC_FEET, DisplayUnitType.DUT_CUBIC_METERS);
                        ele.LookupParameter("Mark").Set(volume.ToString());
                    }
                    else
                    {
                        doc.Delete(ele.Id);
                    }
                }
                tran.Commit();
            }                  
            return Result.Succeeded;         
        }
    }    
}