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
   /// Interaction logic for SegemntationPanel.xaml
   /// </summary>
   public partial class SegmentationPanel : UserControl
   {
      public delegate void UpdateHandler(int newValue);
      public event UpdateHandler onSegmentIndexChanged = (int i) => { };

      public SegmentationPanel()
      {
         InitializeComponent();

         //SegmentsDataGrid.SelectionChanged += (object sender, SelectionChangedEventArgs e) => {
         //   if ((SegmentsDataGrid.SelectedItem as Segment) != null)
         //   {
         //      onSegmentIndexChanged(SegmentsDataGrid.SelectedIndex);
         //   }
         //};
      }
   }
}
