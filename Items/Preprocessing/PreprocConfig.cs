using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;
using Newtonsoft.Json;
using OfficeOpenXml;

namespace GUI.Items.Preprocessing
{
   class PreprocConfig : Framework.IFormConfig
   {
      public PreprocConfig(Framework.Data.MainData mainData_, UserControl gridAndProcessPanel_) :
         base(mainData_, gridAndProcessPanel_, "Preproc")
      {
      }
      protected override void Initialize()
      {
         gridPanel = new GridPanel();
         gridPanel.PreprDataGrid.ItemsSource = mainData.dataGrid.Data;
         processor = new PreprocProcessor(this, "Preproc");

         SelectPicture = new Framework.DelegateCommand((object param) => {
            selectPicture(); });
      }
      protected override void swapToView()
      {
         try
         {
            gridAndProcessPanel.Content = gridPanel;
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message, "Exeption",
               MessageBoxButton.OK, MessageBoxImage.Error);
         }
      }

      void selectPicture()
      {
         OpenFileDialog openFileDialog = new OpenFileDialog();
         openFileDialog.Filter = "Instruction File | *.tif";
         openFileDialog.DefaultExt = "tif";

         if (openFileDialog.ShowDialog() == true)
         {
            SubtractionName = openFileDialog.FileName;
         }
      }

      public GridPanel gridPanel;

      public ICommand SelectPicture { get; private set; }

      public string SubtractionName
      {
         get { return variables.SubtractionName; }
         set
         {
            if (!Equals(variables.SubtractionName, value))
            {
               variables.SubtractionName = value;
               OnPropertyChanged(nameof(SubtractionName));
            }
         }
      }
      public bool SubtractionEnabel { get => variables.SubtractionEnabel; set { variables.SubtractionEnabel = value; OnPropertyChanged(nameof(SubtractionEnabel)); } }
      public bool IntensityEnabel { get => variables.IntensityEnabel; set { variables.IntensityEnabel = value; OnPropertyChanged(nameof(IntensityEnabel)); } }
      public bool CropEnabel { get => variables.CropEnabel; set { variables.CropEnabel = value; OnPropertyChanged(nameof(CropEnabel)); } }
      public bool MaskEnabel { get => variables.MaskEnabel; set { variables.MaskEnabel = value; OnPropertyChanged(nameof(MaskEnabel)); } }


      public override void SetVariables(SaveVariables v) { variables = v as Variables; }
      public override SaveVariables GetVariables() => variables;
      public class Variables : Framework.ConfigItem.SaveVariables
      {
         public string SubtractionName = "";
         public bool SubtractionEnabel { get; set; } = false;
         public bool IntensityEnabel { get; set; } = false;
         public bool CropEnabel { get; set; } = true;
         public bool MaskEnabel { get; set; } = true;
      }
      public Variables variables = new Variables();
   }
}
