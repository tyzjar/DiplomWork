using System.Windows.Input;

namespace GUI.Items.Framework.Data
{
   public class OpenSaveEvents : ViewModelBase
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
      public event InstructionOpenSaveDelegate InstructionSaveEvent = () => { };
      public event InstructionOpenSaveDelegate InstructionSaveAsEvent = () => { };
      public event InstructionOpenSaveDelegate InstructionCreateEvent = () => { };
      public event InstructionOpenSaveDelegate SubfolderSettingsEvent = () => { };

      public OpenSaveEvents()
      {
         OpenCommand = new DelegateCommand((object param) => { InstructionOpenEvent(); });
         SaveCommand = new DelegateCommand( (object param) => { InstructionSaveEvent(); } );
         SaveAsCommand = new DelegateCommand( (object param) => { InstructionSaveAsEvent(); } );
         CreateCommand = new DelegateCommand( (object param) => { InstructionCreateEvent(); } );
         SubfolderSettingsCommand = new DelegateCommand((object param) => { SubfolderSettingsEvent(); });
      }

      public ICommand OpenCommand { get; private set; }
      public ICommand SaveCommand { get; private set; }
      public ICommand SaveAsCommand { get; private set; }
      public ICommand CreateCommand { get; private set; }
      public ICommand SubfolderSettingsCommand { get; private set; }
   }
}
