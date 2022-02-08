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
using System.ComponentModel;

namespace GUI.Items.Morph
{
   /// <summary>
   /// Interaction logic for Morph.xaml
   /// </summary>
   ///
   public partial class Morph : UserControl
   {
      MorphConfig config;
      public Morph(Framework.Data.MainData mainData_)
      {
         InitializeComponent();
         config = new MorphConfig(mainData_, GridAndProcessPanel);

         /// Создание панелей по шаблонам.
         this.DataContext = config;
         AddFolder.Content = new GUI.Items.Framework.AddFolderPanel(mainData_.folderData);
         SelectPanel.Content = new GUI.Items.Framework.SelectedPanel(mainData_.selectedFile);
         OpenSavePanel.Content = new GUI.Items.Framework.OpenSavePanel(mainData_.openSaveEvents);

         /// Заполнение данными элементов управления.
         MorphAgeComboBox.ItemsSource = MorphConfig.AgeValues;
      }

   }
}
