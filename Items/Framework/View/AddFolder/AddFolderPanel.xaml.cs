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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI.Items.Framework
{
   /// <summary>
   /// Interaction logic for AddFolderPanel.xaml
   /// </summary>
   public partial class AddFolderPanel : UserControl
   {
      private Window m_owner;
      public AddFolderPanel(object addFolderData, Window owner)
      {
         InitializeComponent();
         m_owner = owner;
         this.DataContext = addFolderData;
      }

      private void Button_Click(object sender, RoutedEventArgs e)
      {
         m_owner.Close();
      }
   }
}
