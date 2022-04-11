using System;
using System.Windows.Input;
using System.Windows.Controls;
using OfficeOpenXml;
using System.Windows;
using System.Collections.Generic;

namespace GUI.Items.Dalmatian
{
   public abstract class IPanelWithCommand
   {
      public abstract object GetPanel();
      public abstract void UpdatePanel(Framework.ConfigItem segments);
      public abstract void ClearPanel();
      public abstract void Comand(object param, string s);
   }

   public class PanelWithCommand : IPanelWithCommand
   {
      public PanelWithCommand(object panel_)
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
      public override void Comand(object param, string s)
      { }

      private object panel;
   }

   class CellCountConfig : Framework.IFormConfig
   {
      public CellCountConfig(Framework.Data.MainData mainData_, UserControl gridAndProcessPanel_, IPanelWithCommand segmentationPanel_) :
         base(mainData_, gridAndProcessPanel_, "CellCountConfig")
      {
         segmentationPanel = segmentationPanel_;
         SegmentCommand = new Framework.DelegateCommand((object param) => {
            segmentationPanel_.Comand(gridPanel.SamplesDataGrid.SelectedItem,
               mainData.folderData.CellCountSubfolder);
         });

         gridPanel = new GridPanel(segmentationPanel.GetPanel());
         gridPanel.SamplesDataGrid.ItemsSource = mainData.dataGrid.Data;

         gridPanel.SamplesDataGrid.SelectionChanged += (object sender, SelectionChangedEventArgs e) => {
            if ((gridPanel.SamplesDataGrid.SelectedItem as Framework.Data.DataGrid.GridItem) != null)
            {
               segmentationPanel.UpdatePanel(
                 (gridPanel.SamplesDataGrid.SelectedItem as Framework.Data.DataGrid.GridItem).Segments);
            }
            else
               segmentationPanel.ClearPanel();
         };
      swapToView();
      }

