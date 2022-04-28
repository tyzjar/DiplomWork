using System;

namespace Dalmatian.Helper
{
   class HelperControl : GUI.Items.Framework.IHelper
   {
      public override void StartHelp(int descriptor)
      {
         switch (descriptor)
         {
            // HelpStandartFilter
            case 1:
               {
                  var sWindow = new Help(new StandartFilterHelp());
                  sWindow.ShowDialog();
                  break;
               }
            // HelpThreshold
            case 2:
               {
                  var sWindow = new Help(new ThresholdHelp());
                  sWindow.ShowDialog();
                  break;
               }

            // HelpManual
            case 3:
               {
                  var sWindow = new Help(new ManualHelp());
                  sWindow.ShowDialog();
                  break;
               }

         }
      }
   }
}
