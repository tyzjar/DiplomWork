/// <summary>
/// Элемент таблицы с основными данными по образцу.
/// </summary>
/// 

using Newtonsoft.Json;
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
         GroupName = row[i++];
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
      public string GroupName { get; set; } = "";

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
            }
         }
      }

      [JsonIgnore]
      public BitmapImage MaskStatePic
      {
         get 
         {
            return View.StateToImage.GetPicture(false);
         }
      }
      [JsonIgnore]
      public BitmapImage CropStatePic
      {
         get
         {
            return View.StateToImage.GetPicture(false);
         }
      }
      [JsonIgnore]
      public BitmapImage IntensityStatePic
      {
         get
         {
            return View.StateToImage.GetPicture(false);
         }
      }
      [JsonIgnore]
      public BitmapImage SubtractionStatePic
      {
         get
         {
            return View.StateToImage.GetPicture(false);
         }
      }
      [JsonIgnore]
      public BitmapImage AtlasMorphState
      {
         get
         {
            return View.StateToImage.GetPicture(false);
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
      }

   }

}