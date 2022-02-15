﻿using OfficeOpenXml;
using gui_preview;
using MathWorks.MATLAB.NET.Utility;
using MathWorks.MATLAB.NET.Arrays;

namespace GUI.Items.Dalmatian.Preview
{
   class ThresholdPreview : Framework.MatlabProcessor.IMatlab
   {
      public ThresholdPreview(CellCountConfig config_, string tempFileName_) :
         base(tempFileName_)
      {
         config = config_;
      }

      protected override void MatlabThread()
      {
         MatlabPreview preview = new MatlabPreview();

         preview.gui_thresholdPreview(0, new MWCharArray(sampleName),
            config.countkValue, config.trsholdValue);
      }

      protected override void PopulateBuffer(ExcelWorksheet worksheet)
      {
         sampleName = (config.gridPanel.SamplesDataGrid.SelectedItem
            as Framework.Data.DataGrid.GridItem).SampleName + @"\" +
            config.mainData.folderData.CellCountSubfolder + @"\*.tif";
      }

      CellCountConfig config;
      string sampleName;
   }
}
