using System.ComponentModel;
using System.Windows.Input;

namespace GUI.Items.Framework.Data
{
   public class SelectedData : ViewModelBase
   {
      private string SelectedProjectFileValue = "NewFile.xlsx";
      public string SelectedProjectFile
      {
         get
         {
            return this.SelectedProjectFileValue;
         }
         set
         {
            if (value != this.SelectedProjectFileValue)
            {
               this.SelectedProjectFileValue = value;
               OnPropertyChanged(nameof(SelectedProjectFile));
            }
         }
      }

      public delegate void InstructionOpenSaveDelegate();
      public event InstructionOpenSaveDelegate InstructionOpenEvent = () => { };

      public SelectedData()
      {
         OpenCommand = new DelegateCommand( (object param) => { InstructionOpenEvent(); } );
      }

      public ICommand OpenCommand { get; private set; }
   }
}
