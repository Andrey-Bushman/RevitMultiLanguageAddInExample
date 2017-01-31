/* RevitMultiLanguageAddInExample
 * © Andrey Bushman, 2017
 * https://revit-addins.blogspot.ru
 * 
 * ExternalApplication.cs
 * 
 * The external application entry point.
 */
using System;
using Autodesk.Revit.UI;
using System.Reflection;
using System.Windows.Media.Imaging;
using System.Resources;
using Bushman.RevitMultiLanguageAddInExample.Properties;
using System.Windows;
using System.Windows.Interop;
using System.Drawing;
using System.IO;

namespace Bushman.RevitMultiLanguageAddInExample {
    class ExternalApplication : IExternalApplication {

        Result IExternalApplication.OnShutdown(
            UIControlledApplication application) {

            return Result.Succeeded;
        }

        Result IExternalApplication.OnStartup(
            UIControlledApplication application) {

            RevitPatches.PatchCultures(application
                .ControlledApplication.Language);

            // Этот класс будет извлекать ресурсы, локализация 
            // которых соответствует локализации 
            // `Thread.CurrentThread.CurrentUICulture`. Если
            // соответствующей нет, то будет использоваться та, 
            // которая назначена используемой по умолчанию 
            // (default).
            ResourceManager res_mng = new ResourceManager(
                typeof(Resources));

            string tab_name = res_mng.GetString(
                    "Ribbon_tab_name");

            application.CreateRibbonTab(tab_name);

            string panel_name = res_mng.GetString(
                    "Ribbon_panel_name");

            RibbonPanel ribbon_panel = application
                .CreateRibbonPanel(tab_name, panel_name);

            string this_assembly_path = Assembly
                .GetExecutingAssembly().Location;

            // **********************************
            // Из локализованных ресурсов получаем имя нужного
            // локализованного файла справки
            string help_file = Path.Combine(Path
                .GetDirectoryName(this_assembly_path),
                res_mng.GetString("HelpFileName"));

            // Формируем информацию, связывающую конкретную 
            // команду с конкретным разделом справочной системы
            ContextualHelp help = new ContextualHelp(
                ContextualHelpType.ChmFile, help_file);

            help.HelpTopicUrl = "cmd_01"
                ;

            // Создаём кнопку на палитре инструментов
            AddRibbonItem(ribbon_panel,
                "Command_01",
                "Command_01_text",
                "Command_01_tooltip",
                "Command_01_long_description",
                "cmd_01_32x32",
                "flower_01",
                typeof(Command_02),
                typeof(CommandsAvailability),
                help);

            // **********************************
            // Из локализованных ресурсов получаем имя нужного
            // локализованного файла справки
            help_file = Path.Combine(Path
                .GetDirectoryName(this_assembly_path),
                res_mng.GetString("HelpFileName"));

            // Формируем информацию, связывающую конкретную 
            // команду с конкретным разделом справочной системы
            help = new ContextualHelp(
                ContextualHelpType.ChmFile, help_file);
            help.HelpTopicUrl = "cmd_02";

            // Создаём кнопку на палитре инструментов
            AddRibbonItem(ribbon_panel,
                "Command_02",
                "Command_02_text",
                "Command_02_tooltip",
                "Command_02_long_description",
                "cmd_02_32x32",
                "flower_02",
                typeof(Command_01),
                typeof(CommandsAvailability),
                help);

            res_mng.ReleaseAllResources();

            return Result.Succeeded;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="panel">Панель, на которой следует
        /// разместить добавляемую кнопку.</param>
        /// <param name="cmd_name">Имя команды</param>
        /// <param name="cmd_text">Ключ ресурса, содержащего 
        /// отображаемое имя команды.
        /// </param>
        /// <param name="cmd_tooltip">Ключ ресурса, содержащего
        /// краткое описание команды.
        /// </param>
        /// <param name="long_description">Ключ ресурса, 
        /// содержащего развёрнутое описание команды.</param>
        /// <param name="large_img_name">Ключ ресурса, 
        /// содержащего изображение, располагающееся на новой 
        /// кнопке.</param>
        /// <param name="tooltip_img_name">Ключ ресурса, 
        /// содержащего изображение, отображаемое в развёрнутом
        /// описании команды.</param>
        /// <param name="cmd_type">Тип, определяющий команду.
        /// </param>
        /// <param name="aviability_type">Тип, выполняющий 
        /// проверку доступности команды.</param>
        /// <param name="help">Настройки связи создаваемой 
        /// кнопки с конкретным разделом справочной системы.
        /// </param>
        void AddRibbonItem(RibbonPanel panel,
            string cmd_name, string cmd_text,
            string cmd_tooltip, string long_description,
            string large_img_name,
            string tooltip_img_name,
            Type cmd_type, Type aviability_type,
            ContextualHelp help) {

            string this_assembly_path = Assembly
                .GetExecutingAssembly().Location;

            ResourceManager res_mng = new ResourceManager(
                typeof(Resources));

            string text = res_mng.GetString(cmd_text);

            PushButtonData button_data = new PushButtonData(
                cmd_name, text,
                this_assembly_path, cmd_type.FullName);

            button_data.AvailabilityClassName =
                aviability_type.FullName;

            PushButton push_button = panel.AddItem(
                button_data) as PushButton;

            string tooltip = res_mng.GetString(cmd_tooltip);

            push_button.ToolTip = tooltip;

            Bitmap large_image = (Bitmap)res_mng.GetObject(
                large_img_name);

            BitmapSource large_bitmap_src = Imaging
                .CreateBitmapSourceFromHBitmap(
                large_image.GetHbitmap(), IntPtr.Zero,
                Int32Rect.Empty, BitmapSizeOptions
                .FromEmptyOptions());

            push_button.LargeImage = large_bitmap_src;

            Bitmap tooltip_image = (Bitmap)res_mng.GetObject(
                tooltip_img_name);

            BitmapSource tooltip_bitmap_src = Imaging
                .CreateBitmapSourceFromHBitmap(
                tooltip_image.GetHbitmap(), IntPtr.Zero,
                Int32Rect.Empty, BitmapSizeOptions
                .FromEmptyOptions());

            push_button.ToolTipImage = tooltip_bitmap_src;

            push_button.LongDescription = res_mng.GetString(
                long_description);

            push_button.SetContextualHelp(help);

            res_mng.ReleaseAllResources();
        }
    }
}
