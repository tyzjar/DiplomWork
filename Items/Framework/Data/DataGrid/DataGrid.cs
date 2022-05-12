using System.ComponentModel;
using System.Windows;

namespace GUI.Items.Framework.Data.DataGrid
{
   public delegate SegmentsList SegmentsCreator(string sampleName);

   public class DataGrid : ConfigItem
   {
      public DataGrid(FolderData folderData_, SegmentsCreator screator_) :
         base("DataGrid")
      {
         folderData = folderData_;
         screator = screator_;
         initData();
      }

      private void initData()
      {
         variables.Data = new BindingList<GridItem>();
         variables.Data.AllowNew = true;
         variables.Data.AddingNew += (sender, e) =>
         { e.NewObject = NewGridItem(); };
      }

      public void addItem(string[] row)
      {
         Data.Add(NewGridItem(row));
      }

      /// Work with GridItem
      GridItem NewGridItem()
      {
         GridItem item = new GridItem(folderData);
         item.udpateStates();
         item.InSampleName = folderData.InSampleText;
         item.Segments = screator(item.SampleName);
         return item;
      }
      public GridItem NewGridItem(string[] row)
      {
         GridItem item = new GridItem(row, folderData);
         item.udpateStates();
         item.Segments = screator(item.SampleName);
         return item;
      }

      /// Values 
      public BindingList<GridItem> Data { get => variables.Data; }
      private FolderData folderData;
      private SegmentsCreator screator;

      public override void SetVariables(SaveVariables v) 
      {
         Data.Clear();
         var newData = (v as Variables).Data;
         if (newData != null)
         {
            foreach (var item in newData)
            {
               item.InitFolder(folderData);
               Data.Add(item);
            }
         }
      }
      public override SaveVariables GetVariables() => variables;
      public class Variables : ConfigItem.SaveVariables
      {
         public BindingList<GridItem> Data;
      }
      private Variables variables = new Variables();
   }

}
