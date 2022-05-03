using OfficeOpenXml;
using System;
using System.ComponentModel;
using System.Windows;

namespace GUI.Items.Dalmatian
{
   public class DalmatianControl : IControl
   {
      public DalmatianControl(object panel_)
      {
         panel = panel_ as SegmentationPanel;
      }
      public override object GetPanel()
      {
         return panel;
      }
      public override void UpdatePanel(Framework.Data.DataGrid.SegmentsList segments)
      {
         if ((segments as SegmentListControl).segmentsList != null)
         {
            panel.SegmentsDataGrid.ItemsSource =
            (segments as SegmentListControl).segmentsList;
         }
         else
         {
            ClearPanel();
         }
      }
      public override void ClearPanel()
      {
         panel.SegmentsDataGrid.ItemsSource = new BindingList<Segment>();
      }
      public override void Comand(Framework.Data.DataGrid.GridItem param, string s)
      {
         try
         {
            var sWindow = new SegmentsControl();
            sWindow.ShowDialog();
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message, "Exeption",
               MessageBoxButton.OK, MessageBoxImage.Error);
         }
      }
      public override int ExportComand(Framework.Data.DataGrid.GridItem param,
         ExcelWorksheet worksheet, int row, int col)
      { return 0; }
      public override void ImportComand(Framework.Data.DataGrid.GridItem param, string fileName)
      {
      }

      private SegmentationPanel panel;
   }
}
