using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Dalmatian.ROI
{
   /// <summary>
   /// Interaction logic for EditPanel.xaml
   /// </summary>
   public partial class EditPanel : Window
   {
      ROIEdit mainWindow;

      public EditPanel(ROIEdit mainWindow_)
      {
         InitializeComponent();

         mainWindow = mainWindow_;
         Owner = mainWindow;
         SegmentationPanel.Content = mainWindow.imFormControl.panel;
         mainWindow.imFormControl.panel.onSegmentIndexChanged += SegmentIndexChanged;
         SegmentIndexChanged(0);


         // imView context
         this.DataContext = mainWindow.imFormControl.imView;

         // imFormControl context
         GridControlPanel.DataContext = mainWindow.imFormControl;
         ResetButton.DataContext = mainWindow.imFormControl;
      }

      void SegmentIndexChanged(int newValue)
      {
         MainColor.Fill = mainWindow.imFormControl.imView.CurrentBrush;
      }

      void Window_Closing(object sender, CancelEventArgs e)
      {
         Owner.Close();
      }

      private void Button_Click_1(object sender, RoutedEventArgs e)
      {
         ColorDialog colorDialog = new ColorDialog();

         if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
         {
            mainWindow.imFormControl.imView.CurrentColor = Color.FromRgb(
               colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B);
            MainColor.Fill = mainWindow.imFormControl.imView.CurrentBrush;
         }
      }
   }
}
