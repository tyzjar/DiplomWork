using System;
using System.Windows.Input;
using System.Windows.Controls;
using OfficeOpenXml;

namespace GUI.Items.Dalmatian
{
   class CellCountConfig : Framework.IFormConfig
   {
      public CellCountConfig(Framework.Data.MainData mainData_, UserControl gridAndProcessPanel_) :
         base(mainData_, gridAndProcessPanel_, "CellCountConfig")
      {
      }

      protected override void Initialize()
      {
         gridPanel = new GridPanel();
         gridPanel.SamplesDataGrid.ItemsSource = mainData.dataGrid.Data;
         processor = new CellCountProcessor(this, "CellCount");
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

      public override void LoadConfig(ExcelWorksheet worksheet)
      {
         if ((worksheet != null) && (worksheet.Dimension != null))
         {
            for (int i = 1; i <= worksheet.Dimension.End.Row; i++)
            {
               setByName(worksheet.Cells[i, 1].Text, worksheet.Cells[i, 2].Text);
            }
         }
      }
      public override void SaveConfig(ExcelWorksheet worksheet)
      {
         var len = fieldNames.Length;
         var values = getAsRow();

         for (int i = 0; i < len; ++i)
         {
            worksheet.Cells[i + 1, 1].Value = fieldNames[i];
            worksheet.Cells[i + 1, 2].Value = values[i];
         }
      }

      #region Values
      public GridPanel gridPanel;
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
            return mfilterRadValue.ToString();
         }
         set
         {
            mfilterRadValue = Convert.ToDouble(value);
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

      public double sfilterLowpassValue { get; set; }
      public double sfilterHipassValue { get; set; }
      public double trsholdValue { get; set; }
      public double mfilterRadValue { get; set; }
      public double countMinRegionValue { get; set; }
      public double countConfLvlValue { get; set; }
      public double countRMinValue { get; set; }
      public double countRMaxValue { get; set; }
      public double countkValue { get; set; }
      public bool sfilterOnValue { get; set; } = true;
      public bool trsholdOnValue { get; set; } = true;
      public bool mfilterOnValue { get; set; } = false;
      #endregion
   }
}
