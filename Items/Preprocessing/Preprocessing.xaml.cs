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
         SelectPanel.Content = new Framework.SelectedPanel(mainData_.openSaveEvents);

      }

   }
}
