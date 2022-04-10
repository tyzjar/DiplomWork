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
using System.Windows.Shapes;

namespace Dalmatian.ROI
{
   /// <summary>
   /// Interaction logic for ROIEdit.xaml
   /// </summary>
   public partial class ROIEdit : Window
   {
      ImageView imView;
      public ROIEdit(string folder, List<Segment> segmentsList)
      {
         InitializeComponent();
         var panel = new SegmentationPanel();
         SegmantationPanel.Content = panel;
         panel.SegmentsDataGrid.ItemsSource = segmentsList;

         imView = new ImageView(folder, segmentsList);
         imView.StartRender(MainCanvas);

         this.AddHandler(MainWindow.MouseWheelEvent, new RoutedEventHandler(this.MouseWheelHandler), true);
      }

      void MouseWheelHandler(object sender, RoutedEventArgs e)
      {
         if ((e as MouseWheelEventArgs).Delta > 0)
            imView.IncreaseScale();
         else
            imView.DecreaseScale();
      }
   }
}
