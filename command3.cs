using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Linq;
using System.Windows;

namespace WpfControlLibrary1
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    class command3 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uidoc = commandData.Application.ActiveUIDocument;
            var doc = uidoc.Document;

            var eles = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_StructuralColumns)
                .WhereElementIsNotElementType()
                .ToElements();

            using (Transaction tran = new Transaction(doc,"Parameter Transfer"))
            {
                tran.Start();

                foreach (var ele in eles)
                {
                    var volume = ele.LookupParameter("Volume").AsDouble();
                    volume = UnitUtils.Convert(volume, DisplayUnitType.DUT_CUBIC_FEET, DisplayUnitType.DUT_CUBIC_METERS);
                    ele.LookupParameter("Mark").Set(volume.ToString());

                    //double.Parse("0.1111111");
                    //foreach (Parameter para in ele.Parameters)
                    //{
                    //    if(para.Definition.Name == "Volume")
                    //    {

                    //    }
                    //} 
                }


                tran.Commit();
            }

            return Result.Succeeded;
        }
    }
}
