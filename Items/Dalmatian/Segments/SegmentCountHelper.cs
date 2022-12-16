using GUI.Items.Framework.MatlabProcessor;
using OfficeOpenXml;

using gui_segmentscellcount;
using MathWorks.MATLAB.NET.Arrays;

namespace GUI.Items.Dalmatian.Segments
{
   class SegmentCountHelper : IMatlab
   {
      public SegmentCountHelper(CellCountConfig config_, string tempFileName_) :
       base(tempFileName_)
      {
         config = config_;

         /// Synchronizer settings
         Operation = new Framework.MatlabProcessor.TextBlocObject();
         Operation.UpdateSource("Waiting initialize synchronizer");
         SampleName = new Framework.MatlabProcessor.TextBlocObject();
         Progressbar = new Framework.MatlabProcessor.ProgressbarObject();

         config.loadPanel.Operation.DataContext = Operation;
         config.loadPanel.SampleName.DataContext = SampleName;
         config.loadPanel.Progress.DataContext = Progressbar;

         synchronizer.AddSynchObject(nameof(Operation), Operation);
         synchronizer.AddSynchObject(nameof(SampleName), SampleName);
         synchronizer.AddSynchObject(nameof(Progressbar), Progressbar);

         EventProcessStart += PrepareLoad;
         EventProcessEnd += UpdateCellsNumbers;
      }

      protected override void MatlabThread()
      {
         SegmentsCount mccount = new SegmentsCount();
         mccount.gui_segmentscellcount(0, new MWCharArray(synchronizer.getSynchFileName),
            new MWCharArray(tempFileName));
      }

      protected override void PopulateBuffer(ExcelWorksheet worksheet)
      {
         int row = 1;
         worksheet.Cells[row, 1].Value = "Var1";
         row++;

         foreach (var item in config.mainData.dataGrid.Data)
         {
            if (Framework.Utils.CheckFolderForTifFiles(item.CellCountFolder))
            {
               worksheet.Cells[row, 1].Value = item.CellCountFolder;
               row++;
            }
         }
      }

      void UpdateCellsNumbers()
      {
         foreach (var item in config.mainData.dataGrid.Data)
         {
            config.segmentationControl.CalculateSegments(item, item.CellCountFolder + "\\cells_segments.txt");
         }
      }

      public void PrepareLoad()
      {
         Operation.UpdateSource("Waiting initialize synchronizer");
         SampleName.UpdateSource("");
         Progressbar.UpdateSource("0;1");
      }

      CellCountConfig config;
      Framework.MatlabProcessor.TextBlocObject Operation;
      Framework.MatlabProcessor.TextBlocObject SampleName;
      Framework.MatlabProcessor.ProgressbarObject Progressbar;
   }
}
