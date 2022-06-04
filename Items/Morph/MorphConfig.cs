using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using Newtonsoft.Json;
using OfficeOpenXml;

namespace GUI.Items.Morph
{
   class MorphConfig : Framework.IFormConfig
   {
      public MorphConfig(Framework.Data.MainData mainData_, UserControl gridAndProcessPanel_) :
         base(mainData_, gridAndProcessPanel_, "Morph")
      {
      }
      protected override void Initialize()
      {
         gridPanel = new GridPanel();
         gridPanel.MorphDataGrid.ItemsSource = mainData.dataGrid.Data;
         processor = new MorphProcessor(this, "Morph");
      }
      protected override void swapToView()
      {
         gridAndProcessPanel.Content = gridPanel;
      }

      public GridPanel gridPanel;

      public bool Halves { get => variables.Halves; set { variables.Halves = value; OnPropertyChanged(nameof(Halves)); } }
      public string AgeValue { get => variables.AgeValue; set { variables.AgeValue = value; OnPropertyChanged(nameof(AgeValue)); } }
      public double ZExtention { get => variables.ZExtention; set { variables.ZExtention = Convert.ToDouble(value); OnPropertyChanged(nameof(ZExtention)); } }

      public static string[] AgeValues = { "adult", "young" };

      public override void SetVariables(SaveVariables v) 
      {
         variables = v as Variables;

         OnPropertyChanged(nameof(Halves));
         OnPropertyChanged(nameof(AgeValue));
      }
      public override SaveVariables GetVariables() => variables;
      public class Variables : Framework.ConfigItem.SaveVariables
      {
         public bool Halves { get; set; } = false;
         public string AgeValue { get; set; } = AgeValues[0];
         public double ZExtention = 1.0;
      }
      public Variables variables = new Variables();
   }


}
