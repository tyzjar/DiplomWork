using OfficeOpenXml;


namespace GUI.Items.Dalmatian
{
   public abstract class IControl
   {
      public abstract object GetPanel();
      public abstract void UpdatePanel(Framework.ConfigItem segments);
      public abstract void ClearPanel();
      public abstract void Comand(Framework.Data.DataGrid.GridItem param, string s);
      public abstract int ExportComand(Framework.Data.DataGrid.GridItem param,
         ExcelWorksheet worksheet, int row, int col);
      public abstract void ImportComand(Framework.Data.DataGrid.GridItem param, string fileName);
   }

   public class DalmatianControl : IControl
   {
      public DalmatianControl(object panel_)
      {
         panel = panel_;
      }
      public override object GetPanel()
      {
         return panel;
      }
      public override void UpdatePanel(Framework.ConfigItem segments)
      { }
      public override void ClearPanel()
      { }
      public override void Comand(Framework.Data.DataGrid.GridItem param, string s)
      { }
      public override int ExportComand(Framework.Data.DataGrid.GridItem param,
         ExcelWorksheet worksheet, int row, int col)
      { return 0; }
      public override void ImportComand(Framework.Data.DataGrid.GridItem param, string fileName)
      {
      }

      private object panel;
   }
}
