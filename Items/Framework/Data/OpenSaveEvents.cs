using System.Windows.Input;

namespace GUI.Items.Framework.Data
{
   public class OpenSaveEvents : ViewModelBase
   { 
      public delegate void InstructionOpenSaveDelegate();
      public event InstructionOpenSaveDelegate InstructionSaveEvent = () => { };
      public event InstructionOpenSaveDelegate InstructionSaveAsEvent = () => { };
      public event InstructionOpenSaveDelegate InstructionCreateEvent = () => { };
      public event InstructionOpenSaveDelegate SubfolderSettingsEvent = () => { };

      public OpenSaveEvents()
      {
         SaveCommand = new DelegateCommand( (object param) => { InstructionSaveEvent(); } );
         SaveAsCommand = new DelegateCommand( (object param) => { InstructionSaveAsEvent(); } );
         CreateCommand = new DelegateCommand( (object param) => { InstructionCreateEvent(); } );
         SubfolderSettingsCommand = new DelegateCommand((object param) => { SubfolderSettingsEvent(); });
      }

      public ICommand SaveCommand { get; private set; }
      public ICommand SaveAsCommand { get; private set; }
      public ICommand CreateCommand { get; private set; }
      public ICommand SubfolderSettingsCommand { get; private set; }
   }
}
