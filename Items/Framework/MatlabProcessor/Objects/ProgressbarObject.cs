using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GUI.Items.Framework.MatlabProcessor
{
   class ProgressbarObject : ISynchObject
   {
      public override void UpdateSource(string data)
      {
         var s = data.Split(delimiter);

         if (s.Length < 2)
            return;

         var v = Convert.ToDouble(s[0]) / Convert.ToDouble(s[1]) * MaxValue;

         if (Progress != v)
         {
            Progress = v;
            OnPropertyChanged(nameof(Progress));

            if (Progress == MaxValue)
               Thread.Sleep(500);
         }
      }

      public double Progress { get; set; } = 0;
      public double MaxValue { get; set; } = 1000;
   }
 }
