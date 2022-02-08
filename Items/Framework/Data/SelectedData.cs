using System.ComponentModel;
using System.Windows.Input;

namespace GUI.Items.Framework.Data
{
   public class SelectedData : ViewModelBase
   {
      private string SelectedXmlFileValue = "NewFile.xlsx";
      public string SelectedXmlFile
      {
         get
         {
            return this.SelectedXmlFileValue;
         }
         set
         {
            if (value != this.SelectedXmlFileValue)
            {
               this.SelectedXmlFileValue = value;
               OnPropertyChanged(nameof(SelectedXmlFile));
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
