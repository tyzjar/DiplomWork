using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Input;

namespace Dalmatian.ROI
{
   class MouseState
   {
      public bool leftPressed = false;
      public Point p_leftPressed = new Point(0,0);
      public Point p_last = new Point(0, 0);
   }

   class ImageView : GUI.Items.Framework.ViewModelBase
   {
      public ImageView(string folder, List<Segment> segmentsList)
      {
         SegmentsList = segmentsList;

         mainCanvas = new Canvas();
         mainImage = new Image();

         mainCanvas.Children.Add(mainImage);

         // create list of images
         if (System.IO.Directory.Exists(folder))
            imageNames = System.IO.Directory.GetFiles(folder, "*.tif");
         else
            throw (GUI.Items.Framework.StandartExceptions.FolderDoesNotExists());

         // Reference to comands
         AddSegment = new GUI.Items.Framework.DelegateCommand((object param) => {
            SegmentsList.Add(new FigureSegment("New segment"));
            OnPropertyChanged(nameof(SegmentsList));
            });

         DeleteSegment = new GUI.Items.Framework.DelegateCommand((object param) => {
            if (SegmentIndex != 0)
            {
               SegmentsList.RemoveAt(SegmentIndex);
               OnPropertyChanged(nameof(SegmentsList));
            }
         });

         UpdateAllSegment = new GUI.Items.Framework.DelegateCommand((object param) => {
            CountAll();
            OnPropertyChanged(nameof(SegmentsList));
         });

         ConfirmEditSegment = new GUI.Items.Framework.DelegateCommand((object param) => {
            SegmentsList[SegmentIndex].RenderSegment(imStartWidth, imStartHeight, scale);
         });
      }

      #region RENDER
      public void StartRender(UserControl renderCanvas)
      {
         renderCanvas.Content = mainCanvas;
         mainImage.Loaded += EndRender;

         // load central picture
         if (imageNames != null)
         {
            ImageIndex = imageNames.Length / 2;
         }

         // add handlers
         mainCanvas.AddHandler(Canvas.MouseMoveEvent, new MouseEventHandler(this.MouseMove), true);
         mainCanvas.AddHandler(Canvas.MouseLeftButtonDownEvent, new RoutedEventHandler(this.MousePress), true);
         mainCanvas.AddHandler(Canvas.MouseLeftButtonUpEvent, new RoutedEventHandler(this.MouseUp), true);
         mainCanvas.AddHandler(Canvas.MouseLeaveEvent, new RoutedEventHandler(this.MouseLeave), true);
      }
      public void EndRender(object sender, EventArgs e)
      {
         imStartWidth = mainImage.ActualWidth;
         imStartHeight = mainImage.ActualHeight;

         // load Segments
         SegmentsList.ForEach((Segment a) => { mainCanvas.Children.Add(a.DrawSegment(imStartWidth, imStartHeight, scale)); });
         ScaleAll();
      }
      private void RenderPictures()
      {
         // load Image
         mainImage.Source = new BitmapImage(new Uri(imageNames[ImageIndex]));
      }
      #endregion

      #region MOUSE CONTROL
      public void MouseMove(object sender, MouseEventArgs e)
      {
         if (mouseState.leftPressed)
         {
            var p = Segment.ScalePoint(e.GetPosition(SegmentsList[SegmentIndex].pathBox), scale);
            SegmentsList[SegmentIndex].gGroup.Children.Add(new LineGeometry(mouseState.p_last, p));
            mouseState.p_last = p;
            SegmentsList[SegmentIndex].AddPoint(p);
         }
      }
      public void MousePress(object sender, RoutedEventArgs e)
      {
         var p = Segment.ScalePoint((e as MouseEventArgs).GetPosition(SegmentsList[SegmentIndex].pathBox), scale);
         SegmentsList[SegmentIndex].AddPoint(p);

         if (SegmentIndex == 0)
         {
            SegmentsList[SegmentIndex].gGroup.Children.Add(new LineGeometry(p, p));
         }
         else
         {
            SegmentsList[SegmentIndex].AddPoint(p);
            mouseState.leftPressed = true;
         }
         
         mouseState.p_last = p;
      }
      public void MouseUp(object sender, RoutedEventArgs e)
      {
         mouseState.leftPressed = false;
      }
      public void MouseLeave(object sender, RoutedEventArgs e)
      {
         mouseState.leftPressed = false;
      }
      #endregion

      #region SCALE
      private void ScaleAll()
      {
         var dw = imStartWidth * scaleValue;
         var dh = imStartHeight * scaleValue;

         imPosition.X = (mainCanvas.ActualWidth - dw) / 2;
         imPosition.Y = (mainCanvas.ActualHeight - dh) / 2;

         foreach (var item in mainCanvas.Children)
         {
            Canvas.SetLeft((item as UIElement), imPosition.X);
            Canvas.SetTop((item as UIElement), imPosition.Y);

            (item as FrameworkElement).Width = dw;
            (item as FrameworkElement).Height = dh;
         }
      }
      public void IncreaseScale()
      {
         scale += 0.1;
      }
      public void DecreaseScale()
      {
         scale -= 0.1;
      }
      public void Reset()
      {
         scale = 1;
      }
      #endregion

      #region SegmentsList
      public void SegmentIndexUpdate(int newValue)
      {
         SegmentsList[SegmentIndex].Count(SegmentsList[0].Get2DPoints());
         SegmentIndex = newValue;
      }
      public void CountAll()
      {
         SegmentsList.ForEach((Segment s) => { s.Count(SegmentsList[0].Get2DPoints()); });
      }
      #endregion

      #region ComandControl
      public ICommand AddSegment { get; private set; } 
      public ICommand DeleteSegment { get; private set; }
      public ICommand UpdateAllSegment { get; private set; }
      public ICommand ConfirmEditSegment { get; private set; }
      #endregion

      #region VARIABLES
      public double scale
      {
         get
         {
            return scaleValue;
         }

         set
         {
            if ((value != scaleValue) && (value > 0.2) && (value < 10))
            {
               scaleValue = value;
               ScaleAll();
            }
         }
      }
      public int ImageIndex 
      {
         get
         {
            return imageIndex;
         }
         set
         {
            if ((value >= 0) && (value < imageNames.Length))
            {
               imageIndex = value;
               RenderPictures();
               OnPropertyChanged(nameof(ImageIndex));
            }
         }
      }
      public int ImageCount
      {
         get
         {
            return imageNames != null ? imageNames.Length - 1 : 0;
         }
      }
      public int SegmentIndex
      {
         get
         {
            return segmentIndex;
         }
         set
         {
            if ((value >= 0) && (value < SegmentsList.Count))
            {
               segmentIndex = value;
            }
         }
      }
      public List<Segment> SegmentsList { get; set; }
      private Canvas mainCanvas;
      private Image mainImage;
      private Point imPosition = new Point();
      private double imStartWidth = 0;
      private double imStartHeight = 0;
      private string[] imageNames;
      private int imageIndex = 0;
      private int segmentIndex = 0;
      private double scaleValue = 1;
      private MouseState mouseState = new MouseState();
      #endregion
   }
}
