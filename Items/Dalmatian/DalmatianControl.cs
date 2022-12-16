using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;

namespace GUI.Items.Dalmatian
{
   public class DalmatianControl : IControl
   {
      public DalmatianControl()
      {
         panel = new SegmentationPanel();
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
      public override void Comand(Framework.Data.DataGrid.GridItem param, Framework.Data.MainData mainData)
      {
         try
         {
            if (param == null)
               throw (Framework.StandartExceptions.NoSelectedItem());

            var sWindow = new SegmentsControl(param, mainData);
            sWindow.StartView();
         }
         catch (Framework.StandartExceptions dex)
         {
            MessageBox.Show(dex.Message, "Exeption",
               MessageBoxButton.OK, MessageBoxImage.Warning);
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message, "Exeption",
               MessageBoxButton.OK, MessageBoxImage.Error);
         }
      }
      public override int ExportComand(Framework.Data.DataGrid.GridItem param,
         ExcelWorksheet worksheet, int row, int col)
      {
         try
         {
            if (param == null)
               throw (GUI.Items.Framework.StandartExceptions.NoSelectedItem());

            var i = (param.Segments as SegmentListControl).Export(worksheet, row, col + 1);
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
      public override void ImportComand(Framework.Data.DataGrid.GridItem param, string fileName)
      {
         FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
         StreamReader workFile = new StreamReader(fileStream);

         string line;
         var delimeter = ',';
         int x, y, z;
         var l = (param.Segments as SegmentListControl).segmentsList[0];
         l.RemoveAll();
         while ((line = workFile.ReadLine()) != null)
         {
            var objectData = line.Split(delimeter);
            if (objectData.Length < 3)
               continue;

            if (int.TryParse(objectData[0], out x))
               if (int.TryParse(objectData[1], out y))
                  if (int.TryParse(objectData[2], out z))
                     l.AddPoint(x, y, z);
         }
      }

      public override void CalculateSegments(Framework.Data.DataGrid.GridItem param, string fileName)
      {
         FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
         StreamReader workFile = new StreamReader(fileStream);
         var sl = (param.Segments as SegmentListControl).segmentsList;

         string line;
         var delimeter = ',';
         int x, y, z, sId;
         var AllCells = sl[0];
         AllCells.RemoveAll();
         while ((line = workFile.ReadLine()) != null)
         {
            var objectData = line.Split(delimeter);
            if (objectData.Length < 4)
               continue;

            if (int.TryParse(objectData[0], out x))
               if (int.TryParse(objectData[1], out y))
                  if (int.TryParse(objectData[2], out z))
                     if (int.TryParse(objectData[3], out sId))
                     {
                        AllCells.AddPoint(x, y, z);
                        AddCellInSegments(sl, x, y, z, sId);
                     }
         }
      }
      private void AddCellInSegments(BindingList<Segment> sl, int x, int y, int z, int id)
      {
         foreach (var segment in sl)
         {
            if (segment.CheckId(id))
            {
               segment.AddPoint(x, y, z);
            }
         }
      }

      private SegmentationPanel panel;
   }
}
