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
   /// <summary>
   /// Interaction logic for GridPanel.xaml
   /// </summary>
   public partial class GridPanel : UserControl
   {
      public GridPanel(object segmentationPanel, bool dalmatian)
      {
         InitializeComponent();
         SegmantationPanel.Content = segmentationPanel;
         AtlasStateColumn.Visibility = dalmatian ? Visibility.Hidden : Visibility.Visible;
      }
   }
}
