using System.ComponentModel;

namespace GUI.Items.Framework
{
   public class ViewModelBase: INotifyPropertyChanged
   {
      public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
      public void OnPropertyChanged(string propertyName) 
      { 
         PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); 
      } 
   }
}