      protected override void Initialize()
      {
         processor = new CellCountProcessor(this, "CellCount");
         commonPreview = new Preview.CommonPreview(this, "CommonPreview");
         sfilterPreview = new Preview.sFilterPreview(this, "sFilterPreview");
         thresholdPreview = new Preview.ThresholdPreview(this, "ThresholdPreview");
         originalView = new Preview.OriginalView(this, "OriginalView");

         /// Filters preview events
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
            segmentationPanel.UpdatePanel(
               (gridPanel.SamplesDataGrid.SelectedItem as Framework.Data.DataGrid.GridItem).Segments);
         });
      }
      protected override void swapToView()
      {
         gridAndProcessPanel.Content = gridPanel;
      }

      public static string[] fieldNames = new []{ nameof(sfilterLowpass), nameof(sfilterHipass), 
         nameof(trshold), nameof(mfilterRad), nameof(countMinRegion), nameof(countConfLvl),
         nameof(countRMin), nameof(countRMax), nameof(countk), nameof(sfilterOn), nameof(trsholdOn), nameof(mfilterOn) };

      public void setByName(string paramName, string value)
      {
         switch(paramName)
         {
            case nameof(sfilterLowpass): sfilterLowpass = value; break;
            case nameof(sfilterHipass): sfilterHipass = value; break;
            case nameof(trshold): trshold = value; break;
            case nameof(mfilterRad): mfilterRad = value; break;
            case nameof(countMinRegion): countMinRegion = value; break;
            case nameof(countConfLvl): countConfLvl = value; break;
            case nameof(countRMin): countRMin = value; break;
            case nameof(countRMax): countRMax = value; break;
            case nameof(countk): countk = value; break;
            case nameof(sfilterOn): sfilterOn = value; break;
            case nameof(trsholdOn): trsholdOn = value; break;
            case nameof(mfilterOn): mfilterOn = value; break;
         }
      }

      public string[] getAsRow()
      { 
         string[] row = new []{ sfilterLowpass,sfilterHipass, 
         trshold, mfilterRad, countMinRegion, countConfLvl,
         countRMin, countRMax, countk, sfilterOn, trsholdOn, mfilterOn };
         return row;
      }

      public override List<GUI.Items.Framework.ConfigItem> LoadConfig(ExcelWorksheet worksheet)
      {
         if ((worksheet != null) && (worksheet.Dimension != null))
         {
            for (int i = 1; i <= worksheet.Dimension.End.Row; i++)
            {
               setByName(worksheet.Cells[i, 1].Text, worksheet.Cells[i, 2].Text);
            }
         }

         return null;
      }
      public override List<GUI.Items.Framework.ConfigItem> SaveConfig(ExcelWorksheet worksheet)
      {
         var len = fieldNames.Length;
         var values = getAsRow();

         for (int i = 0; i < len; ++i)
         {
            worksheet.Cells[i + 1, 1].Value = fieldNames[i];
            worksheet.Cells[i + 1, 2].Value = values[i];
         }

         return null;
      }

      public ICommand CommonPreviewCommand { get; private set; }
      public ICommand sFilterPreviewCommand { get; private set; }
      public ICommand ThresholdCommand { get; private set; }
      public ICommand OriginalViewCommand { get; private set; }
      public ICommand SegmentCommand { get; private set; }
      public ICommand SelectedChangedCommand { get; private set; }

      #region Values
      public GridPanel gridPanel;
      public IPanelWithCommand segmentationPanel;
      private Preview.CommonPreview commonPreview;
      private Preview.sFilterPreview sfilterPreview;
      private Preview.ThresholdPreview thresholdPreview;
      private Preview.OriginalView originalView;
      public string sfilterLowpass
      {
         get
         {
            return sfilterLowpassValue.ToString();
         }
         set
         {
            sfilterLowpassValue = Convert.ToDouble(value);
            OnPropertyChanged(nameof(sfilterLowpass));
         }
      }
      public string sfilterHipass
      {
         get
         {
            return sfilterHipassValue.ToString();
         }
         set
         {
            sfilterHipassValue = Convert.ToDouble(value);
            OnPropertyChanged(nameof(sfilterHipass));
         }
      }
      public string trshold
      {
         get
         {
            return trsholdValue.ToString();
         }
         set
         {
            trsholdValue = Convert.ToDouble(value);
            OnPropertyChanged(nameof(trshold));
         }
      }
      public string mfilterRad
      {
         get
         {
            return mfilterRadValue.ToString();
         }
         set
         {
            mfilterRadValue = Convert.ToDouble(value);
            OnPropertyChanged(nameof(mfilterRad));
         }
      }
      public string countMinRegion
      {
         get
         {
            return mfilterRadValue.ToString();
         }
         set
         {
            mfilterRadValue = Convert.ToDouble(value);
            OnPropertyChanged(nameof(countMinRegion));
         }
      }
      public string countConfLvl
      {
         get
         {
            return countConfLvlValue.ToString();
         }
         set
         {
            countConfLvlValue = Convert.ToDouble(value);
            OnPropertyChanged(nameof(countConfLvl));
         }
      }
      public string countRMin
      {
         get
         {
            return countRMinValue.ToString();
         }
         set
         {
            countRMinValue = Convert.ToDouble(value);
            OnPropertyChanged(nameof(countRMin));
         }
      }
      public string countRMax
      {
         get
         {
            return countRMaxValue.ToString();
         }
         set
         {
            countRMaxValue = Convert.ToDouble(value);
            OnPropertyChanged(nameof(countRMax));
         }
      }
      public string countk
      {
         get
         {
            return countkValue.ToString();
         }
         set
         {
            countkValue = Convert.ToDouble(value);
            OnPropertyChanged(nameof(countk));
         }
      }
      public string sfilterOn
      {
         get
         {
            return sfilterOnValue.ToString();
         }
         set
         {
            sfilterOnValue = Convert.ToBoolean(value);
            OnPropertyChanged(nameof(sfilterOn));
         }
      }
      public string trsholdOn
      {
         get
         {
            return trsholdOnValue.ToString();
         }
         set
         {
            trsholdOnValue = Convert.ToBoolean(value);
            OnPropertyChanged(nameof(trsholdOn));
         }
      }
      public string mfilterOn
      {
         get
         {
            return mfilterOnValue.ToString();
         }
         set
         {
            mfilterOnValue = Convert.ToBoolean(value);
            OnPropertyChanged(nameof(mfilterOn));
         }
      }

      public double sfilterLowpassValue { get; set; } = 2;
      public double sfilterHipassValue { get; set; } = 100;
      public double trsholdValue { get; set; } = 0;
      public double mfilterRadValue { get; set; } = 3;
      public double countMinRegionValue { get; set; } = 100;
      public double countConfLvlValue { get; set; } = 0.1;
      public double countRMinValue { get; set; } = 0;
      public double countRMaxValue { get; set; } = 15;
      public double countkValue { get; set; } = 1;
      public bool sfilterOnValue { get; set; } = true;
      public bool trsholdOnValue { get; set; } = true;
      public bool mfilterOnValue { get; set; } = false;
      #endregion
   }
}
