/* RevitMultiLanguageAddInExample
 * RevitPatches.cs
 * © Andrey Bushman, 2017
 * https://revit-addins.blogspot.ru/2017/01/revit-201711.html
 * 
 * RevitPatches class contains the custom patches of Revit API.
 */
using Autodesk.Revit.ApplicationServices;
using System;
using System.Globalization;
using System.Threading;

namespace Bushman.RevitMultiLanguageAddInExample {

    /// <summary>
    /// The static class with the Revit API patches.
    /// </summary>
    public static class RevitPatches {

        /// <summary>
        /// This patch fixes the bug of localization switching
        /// for Revit 2017.1.1. It switches the 'Thread
        /// .CurrentThread.CurrentUICulture' and 'Thread
        /// .CurrentThread.CurrentCulture' properties according 
        /// the UI localization of Revit current session.
        /// </summary>
        /// <param name="lang">The target language.</param>
        public static void PatchCultures(LanguageType lang) {

            if (!Enum.IsDefined(typeof(LanguageType), lang)) {
                throw new ArgumentException(nameof(lang));
            }

            string language;

            switch (lang) {
                case LanguageType.Unknown:
                    language = "";
                    break;
                case LanguageType.English_USA:
                    language = "en-US";
                    break;
                case LanguageType.German:
                    language = "de-DE";
                    break;
                case LanguageType.Spanish:
                    language = "es-ES";
                    break;
                case LanguageType.French:
                    language = "fr-FR";
                    break;
                case LanguageType.Italian:
                    language = "it-IT";
                    break;
                case LanguageType.Dutch:
                    language = "nl-BE";
                    break;
                case LanguageType.Chinese_Simplified:
                    language = "zh-CHS";
                    break;
                case LanguageType.Chinese_Traditional:
                    language = "zh-CHT";
                    break;
                case LanguageType.Japanese:
                    language = "ja-JP";
                    break;
                case LanguageType.Korean:
                    language = "ko-KR";
                    break;
                case LanguageType.Russian:
                    language = "ru-RU";
                    break;
                case LanguageType.Czech:
                    language = "cs-CZ";
                    break;
                case LanguageType.Polish:
                    language = "pl-PL";
                    break;
                case LanguageType.Hungarian:
                    language = "hu-HU";
                    break;
                case LanguageType.Brazilian_Portuguese:
                    language = "pt-BR";
                    break;
                default:
                    // Maybe new value of the enum hasn't own 
                    // `case`...
                    throw new ArgumentException(nameof(lang));
                    break;
            }

            CultureInfo ui_culture = new CultureInfo(language);
            CultureInfo culture = new CultureInfo(language);

            Thread.CurrentThread.CurrentUICulture = ui_culture;
            Thread.CurrentThread.CurrentCulture = culture;
        }
    }
}
