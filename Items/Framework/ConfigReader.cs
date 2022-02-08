using System.Collections.Generic;
using OfficeOpenXml;
using System.IO;
using System;
using System.Windows;

namespace GUI.Items.Framework
{
   public abstract class ConfigItem : ViewModelBase
   {
      public ConfigItem(string worksheetName)
      {
         WorksheetName = worksheetName;
      }
      public abstract void LoadConfig(ExcelWorksheet worksheet);
      public abstract void SaveConfig(ExcelWorksheet worksheet);
      public string WorksheetName;
   }


   public class ConfigReader
   {
      List<ConfigItem> configItems = new List<ConfigItem>();

      public void AddItem(ConfigItem item)
      {
         configItems.Add(item);
      }

      public void OpenXml(string fileName)
      {
         FileInfo workFile = new FileInfo(fileName);
         ExcelPackage excelPackage = new ExcelPackage(workFile);

         foreach (var item in configItems)
         {
            item.LoadConfig(excelPackage.Workbook.Worksheets[item.WorksheetName]);
         }
      }

      public void SaveXml(string fileName)
      {
         FileStream workFile = File.Create(fileName);
         ExcelPackage excelPackage = new ExcelPackage(workFile);

         foreach (var item in configItems)
         {
            item.SaveConfig(excelPackage.Workbook.Worksheets.Add(item.WorksheetName));
         }

         excelPackage.SaveAs(workFile);
         workFile.Close();
      }
   }
}
