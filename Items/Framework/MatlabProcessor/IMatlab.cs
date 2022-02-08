﻿using System.Windows;
using System.IO;
using System;
using OfficeOpenXml;
using System.Threading;
using System.Threading.Tasks;

namespace GUI.Items.Framework.MatlabProcessor
{
   abstract class IMatlab
   {
      protected IMatlab(string tempFileName_)
      {
         tempFileName = @folder + @tempFileName_ + @".xlsx";
         synchronizer = new Synchronizer(folder + tempFileName_ + ".txt");
         EventProcessEnd += synchronizer.Stop;
      }

      public void StartProcess()
      {
         if (working)
         {
            return;
         }

         try
         {
            working = true;
            EventProcessStart();

            if (!Directory.Exists(folder))
            {
               Directory.CreateDirectory(folder);
            }

            /// Create buffer
            FileInfo workFile = new FileInfo(tempFileName);
            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("List1");
            PopulateBuffer(worksheet);
            excelPackage.SaveAs(workFile);

            ///Start main thread with Synchronizer
            synchronizer.Start();
            mainThread = new Thread(Work);
            mainThread.Start(SynchronizationContext.Current);
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message, "Exeption",
               MessageBoxButton.OK, MessageBoxImage.Error);
         }
      }
      protected void Work(object state)
      {
         try
         {
            /// do Matlab work
            MatlabThread();
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message, "Exeption",
               MessageBoxButton.OK, MessageBoxImage.Error);
         }

         SynchronizationContext uiContext = state as SynchronizationContext;
         uiContext.Send(OnProcessEnd, "update");

         /// whait to synchronizer end
         synchronizer.mainThread.Join();

         working = false;
      }
      public void OnProcessEnd(object state)
      {
         EventProcessEnd();
      }

      protected abstract void MatlabThread();
      protected abstract void PopulateBuffer(ExcelWorksheet worksheet);

      public delegate void ProcessStateHandler();
      public event ProcessStateHandler EventProcessEnd = () => { };
      public event ProcessStateHandler EventProcessStart = () => { };
      protected string tempFileName { get; set; }
      protected Synchronizer synchronizer;
      protected Thread mainThread;
      protected volatile bool working = false;
      public const string folder = @"Temp\";
   }
}
