/// <summary>
/// Элемент таблицы с основными данными по образцу.
/// </summary>
/// 

using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Windows;

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
         MaskStateValue = new PreprocState(row[i++]);
         CropStateValue = new PreprocState(row[i++]);
         IntensityStateValue = new PreprocState(row[i++]);
         SubtractionStateValue = new PreprocState(row[i++]);
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
      public string MaskState
      {
         get
         {
            return PreprocState.getStringState(MaskStateValue.state);
         }

         set
         {
            if (!Equals(MaskState, value))
            {
               MaskStateValue.setStateFromString(value);
               OnPropertyChanged(nameof(MaskState));
            }
         }
      }
      public string CropState
      {
         get
         {
            return PreprocState.getStringState(CropStateValue.state);
         }

         set
         {
            if (!Equals(CropState, value))
            {
               CropStateValue.setStateFromString(value);
               OnPropertyChanged(nameof(CropState));
            }
         }
      }
      public string IntensityState
      {
         get
         {
            return PreprocState.getStringState(IntensityStateValue.state);
         }

         set
         {
            if (!Equals(IntensityState, value))
            {
               IntensityStateValue.setStateFromString(value);
               OnPropertyChanged(nameof(IntensityState));
            }
         }
      }
      public string SubtractionState
      {
         get
         {
            return PreprocState.getStringState(SubtractionStateValue.state);
         }

         set
         {
            if (!Equals(SubtractionState, value))
            {
               SubtractionStateValue.setStateFromString(value);
               OnPropertyChanged(nameof(SubtractionState));
            }
         }
      }


      private string SampleNameValue = "";
      [JsonIgnore]
      public PreprocState MaskStateValue;
      [JsonIgnore]
      public PreprocState CropStateValue;
      [JsonIgnore]
      public PreprocState IntensityStateValue;
      [JsonIgnore]
      public PreprocState SubtractionStateValue;
      public SegmentsList Segments;

      public void udpateStates(FolderData folderData)
      {
         if (MaskStateValue.state != PreprocState.States.failed)
            MaskState = PreprocState.StateByFolder(SampleName + "\\" + folderData.MaskSubfolder);
         if (CropStateValue.state != PreprocState.States.failed)
            CropState = PreprocState.StateByFolder(SampleName + "\\" + folderData.CropSubfolder);
         if (IntensityStateValue.state != PreprocState.States.failed)
            IntensityState = PreprocState.StateByFolder(SampleName + "\\" + folderData.IntensitySubfolder);
         if (SubtractionStateValue.state != PreprocState.States.failed)
            SubtractionState = PreprocState.StateByFolder(SampleName + "\\" + folderData.SubtractionSubfolder);
      }

   }

}