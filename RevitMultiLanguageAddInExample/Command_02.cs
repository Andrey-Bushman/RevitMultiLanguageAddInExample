/* RevitMultiLanguageAddInExample
 * Command_02.cs
 * © Andrey Bushman, 2017
 * https://revit-addins.blogspot.ru
 * 
 * The example of second localized command.
 */
using Autodesk.Revit.UI;
using System.Resources;
using Bushman.RevitMultiLanguageAddInExample.Properties;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;

namespace Bushman.RevitMultiLanguageAddInExample {

    [Transaction(TransactionMode.ReadOnly)]
    [Journaling(JournalingMode.NoCommandData)]
    class Command_02 : IExternalCommand {

        Result IExternalCommand.Execute(
            ExternalCommandData commandData,
            ref string message, ElementSet elements) {

            ResourceManager res_mng = new ResourceManager(
                typeof(Resources));

            TaskDialog.Show(res_mng.GetString(
                "TaskDialog_Title"), res_mng.GetString(
                    "TaskDialog_Message_02"));

            res_mng.ReleaseAllResources();

            return Result.Succeeded;
        }
    }
}
