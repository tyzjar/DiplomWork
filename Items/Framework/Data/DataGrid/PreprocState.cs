 /// <summary>
/// Возможные состояния образцов, вкладка препроцессинг.
/// </summary>
using System.IO;


namespace GUI.Items.Framework.Data.DataGrid
{

   public class PreprocState
   { 
      public enum States
      {
         notStarted,
         failed,
         compleate
      }

      public PreprocState()
      { 
         state = States.notStarted;
      }

      public PreprocState(string beginState)
      { 
         setStateFromString(beginState);
      }

      public States state { get; set; }

      public static string getStringState(States state_)
      { 
         var s = "";
         switch(state_)
         {
            case States.notStarted : s = "not started"; break;
            case States.failed: s = "failed"; break;
            case States.compleate: s = "compleate"; break;
         }
         return s;
      }

      public static string StateByFolder(string folderName)
      {
         if (Directory.Exists(folderName))
            return getStringState(States.compleate);

         return getStringState(States.notStarted);
      }

      public void setStateFromString(string newState)
      { 
         switch(newState)
         {
            case "not started": state = States.notStarted; break;
            case "failed": state = States.failed; break;
            case "compleate": state = States.compleate; break;
            default: break;
         }
      }

      /// String values should be same in Matlab Preproc script
      public string ShouldProcess 
      {
         get 
         {
            if ((state == States.notStarted) || (state == States.failed))
               return "y";
            else
               return "d";
         }
      }
   };

}
