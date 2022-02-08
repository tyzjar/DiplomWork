using System.Windows.Input;
using System.Windows;

namespace GUI.Items.Framework.Data
{
   public class FolderData : ViewModelBase
   {
      public FolderData()
      {
         AddFolderCommand = new DelegateCommand((object param) => { AddFolderEvent(); });
      }

      public delegate void AddFolderDelegate();
      public event AddFolderDelegate AddFolderEvent = () => { };
      public ICommand AddFolderCommand { get; private set; }

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

      public string XmlAddFolderValue { get; set; } = "";
      public string XmlInSampleValue { get; set; } = "";
      public string MorphSubfolderValue { get; set; } = "Morph";
      public string MaskSubfolderValue { get; set; } = "Masked";
      public string SampleSubfolderValue { get; set; } = "Pattern 1";
      public string CropSubfolderValue { get; set; } = "Crop";
      public string IntensitySubfolderValue { get; set; } = "Without_aberration";
      public string SubtractionSubfolderValue { get; set; } = "Subtraction_picture";
   }
}
