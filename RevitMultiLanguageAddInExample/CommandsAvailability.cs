/* RevitMultiLanguageAddInExample
 * CommandsAvailability.cs
 * © Andrey Bushman, 2017
 * https://revit-addins.blogspot.ru
 * 
 * The checking of the commands availability.
 */
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;

namespace Bushman.RevitMultiLanguageAddInExample {
    class CommandsAvailability :
        IExternalCommandAvailability {

        bool IExternalCommandAvailability.IsCommandAvailable(
            UIApplication applicationData,
            CategorySet selectedCategories) {

            if (selectedCategories.IsEmpty)
                return true;
            else
                return false;
        }
    }
}
