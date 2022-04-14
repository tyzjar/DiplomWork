using System.ComponentModel;
using System.Windows;


namespace Dalmatian.ROI
{
   /// <summary>
   /// Interaction logic for EditPanel.xaml
   /// </summary>
   public partial class EditPanel : Window
   {
      public EditPanel(ROIEdit mainWindow)
      {
         InitializeComponent();

         Owner = mainWindow;
         SegmentationPanel.Content = mainWindow.panel;
         this.DataContext = mainWindow.imView;
      }

      void Window_Closing(object sender, CancelEventArgs e)
      {
         Owner.Close();
      }
   }
}
