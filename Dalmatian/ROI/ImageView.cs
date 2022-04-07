using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Dalmatian.ROI
{
   class ImageView : GUI.Items.Framework.ViewModelBase
   {
      public ImageView(string folder, List<Segment> segmentsList)
      {
         SegmentsList = segmentsList;
         mainCanvas = new Canvas();
         mainImage = new Image();
         mainCanvas.Children.Add(mainImage);

         // create list of images
         try
         {
            imageNames = System.IO.Directory.GetFiles(folder, "*.tif");
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message, "Exeption",
               MessageBoxButton.OK, MessageBoxImage.Error);
         }
      }

      public void StartRender(UserControl renderCanvas)
      {
         renderCanvas.Content = mainCanvas;
         mainImage.Loaded += EndRender;

         // load central picture
         ImageIndex = imageNames.Length/2;
      }
      public void EndRender(object sender, EventArgs e)
      {
         imStartWidth = mainImage.ActualWidth;
         imStartHeight = mainImage.ActualHeight;
         ScaleAll();
      }

      private void RenderPictures()
      {
         // load Image
         mainImage.Source = new BitmapImage(new Uri(imageNames[ImageIndex]));

         // load Segments
         SegmentsList.ForEach((Segment a) => { mainCanvas.Children.Add(a.DrawSegment()); });
      }

      private void ScaleAll()
      {
         var dw = imStartWidth * scaleValue;
         var dh = imStartHeight * scaleValue;

         double positionX = (mainCanvas.ActualWidth - dw) / 2;
         double positionY = (mainCanvas.ActualHeight - dh) / 2;

         foreach (var item in mainCanvas.Children)
         {
            Canvas.SetLeft((item as UIElement), positionX);
            Canvas.SetTop((item as UIElement), positionY);
            (item as FrameworkElement).Width = dw;
            (item as FrameworkElement).Height = dh;
         }

         // Image scale&move
         //mainImage.Width = dw;
         //mainImage.Height = dh;
         //Canvas.SetLeft(mainImage, positionX);
         //Canvas.SetTop(mainImage, positionY);

         // Segments scale&move
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
            }
         }
      }

      public List<Segment> SegmentsList;

      private Canvas mainCanvas;
      private Image mainImage;
      private double imStartWidth = 0;
      private double imStartHeight = 0;
      private string[] imageNames;
      private int imageIndex = 0;
      private double scaleValue = 1; 
   }
}
