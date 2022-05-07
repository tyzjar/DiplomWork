using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace GUI.Items.Framework.View
{
   class StateToImage
   {

      public static BitmapImage GetPicture(bool state)
      {
         return state ? image_true : image_false;
      }

      static BitmapImage image_true = new BitmapImage(new Uri(@"pack://application:,,,/GUI;component/Items/Framework/View/Images/Yes.png"));
      static BitmapImage image_false = new BitmapImage(new Uri(@"pack://application:,,,/GUI;component/Items/Framework/View/Images/No.png"));
   }
}
