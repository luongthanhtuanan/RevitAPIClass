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
    class calculator : IExternalCommand
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


            using (var tran = new Transaction(doc, "set information to parameter"))
            {
                tran.Start();
                foreach (var ele in eles)
                {
                    var otp = new Options();
                    //otp.DetailLevel = ViewDetailLevel.Fine;
                    //GeometryElement geo = ele.get_Geometry( new Options());
                    GeometryElement geo = ele.get_Geometry(otp);
                    
                    //declare variable
                    int totalface = 0;
                    double totalarea = 0.0;

                    foreach (var obj in geo)
                    {
                        var solid = obj as Solid;
                        if (solid != null)
                        {
                            foreach (Face f in solid.Faces)
                            {
                                totalarea += f.Area;
                                totalface++;
                            }
                        }
                    }
                    totalarea = UnitUtils.Convert(totalarea, DisplayUnitType.DUT_SQUARE_FEET, DisplayUnitType.DUT_SQUARE_METERS);
                    ele.LookupParameter("Comments").Set(totalarea.ToString());
                    ele.LookupParameter("Mark").Set(totalface.ToString());
                }
                tran.Commit();
            }                  
            return Result.Succeeded;         
        }
    }    
}