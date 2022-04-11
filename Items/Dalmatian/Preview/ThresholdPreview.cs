using OfficeOpenXml;
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

         if (!System.IO.Directory.Exists(sampleName))
            throw (Framework.StandartExceptions.FolderDoesNotExists());
      }

      CellCountConfig config;
      string sampleName;
   }
}
