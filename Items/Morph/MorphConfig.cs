using System.Windows.Controls;
using System.Windows.Input;
using OfficeOpenXml;

namespace GUI.Items.Morph
{

   class MorphConfig : Framework.IFormConfig
   {
      public MorphConfig(Framework.Data.MainData mainData_, UserControl gridAndProcessPanel_) :
         base(mainData_, gridAndProcessPanel_,"MorphConfig")
      {
      }

      protected override void Initialize()
      {
         gridPanel = new GridPanel();
         gridPanel.MorphDataGrid.ItemsSource = mainData.dataGrid.Data;
         processor = new MorphProcessor(this, "Morph");
      }

      public override void LoadConfig(ExcelWorksheet worksheet)
      { }
      public override void SaveConfig(ExcelWorksheet worksheet)
      { }
      protected override void swapToView()
      {
         gridAndProcessPanel.Content = gridPanel;
      }

      public GridPanel gridPanel;

      public bool CellCalc { get; set; } = false;
      public bool Halves { get; set; } = false;
      public string AgeValue { get; set; } = AgeValues[0];

      public static string[] AgeValues = { "adult", "young" };
   }


}
