using System;
using OfficeOpenXml;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Input;
using System.ComponentModel;

namespace Dalmatian.ROI
{
   public delegate void VoidHandler();

   public class MouseState
   {
      public bool b_pressed = false;
      public Point p_pressed = new Point(0,0);
      public Point p_last = new Point(0, 0);
   }

   class ImageState
   {
      public event VoidHandler onScale = () => { };

      public ImageState(double w, double h, double s)
      {
         ImStartWidth = w;
         ImStartHeight = h;

         scale = s;
         StartScale = s;
      }

      public void ResetImageState(double w, double h)
      {
         if (ImCurrentWidht > 0)
            if (ImStartWidth != w)
               MessageBox.Show("Image size was changed. PLease check the sample!",
                  "Exeption", MessageBoxButton.OK, MessageBoxImage.Warning);

         scale = ImCurrentWidht / w;

         ImStartWidth = w;
         ImStartHeight = h;
         onScale();
      }

      public void SetScaleBySize(double newW, double newH)
      {
         Scale = newW / ImStartWidth;
      }

      public double ImCurrentWidht
      {
         get
         {
            return ImStartWidth * scale;
         }
      }
      public double ImCurrentHeight
      {
         get
         {
            return ImStartHeight * scale;
         }
      }

      public double Scale
      {
         get
         {
            return scale;
         }
         set
         {
            if ((value != scale) && (value >= с_minScale) && (value <= с_maxScale))
            {
               scale = value;
               onScale();
            }
         }
      }

      public double ImStartWidth = 0;
      public double ImStartHeight = 0;
      public double StartScale = 1; // need for reset function

      public const double с_minScale = 0.01;
      public const double с_maxScale = 10.0;

      private double scale = 1;
   }

   public class ImageView : GUI.Items.Framework.ViewModelBase
   {
      public ImageView(string folder, BindingList<Segment> segmentsList_)
      {
         segmentsList = segmentsList_;

         mainCanvas = new Canvas();
         mainImage = new Image();
         imState = new ImageState(0, 0, 1);

         mainCanvas.Children.Add(mainImage);

         // create list of images
         if (System.IO.Directory.Exists(folder))
            imageNames = System.IO.Directory.GetFiles(folder, "*.tif");
         else
            throw (GUI.Items.Framework.StandartExceptions.FolderDoesNotExists(folder));

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
         mainCanvas.AddHandler(Canvas.MouseLeaveEvent, new RoutedEventHandler(this.MouseLeave), true);
      }
      public void EndRender(object sender, EventArgs e)
      {

         imState.SetScaleBySize(mainImage.ActualWidth, mainImage.ActualHeight);

         // load Segments
         foreach (var item in segmentsList)
         {
            mainCanvas.Children.Add(item.DrawSegment(imState.ImStartWidth, imState.ImStartHeight));
         }

         if ((imState.ImStartWidth > mainCanvas.ActualWidth) || (imState.ImStartHeight > mainCanvas.ActualHeight))
         {
            imState.StartScale = imState.Scale * Math.Max(Math.Min(mainCanvas.ActualWidth / (imState.Scale * imState.ImStartWidth),
                                      mainCanvas.ActualHeight / (imState.Scale * imState.ImStartHeight)), ImageState.с_minScale);
            Reset();
         }

         ScaleAll();
         CountAll();

         imState.onScale += ScaleAll;
         onEndRender();
      }
      private void RenderPictures()
      {
         var bmi = new BitmapImage(new Uri(imageNames[ImageIndex]));
         mainImage.Source = bmi;
         imState.ResetImageState(bmi.PixelWidth, bmi.PixelHeight);
      }
      #endregion

      #region MOUSE CONTROL
      public void MouseMove(object sender, MouseEventArgs e)
      {
         if (mouseState.b_pressed)
         {
            var p = Segment.DeScalePoint(e.GetPosition(segmentsList[SegmentIndex].pathBox), Scale);
            segmentsList[SegmentIndex].gGroup.Children.Add(new LineGeometry(mouseState.p_last, p));
            mouseState.p_last = p;
            segmentsList[SegmentIndex].AddPoint(p);
         }
      }
      public void MouseLeftPress(object sender, RoutedEventArgs e)
      {
         var p = Segment.DeScalePoint((e as MouseEventArgs).GetPosition(segmentsList[SegmentIndex].pathBox), Scale);

         if (EraserOn)
         {
            segmentsList[SegmentIndex].DeletePoint(p);
         }
         else
         {
            segmentsList[SegmentIndex].AddAndDrawPoint(p);

            if (SegmentIndex == 0)
            {
               segmentsList[0].Count(null);
            }
            else
            {
               mouseState.b_pressed = true;
            }
         }

         mouseState.p_last = p;
      }
      public void MouseLeftUp(object sender, RoutedEventArgs e)
      {
         mouseState.b_pressed = false;
      }
      public void MouseLeave(object sender, RoutedEventArgs e)
      {
         mouseState.b_pressed = false;
      }
      #endregion

