using System.Collections.Generic;
using OfficeOpenXml;
using System.ComponentModel;
using System.Windows;

namespace GUI.Items.Framework.Data.DataGrid
{
   public class DataGrid : ConfigItem
   {
      public DataGrid(FolderData folderData_) :
         base("SamplesInfo")
      {
         folderData = folderData_;

         Data = new BindingList<GridItem>();
         Data.AllowNew = true;
         Data.AddingNew += (sender, e) =>
         { e.NewObject = NewGridItem(); };
      }

      public void addItem(string[] row)
      {
         Data.Add(NewGridItem(row));
      }

      public override void LoadConfig(ExcelWorksheet worksheet)
      {
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
               Data.Add(item);
            }
         }
        
      }

      public override void SaveConfig(ExcelWorksheet worksheet)
      {
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
         }
      }


      /// Work with GridItem
      GridItem NewGridItem()
      {
         GridItem item = new GridItem();
         item.PropertyChanged += UpdateItem;
         item.udpateStates(folderData);
         return item;
      }

      public GridItem NewGridItem(string[] row)
      {
         GridItem item = new GridItem(row);
         item.PropertyChanged += UpdateItem;
         item.udpateStates(folderData);
         return item;
      }

      public void UpdateItem(object sender, PropertyChangedEventArgs e)
      {
         (sender as GridItem).udpateStates(folderData);
      }

      /// Values 
      public BindingList<GridItem> Data;
      FolderData folderData;
   }

}
