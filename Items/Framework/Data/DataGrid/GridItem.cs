/// <summary>
/// Элемент таблицы с основными данными по образцу.
/// </summary>
/// 

using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Media.Imaging;

namespace GUI.Items.Framework.Data.DataGrid
{

   public abstract class SegmentsList { }

   public class GridItem : ViewModelBase
   {
      #region CONSTRUCTOR
      private void init()
      {
         PropertyChanged += propertyChanged;

         MaskStateValue = PreprocState.notStarted;
         CropStateValue = PreprocState.notStarted;
         IntensityStateValue = PreprocState.notStarted;
         SubtractionStateValue = PreprocState.notStarted;
      }
      public void InitFolder(FolderData folderData_)
      {
         folderData = folderData_;
         init();
      }

      // Only for JsonLoad, be careful! After that must be called InitFolder.
      public GridItem()
      {
      }

      public GridItem(FolderData folderData_)
      {
         folderData = folderData_;
         init();
         InitFolder(folderData_);
      }

      public GridItem(string[] row, FolderData folderData_)
      {
         int i = 0;
         SampleName = row[i++];
         InSampleName = row[i++];

         init();
         InitFolder(folderData_);
      }
      #endregion

      public void udpateStates()
      {
         if (MaskState != PreprocState.failed)
            MaskState = PreprocStateHelper.StateByFolder(SampleName + "\\" + folderData.MaskSubfolder);
         if (CropState != PreprocState.failed)
            CropState = PreprocStateHelper.StateByFolder(SampleName + "\\" + folderData.CropSubfolder);
         if (IntensityState != PreprocState.failed)
            IntensityState = PreprocStateHelper.StateByFolder(SampleName + "\\" + folderData.IntensitySubfolder);
         if (SubtractionState != PreprocState.failed)
            SubtractionState = PreprocStateHelper.StateByFolder(SampleName + "\\" + folderData.SubtractionSubfolder);
         AtlasTUpdate();
      }

      public bool AtlasTAvailible()
      {
         if (AtlasTFiles.Count == 0)
            return false;

         foreach (var file in AtlasTFiles)
         {
            var fileName = SampleName + "\\" + file.FileName;
            if ((!Directory.Exists(SampleName)) || (!File.Exists(fileName)))
               return false;
         }
         return true;
      }
      public void AtlasTUpdate()
      {
         if ((AtlasTFiles.Count == 0) && (Directory.Exists(SampleName)))
         {
            var str = new string[] {FolderData.Atlas + FolderData.AtlasExtension,
               FolderData.AtlasReference + FolderData.AtlasExtension};

            foreach (var file in str)
            {
               if (File.Exists(SampleName + "\\" + file))
               {
                  AtlasTFiles.Add(new AtlasTransformationFile(file));
                  break;
               }
            }
         }
         OnPropertyChanged(nameof(AtlasMorphStatePic));
      }

      public bool AtlasTContain(string name)
      {
         foreach (var file in AtlasTFiles)
         {
            if (Equals(file.FileName, name))
               return true;
         }
         return false;
      }
      private void propertyChanged(object sender, PropertyChangedEventArgs e)
      {
         if (e.PropertyName == "SampleName")
         {
            udpateStates();
         }
      }

      public void ReloadTfiles(string TFile)
      {
         AtlasTFiles.Clear();
         AtlasTFiles.Add(new AtlasTransformationFile(TFile));
      }

      #region PROPERTIES
      public string SampleName
      {
         get
         {
            return SampleNameValue;
         }
         set
         {
            if (!Equals(SampleNameValue, value))
            {
               SampleNameValue = value.Trim(Utils.delims);
               OnPropertyChanged(nameof(SampleName));
            }
         }
      }
      public string InSampleName
      {
         get => InSampleNameValue;
         set
         {
            if (!Equals(InSampleNameValue, value))
            {
               InSampleNameValue = value.Trim(Utils.delims);
               OnPropertyChanged(nameof(InSampleNameValue));
            }
         }
      }
      public static List<string> InSampleValues
      {
         get => FolderData.InSampleValues;
      }
      public PreprocState MaskState
      {
         get
         {
            return MaskStateValue;
         }

         set
         {
            if (MaskStateValue != value)
            {
               MaskStateValue = value;
               OnPropertyChanged(nameof(MaskState));
               OnPropertyChanged(nameof(MaskStatePic));
            }
         }
      }
      public PreprocState CropState
      {
         get
         {
            return CropStateValue;
         }

         set
         {
            if (CropStateValue != value)
            {
               CropStateValue = value;
               OnPropertyChanged(nameof(CropState));
               OnPropertyChanged(nameof(CropStatePic));
            }
         }
      }
      public PreprocState IntensityState
      {
         get
         {
            return IntensityStateValue;
         }

         set
         {
            if (IntensityStateValue != value)
            {
               IntensityStateValue = value;
               OnPropertyChanged(nameof(IntensityState));
               OnPropertyChanged(nameof(IntensityStatePic));
            }
         }
      }
      public PreprocState SubtractionState
      {
         get
         {
            return SubtractionStateValue;
         }

         set
         {
            if (SubtractionStateValue != value)
            {
               SubtractionStateValue = value;
               OnPropertyChanged(nameof(SubtractionState));
               OnPropertyChanged(nameof(SubtractionStatePic));
            }
         }
      }

      [JsonIgnore]
      public BitmapImage MaskStatePic
      {
         get
         {
            return View.StateToImage.GetPicture(PreprocStateHelper.IsDone(MaskState));
         }
      }
      [JsonIgnore]
      public BitmapImage CropStatePic
      {
         get
         {
            return View.StateToImage.GetPicture(PreprocStateHelper.IsDone(CropState));
         }
      }
      [JsonIgnore]
      public BitmapImage IntensityStatePic
      {
         get
         {
            return View.StateToImage.GetPicture(PreprocStateHelper.IsDone(IntensityState));
         }
      }
      [JsonIgnore]
      public BitmapImage SubtractionStatePic
      {
         get
         {
            return View.StateToImage.GetPicture(PreprocStateHelper.IsDone(SubtractionState));
         }
      }
      [JsonIgnore]
      public BitmapImage AtlasMorphStatePic
      {
         get
         {
            return View.StateToImage.GetPicture(AtlasTAvailible());
         }
      }
      #endregion


      public string SampleFolder
      { 
         get => (SampleName + "\\" + folderData.SampleSubfolder).Trim(Utils.delims);
      }
      public string MaskFolder
      {
         get => (SampleName + "\\" + folderData.MaskSubfolder).Trim(Utils.delims);
      }
      public string CellCountFolder
      {
         get => (SampleName + "\\" + folderData.CellCountSubfolder).Trim(Utils.delims);
      }
      public string MorphSaveFolder
      {
         get => (SampleName + "\\" + folderData.MorphSaveSubfolder).Trim(Utils.delims);
      }

      public string MorphToFolder
      {
         get => (SampleName + "\\" + folderData.MorphToSubfolder).Trim(Utils.delims);
      }


      #region VARIABLES
      public SegmentsList Segments;
      public BindingList<AtlasTransformationFile> AtlasTFiles = new BindingList<AtlasTransformationFile>();

      private string SampleNameValue = "";
      private string InSampleNameValue = "";
      private PreprocState MaskStateValue;
      private PreprocState CropStateValue;
      private PreprocState IntensityStateValue;
      private PreprocState SubtractionStateValue;
      private FolderData folderData;
      #endregion
   }

}