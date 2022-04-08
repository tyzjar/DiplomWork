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

         var l = new List<Segment>();
         var s = new Segment();
         s.AddPoint(1,1);
         s.AddPoint(300, 1);
         s.AddPoint(100, 100);
         s.AddPoint(112, 40);
         s.AddPoint(100, 11);
         s.AddPoint(500, 344);
         s.AddPoint(700, 20);

         l.Add(s);

         imView = new ImageView(@"P:\DiplomRabota\Test_1\Masked", l);
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
