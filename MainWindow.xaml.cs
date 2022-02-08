using System.Windows;
using System;
using System.Threading;


namespace GUI
{
   #region Constructor
   /// <summary>
   /// Логика взаимодействия для MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      public MainWindow()
      {
         InitializeComponent();
         componentsFactory();
      }

      Items.Framework.Data.MainData mainData;

      void componentsFactory()
      {
         var selectedFile = new GUI.Items.Framework.Data.SelectedData();
         mainData = new Items.Framework.Data.MainData();

         Preprocessing.Content = new Items.Preprocessing.Preprocessing(mainData);
         Dalmatian.Content = new Items.Dalmatian.Dalmatian(mainData);
         Morph.Content = new Items.Morph.Morph(mainData);
      }
   }
   #endregion

}
