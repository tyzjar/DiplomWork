using System.Windows;


namespace Dalmatian
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      ROI.DalmatianControl panelContol;
      public MainWindow()
      {
         InitializeComponent();
         componentsFactory();
      }

      GUI.Items.Framework.Data.MainData mainData;

      void componentsFactory()
      {
         panelContol = new ROI.DalmatianControl();

         mainData = new GUI.Items.Framework.Data.MainData(ROI.SegmentListControl.SegmentListControlCreate, true);
         this.Content = new GUI.Items.Dalmatian.Dalmatian(mainData, panelContol, new Helper.HelperControl());
      }
   }
}
