using System;
using System.Windows;


namespace Dalmatian
{
   public class PanelWithCommand : GUI.Items.Dalmatian.IPanelWithCommand
   {
      public PanelWithCommand(object panel_)
      {
         panel = panel_ as ROI.SegmentationPanel;
      }
      public override object GetPanel()
      {
         return panel;
      }
      public override void UpdatePanel(GUI.Items.Framework.ConfigItem segments)
      {
         //if ((segments as ROI.SegmentListControl).segmentsList != null)
         //{
         //   panel.SegmentsDataGrid.ItemsSource =
         //   (segments as ROI.SegmentListControl).segmentsList;
         //}
      }
      public override void Comand(object param, string s)
      {
         var p = (param as GUI.Items.Framework.Data.DataGrid.GridItem);
         var folder = p.SampleName + @"\" + s + @"\";
         try
         {
            ROI.ROIEdit sWindow = new ROI.ROIEdit(folder,
            (p.Segments as ROI.SegmentListControl).segmentsList);
            sWindow.ShowDialog();
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message, "Exeption",
               MessageBoxButton.OK, MessageBoxImage.Error);
         }
      }

      private ROI.SegmentationPanel panel;
   }

   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      PanelWithCommand panel;
      public MainWindow()
      {
         InitializeComponent();
         componentsFactory();
      }

      GUI.Items.Framework.Data.MainData mainData;

      void componentsFactory()
      {
         var selectedFile = new GUI.Items.Framework.Data.SelectedData();
         panel = new PanelWithCommand(new ROI.SegmentationPanel());

         mainData = new GUI.Items.Framework.Data.MainData(ROI.SegmentListControl.SegmentListControlCreate);
         this.Content = new GUI.Items.Dalmatian.Dalmatian(mainData, panel);
      }
   }
}
