using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Items.Framework
{
   public abstract class IHelper
   {
      public abstract void StartHelp(int descriptor);
   }


   public class HelperControl : IHelper
   {
      public override void StartHelp(int descriptor) { }
   }
}
