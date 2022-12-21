using System;
using System.Windows.Input;
using System.Windows.Controls;
using OfficeOpenXml;
using System.Windows;
using System.Collections.Generic;
using Microsoft.Win32;
using System.IO;
using OfficeOpenXml.Style;

namespace GUI.Items.Dalmatian
{
   class CellCountConfig : Framework.IFormConfig
   {
      public CellCountConfig(Framework.Data.MainData mainData_, UserControl gridAndProcessPanel_,
         IControl segmentationControl_, Framework.IHelper helper_) :
         base(mainData_, gridAndProcessPanel_, "CellCount")
      {
         helper = helper_;
         segmentationControl = segmentationControl_;
         SegmentCommand = new Framework.DelegateCommand((object param) => {
            segmentationControl.Comand( (gridPanel.SamplesDataGrid.SelectedItem as Framework.Data.DataGrid.GridItem),
               mainData);
         });

         gridPanel = new GridPanel(segmentationControl.GetPanel(), mainData.dalmatian);
         gridPanel.SamplesDataGrid.ItemsSource = mainData.dataGrid.Data;
         gridPanel.SamplesDataGrid.SelectionChanged += (object sender, SelectionChangedEventArgs e) => {
            if ((gridPanel.SamplesDataGrid.SelectedItem as Framework.Data.DataGrid.GridItem) != null)
            {
               segmentationControl.UpdatePanel(
                 (gridPanel.SamplesDataGrid.SelectedItem as Framework.Data.DataGrid.GridItem).Segments);
            }
            else
               segmentationControl.ClearPanel();
         };

         swapToView();
      }

