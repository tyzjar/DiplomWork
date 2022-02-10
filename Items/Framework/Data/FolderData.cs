using System.Windows.Input;
using OfficeOpenXml;

namespace GUI.Items.Framework.Data
{
   public class FolderData : ConfigItem
   {
      public FolderData():
         base("FolderData")
      {
         AddFolderCommand = new DelegateCommand((object param) => { AddFolderEvent(); });
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

      public string[] getAsRow()
      {
         string[] row = new[]{ MorphSubfolder,MaskSubfolder,
         SampleSubfolder, CropSubfolder, IntensitySubfolder,
         SubtractionSubfolder, CellCountSubfolder};
         return row;
      }
      public void setByName(string paramName, string value)
      {
         switch (paramName)
         {
            case nameof(MorphSubfolder): MorphSubfolder = value; break;
            case nameof(MaskSubfolder): MaskSubfolder = value; break;
            case nameof(SampleSubfolder): SampleSubfolder = value; break;
            case nameof(CropSubfolder): CropSubfolder = value; break;
            case nameof(IntensitySubfolder): IntensitySubfolder = value; break;
            case nameof(SubtractionSubfolder): SubtractionSubfolder = value; break;
            case nameof(CellCountSubfolder): CellCountSubfolder = value; break;
         }
      }

      #region events
      public delegate void AddFolderDelegate();
      public event AddFolderDelegate AddFolderEvent = () => { };
      public ICommand AddFolderCommand { get; private set; }
      #endregion

      public string XmlAddFolder 
      {
         get
         {
            return XmlAddFolderValue;
         }
         set
         {
            if (value != XmlAddFolderValue)
            {
               XmlAddFolderValue = value;
               OnPropertyChanged(nameof(XmlAddFolder));
            }
         } 
      }
      public string XmlInSample
      {
         get
         {
            return XmlInSampleValue;
         }
         set
         {
            if (value != XmlInSampleValue)
            {
               XmlInSampleValue = value;
               OnPropertyChanged(nameof(XmlInSample));
            }
         }
      }
      public string MorphSubfolder
      {
         get
         {
            return MorphSubfolderValue;
         }
         set
         {
            if (value != MorphSubfolderValue)
            {
               MorphSubfolderValue = value;
               OnPropertyChanged(nameof(MorphSubfolder));
            }
         }
      }
      public string MaskSubfolder
      {
         get
         {
            return MaskSubfolderValue;
         }
         set
         {
            if (value != MaskSubfolderValue)
            {
               MaskSubfolderValue = value;
               OnPropertyChanged(nameof(MaskSubfolder));
            }
         }
      }
      public string SampleSubfolder
      {
         get
         {
            return SampleSubfolderValue;
         }
         set
         {
            if (value != SampleSubfolderValue)
            {
               SampleSubfolderValue = value;
               OnPropertyChanged(nameof(SampleSubfolder));
            }
         }
      }
      public string CropSubfolder
      {
         get
         {
            return CropSubfolderValue;
         }
         set
         {
            if (value != CropSubfolderValue)
            {
               CropSubfolderValue = value;
               OnPropertyChanged(nameof(CropSubfolder));
            }
         }
      }
      public string IntensitySubfolder
      {
         get
         {
            return IntensitySubfolderValue;
         }
         set
         {
            if (value != IntensitySubfolderValue)
            {
               IntensitySubfolderValue = value;
               OnPropertyChanged(nameof(IntensitySubfolder));
            }
         }
      }
      public string SubtractionSubfolder
      {
         get
         {
            return SubtractionSubfolderValue;
         }
         set
         {
            if (value != SubtractionSubfolderValue)
            {
               SubtractionSubfolderValue = value;
               OnPropertyChanged(nameof(SubtractionSubfolder));
            }
         }
      }
      public string CellCountSubfolder
      {
         get
         {
            return CellCountSubfolderValue;
         }
         set
         {
            if (value != CellCountSubfolderValue)
            {
               CellCountSubfolderValue = value;
               OnPropertyChanged(nameof(CellCountSubfolder));
            }
         }
      }

      private string XmlAddFolderValue { get; set; } = "";
      private string XmlInSampleValue { get; set; } = "";
      private string MorphSubfolderValue { get; set; } = "Morph";
      private string MaskSubfolderValue { get; set; } = "Masked";
      private string SampleSubfolderValue { get; set; } = "Pattern 1";
      private string CropSubfolderValue { get; set; } = "Crop";
      private string IntensitySubfolderValue { get; set; } = "Without_aberration";
      private string SubtractionSubfolderValue { get; set; } = "Subtraction_picture";
      private string CellCountSubfolderValue { get; set; } = "Masked";

      public static string[] fieldNames = new[]{ nameof(MorphSubfolder), nameof(MaskSubfolder),
         nameof(SampleSubfolder), nameof(CropSubfolder), nameof(IntensitySubfolder),
         nameof(SubtractionSubfolder), nameof(CellCountSubfolder) };
   }
}
