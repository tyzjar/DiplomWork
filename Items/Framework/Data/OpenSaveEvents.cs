using System.Windows.Input;

namespace GUI.Items.Framework.Data
{
   public class OpenSaveEvents : ViewModelBase
   { 
      public delegate void InstructionOpenSaveDelegate();
      public event InstructionOpenSaveDelegate InstructionSaveEvent = () => { };
      public event InstructionOpenSaveDelegate InstructionSaveAsEvent = () => { };
      public event InstructionOpenSaveDelegate InstructionCreateEvent = () => { };

      public OpenSaveEvents()
      {
         SaveCommand = new DelegateCommand( (object param) => { InstructionSaveEvent(); } );
         SaveAsCommand = new DelegateCommand( (object param) => { InstructionSaveAsEvent(); } );
         CreateCommand = new DelegateCommand( (object param) => { InstructionCreateEvent(); } );
      }

      public ICommand SaveCommand { get; private set; }
      public ICommand SaveAsCommand { get; private set; }
      public ICommand CreateCommand { get; private set; }
   }
}
