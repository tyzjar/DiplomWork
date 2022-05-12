using System.Windows;
using System.IO;
using System;
using OfficeOpenXml;
using System.Threading;

using gui_morph;
using MathWorks.MATLAB.NET.Arrays;

namespace GUI.Items.Morph
{
   class MorphProcessor : Framework.MatlabProcessor.IMatlab
   {
      public MorphProcessor(MorphConfig config_, string tempFileName_) :
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
         EventProcessEnd += UpdateAtlasStates;
      }
      protected override void MatlabThread()
      {
         MorphMatlab mMorph = new MorphMatlab();
         mMorph.gui_morph(0, new MWCharArray(synchronizer.getSynchFileName),
            new MWCharArray(tempFileName),
            new MWCharArray(config.mainData.folderData.MorphSaveSubfolder),
            new MWCharArray(config.AgeValue));
      }

      protected override void PopulateBuffer(ExcelWorksheet worksheet)
      {
         int row = 1;
         worksheet.Cells[row, 1].Value = "Var1";
         worksheet.Cells[row, 2].Value = "Var2";
         row++;

         foreach (var item in config.mainData.dataGrid.Data)
         {
            var sampleFrom = item.MaskFolder;
            var sampleTo = item.InSampleName;
            var saveFileName = "";

            if (!config.mainData.folderData.AtlasAndAtalasRefCheckFolderName(ref sampleTo, ref saveFileName))
            {
               saveFileName = Framework.Utils.CreateSaveName(item.SampleName, item.InSampleName);
               sampleTo = item.MorphToFolder;
            }

            if (!Framework.Utils.CheckFolderForTifFiles(sampleTo))
            {
               sampleTo = item.InSampleName;
            }

            if (Framework.Utils.CheckFolderForTifFiles(sampleFrom) && Framework.Utils.CheckFolderForTifFiles(sampleTo))
            {
               worksheet.Cells[row, 1].Value = sampleFrom;
               worksheet.Cells[row, 2].Value = sampleTo;
               worksheet.Cells[row, 3].Value = saveFileName;
               item.AtlasTFiles.Reload(saveFileName);
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

      void UpdateAtlasStates()
      {
         foreach (var item in config.mainData.dataGrid.Data)
         {
            item.AtlasTUpdate();
         }
      }

      MorphConfig config;
      Framework.MatlabProcessor.TextBlocObject Operation;
      Framework.MatlabProcessor.TextBlocObject SampleName;
      Framework.MatlabProcessor.ProgressbarObject Progressbar;
   }
}
