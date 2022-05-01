using System.Windows;
using System.IO;
using System;
using OfficeOpenXml;
using System.Threading.Tasks;

using gui_cellcount;
using MathWorks.MATLAB.NET.Utility;
using MathWorks.MATLAB.NET.Arrays;

namespace GUI.Items.Dalmatian
{
   class CellCountProcessor : Framework.MatlabProcessor.IMatlab
   {
      public CellCountProcessor(CellCountConfig config_, string tempFileName_) :
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
      }

      protected override void MatlabThread()
      {
         CellCountMatlab mccount = new CellCountMatlab();
         mccount.gui_cellcount(0, new MWCharArray(synchronizer.getSynchFileName),
            new MWCharArray(tempFileName), config.mainData.folderData.CellCountSubfolder,
            config.variables.sfilterHipass,
            config.variables.sfilterLowpass, config.variables.trshold,
            config.variables.countMinRegion, config.variables.countConfLvl,
            config.variables.countRMin, config.variables.countRMax,
            config.variables.countk);

         foreach (var item in config.mainData.dataGrid.Data)
         {
            config.segmentationControl.ImportComand(item,
               item.SampleName +"\\" + config.mainData.folderData.CellCountSubfolder
               + "\\" + "cells.txt");
         }
      }

      protected override void PopulateBuffer(ExcelWorksheet worksheet)
      {
         int row = 1;
         worksheet.Cells[row, 1].Value = "Var1";
         row++;

         foreach (var item in config.mainData.dataGrid.Data)
         {
            if (!String.IsNullOrWhiteSpace(item.SampleName))
            {
               worksheet.Cells[row, 1].Value = item.SampleName;
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

      CellCountConfig config;
      Framework.MatlabProcessor.TextBlocObject Operation;
      Framework.MatlabProcessor.TextBlocObject SampleName;
      Framework.MatlabProcessor.ProgressbarObject Progressbar;
   }
}
