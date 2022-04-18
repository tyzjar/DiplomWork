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
      public ImageView imView;
      public SegmentationPanel panel;
      public ROIEdit(string folder, BindingList<Segment> segmentsList)
      {
         InitializeComponent();

         panel = new SegmentationPanel();
         imView = new ImageView(folder, segmentsList);
         imView.StartRender(MainGrid, MainCanvas);

         panel.SegmentsDataGrid.ItemsSource = imView.SegmentsList;
         panel.onSegmentIndexChanged += imView.SegmentIndexUpdate;

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

      bool _shown;
      protected override void OnContentRendered(EventArgs e)
      {
         base.OnContentRendered(e);

         if (_shown)
            return;

         _shown = true;
         var eWindow = new EditPanel(this);
         eWindow.Show();

      }

   }
}
