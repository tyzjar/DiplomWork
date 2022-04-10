using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;
using OfficeOpenXml;

namespace GUI.Items.Preprocessing
{
   class PreprocConfig : Framework.IFormConfig
   {
      public PreprocConfig(Framework.Data.MainData mainData_, UserControl gridAndProcessPanel_) :
         base(mainData_, gridAndProcessPanel_, "PreprocConfig")
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
      public bool SubtractionEnabel { get; set; } = false;
      public string SubtractionName
      {
         get => SubtractionNameValue;
         set
         {
            if (!Equals(SubtractionNameValue, value))
            {
               SubtractionNameValue = value;
               OnPropertyChanged(nameof(SubtractionName));
            }
         }
      }
      public string SubtractionNameValue = "";
      public bool IntensityEnabel { get; set; } = false;
      public bool CropEnabel { get; set; } = true;
      public bool MaskEnabel { get; set; } = true;

   }
}
