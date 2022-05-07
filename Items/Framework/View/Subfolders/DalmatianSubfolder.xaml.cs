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
   /// Interaction logic for DalmatianSubfolder.xaml
   /// </summary>
   public partial class DalmatianSubfolder : Window
   {
      public DalmatianSubfolder(Data.FolderData folderData_)
      {
         InitializeComponent();
         this.DataContext = folderData_;
      }


      private void KeyEvents(object sender, KeyEventArgs e)
      {
         if ((e.Key == Key.Enter) || (e.Key == Key.Escape))
            this.Close();
      }

      private void Button_Click(object sender, RoutedEventArgs e)
      {
         this.Close();
      }
   }
}
