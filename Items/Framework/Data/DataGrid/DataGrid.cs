using System.Collections.Generic;
using OfficeOpenXml;
using System.ComponentModel;
using System.Windows;

namespace GUI.Items.Framework.Data.DataGrid
{
   public delegate ConfigItem SegmentsCreator(string sampleName);

   public class DataGrid : ConfigItem
   {
      public DataGrid(FolderData folderData_, SegmentsCreator screator_) :
         base("SamplesInfo")
      {
         folderData = folderData_;
         screator = screator_;

         Data = new BindingList<GridItem>();
         Data.AllowNew = true;
         Data.AddingNew += (sender, e) =>
         { e.NewObject = NewGridItem(); };
      }

      public override void UpdateName(string newName)
      {}

      public void addItem(string[] row)
      {
         Data.Add(NewGridItem(row));
      }

      public override List<ConfigItem> LoadConfig(ExcelWorksheet worksheet)
      {
         List<ConfigItem> segmentsConfigs = new List<ConfigItem>();

         Data.Clear();

         if ( (worksheet != null)&&(worksheet.Dimension != null) )
         {
            List<string> columnNames = new List<string>(); 

            foreach (var cell in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
            {
               columnNames.Add(cell.Text.Trim());
            }

            for (int i = 2; i <= worksheet.Dimension.End.Row; i++)
            {
               var row = worksheet.Cells[i, 1, i, worksheet.Dimension.End.Column];
               var item = NewGridItem();
               foreach (var cell in row)
               {
                  item.setByName(columnNames[cell.Start.Column - 1],cell.Text);
               }

               item.Segments = screator(item.SampleName);
               Data.Add(item);
               segmentsConfigs.Add(item.Segments);
            }
         }

         return segmentsConfigs;
      }
      public override List<ConfigItem> SaveConfig(ExcelWorksheet worksheet)
      {
         List<ConfigItem> segmentsConfigs = new List<ConfigItem>();
         var row = 2;
         var column = 1;
         var len = GridItem.fieldNames.Length;

         worksheet.Cells[1, 1, 1, len].LoadFromArrays(new object[][]
            { GridItem.fieldNames });

         foreach(var item in Data)
         {
            worksheet.Cells[row, column, row, column + len].LoadFromArrays(
               new object[][]{ item.getAsRow() });
            row++;

            segmentsConfigs.Add(item.Segments);
         }

         return segmentsConfigs;
      }

      /// Work with GridItem
      GridItem NewGridItem()
      {
         GridItem item = new GridItem();
         item.PropertyChanged += UpdateItem;
         item.udpateStates(folderData);
         item.Segments = screator(item.SampleName);
         return item;
      }

      public GridItem NewGridItem(string[] row)
      {
         GridItem item = new GridItem(row);
         item.PropertyChanged += UpdateItem;
         item.udpateStates(folderData);
         item.Segments = screator(item.SampleName);
         return item;
      }

      public void UpdateItem(object sender, PropertyChangedEventArgs e)
      {
         var item = (sender as GridItem);
         if (e.PropertyName == "SampleName")
         {
            item.Segments.UpdateName(item.SampleName);
         }

         item.udpateStates(folderData);
      }

      /// Values 
      public BindingList<GridItem> Data;
      private FolderData folderData;
      private SegmentsCreator screator;
   }

}
