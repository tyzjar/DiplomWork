using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;


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
            imageNames = Directory.GetFiles(folder, "*.tif");
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
      }
      private void ScaleAll()
      {
         double positionX = (mainCanvas.ActualWidth - imStartWidth * scaleValue) / 2;
         double positionY = (mainCanvas.ActualHeight - imStartHeight * scaleValue) / 2;

         // Image scale&move
         mainImage.Width = imStartWidth * scaleValue;
         mainImage.Height = imStartHeight * scaleValue;
         Canvas.SetLeft(mainImage, positionX);
         Canvas.SetTop(mainImage, positionY);

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
      private double imStartWidth;
      private double imStartHeight;
      private string[] imageNames;
      private int imageIndex = 0;
      private double scaleValue = 1; 
   }
}
