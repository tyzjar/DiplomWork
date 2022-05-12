using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Items.Framework
{

   public class StandartExceptions : SystemException
   {
      public static StandartExceptions FolderDoesNotExists()
      {
         return new StandartExceptions("Folder did not found. Please check in the setting CellCount subfolder " +
               "and make sure it exists in sample folder. ", false);
      }

      public static StandartExceptions FilesDoesNotExists()
      {
         return new StandartExceptions("Files did not found. Please check in the setting CellCount subfolder " +
               "and make sure it have sample files. ", false);
      }

      public static StandartExceptions NoSelectedItem()
      {
         return new StandartExceptions("Please select sample before !", false);
      }

      public StandartExceptions(string msgr, bool critical_)
         : base(msgr)
      {
         critical = critical_;
      }

      public bool critical;
   }
}
