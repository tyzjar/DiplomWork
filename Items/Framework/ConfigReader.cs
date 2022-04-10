using System.Collections.Generic;
using OfficeOpenXml;
using System.IO;


namespace GUI.Items.Framework
{
   public abstract class ConfigItem : ViewModelBase
   {
      public ConfigItem(string worksheetName)
      {
         WorksheetName = worksheetName;
      }
      public abstract List<ConfigItem> LoadConfig(ExcelWorksheet worksheet);
      public abstract List<ConfigItem> SaveConfig(ExcelWorksheet worksheet);
      public string WorksheetName;
   }


   public class ConfigReader
   {
      public static string delete_symbol(string s, char c)
      {
         int i = 0;
         while (( i = s.IndexOf(c)) >= 0 )
         {
            s = s.Remove(i, 1);
         }
         return s;
      }

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
            var it = item.LoadConfig(excelPackage.Workbook.Worksheets[item.WorksheetName]);
            if (it != null)
            {
               foreach (var item_ in it)
               {
                  if (item_ != null)
                  {
                     item_.LoadConfig(excelPackage.Workbook.Worksheets[item_.WorksheetName]);
                  }
               }
            }
         }
      }

      public void SaveXml(string fileName)
      {
         FileStream workFile = File.Create(fileName);
         ExcelPackage excelPackage = new ExcelPackage(workFile);

         foreach (var item in configItems)
         {
            var it = item.SaveConfig(excelPackage.Workbook.Worksheets.Add(item.WorksheetName));
            if (it != null)
            {
               foreach (var item_ in it)
               {
                  if (item_ != null)
                  {
                     item_.SaveConfig(excelPackage.Workbook.Worksheets.Add(item_.WorksheetName));
                  }
               }
            }
         }

         excelPackage.SaveAs(workFile);
         workFile.Close();
      }
   }
}
