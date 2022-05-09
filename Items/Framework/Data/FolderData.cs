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
         SelectAddFolder = new DelegateCommand((object param) => { Utils.SelectFolder((string value) => { AddFolderText = value; }); });
         SelectAddInSample = new DelegateCommand((object param) => { Utils.SelectFolder((string value) => { InSampleText = value; }); });
         SelectAtlasFolder = new DelegateCommand((object param) => { Utils.SelectFolder((string value)=> { AtlasFolder = value; }); });
         SelectAtlasReferenceFolder = new DelegateCommand((object param) => { Utils.SelectFolder((string value) => { AtlasRefFolder = value; }); });
      }

      #region events
      public delegate void EmptyHandler();
      public event EmptyHandler AddFolderEvent = () => { };
      public ICommand AddFolderCommand { get; private set; }
      public ICommand SelectAddFolder { get; private set; }
      public ICommand SelectAddInSample { get; private set; }
      public ICommand SelectAtlasFolder { get; private set; }
      public ICommand SelectAtlasReferenceFolder { get; private set; }
      #endregion

      public string AddFolderText 
      {
         get
         {
            return variables.AddFolderText;
         }
         set
         {
            if (value != variables.AddFolderText)
            {
               variables.AddFolderText = value;
               OnPropertyChanged(nameof(AddFolderText));
            }
         } 
      }
      public string InSampleText
      {
         get
         {
            return variables.InSampleText;
         }
         set
         {
            if (value != variables.InSampleText)
            {
               variables.InSampleText = value;
               OnPropertyChanged(nameof(InSampleText));
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
      public string AtlasFolder
      {
         get
         {
            return variables.AtlasFolder;
         }
         set
         {
            if (value != variables.AtlasFolder)
            {
               variables.AtlasFolder = value;
               OnPropertyChanged(nameof(AtlasFolder));
            }
         }
      }
      public string AtlasRefFolder
      {
         get
         {
            return variables.AtlasRefFolder;
         }
         set
         {
            if (value != variables.AtlasRefFolder)
            {
               variables.AtlasRefFolder = value;
               OnPropertyChanged(nameof(AtlasRefFolder));
            }
         }
      }


      public override void SetVariables(SaveVariables v) { variables = v as Variables; }
      public override SaveVariables GetVariables() => variables;
      public class Variables : Framework.ConfigItem.SaveVariables
      {
         public string AddFolderText { get; set; } = "";
         public string InSampleText { get; set; } = "";
         public string MorphSubfolder { get; set; } = "Morph";
         public string MaskSubfolder { get; set; } = "Masked";
         public string SampleSubfolder { get; set; } = "Pattern 1";
         public string CropSubfolder { get; set; } = "Crop";
         public string IntensitySubfolder { get; set; } = "Without_aberration";
         public string SubtractionSubfolder { get; set; } = "Subtraction_picture";
         public string CellCountSubfolder { get; set; } = "Pattern 1";
         public string AtlasFolder { get; set; } = "";
         public string AtlasRefFolder { get; set; } = "";

      }

      private Variables variables = new Variables();
   }
}
