﻿using OfficeOpenXml;
using gui_preview;
using MathWorks.MATLAB.NET.Arrays;
using System.Windows;

namespace GUI.Items.Dalmatian.Preview
{
   class OriginalView : Framework.MatlabProcessor.IMatlab
   {
      public OriginalView(CellCountConfig config_, string tempFileName_) :
         base(tempFileName_)
      {
         config = config_;
      }

      protected override void MatlabThread()
      {
         MatlabPreview preview = new MatlabPreview();
         MessageBox.Show(sampleName);
         preview.gui_originalView(0, new MWCharArray(sampleName),
            config.countk);
      }

      protected override void PopulateBuffer(ExcelWorksheet worksheet)
      {
         if ((config.gridPanel.SamplesDataGrid.SelectedItem
            as Framework.Data.DataGrid.GridItem) == null)
            throw (Framework.StandartExceptions.NoSelectedItem());

         if (Equals(config.mainData.folderData.CellCountSubfolder, ""))
         {
            sampleName = (config.gridPanel.SamplesDataGrid.SelectedItem
             as Framework.Data.DataGrid.GridItem).SampleName + @"\*.tif";
         }
         else
         {
            sampleName = (config.gridPanel.SamplesDataGrid.SelectedItem
             as Framework.Data.DataGrid.GridItem).SampleName + @"\" +
             config.mainData.folderData.CellCountSubfolder + @"\*.tif";
         }

         if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(sampleName)))
            throw (Framework.StandartExceptions.FolderDoesNotExists());
      }

      CellCountConfig config;
      string sampleName;
   }
}
