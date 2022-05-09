 /// <summary>
/// Возможные состояния образцов, вкладка препроцессинг.
/// </summary>
using System.IO;


namespace GUI.Items.Framework.Data.DataGrid
{

   public enum PreprocState
   {
      notStarted,
      failed,
      compleate
   }

   public class PreprocStateHelper
   { 

      public static PreprocState StateByFolder(string folderName)
      {
         if (Directory.Exists(folderName))
            return PreprocState.compleate;

         return PreprocState.notStarted;
      }

      public static PreprocState StateByFiles(string [] files)
      {
         foreach(var file in files)
            if (File.Exists(file))
               return PreprocState.compleate;

         return PreprocState.notStarted;
      }

      /// String values should be same in Matlab Preproc script
      public static string ShouldProcess (PreprocState state)
      {
         if ((state == PreprocState.notStarted) || (state == PreprocState.failed))
            return "y";
         else
            return "d";
      }
   };

}
