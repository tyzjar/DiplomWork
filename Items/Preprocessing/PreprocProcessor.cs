using System;
using OfficeOpenXml;
using gui_preproc;
using MathWorks.MATLAB.NET.Arrays;
using GUI.Items.Framework.Data.DataGrid;

namespace GUI.Items.Preprocessing
{
   class PreprocProcessor : Framework.MatlabProcessor.IMatlab
   {
      public PreprocProcessor(PreprocConfig config_, string tempFileName_) :
         base(tempFileName_)
      {
         config = config_;

         /// Synchronizer settings
         Operation = new Framework.MatlabProcessor.TextBlocObject();
         SampleName = new Framework.MatlabProcessor.TextBlocObject();
         Progressbar = new Framework.MatlabProcessor.ProgressbarObject();

         config.loadPanel.Operation.DataContext = Operation;
         config.loadPanel.SampleName.DataContext = SampleName;
         config.loadPanel.Progress.DataContext = Progressbar;

         synchronizer.AddSynchObject(nameof(Operation), Operation);
         synchronizer.AddSynchObject(nameof(SampleName), SampleName);
         synchronizer.AddSynchObject(nameof(Progressbar), Progressbar);

         EventProcessStart += PrepareLoad;
         EventProcessEnd += UpdateTable;
      }
      protected override void MatlabThread()
      {
         PreprocMatlab preproc = new PreprocMatlab();
         preproc.gui_preproc(0, new MWCharArray(synchronizer.getSynchFileName),
            new MWCharArray(tempFileName),
            new MWCharArray(config.mainData.folderData.SampleSubfolder),
            0.025);
      }

      protected override void PopulateBuffer(ExcelWorksheet worksheet)
      {
         int row = 1;
         int col = 1;

         /// All columns startin from Var
         for (int i=1; i<=6; ++i)
         {
            worksheet.Cells[row, i].Value = "Var"+Convert.ToString(i);
         }

         /// Index of number of samples
         var rowCount = ++row;
         var colCount = col;
         uint count = 0;

         /// Add Subtraction_picture
         worksheet.Cells[++row, col].Value = config.SubtractionName;

         /// Add folders
         worksheet.Cells[++row, col].Value = config.mainData.folderData.SubtractionSubfolder;
         worksheet.Cells[++row, col].Value = config.mainData.folderData.IntensitySubfolder;
         worksheet.Cells[++row, col].Value = config.mainData.folderData.CropSubfolder;
         worksheet.Cells[++row, col].Value = config.mainData.folderData.MaskSubfolder;
         row = 2;

         /// Add Samples
         var DoNotProcess = "not";
         foreach (var item in config.mainData.dataGrid.Data)
         {
            if (Framework.Utils.CheckFolderForTifFiles(item.SampleFolder))
            {
               count++;
               col = 2;

               worksheet.Cells[row, col++].Value = item.SampleName;
               worksheet.Cells[row, col++].Value = config.SubtractionEnabel ?
                  PreprocStateHelper.ShouldProcess(item.SubtractionState) : DoNotProcess;
               worksheet.Cells[row, col++].Value = config.IntensityEnabel ?
                  PreprocStateHelper.ShouldProcess(item.IntensityState) : DoNotProcess;
               worksheet.Cells[row, col++].Value = config.CropEnabel ?
                  PreprocStateHelper.ShouldProcess(item.CropState) : DoNotProcess;
               worksheet.Cells[row, col++].Value = config.MaskEnabel ?
                  PreprocStateHelper.ShouldProcess(item.MaskState) : DoNotProcess;
               row++;

            }
         }

         /// Add number of samples
         worksheet.Cells[rowCount, colCount].Value = count;
      }

      public void PrepareLoad()
      {
         Operation.UpdateSource("Waiting initialize synchronizer");
         SampleName.UpdateSource("");
         Progressbar.UpdateSource("0;1");
      }
      void UpdateTable()
      {
         foreach (var item in config.mainData.dataGrid.Data)
         {
            item.udpateStates();
         }
      }

      PreprocConfig config;
      Framework.MatlabProcessor.TextBlocObject Operation;
      Framework.MatlabProcessor.TextBlocObject SampleName;
      Framework.MatlabProcessor.ProgressbarObject Progressbar;
   }
}
