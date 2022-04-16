using System;
using OfficeOpenXml;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Input;
using System.ComponentModel;

namespace Dalmatian.ROI
{
   class MouseState
   {
      public bool leftPressed = false;
      public bool rightPressed = false;
      public Point p_leftPressed = new Point(0,0);
      public Point p_rightPressed = new Point(0, 0);
      public Point p_last = new Point(0, 0);
   }

   public class ImageView : GUI.Items.Framework.ViewModelBase
   {
      public ImageView(string folder, BindingList<Segment> segmentsList)
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

         // Add segment button
         AddSegment = new GUI.Items.Framework.DelegateCommand((object param) => {
            var item = new FigureSegment("New segment");
            item.Thickness = CurrentThickness;
            mainCanvas.Children.Add(item.DrawSegment(imStartWidth, imStartHeight));
            ScaleItem(item.pathBox);
            SegmentsList.Add(item);
            SegmentIndex = SegmentsList.Count - 1;
            });

         // Delete segment button
         DeleteSegment = new GUI.Items.Framework.DelegateCommand((object param) => {
            if (SegmentIndex != 0)
            {
               mainCanvas.Children.Remove(SegmentsList[SegmentIndex].pathBox);
               SegmentsList.RemoveAt(SegmentIndex);
               SegmentIndex = SegmentsList.Count - 1;
            }
         });

         // Delete All button
         UpdateAllSegment = new GUI.Items.Framework.DelegateCommand((object param) => {
            CountAll();
         });

         // Confirm button
         ConfirmEditSegment = new GUI.Items.Framework.DelegateCommand((object param) => {
            SegmentsList[SegmentIndex].RenderSegment(imStartWidth, imStartHeight);
            SegmentsList[SegmentIndex].Count(SegmentsList[0].Get2DPoints());
         });

         // Reset button
         ResetScale = new GUI.Items.Framework.DelegateCommand((object param) => {
            ResetCenter();
            Reset();
         });
      }

      #region RENDER
      public void StartRender(UserControl renderCanvas)
      {
         mainImage.Loaded += EndRender;
         renderCanvas.Content = mainCanvas;

         // load central picture
         if (imageNames != null)
         {
            ImageIndex = imageNames.Length / 2;
         }

         // add handlers
         mainCanvas.AddHandler(Canvas.MouseMoveEvent, new MouseEventHandler(this.MouseMove), true);
         mainCanvas.AddHandler(Canvas.MouseLeftButtonDownEvent, new RoutedEventHandler(this.MouseLeftPress), true);
         mainCanvas.AddHandler(Canvas.MouseLeftButtonUpEvent, new RoutedEventHandler(this.MouseLeftUp), true);
         mainCanvas.AddHandler(Canvas.MouseRightButtonDownEvent, new RoutedEventHandler(this.MouseRightPress), true);
         mainCanvas.AddHandler(Canvas.MouseRightButtonUpEvent, new RoutedEventHandler(this.MouseRightUp), true);
         mainCanvas.AddHandler(Canvas.MouseLeaveEvent, new RoutedEventHandler(this.MouseLeave), true);
      }
      public void EndRender(object sender, EventArgs e)
      {
         imStartWidth = mainImage.ActualWidth;
         imStartHeight = mainImage.ActualHeight;

         // load Segments
         foreach(var item in SegmentsList)
         {
            mainCanvas.Children.Add(item.DrawSegment(imStartWidth, imStartHeight));
         }

         if ((imStartWidth > mainCanvas.ActualWidth) || (imStartHeight > mainCanvas.ActualHeight))
         {
            startScaleValue = Math.Max(Math.Min(mainCanvas.ActualWidth / imStartWidth,
                                      mainCanvas.ActualHeight / imStartHeight), с_minScale);
            Reset();
         }
         else
            ScaleAll();

         ResetCenter();
         CountAll();
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
         if (mouseState.rightPressed)
         {
            var p = Segment.DeScalePoint(e.GetPosition(SegmentsList[SegmentIndex].pathBox), scale);
            DragAll(mouseState.p_last - p);
            mouseState.p_last = p;
            return;
         }

         if (mouseState.leftPressed)
         {
            var p = Segment.DeScalePoint(e.GetPosition(SegmentsList[SegmentIndex].pathBox), scale);
            SegmentsList[SegmentIndex].gGroup.Children.Add(new LineGeometry(mouseState.p_last, p));
            mouseState.p_last = p;
            SegmentsList[SegmentIndex].AddPoint(p);
         }
      }
      public void MouseLeftPress(object sender, RoutedEventArgs e)
      {
         var p = Segment.DeScalePoint((e as MouseEventArgs).GetPosition(SegmentsList[SegmentIndex].pathBox), scale);
         SegmentsList[SegmentIndex].AddPoint(p);
         SegmentsList[SegmentIndex].gGroup.Children.Add(new LineGeometry(p, p));
         if (SegmentIndex == 0)
         {
            SegmentsList[0].Count(null);
         }
         else
         {
            mouseState.leftPressed = true;
         }
         
         mouseState.p_last = p;
      }
      public void MouseLeftUp(object sender, RoutedEventArgs e)
      {
         mouseState.leftPressed = false;
      }
      public void MouseRightPress(object sender, RoutedEventArgs e)
      {
         var p = Segment.DeScalePoint((e as MouseEventArgs).GetPosition(SegmentsList[SegmentIndex].pathBox), scale);
         mouseState.rightPressed = true;
         mouseState.p_last = p;
      }
      public void MouseRightUp(object sender, RoutedEventArgs e)
      {
         mouseState.rightPressed = false;
      }
      public void MouseLeave(object sender, RoutedEventArgs e)
      {
         mouseState.leftPressed = false;
         mouseState.rightPressed = false;
      }
      #endregion

