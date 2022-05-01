using System.Collections.Generic;
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
     

      #region events
      public delegate void AddFolderDelegate();
      public event AddFolderDelegate AddFolderEvent = () => { };
      public ICommand AddFolderCommand { get; private set; }
      #endregion

      public string XmlAddFolder 
      {
         get
         {
            return variables.XmlAddFolder;
         }
         set
         {
            if (value != variables.XmlAddFolder)
            {
               variables.XmlAddFolder = value;
               OnPropertyChanged(nameof(XmlAddFolder));
            }
         } 
      }
      public string XmlInSample
      {
         get
         {
            return variables.XmlInSample;
         }
         set
         {
            if (value != variables.XmlInSample)
            {
               variables.XmlInSample = value;
               OnPropertyChanged(nameof(XmlInSample));
            }
         }
      }
      public string MorphSubfolder
      {
         get
         {
            return variables.MorphSubfolder;
         }
         set
         {
            if (value != variables.MorphSubfolder)
            {
               variables.MorphSubfolder = value;
               OnPropertyChanged(nameof(MorphSubfolder));
            }
         }
      }
      public string MaskSubfolder
      {
         get
         {
            return variables.MaskSubfolder;
         }
         set
         {
            if (value != variables.MaskSubfolder)
            {
               variables.MaskSubfolder = value;
               OnPropertyChanged(nameof(MaskSubfolder));
            }
         }
      }
      public string SampleSubfolder
      {
         get
         {
            return variables.SampleSubfolder;
         }
         set
         {
            if (value != variables.SampleSubfolder)
            {
               variables.SampleSubfolder = value;
               OnPropertyChanged(nameof(SampleSubfolder));
            }
         }
      }
      public string CropSubfolder
      {
         get
         {
            return variables.CropSubfolder;
         }
         set
         {
            if (value != variables.CropSubfolder)
            {
               variables.CropSubfolder = value;
               OnPropertyChanged(nameof(CropSubfolder));
            }
         }
      }
      public string IntensitySubfolder
      {
         get
         {
            return variables.IntensitySubfolder;
         }
         set
         {
            if (value != variables.IntensitySubfolder)
            {
               variables.IntensitySubfolder = value;
               OnPropertyChanged(nameof(IntensitySubfolder));
            }
         }
      }
      public string SubtractionSubfolder
      {
         get
         {
            return variables.SubtractionSubfolder;
         }
         set
         {
            if (value != variables.SubtractionSubfolder)
            {
               variables.SubtractionSubfolder = value;
               OnPropertyChanged(nameof(SubtractionSubfolder));
            }
         }
      }
      public string CellCountSubfolder
      {
         get
         {
            return variables.CellCountSubfolder;
         }
         set
         {
            if (value != variables.CellCountSubfolder)
            {
               variables.CellCountSubfolder = value;
               OnPropertyChanged(nameof(CellCountSubfolder));
            }
         }
      }


      public override void SetVariables(SaveVariables v) { variables = v as Variables; }
      public override SaveVariables GetVariables() => variables;
      public class Variables : Framework.ConfigItem.SaveVariables
      {
         public string XmlAddFolder { get; set; } = "";
         public string XmlInSample { get; set; } = "";
         public string MorphSubfolder { get; set; } = "Morph";
         public string MaskSubfolder { get; set; } = "Masked";
         public string SampleSubfolder { get; set; } = "Pattern 1";
         public string CropSubfolder { get; set; } = "Crop";
         public string IntensitySubfolder { get; set; } = "Without_aberration";
         public string SubtractionSubfolder { get; set; } = "Subtraction_picture";
         public string CellCountSubfolder { get; set; } = "Pattern 1";
      }

      private Variables variables = new Variables();
   }
}
