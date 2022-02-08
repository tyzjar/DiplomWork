using System.Windows.Controls;


namespace GUI.Items.Preprocessing
{
   /// <summary>
   /// Interaction logic for Preprocessing.xaml
   /// </summary>
   public partial class Preprocessing : UserControl
   {
      PreprocConfig config;
      public Preprocessing(Framework.Data.MainData mainData_)
      {
         InitializeComponent();
         config = new PreprocConfig(mainData_, GridAndProcessPanel);

         this.DataContext = config;
         AddFolder.Content = new GUI.Items.Framework.AddFolderPanel(mainData_.folderData);
         SelectPanel.Content = new GUI.Items.Framework.SelectedPanel(mainData_.selectedFile);
         OpenSavePanel.Content = new GUI.Items.Framework.OpenSavePanel(mainData_.openSaveEvents);

      }

   }
}
