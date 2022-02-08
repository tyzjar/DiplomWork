using System;
using System.Windows;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GUI.Items.Framework.MatlabProcessor
{
   class Synchronizer
   {
      public Synchronizer(string fileName)
      {
         synchFileName = fileName;
      }

      public void Start()
      {
         if (working)
         {
            return;
         }

         working = true;
         File.Delete(synchFileName);
         mainThread = new Thread(Work);
         mainThread.Start();
      }

      public void Stop()
      {
         working = false;
      }

      public void AddSynchObject(string name, ISynchObject synchObject)
      {
         lock (locker)
         {
            synchObjects.Add(name, synchObject);
         }
      }


      void Work()
      {
         try
         {
            while (working && (!File.Exists(synchFileName)))
            {
               Thread.Sleep(1000);
            }

            if (!working)
            {
               return;
            }

            FileStream fileStream = new FileStream(synchFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            StreamReader workFile = new StreamReader(fileStream);
            string line;
            string[] objectData;

            while (working)
            {
               while ((line = workFile.ReadLine()) != null)
               {
                  objectData = line.Split(delimeter);
                  if (objectData.Length < 2)
                     continue;
                  synchObjects[objectData[0]].UpdateSource(objectData[1]);
               }
               Thread.Sleep(1000);
            }
            workFile.Close();
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message, "Exeption",
               MessageBoxButton.OK, MessageBoxImage.Error);
         }
         working = false;
      }

      public string getSynchFileName 
      {
         get
         {
            return synchFileName;
         } 
      }

      static object locker = new object();
      const char delimeter = '@';
      Dictionary<string, ISynchObject> synchObjects = 
         new Dictionary<string, ISynchObject>();
      public Thread mainThread;
      string synchFileName { get; set; }
      volatile bool working = false;
   }
}
