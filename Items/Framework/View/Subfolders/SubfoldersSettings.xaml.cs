using System.Windows;
using System.Windows.Input;

namespace GUI.Items.Framework.View
{
   /// <summary>
   /// Interaction logic for SubfoldersSettings.xaml
   /// </summary>
   public partial class SubfoldersSettings : Window
   {
      public SubfoldersSettings(Data.FolderData folderData_)
      {
         InitializeComponent();
         this.DataContext = folderData_;
      }


      private void KeyEvents(object sender, KeyEventArgs e)
      {
         if((e.Key == Key.Enter)|| (e.Key == Key.Escape))
            this.Close();
      }

      private void Button_Click(object sender, RoutedEventArgs e)
      {
         this.Close();
      }
   }
}
