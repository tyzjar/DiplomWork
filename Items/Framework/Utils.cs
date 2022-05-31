using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Items.Framework
{
   public static class Utils
   {
      public delegate void FolderSetter(string value);
      public static string format = "*.tif";
      public static char[] delims = { '\\', '/' };
      public static void SelectFolder(FolderSetter folderSetter)
      {
         // Create a "Save As" dialog for selecting a directory (HACK)
         var dialog = new Microsoft.Win32.SaveFileDialog();
         dialog.Title = "Select a Directory"; // instead of default "Save As"
         dialog.Filter = "Directory|*.this.directory"; // Prevents displaying files
         dialog.FileName = "select"; // Filename will then be "select.this.directory"
         if (dialog.ShowDialog() == true)
         {
            string path = dialog.FileName;
            // Remove fake filename from resulting path
            path = path.Replace("\\select.this.directory", "");
            path = path.Replace(".this.directory", "");
            // If user has changed the filename, create the new directory
            if (!System.IO.Directory.Exists(path))
            {
               System.IO.Directory.CreateDirectory(path);
            }
            // Our final value is in path
            folderSetter(path);
         }
      }

      public static bool CheckFolderForTifFiles(string folder)
      {
         if (Directory.Exists(folder))
            return Directory.EnumerateFiles(folder, format).Any();

         throw (StandartExceptions.FolderDoesNotExists(folder));
      }

      public static bool CheckFolderForTifFilesNoThrow(string folder)
      {
         if (Directory.Exists(folder))
            return Directory.EnumerateFiles(folder, format).Any();

         return false;
      }


      public static void RemoveSubfolder(ref string folder, string subfolder)
      {
         var dels = new char[] { '/', '\\' };
         folder = folder.Trim(dels);

         foreach (var del in dels)
         {
            var fld = folder.Split(del);
            if ((fld.Length > 0) && Equals(fld[fld.Length - 1], subfolder))
            {
               var i = folder.LastIndexOf(subfolder);
               if (i > 0)
               {
                  folder = folder.Substring(0, i);
                  folder = folder.Trim(del);
                  return;
               }
            }
         }
      }

      public static string CreateSaveName(string sampleFrom, string sampleTo)
      {
         var dels = new char[] { '/', '\\' };
         var mainDel = ';';

         sampleFrom = sampleFrom.Trim(dels);
         sampleTo = sampleTo.Trim(dels);

         foreach (var del in dels)
         {
            sampleFrom = sampleFrom.Replace(del, mainDel);
            sampleTo = sampleTo.Replace(del, mainDel);
         }

         var sf = sampleFrom.Split(mainDel);
         var st = sampleTo.Split(mainDel);

         if ((sf.Length > 0) && (st.Length > 0))
         {
            return sf[sf.Length - 1] +"_to_"+ st[st.Length - 1];
         }

         return "Default file name";
      }
   }
}
