using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System;
using System.Windows;

namespace GUI.Items.Framework
{
   // Класс для наследования сохранённых переменных
   public abstract class ConfigItem : ViewModelBase
   {
      public ConfigItem(string name)
      {
         ConfigName = name;
      }

      public class SaveVariables
      { }
      public abstract void SetVariables(SaveVariables v);
      public abstract SaveVariables GetVariables();
      public string ConfigName;
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

      public void AddItem(ConfigItem item)
      {
         configItems.Add(item.ConfigName, item);
      }
      public async void OpenProject(string fileName)
      {
         try
         {
            using (StreamReader reader = new StreamReader(@fileName))
            {
               // Reads all characters from the current position to the end of the stream asynchronously
               // and returns them as one string.
               string jsonString = await reader.ReadToEndAsync();
               var items = JsonConvert.DeserializeObject(jsonString, settings) as Dictionary<string, ConfigItem.SaveVariables>;
               foreach (var item in items)
               {
                  configItems[item.Key].SetVariables(item.Value);
               }
            }
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message, "Exeption",
               MessageBoxButton.OK, MessageBoxImage.Error);
         }
      }
      public async void SaveProject(string fileName)
      {
         try
         {
            using (StreamWriter writer = new StreamWriter(@fileName))
            {
               Dictionary<string, ConfigItem.SaveVariables> save_items = new Dictionary<string, ConfigItem.SaveVariables>();
               foreach (var item in configItems)
               {
                  save_items.Add(item.Key, item.Value.GetVariables());
               }
               var jsonString = JsonConvert.SerializeObject(save_items, settings);
               await writer.WriteAsync(jsonString);
            }
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message, "Exeption",
               MessageBoxButton.OK, MessageBoxImage.Error);
         }
      }


      private JsonSerializerSettings settings = new JsonSerializerSettings
      {
         TypeNameHandling = TypeNameHandling.Objects,
         Formatting = Formatting.Indented
      };


      private Dictionary<string,ConfigItem> configItems = new Dictionary<string, ConfigItem> ();
   }
}
