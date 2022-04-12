using System.Windows;


namespace Dalmatian
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      ROI.DalmatianControl panel;
      public MainWindow()
      {
         InitializeComponent();
         componentsFactory();
      }

      GUI.Items.Framework.Data.MainData mainData;

      void componentsFactory()
      {
         var selectedFile = new GUI.Items.Framework.Data.SelectedData();
         panel = new ROI.DalmatianControl(new ROI.SegmentationPanel());

         mainData = new GUI.Items.Framework.Data.MainData(ROI.SegmentListControl.SegmentListControlCreate);
         this.Content = new GUI.Items.Dalmatian.Dalmatian(mainData, panel);
      }
   }
}
