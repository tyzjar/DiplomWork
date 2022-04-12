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
         var selectedFile = new Items.Framework.Data.SelectedData();
         mainData = new Items.Framework.Data.MainData( (string name)=> { return null; } );

         Preprocessing.Content = new Items.Preprocessing.Preprocessing(mainData);
         Dalmatian.Content = new Items.Dalmatian.Dalmatian(mainData,
            new Items.Dalmatian.DalmatianControl(new Items.Dalmatian.SegmentationPanel()),
            new Items.Framework.HelperControl());
         Morph.Content = new Items.Morph.Morph(mainData);
      }
   }
   #endregion

}
