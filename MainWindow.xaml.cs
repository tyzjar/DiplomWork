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
         mainData = new Items.Framework.Data.MainData(Items.Dalmatian.SegmentListControl.SegmentListControlCreate, false) ;

         Preprocessing.Content = new Items.Preprocessing.Preprocessing(mainData);
         Dalmatian.Content = new Items.Dalmatian.Dalmatian(mainData, new Items.Dalmatian.DalmatianControl(),
            new Dalmatian.Helper.HelperControl());
         Morph.Content = new Items.Morph.Morph(mainData);
      }
   }
   #endregion

}
