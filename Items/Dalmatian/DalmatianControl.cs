using OfficeOpenXml;
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
                  if (int.TryParse(objectData[1], out z))
                     l.AddPoint(x, y, z);
         }

         //(param.Segments as SegmentListControl).CountAll();
      }

      private SegmentationPanel panel;
   }
}
