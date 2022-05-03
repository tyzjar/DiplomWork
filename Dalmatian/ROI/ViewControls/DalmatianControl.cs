using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;

namespace Dalmatian.ROI
{
   public class DalmatianControl : GUI.Items.Dalmatian.IControl
   {
      public DalmatianControl(object panel_)
      {
         panel = panel_ as SegmentationPanel;
      }
      public override object GetPanel()
      {
         return panel;
      }
      public override void UpdatePanel(GUI.Items.Framework.Data.DataGrid.SegmentsList segments)
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
      public override void Comand(GUI.Items.Framework.Data.DataGrid.GridItem param, string s)
      {
         ROIEdit sWindow = null;

         try
         {
            if (param == null)
               throw (GUI.Items.Framework.StandartExceptions.NoSelectedItem());

            var folder = param.SampleName + @"\" + s + @"\";

            sWindow = new ROIEdit(folder,
            (param.Segments as SegmentListControl).segmentsList);
            sWindow.ShowDialog();
         }
         catch (GUI.Items.Framework.StandartExceptions dex)
         {
            MessageBox.Show(dex.Message, "Exeption",
               MessageBoxButton.OK, MessageBoxImage.Warning);
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message, "Exeption",
               MessageBoxButton.OK, MessageBoxImage.Error);
         }

         if (sWindow != null)
         {
            sWindow.Close();
         }
      }
      public override int ExportComand(GUI.Items.Framework.Data.DataGrid.GridItem param,
         ExcelWorksheet worksheet, int row, int col)
      {
         try
         {
            if (param == null)
               throw (GUI.Items.Framework.StandartExceptions.NoSelectedItem());

            var i = (param.Segments as SegmentListControl).Export(worksheet, row, col+1);
            worksheet.Cells[row, col, row + i - 1, col].Merge = true;
            worksheet.Cells[row, col, row + i - 1, col].Value = param.SampleName;
            worksheet.Cells[row, col, row + i - 1, col].Style.VerticalAlignment =
               ExcelVerticalAlignment.Center;
            worksheet.Cells[row, col, row + i - 1, col].Style.HorizontalAlignment = 
               ExcelHorizontalAlignment.CenterContinuous;

            return i;
         }
         catch (GUI.Items.Framework.StandartExceptions dex)
         {
            MessageBox.Show(dex.Message, "Exeption",
               MessageBoxButton.OK, dex.critical ? MessageBoxImage.Error :
               MessageBoxImage.Warning);
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message, "Exeption",
               MessageBoxButton.OK, MessageBoxImage.Error);
         }
         return 0;
      }
      public override void ImportComand(GUI.Items.Framework.Data.DataGrid.GridItem param, string fileName)
      {
         FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
         StreamReader workFile = new StreamReader(fileStream);

         string line;
         var delimeter = ',';
         double x, y, z;
         var l = (param.Segments as SegmentListControl).segmentsList[0];
         l.RemoveAll();
         while ((line = workFile.ReadLine()) != null)
         {
            var objectData = line.Split(delimeter);
            if (objectData.Length < 3)
               continue;

            if (double.TryParse(objectData[0],out x))
               if (double.TryParse(objectData[1], out y))
                  l.AddPoint(x,y);
         }
         (param.Segments as SegmentListControl).CountAll();
      }

      private ROI.SegmentationPanel panel;
   }
}
