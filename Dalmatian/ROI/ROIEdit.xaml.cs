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
      public ImageFormControl imFormControl;
      public ROIEdit(string folder, BindingList<Segment> segmentsList)
      {
         InitializeComponent();
         imFormControl = new ImageFormControl(folder, segmentsList, MainGrid);

         this.AddHandler(MainWindow.MouseWheelEvent, new RoutedEventHandler(imFormControl.MouseWheelHandler), true);
         MainGrid.AddHandler(MainWindow.MouseMoveEvent, new MouseEventHandler(imFormControl.MouseMove), true);
         MainGrid.AddHandler(MainWindow.MouseRightButtonDownEvent, new MouseButtonEventHandler(imFormControl.MouseRightPress), true);
         MainGrid.AddHandler(MainWindow.MouseRightButtonUpEvent, new MouseButtonEventHandler(imFormControl.MouseRightUp), true);
         //this.AddHandler(MainWindow.MouseLeaveEvent, new RoutedEventHandler(this.MouseLeave), true);
         this.DataContext = imFormControl;
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
