using System.Collections.Generic;
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

      public override List<GUI.Items.Framework.ConfigItem> LoadConfig(ExcelWorksheet worksheet)
      {
         return null;
      }
      public override List<GUI.Items.Framework.ConfigItem> SaveConfig(ExcelWorksheet worksheet)
      {
         return null;
      }
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