      public void ExportCell()
      {
         try
         {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Instruction File | *.xlsx";
            saveFileDialog.DefaultExt = "xlsx";
            saveFileDialog.FileName = "Cells Report " + Path.GetFileNameWithoutExtension(mainData.openSaveEvents.SelectedProjectFile);

            if (saveFileDialog.ShowDialog() == true)
            {
               FileStream workFile = File.Create(saveFileDialog.FileName);
               ExcelPackage excelPackage = new ExcelPackage(workFile);
               var wsh = excelPackage.Workbook.Worksheets.Add("Dalmatian");

               var row = 1;
               var col = 1;
               foreach (var item in mainData.dataGrid.Data)
               {
                  row += segmentationControl.ExportComand(item, wsh, row, col);
               }

               wsh.Cells.AutoFitColumns();
               wsh.Column(1).Width = 60;
               excelPackage.SaveAs(workFile);
               workFile.Close();
            }
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message, "Exeption",
               MessageBoxButton.OK, MessageBoxImage.Error);
         }
      }
      public void ExportCommonCells()
      {
         try
         {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Instruction File | *.xlsx";
            saveFileDialog.DefaultExt = "xlsx";
            saveFileDialog.FileName = "Cells Report " + Path.GetFileNameWithoutExtension(mainData.openSaveEvents.SelectedProjectFile);

            if (saveFileDialog.ShowDialog() == true)
            {
               FileStream workFile = File.Create(saveFileDialog.FileName);
               ExcelPackage excelPackage = new ExcelPackage(workFile);
               var wsh = excelPackage.Workbook.Worksheets.Add("Dalmatian");

               var row = 1;
               var col = 1;

               if (mainData.dataGrid.Data.Count == 0)
                  return;

               (mainData.dataGrid.Data[0].Segments as SegmentListControl).CreateCols(wsh, ref row, col + 1);

               foreach (var item in mainData.dataGrid.Data)
               {
                  wsh.Cells[row, col].Value = item.SampleName;
                  (item.Segments as SegmentListControl).ExportCol(wsh, ref row, col+1);
               }

               wsh.Cells.AutoFitColumns();
               wsh.Column(1).Width = 60;
               excelPackage.SaveAs(workFile);
               workFile.Close();
            }
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message, "Exeption",
               MessageBoxButton.OK, MessageBoxImage.Error);
         }
      }

      protected override void Initialize()
      {
         processor = new CellCountProcessor(this, "CellCount");
         commonPreview = new Preview.CommonPreview(this, "CommonPreview");
         sfilterPreview = new Preview.sFilterPreview(this, "sFilterPreview");
         thresholdPreview = new Preview.ThresholdPreview(this, "ThresholdPreview");
         originalView = new Preview.OriginalView(this, "OriginalView");
         segmentsCount = new Segments.SegmentCountHelper(this, "3dCountHelper");

         /// Buttons events
         CommonPreviewCommand = new Framework.DelegateCommand((object param) => {
            commonPreview.StartProcess();
         });
         sFilterPreviewCommand = new Framework.DelegateCommand((object param) => {
            sfilterPreview.StartProcess();
         });
         ThresholdCommand = new Framework.DelegateCommand((object param) => {
            thresholdPreview.StartProcess();
         });
         OriginalViewCommand = new Framework.DelegateCommand((object param) => {
            originalView.StartProcess();
         });
         SelectedChangedCommand = new Framework.DelegateCommand((object param) => {
            segmentationControl.UpdatePanel(
               (gridPanel.SamplesDataGrid.SelectedItem as Framework.Data.DataGrid.GridItem).Segments);
         });
         ExportCommand = new Framework.DelegateCommand((object param) => {
            ExportCell();
         });
         ExportCommonCommand = new Framework.DelegateCommand((object param) => {
            ExportCommonCells();
         });
         SegmentsCountCommand = new Framework.DelegateCommand((object param) => {
            segmentsCount.StartProcess();
         });
         sFilterHelpCommand = new Framework.DelegateCommand((object param) => {
            helper.StartHelp(1);
         });
         ThresholdHelpCommand = new Framework.DelegateCommand((object param) => {
            helper.StartHelp(2);
         });
         CountHelpCommand = new Framework.DelegateCommand((object param) => {
            helper.StartHelp(3);
         });
      }
      protected override void swapToView()
      {
         gridAndProcessPanel.Content = gridPanel;
      }

      public ICommand CommonPreviewCommand { get; private set; }
      public ICommand sFilterPreviewCommand { get; private set; }
      public ICommand ThresholdCommand { get; private set; }
      public ICommand OriginalViewCommand { get; private set; }
      public ICommand SegmentCommand { get; private set; }
      public ICommand SegmentsCountCommand { get; private set; }
      public ICommand SelectedChangedCommand { get; private set; }
      public ICommand ExportCommand { get; private set; }
      public ICommand ExportCommonCommand { get; private set; }
      public ICommand sFilterHelpCommand { get; private set; }
      public ICommand ThresholdHelpCommand { get; private set; }
      public ICommand CountHelpCommand { get; private set; }


      #region Values

      public GridPanel gridPanel;
      public IControl segmentationControl;
      public Framework.IHelper helper;

      private Preview.CommonPreview commonPreview;
      private Preview.sFilterPreview sfilterPreview;
      private Preview.ThresholdPreview thresholdPreview;
      private Preview.OriginalView originalView;
      private Segments.SegmentCountHelper segmentsCount;
      public string sfilterLowpass
      {
         get
         {
            return variables.sfilterLowpass.ToString();
         }
         set
         {
            variables.sfilterLowpass = Convert.ToDouble(value);
            OnPropertyChanged(nameof(sfilterLowpass));
         }
      }
      public string sfilterHipass
      {
         get
         {
            return variables.sfilterHipass.ToString();
         }
         set
         {
            variables.sfilterHipass = Convert.ToDouble(value);
            OnPropertyChanged(nameof(sfilterHipass));
         }
      }
      public string trshold
      {
         get
         {
            return variables.trshold.ToString();
         }
         set
         {
            variables.trshold = Convert.ToDouble(value);
            OnPropertyChanged(nameof(trshold));
         }
      }
      public string mfilterRad
      {
         get
         {
            return variables.mfilterRad.ToString();
         }
         set
         {
            variables.mfilterRad = Convert.ToDouble(value);
            OnPropertyChanged(nameof(mfilterRad));
         }
      }
      public string countMinRegion
      {
         get
         {
            return variables.mfilterRad.ToString();
         }
         set
         {
            variables.mfilterRad = Convert.ToDouble(value);
            OnPropertyChanged(nameof(countMinRegion));
         }
      }
      public string countConfLvl
      {
         get
         {
            return variables.countConfLvl.ToString();
         }
         set
         {
            variables.countConfLvl = Convert.ToDouble(value);
            OnPropertyChanged(nameof(countConfLvl));
         }
      }
      public string countRMin
      {
         get
         {
            return variables.countRMin.ToString();
         }
         set
         {
            variables.countRMin = Convert.ToDouble(value);
            OnPropertyChanged(nameof(countRMin));
         }
      }
      public string countRMax
      {
         get
         {
            return variables.countRMax.ToString();
         }
         set
         {
            variables.countRMax = Convert.ToDouble(value);
            OnPropertyChanged(nameof(countRMax));
         }
      }
      public string countk
      {
         get
         {
            return variables.countk.ToString();
         }
         set
         {
            variables.countk = Convert.ToDouble(value);
            OnPropertyChanged(nameof(countk));
         }
      }
      public string sfilterOn
      {
         get
         {
            return variables.sfilterOn.ToString();
         }
         set
         {
            variables.sfilterOn = Convert.ToBoolean(value);
            OnPropertyChanged(nameof(sfilterOn));
         }
      }
      public string trsholdOn
      {
         get
         {
            return variables.trsholdOn.ToString();
         }
         set
         {
            variables.trsholdOn = Convert.ToBoolean(value);
            OnPropertyChanged(nameof(trsholdOn));
         }
      }
      public string mfilterOn
      {
         get
         {
            return variables.mfilterOn.ToString();
         }
         set
         {
            variables.mfilterOn = Convert.ToBoolean(value);
            OnPropertyChanged(nameof(mfilterOn));
         }
      }

      public override void SetVariables(SaveVariables v) 
      { 
         variables = v as Variables;
         gridPanel.SamplesDataGrid.ItemsSource = mainData.dataGrid.Data;

         OnPropertyChanged(nameof(sfilterLowpass));
         OnPropertyChanged(nameof(sfilterHipass));
         OnPropertyChanged(nameof(trshold));
         OnPropertyChanged(nameof(mfilterRad));
         OnPropertyChanged(nameof(countMinRegion));
         OnPropertyChanged(nameof(countConfLvl));
         OnPropertyChanged(nameof(countRMin));
         OnPropertyChanged(nameof(countRMax));
         OnPropertyChanged(nameof(countk));
         OnPropertyChanged(nameof(sfilterOn));
         OnPropertyChanged(nameof(trsholdOn));
         OnPropertyChanged(nameof(mfilterOn));
      }
      public override SaveVariables GetVariables() => variables;
      public class Variables : Framework.ConfigItem.SaveVariables
      { 
         public double sfilterLowpass { get; set; } = 1;
         public double sfilterHipass { get; set; } = 5;
         public double trshold { get; set; } = 0;
         public double mfilterRad { get; set; } = 15;
         public double countMinRegion { get; set; } = 15;
         public double countConfLvl { get; set; } = 0.1;
         public double countRMin { get; set; } = 0;
         public double countRMax { get; set; } = 15;
         public double countk { get; set; } = 1;
         public bool sfilterOn { get; set; } = true;
         public bool trsholdOn { get; set; } = true;
         public bool mfilterOn { get; set; } = false;
      }

      public Variables variables = new Variables();
      #endregion
   }
}
