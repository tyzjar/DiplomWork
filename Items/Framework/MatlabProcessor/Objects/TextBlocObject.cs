using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Items.Framework.MatlabProcessor
{
   class TextBlocObject : ISynchObject
   {
      public override void UpdateSource(string data)
      {
         Text = data;
         OnPropertyChanged(nameof(Text));
      }

      public string Text { get; set; }
   }
}
