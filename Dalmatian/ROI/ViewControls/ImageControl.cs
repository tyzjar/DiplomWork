using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Dalmatian.ROI
{
   public class ImageFormControl : GUI.Items.Framework.ViewModelBase
   {
      public ImageView imView;
      public SegmentationPanel panel;

      MouseState mouseState = new MouseState();
      UserControl mainCanvas = new UserControl();

      Grid MainControl;

      public ImageFormControl(string folder, BindingList<Segment> segmentsList, Grid mainControl)
      {
         MainControl = mainControl;

         panel = new SegmentationPanel();
         imView = new ImageView(folder, segmentsList);


         MainControl.Children.Add(mainCanvas);
         imView.StartRender(mainCanvas);

         panel.SegmentsDataGrid.ItemsSource = imView.SegmentsList;
         panel.onSegmentIndexChanged += imView.SegmentIndexUpdate;

         // Reference to comands

         // Add segment button
         AddSegment = new GUI.Items.Framework.DelegateCommand((object param) => {
            imView.AddSegment();
         });

         // Delete segment button
         DeleteSegment = new GUI.Items.Framework.DelegateCommand((object param) => {
            imView.DeleteSegment();
         });

         // Delete All button
         UpdateAllSegment = new GUI.Items.Framework.DelegateCommand((object param) => {
            imView.CountAll();
         });

         // Confirm button
         ConfirmEditSegment = new GUI.Items.Framework.DelegateCommand((object param) => {
            imView.Confirm();
         });

         // Reset button
         ResetScale = new GUI.Items.Framework.DelegateCommand((object param) => {
            ImageCenter();
            imView.Reset();
         });
      }

      public void MouseWheelHandler(object sender, RoutedEventArgs e)
      {
         if ((e as MouseWheelEventArgs).Delta > 0)
            imView.IncreaseScale();
         else
            imView.DecreaseScale();
      }


      #region ComandControl
      public ICommand AddSegment { get; private set; }
      public ICommand DeleteSegment { get; private set; }
      public ICommand UpdateAllSegment { get; private set; }
      public ICommand ConfirmEditSegment { get; private set; }
      public ICommand ResetScale { get; private set; }
      #endregion


      public void ImageCenter()
      {
         var transform = mainCanvas.RenderTransform as TranslateTransform;
         if (transform == null)
         {
            transform = new TranslateTransform();
            mainCanvas.RenderTransform = transform;
         }
         transform.X = 0;
         transform.Y = 0;
      }


      public void MouseMove(object sender, MouseEventArgs e)
      {
         var draggableControl = mainCanvas;


         if (mouseState.b_pressed && draggableControl != null)
         {
            Point currentPosition = e.GetPosition(MainControl);

            var transform = draggableControl.RenderTransform as TranslateTransform;
            if (transform == null)
            {
               transform = new TranslateTransform();
               draggableControl.RenderTransform = transform;
            }

            transform.X = currentPosition.X - mouseState.p_last.X;
            transform.Y = currentPosition.Y - mouseState.p_last.Y;
         }
      }
      public void MouseRightPress(object sender, RoutedEventArgs e)
      {
         var draggableControl = mainCanvas;
         mouseState.p_last = (e as MouseEventArgs).GetPosition(mainCanvas);
         draggableControl.CaptureMouse();
         mouseState.b_pressed = true;
      }
      public void MouseRightUp(object sender, RoutedEventArgs e)
      {
         mouseState.b_pressed = false;
         mainCanvas.ReleaseMouseCapture();
      }

   }
}
