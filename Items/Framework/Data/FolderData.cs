using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Linq;

namespace GUI.Items.Framework.Data
{
   public class FolderData : ConfigItem
   {
      //var s = @"aasaasasas/ggg/ooooo/ggg//"; Utils.RemoveSubfolder(ref s, "ggg"); MessageBox.Show(s);
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

      public static string Atlas = "Atlas";
      public static string AtlasReference = "AtlasReference";
      public static string AtlasExtension = ".mat";

      public bool AtlasAndAtalasRefCheckFolderName(ref string folder, ref string fileName)
      {
         if (Equals(folder, Atlas))
         {
            folder = AtlasFolder;
            fileName = Atlas + AtlasExtension;
            return true;
         }

         if (Equals(folder, AtlasReference))
         {
            folder = AtlasRefFolder;
            fileName = AtlasReference + AtlasExtension;
            return true;
         }

         var atl = AtlasFolder;
         var atlref = AtlasRefFolder;
         atl = atl.Trim(Utils.delims);
         atlref = atlref.Trim(Utils.delims);

         if (Equals(folder, atl))
         {
            fileName = Atlas + AtlasExtension;
            return true;
         }

         if (Equals(folder, atlref))
         {
            fileName = AtlasReference + AtlasExtension;
            return true;
         }

         Utils.RemoveSubfolder(ref atl, MorphToSubfolder);
         Utils.RemoveSubfolder(ref atlref, MorphToSubfolder);
         folder = folder.Trim(Utils.delims);

         if (Equals(folder, atl))
         {
            fileName = Atlas + AtlasExtension;
            return true;
         }

         if (Equals(folder, atlref))
         {
            fileName = AtlasReference + AtlasExtension;
            return true;
         }

         return false;
      }

      public static List<string> InSampleValues { get; set; } = new List<string>(new string[] { Atlas, AtlasReference });

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
      public string MorphToSubfolder
      {
         get
         {
            return variables.MorphToSubfolder;
         }
         set
         {
            if (value != variables.MorphToSubfolder)
            {
               variables.MorphToSubfolder = value;
               OnPropertyChanged(nameof(MorphToSubfolder));
            }
         }
      }
      public string MorphSaveSubfolder
      {
         get
         {
            return variables.MorphSaveSubfolder;
         }
         set
         {
            if (value != variables.MorphSaveSubfolder)
            {
               variables.MorphSaveSubfolder = value;
               OnPropertyChanged(nameof(MorphSaveSubfolder));
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
               variables.AtlasFolder = value.Trim(Utils.delims);
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
               variables.AtlasRefFolder = value.Trim(Utils.delims);
               OnPropertyChanged(nameof(AtlasRefFolder));
            }
         }
      }


      public override void SetVariables(SaveVariables v) 
      { 
         variables = v as Variables;

         OnPropertyChanged(nameof(AddFolderText));
         OnPropertyChanged(nameof(InSampleText));
         OnPropertyChanged(nameof(MorphToSubfolder));
         OnPropertyChanged(nameof(MorphSaveSubfolder));
         OnPropertyChanged(nameof(MaskSubfolder));
         OnPropertyChanged(nameof(SampleSubfolder));
         OnPropertyChanged(nameof(CropSubfolder));
         OnPropertyChanged(nameof(IntensitySubfolder));
         OnPropertyChanged(nameof(SubtractionSubfolder));
         OnPropertyChanged(nameof(CellCountSubfolder));
         OnPropertyChanged(nameof(AtlasFolder));
         OnPropertyChanged(nameof(AtlasRefFolder));
      }
      public override SaveVariables GetVariables() => variables;
      public class Variables : Framework.ConfigItem.SaveVariables
      {
         public string AddFolderText = "";
         public string InSampleText = Atlas;
         public string MorphToSubfolder = "Morph";
         public string MorphSaveSubfolder = "Morph";
         public string MaskSubfolder = "Masked";
         public string SampleSubfolder = "Pattern 1";
         public string CropSubfolder = "Crop";
         public string IntensitySubfolder = "Without_aberration";
         public string SubtractionSubfolder = "Subtraction_picture";
         public string CellCountSubfolder = "Masked";
         public string AtlasFolder = "";
         public string AtlasRefFolder = "";

      }

      private Variables variables = new Variables();
   }
}
