using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;

namespace GUI.Items.Dalmatian
{
   
   public class LoadSegments
   {
      public static string FileName = "struct.json";
      public delegate void LoadEndEventHandler();
      public event LoadEndEventHandler onLoadEnd = () => {};

      public void AddRangeSegments(Segment segment)
      {
         if (segment.Childrens != null)
         {
            foreach (var item in segment.Childrens)
            {
               AddRangeSegments(item);
            }
         }

         ViewSegments.AddRange(segment.Childrens);
      }

      public async void ReadJson()
      {
         try
         {
            
            var fullName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\" + FileName;
            using (StreamReader reader = new StreamReader(fullName))
            {
               // Reads all characters from the current position to the end of the stream asynchronously
               // and returns them as one string.
               string jsonString = await reader.ReadToEndAsync();
               var rootSegment = JsonConvert.DeserializeObject(jsonString, typeof(Segment), settings) as Segment;
               AddRangeSegments(rootSegment);
            }
            onLoadEnd();
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message, "Exeption",
               MessageBoxButton.OK, MessageBoxImage.Error);
         }
}

      public List<Segment> ViewSegments = new List<Segment>();
      private JsonSerializerSettings settings = new JsonSerializerSettings
      {
         Formatting = Formatting.Indented
      };
   }
}
