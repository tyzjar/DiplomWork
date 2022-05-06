using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GUI.Items.Dalmatian
{
   class SegmentsControl
   {
      public SegmentsControl(Framework.Data.DataGrid.GridItem item, Framework.Data.MainData mainData_)
      {

         try
         {
            mainData = mainData_;
            selectedList = item.Segments as SegmentListControl;
            ViewWindow = new SegmentsView();
            panel = new SegmentationPanel();

            panel.SegmentsDataGrid.ItemsSource = selectedList.segmentsList;
            ViewWindow.SegmentationPanelView.Content = panel;
            ViewWindow.DataContext = this;

            // Add segment button
            AddSegment = new Framework.DelegateCommand((object param) => {
               addSegment();
            });

            // Delete segment button
            DeleteSegment = new Framework.DelegateCommand((object param) => {
               deleteSegment();
            });

            // Apply to all button
            ApplyToAllSegment = new Framework.DelegateCommand((object param) => {
               applyToAllSegment();
            });

            // Count button
            ApplyToAllSegment = new Framework.DelegateCommand((object param) => {
               count();
            });

            // Count all button
            ApplyToAllSegment = new Framework.DelegateCommand((object param) => {
               countAll();
            });


            loadSegments.ReadJson();
            loadSegments.onLoadEnd += LoadItems;
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message, "Exeption",
               MessageBoxButton.OK, MessageBoxImage.Error);
         }
      }

      public void StartView()
      {
         ViewWindow.ShowDialog();
      }

      public AutoCompleteFilterPredicate<object> SegmentFilter
      {
         get
         {
            return (searchText, obj) =>
                (obj as Segment).SegmentName.Contains(searchText)
                || (obj as Segment).Id.ToString().Contains(searchText);
         }
      }

      void LoadItems()
      {
         ViewWindow.SegmentNameBox.ItemsSource = loadSegments.ViewSegments;
         ViewWindow.SegmentNameBox.ItemFilter = SegmentFilter;
      }

      void addSegment()
      {
         try
         {
            if (ViewWindow.SegmentNameBox.SelectedItem == null)
               throw new Framework.StandartExceptions("Please select correct segment in the list!", false);
            selectedList.segmentsList.Add(ViewWindow.SegmentNameBox.SelectedItem as Segment);
         }
         catch (Framework.StandartExceptions sex)
         {
            MessageBox.Show(sex.Message, "Exeption",
               MessageBoxButton.OK, sex.critical ? MessageBoxImage.Error
               : MessageBoxImage.Warning);
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message, "Exeption",
               MessageBoxButton.OK, MessageBoxImage.Error);
         }
      }

      void deleteSegment()
      {
         var i = panel.SegmentsDataGrid.SelectedIndex;
         if (i > 0)
         {
            try
            {
               selectedList.segmentsList.RemoveAt(i);
               panel.SegmentsDataGrid.SelectedIndex = i - 1;
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message, "Exeption",
                  MessageBoxButton.OK, MessageBoxImage.Error);
            }
         }
      }

      void applyToAllSegment()
      {
         try
         {
            foreach (var item in mainData.dataGrid.Data)
            {
               item.Segments = selectedList;
            }
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message, "Exeption",
               MessageBoxButton.OK, MessageBoxImage.Error);
         }
      }

      void count()
      { 
      }

      void countAll()
      {
         try
         {

         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message, "Exeption",
               MessageBoxButton.OK, MessageBoxImage.Error);
         }
      }

      #region ComandControl
      public ICommand AddSegment { get; private set; }
      public ICommand DeleteSegment { get; private set; }
      public ICommand ApplyToAllSegment { get; private set; }
      public ICommand Count { get; private set; }
      public ICommand CountAll { get; private set; }
      #endregion

      private SegmentsView ViewWindow;
      private SegmentationPanel panel;
      private SegmentListControl selectedList;
      private LoadSegments loadSegments = new LoadSegments();
      private Framework.Data.MainData mainData;
   }
}
