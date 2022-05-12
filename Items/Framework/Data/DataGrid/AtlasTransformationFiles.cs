using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Items.Framework.Data.DataGrid
{
   public class AtlasTransformationFiles
   {
      public void Reload(string TFile)
      {
         TransformationFiles.Clear();
         TransformationFiles.Add(TFile);
      }
      public BindingList<string> TransformationFiles { get; set; } = new BindingList<string>();
   }
}
