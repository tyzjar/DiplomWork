using System;
using OfficeOpenXml;
using gui_preproc;
using MathWorks.MATLAB.NET.Arrays;


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

         /// Add number of samples
         worksheet.Cells[++row, col].Value = config.mainData.dataGrid.Data.Count;

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
            if (!String.IsNullOrWhiteSpace(item.SampleName))
            {
               col = 2;
               worksheet.Cells[row, col++].Value = item.SampleName;
               worksheet.Cells[row, col++].Value = config.SubtractionEnabel ?
                  item.SubtractionStateValue.ShouldProcess : DoNotProcess;
               worksheet.Cells[row, col++].Value = config.IntensityEnabel ?
                  item.IntensityStateValue.ShouldProcess : DoNotProcess;
               worksheet.Cells[row, col++].Value = config.CropEnabel ?
                  item.CropStateValue.ShouldProcess : DoNotProcess;
               worksheet.Cells[row, col++].Value = config.MaskEnabel ?
                  item.MaskStateValue.ShouldProcess : DoNotProcess;
               row++;
            }
         }
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
            item.udpateStates(config.mainData.folderData);
         }
      }

      PreprocConfig config;
      Framework.MatlabProcessor.TextBlocObject Operation;
      Framework.MatlabProcessor.TextBlocObject SampleName;
      Framework.MatlabProcessor.ProgressbarObject Progressbar;
   }
}