      #region SCALE
      private void ScaleAll()
      {
         OnPropertyChanged(nameof(ScaleIndex));
         OnPropertyChanged(nameof(ScaleView));

         var dw = imState.ImCurrentWidht;
         var dh = imState.ImCurrentHeight;

         mainCanvas.Width = dw;
         mainCanvas.Height = dh;

         foreach (var item in mainCanvas.Children)
         {
            (item as FrameworkElement).Width = dw;
            (item as FrameworkElement).Height = dh;
         }
      }
      private void ScaleItem(object item)
      {
         (item as FrameworkElement).Width = imState.ImCurrentWidht;
         (item as FrameworkElement).Height = imState.ImCurrentHeight;
      }

      public void IncreaseScale()
      {
         imState.Scale += Delta_scale;
      }
      public void DecreaseScale()
      {
         imState.Scale -= Delta_scale;
      }
      public void Reset()
      {
         imState.Scale = imState.StartScale;
      }


      public double Scale
      {
         get
         {
            return imState.Scale;
         }

         set
         {
            imState.Scale = value;
         }
      }
      public double Delta_scale
      {
         get
         {
            if (imState.Scale > 0.2)
               return 0.1;
            return 0.01;
         }

      }
      public int ScaleIndex
      {
         get
         {
            return Convert.ToInt32(imState.Scale * 100);
         }
         set
         {
            imState.Scale = Convert.ToDouble(value) / 100.0;
         }
      }

      public string ScaleView
      {
         get
         {
            return string.Format("{0:F2}", Scale);
         }
      }
      #endregion

      #region segmentsList

      public void AddSegment()
      {
         try
         {
            var item = new FigureSegment("New segment " + segmentsList.Count.ToString());
            item.Thickness = CurrentThickness;
            mainCanvas.Children.Add(item.DrawSegment(imState.ImStartWidth, imState.ImStartHeight));
            ScaleItem(item.pathBox);
            segmentsList.Add(item);
            SegmentIndex = segmentsList.Count - 1;
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message, "Exeption",
               MessageBoxButton.OK, MessageBoxImage.Error);
         }
      }

      public void DeleteSegment()
      {
         try
         {
            if (SegmentIndex > 0)
            {
               mainCanvas.Children.Remove(segmentsList[SegmentIndex].pathBox);
               segmentsList.RemoveAt(SegmentIndex);
               SegmentIndex = SegmentIndex - 1;
            }
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message, "Exeption",
               MessageBoxButton.OK, MessageBoxImage.Error);
         }
      }

      public void Confirm()
      {
         segmentsList[SegmentIndex].RenderSegment(imState.ImStartWidth, imState.ImStartHeight);
         segmentsList[SegmentIndex].Count(segmentsList[0].Get2DPoints());
      }

      public void SegmentIndexUpdate(int newValue)
      {
         segmentsList[SegmentIndex].Count(segmentsList[0].Get2DPoints());
         SegmentIndex = newValue;
      }

      public void CountAll()
      {
         foreach (var item in segmentsList)
         {
            item.Count(segmentsList[0].Get2DPoints());
         }
      }
      public int Export(ExcelWorksheet worksheet, int x, int y)
      {
         CountAll();
         int count = 0;

         foreach (var item in segmentsList)
         {
            worksheet.Cells[x, y].Value = item.Name;
            worksheet.Cells[x, y+1].Value = item.CellNumber;
            count++;
         }

         return count;
      }
      #endregion

      #region VARIABLES

      event VoidHandler onEndRender = () => { };
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
            if ((value >= 0) && (value < segmentsList.Count))
            {
               segmentIndex = value;
               OnPropertyChanged(nameof(SegmentIndex));
               OnPropertyChanged(nameof(CurrentBrush));
            }
         }
      }
      public Color CurrentColor
      {
         get
         {
            return segmentsList[segmentIndex].ColorView;
         }
         set
         {
            segmentsList[segmentIndex].ColorView = value;
         }
      }
      public Brush CurrentBrush
      {
         get
         {
            return segmentsList[segmentIndex].BrushView;
         }
      }
      public string CurrentThicknessView
      {
         get
         {
            return string.Format("{0:F2}", segmentsList[segmentIndex].Thickness);
         }
      }
      public double CurrentThickness
      {
         get
         {
            return segmentsList[segmentIndex].Thickness;
         }
         set
         {
            foreach (var item in segmentsList)
            {
               item.Thickness = value;
            }
            OnPropertyChanged("CurrentThickness");
            OnPropertyChanged("CurrentThicknessView");
         }
      }
      public bool EraserOn { get; set; } = false;
      public BindingList<Segment> SegmentsList { get { return segmentsList; }  }

      public int ScaleCount { get; set; } = 1000;

      private Canvas mainCanvas;
      private Image mainImage;
      private ImageState imState;
      private MouseState mouseState = new MouseState();

      private BindingList<Segment> segmentsList;
      private int segmentIndex = 0;

      private string[] imageNames;
      private int imageIndex = 0;
      #endregion
   }
}