      #region Drag
      public void DragAll(Vector dv)
      {
         foreach (var item in mainCanvas.Children)
         {
            Canvas.SetLeft((item as UIElement), Canvas.GetLeft((item as UIElement)) - dv.X * scaleValue);
            Canvas.SetTop((item as UIElement), Canvas.GetTop((item as UIElement)) - dv.Y * scaleValue);
         }
      }

      public void ResetCenter()
      {
         var dw = imStartWidth * scaleValue;
         var dh = imStartHeight * scaleValue;

         imPosition.X = (mainCanvas.ActualWidth - dw) / 2;
         imPosition.Y = (mainCanvas.ActualHeight - dh) / 2;

         foreach (var item in mainCanvas.Children)
         {
            Canvas.SetLeft((item as UIElement), imPosition.X);
            Canvas.SetTop((item as UIElement), imPosition.Y);
         }
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
      private void ScaleItem(object item)
      {
         var dw = imStartWidth * scaleValue;
         var dh = imStartHeight * scaleValue;

         imPosition.X = (mainCanvas.ActualWidth - dw) / 2;
         imPosition.Y = (mainCanvas.ActualHeight - dh) / 2;

         Canvas.SetLeft((item as UIElement), imPosition.X);
         Canvas.SetTop((item as UIElement), imPosition.Y);

         (item as FrameworkElement).Width = dw;
         (item as FrameworkElement).Height = dh;
      }

      public void IncreaseScale()
      {
         scale += delta_scale;
      }
      public void DecreaseScale()
      {
         scale -= delta_scale;
      }
      public void Reset()
      {
         scale = startScaleValue;
      }
      public double scale
      {
         get
         {
            return scaleValue;
         }

         set
         {
            if ((value != scaleValue) && (value >= с_minScale) && (value <= с_maxScale))
            {
               scaleValue = value;
               ScaleAll();
               OnPropertyChanged(nameof(ScaleIndex));
               OnPropertyChanged(nameof(ScaleView));
            }
         }
      }
      public double delta_scale
      {
         get
         {
            if (scaleValue > 0.2)
               return 0.1;
            return 0.01;
         }

      }
      public int ScaleIndex
      {
         get
         {
            return Convert.ToInt32(scaleValue * 100);
         }
         set
         {
            scale = Convert.ToDouble(value) / 100.0;
         }
      }

      public string ScaleView
      {
         get
         {
            return string.Format("{0:F2}", scale);
         }
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
         foreach (var item in SegmentsList)
         {
            item.Count(SegmentsList[0].Get2DPoints());
         }
      }
      public int Export(ExcelWorksheet worksheet, int x, int y)
      {
         CountAll();
         int count = 0;

         foreach (var item in SegmentsList)
         {
            worksheet.Cells[x, y].Value = item.Name;
            worksheet.Cells[x, y+1].Value = item.CellNumber;
            count++;
         }

         return count;
      }
      #endregion

      #region ComandControl
      public ICommand AddSegment { get; private set; } 
      public ICommand DeleteSegment { get; private set; }
      public ICommand UpdateAllSegment { get; private set; }
      public ICommand ConfirmEditSegment { get; private set; }
      public ICommand ResetScale { get; private set; }
      #endregion

      #region VARIABLES

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
      public Color CurrentColor
      {
         get
         {
            return SegmentsList[segmentIndex].ColorView;
         }
         set
         {
            SegmentsList[segmentIndex].ColorView = value;
         }
      }
      public Brush CurrentBrush
      {
         get
         {
            return SegmentsList[segmentIndex].BrushView;
         }
      }
      public string CurrentThicknessView
      {
         get
         {
            return string.Format("{0:F2}", SegmentsList[segmentIndex].Thickness);
         }
      }
      public double CurrentThickness
      {
         get
         {
            return SegmentsList[segmentIndex].Thickness;
         }
         set
         {
            foreach (var item in SegmentsList)
            {
               item.Thickness = value;
            }
            OnPropertyChanged("CurrentThickness");
            OnPropertyChanged("CurrentThicknessView");
         }
      }

      public BindingList<Segment> SegmentsList { get; set; }

      public const double с_minScale = 0.01;
      public const double с_maxScale = 10.0;
      public int ScaleCount { get; set; } = 1000;

      private Canvas mainCanvas;
      private Image mainImage;
      private Point imPosition = new Point();
      private Point canvasPosition = new Point();
      private double imStartWidth = 0;
      private double imStartHeight = 0;
      private string[] imageNames;
      private int imageIndex = 0;
      private int segmentIndex = 0;
      private double scaleValue = 1;
      private double startScaleValue = 1;
      private MouseState mouseState = new MouseState();
      #endregion
   }
}
