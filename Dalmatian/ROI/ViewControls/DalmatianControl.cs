﻿using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;

namespace Dalmatian.ROI
{
   public class DalmatianControl : GUI.Items.Dalmatian.IControl
   {
      public DalmatianControl()
      {
         panel = new SegmentationPanel();
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
      public override void Comand(GUI.Items.Framework.Data.DataGrid.GridItem param, GUI.Items.Framework.Data.MainData mainData)
      {
         ROIEdit sWindow = null;

         try
         {
            if (param == null)
               throw (GUI.Items.Framework.StandartExceptions.NoSelectedItem());

            var folder = param.CellCountFolder;

            if (!GUI.Items.Framework.Utils.CheckFolderForTifFiles(folder))
               throw new GUI.Items.Framework.StandartExceptions("Did not find segments pictures. Plaese check the folder \""
                  + folder + "\" and confirm that samples picture are there. Also check subfolder in settings. Current subfolder is \""
                  + mainData.folderData.CellCountSubfolder + "\".", false);

            sWindow = new ROIEdit(folder, (param.Segments as SegmentListControl).segmentsList);
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

            if (double.TryParse(objectData[0],out y))
               if (double.TryParse(objectData[1], out x))
                  l.AddPoint(x,y);
         }
         (param.Segments as SegmentListControl).CountAll();
      }

      public override void CalculateSegments(GUI.Items.Framework.Data.DataGrid.GridItem param, string fileName)
      {
         // this dont work for dalmation programm
         (param.Segments as SegmentListControl).CountAll();
      }

      private ROI.SegmentationPanel panel;
   }
}
