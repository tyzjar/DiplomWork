using System.ComponentModel;


namespace GUI.Items.Framework.Data.DataGrid
{
   public class AtlasTransformationFile
   {
      public AtlasTransformationFile()
      { }

      public AtlasTransformationFile(string name)
      {
         FileName = name;
      }

      public string FileName { get; set; } = "";
   }
}
