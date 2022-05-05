using OfficeOpenXml;


namespace GUI.Items.Dalmatian
{
   public abstract class IControl
   {
      public abstract object GetPanel();
      public abstract void UpdatePanel(Framework.Data.DataGrid.SegmentsList segments);
      public abstract void ClearPanel();
      public abstract void Comand(Framework.Data.DataGrid.GridItem param, Framework.Data.MainData mainData);
      public abstract int ExportComand(Framework.Data.DataGrid.GridItem param,
         ExcelWorksheet worksheet, int row, int col);
      public abstract void ImportComand(Framework.Data.DataGrid.GridItem param, string fileName);
   }
}
