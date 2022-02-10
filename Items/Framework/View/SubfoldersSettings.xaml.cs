using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

      private void Button_Click(object sender, RoutedEventArgs e)
      {
         this.Close();
      }
   }
}
