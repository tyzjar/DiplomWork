﻿using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Dalmatian.ROI
{
   /// <summary>
   /// Interaction logic for EditPanel.xaml
   /// </summary>
   public partial class EditPanel : Window
   {

      Path myColorPath = new Path();
      ROIEdit mainWindow;

      public EditPanel(ROIEdit mainWindow_)
      {
         InitializeComponent();

         mainWindow = mainWindow_;
         Owner = mainWindow;
         SegmentationPanel.Content = mainWindow.panel;
         mainWindow.panel.onSegmentIndexChanged += SegmentIndexChanged;
         ColorRender();
         this.DataContext = mainWindow.imView;
      }

      void SegmentIndexChanged(int newValue)
      {
         MainColor.Fill = mainWindow.imView.CurrentColor.CreateBrush();
      }

      void ColorRender()
      {
         MainColor.Fill = mainWindow.imView.CurrentColor.CreateBrush();
      }


      void Window_Closing(object sender, CancelEventArgs e)
      {
         Owner.Close();
      }
   }
}
