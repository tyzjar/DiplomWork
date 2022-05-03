using GUI.Items.Dalmatian.Segments;
using System.Windows;


namespace GUI.Items.Dalmatian
{
   /// <summary>
   /// Interaction logic for SegmentsControl.xaml
   /// </summary>
   public partial class SegmentsControl : Window
   {
      LoadSegments loadSegments = new LoadSegments();
      public SegmentsControl()
      {
         InitializeComponent();
         loadSegments.ReadJson();

         SegmentationPanelView = new SegmentationPanel();
         SegmentComboBox.ItemsSource = loadSegments.ViewSegments;
      }
   }
}
