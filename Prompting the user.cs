using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Windows;

namespace WpfControlLibrary1
{
    [Transaction(TransactionMode.ReadOnly)]

    class Prompting_the_user : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                var uidoc = commandData.Application.ActiveUIDocument;
                var doc = uidoc.Document;

                var ids = doc.Delete(uidoc.Selection.GetElementIds());

                var taskdialog = new TaskDialog("HELLO AN");
                taskdialog.MainContent = ("Click Yes to return Succeeded. Selected members will be deleted.\n" +
                    Environment.NewLine + "Click No to return Failed. Selected members will not be deleted.\n" +
                    Environment.NewLine + "Click Cancel to return Cancelled. Selected members will not be deleted.");
                var buttons = TaskDialogCommonButtons.Yes | TaskDialogCommonButtons.No | TaskDialogCommonButtons.Cancel;
                taskdialog.CommonButtons = buttons;
                var taskdialogresult = taskdialog.Show();

                if (taskdialogresult == TaskDialogResult.Yes)
                {
                    return Result.Succeeded;
                }
                else if (taskdialogresult == TaskDialogResult.No)
                {
                    message = "failed to delete selection.";
                    return Result.Failed;
                }
                else
                {
                    return Result.Cancelled;
                }
            }
            catch
            {
                message = "unexpected exception thrown.";
                return Result.Cancelled;
            }
        }
    }
}
