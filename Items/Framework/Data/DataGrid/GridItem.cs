/// <summary>
/// Элемент таблицы с основными данными по образцу.
/// </summary>
/// 

using Newtonsoft.Json;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace GUI.Items.Framework.Data.DataGrid
{

   public abstract class SegmentsList { }

   public class GridItem : ViewModelBase
   {
      public GridItem()
      {
         MaskStateValue = new PreprocState();
         CropStateValue = new PreprocState();
         IntensityStateValue = new PreprocState();
         SubtractionStateValue = new PreprocState();
      }

      public GridItem(string[] row)
      {
         int i = 0;
         SampleName = row[i++];
         InSampleName = row[i++];
         MaskStateValue = PreprocState.notStarted;
         CropStateValue = PreprocState.notStarted;
         IntensityStateValue = PreprocState.notStarted;
         SubtractionStateValue = PreprocState.notStarted;
         AtlasMorphStateValue = PreprocState.notStarted;
      }

      public string SampleName 
      {
         get
         {
            return SampleNameValue;
         }
         set
         {
            if (!Equals(SampleName, value))
            {
               SampleNameValue = value;
               OnPropertyChanged(nameof(SampleName));
            }
         } 
      }
      public string InSampleName { get; set; } = "";
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
      public PreprocState AtlasMorphState
      {
         get
         {
            return AtlasMorphStateValue;
         }

         set
         {
            if (AtlasMorphStateValue != value)
            {
               AtlasMorphStateValue = value;
               OnPropertyChanged(nameof(AtlasMorphState));
               OnPropertyChanged(nameof(AtlasMorphStatePic));
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
            return View.StateToImage.GetPicture(PreprocStateHelper.IsDone(AtlasMorphState));
         }
      }

      public SegmentsList Segments;
      private string SampleNameValue = "";
      private PreprocState MaskStateValue;
      private PreprocState CropStateValue;
      private PreprocState IntensityStateValue;
      private PreprocState SubtractionStateValue;
      private PreprocState AtlasMorphStateValue;

      public void udpateStates(FolderData folderData)
      {
         if (MaskState != PreprocState.failed)
            MaskState = PreprocStateHelper.StateByFolder(SampleName + "\\" + folderData.MaskSubfolder);
         if (CropState != PreprocState.failed)
            CropState = PreprocStateHelper.StateByFolder(SampleName + "\\" + folderData.CropSubfolder);
         if (IntensityState != PreprocState.failed)
            IntensityState = PreprocStateHelper.StateByFolder(SampleName + "\\" + folderData.IntensitySubfolder);
         if (SubtractionState != PreprocState.failed)
            SubtractionState = PreprocStateHelper.StateByFolder(SampleName + "\\" + folderData.SubtractionSubfolder);

         AtlasMorphState = PreprocStateHelper.StateByFiles(new string[] { SampleName + "\\" + FolderData.Atlas + FolderData.AtlasExtension,
            SampleName + "\\" + FolderData.AtlasReference + FolderData.AtlasExtension});
      }
   }

}