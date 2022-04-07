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
      public ROIEdit(object segmentationPanel)
      {
         InitializeComponent();
         SegmantationPanel.Content = segmentationPanel;
         imView = new ImageView(@"P:\DiplomRabota\Test_1\Masked", new List<Segment>());

         //var a  =MainCanvas.Children.Add(new Line { X1 = 1, Y1 = 1, X2 = 10000, Y2 = 10000,
         //   StrokeStartLineCap = PenLineCap.Round,
         //   StrokeEndLineCap = PenLineCap.Round,
         //   StrokeThickness = 1,
         //   Stroke = Brushes.Black
         //});
         //MainCanvas.Children.Remove

         imView.StartRender(MainCanvas);
         this.AddHandler(MainWindow.MouseWheelEvent, new RoutedEventHandler(this.MouseWheel_1), true);
      }
      protected override void OnMouseWheel(MouseWheelEventArgs e)
      {
         e.Handled = true;
         base.OnMouseWheel(e);
         if (e.Delta > 0)
            imView.IncreaseScale();
         else
            imView.DecreaseScale();
      }
      void MouseWheel_1(object sender, RoutedEventArgs e)
      {
         if ((e as MouseWheelEventArgs).Delta > 0)
            imView.IncreaseScale();
         else
            imView.DecreaseScale();
      }

      void MouseWheelHandler(object sender, MouseWheelEventArgs e)
      {

         //if (e.Delta > 0)
         //   imView.IncreaseScale();
         //else
         //   imView.DecreaseScale();
      }
   }
}
