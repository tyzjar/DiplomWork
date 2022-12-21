using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI.Items.Dalmatian
{
   public partial class Dalmatian : UserControl
   {
      CellCountConfig config;
      public Dalmatian(Framework.Data.MainData mainData_, IControl segmentationPanel, Framework.IHelper helper)
      {
         InitializeComponent();
         config = new CellCountConfig(mainData_, GridAndProcessPanel, segmentationPanel, helper);

         this.DataContext = config;
         SelectPanel.Content = new GUI.Items.Framework.SelectedPanel(mainData_.openSaveEvents);

         if (mainData_.dalmatian)
         {
            this.cExportCommonTable.Visibility = Visibility.Hidden;
            this.cSegmentsButton.Visibility = Visibility.Hidden;
         }
      }

   }
}
