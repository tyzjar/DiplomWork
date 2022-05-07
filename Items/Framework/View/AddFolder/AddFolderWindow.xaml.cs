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
   /// Interaction logic for AddFolderWindow.xaml
   /// </summary>
   public partial class AddFolderWindow : Window
   {
      public AddFolderWindow(object addFolderData, bool dalmation)
      {
         InitializeComponent();

         var content = new AddFolderPanel(addFolderData, this);
         AddFolder.Content = content;
         content.MorphInPanel.Visibility = dalmation ? Visibility.Hidden : Visibility.Visible;
      }

      private void KeyEvents(object sender, KeyEventArgs e)
      {
         if ((e.Key == Key.Enter) || (e.Key == Key.Escape))
            this.Close();
      }
   }
}
