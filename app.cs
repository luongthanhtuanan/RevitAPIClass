using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System.Reflection;
using System.IO;

namespace WpfControlLibrary1
{
    class app : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            application.CreateRibbonTab("Quyet");
            var pannel = application.CreateRibbonPanel("Quyet", "Hello World");

            var buttondata = new PushButtonData("button1", 
                "Hello World",
                Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),"WpfControlLibrary1.dll"), 
                "WpfControlLibrary1.command");
            pannel.AddItem(buttondata);

            var buttondata1 = new PushButtonData("button2",
                 "Hello World",
                 Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "WpfControlLibrary1.dll"),
                 "WpfControlLibrary1.command1");

            pannel.AddItem(buttondata1);

            return Result.Succeeded;
        }
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}
