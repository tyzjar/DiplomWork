using System;
using System.Collections.Generic;
using System.ComponentModel;
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
      SegmentationPanel panel;
      public ROIEdit(string folder, BindingList<Segment> segmentsList)
      {
         InitializeComponent();

         panel = new SegmentationPanel();
         SegmantationPanel.Content = panel;

         imView = new ImageView(folder, segmentsList);
         imView.StartRender(MainCanvas);

         panel.SegmentsDataGrid.ItemsSource = imView.SegmentsList;
         panel.onSegmentIndexChanged += imView.SegmentIndexUpdate;
         //panel.SegmentsDataGrid.SetBinding(DataGrid.ItemsSourceProperty, imView.SegmentsList);


         this.DataContext = imView;

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
