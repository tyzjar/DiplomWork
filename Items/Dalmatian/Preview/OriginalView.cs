using OfficeOpenXml;
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
         preview.gui_originalView(0, new MWCharArray(sampleName),
            config.variables.countk);
      }

      protected override void PopulateBuffer(ExcelWorksheet worksheet)
      {
         if ((config.gridPanel.SamplesDataGrid.SelectedItem
            as Framework.Data.DataGrid.GridItem) == null)
            throw (Framework.StandartExceptions.NoSelectedItem());

         sampleName = (config.gridPanel.SamplesDataGrid.SelectedItem
             as Framework.Data.DataGrid.GridItem).SampleFolder;

         if (!Framework.Utils.CheckFolderForTifFiles(sampleName))
            throw (Framework.StandartExceptions.FilesDoesNotExists());

         sampleName += @"\*.tif";
      }

      CellCountConfig config;
      string sampleName;
   }
}
