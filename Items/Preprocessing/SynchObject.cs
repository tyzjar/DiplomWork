using System;
using System.Windows;

namespace GUI.Items.Preprocessing
{
   class SynchObject : Framework.MatlabProcessor.ISynchObject
   {
      public override void UpdateSource(string data)
      {
         MessageBox.Show(data);
      }

       
   }
}
