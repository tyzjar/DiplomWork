using OfficeOpenXml;
using gui_preview;
using MathWorks.MATLAB.NET.Utility;
using MathWorks.MATLAB.NET.Arrays;

namespace GUI.Items.Dalmatian.Preview
{
   class CommonPreview : Framework.MatlabProcessor.IMatlab
   {
      public CommonPreview(CellCountConfig config_, string tempFileName_) :
         base(tempFileName_)
      {
         config = config_;
      }

      protected override void MatlabThread()
      {
         MatlabPreview preview = new MatlabPreview();

         preview.gui_preview(0, new MWCharArray(sampleName),
            config.variables.countk,
            config.variables.sfilterLowpass, config.variables.sfilterHipass,
            config.variables.trshold);
      }

      protected override void PopulateBuffer(ExcelWorksheet worksheet)
      {
         if ((config.gridPanel.SamplesDataGrid.SelectedItem
               as Framework.Data.DataGrid.GridItem) == null)
            throw (Framework.StandartExceptions.NoSelectedItem());

         sampleName = (config.gridPanel.SamplesDataGrid.SelectedItem
             as Framework.Data.DataGrid.GridItem).CellCountFolder;

         if (!Framework.Utils.CheckFolderForTifFiles(sampleName))
            throw (Framework.StandartExceptions.FilesDoesNotExists());

         sampleName += @"\*.tif";
      }

      string sampleName;
      CellCountConfig config;
   }
}
