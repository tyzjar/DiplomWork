using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Items.Framework.MatlabProcessor
{
   abstract class ISynchObject : ViewModelBase
   {
      public abstract void UpdateSource(string data);

      public static char delimiter = ';';
   }
}
