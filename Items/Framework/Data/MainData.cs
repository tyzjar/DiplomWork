/// <summary>
/// Контейнер всех данных для процессов
/// </summary>

using Microsoft.Win32;
using System.IO;
using System;
using System.Windows;
using System.Collections.Generic;

namespace GUI.Items.Framework.Data
{
   public class MainData : ViewModelBase
   {
      public FolderData folderData;
      public DataGrid.DataGrid dataGrid;
      public OpenSaveEvents openSaveEvents;
      public ConfigReader configReader;
      public bool dalmatian;
      private string filter;
      private string defaultExt;

      public MainData(DataGrid.SegmentsCreator screator, bool dalmatian_)
      {
         dalmatian = dalmatian_;

         /// Основные элементы хранилища
         folderData = new FolderData();
         dataGrid = new DataGrid.DataGrid(folderData, screator);

         openSaveEvents = new OpenSaveEvents();
         configReader = new ConfigReader();

         ///Связывание элементов событий для их обработки.
         configReader.AddItem(folderData);
         configReader.AddItem(dataGrid);

         openSaveEvents.InstructionSaveEvent += saveProject;
         openSaveEvents.InstructionSaveAsEvent += saveAsProject;
         openSaveEvents.InstructionCreateEvent += createProject;
         openSaveEvents.SubfolderSettingsEvent += SubfolderSettingsStart;
         openSaveEvents.InstructionOpenEvent += openProject;
         folderData.AddFolderEvent += AddFolder;

         initStartFileAndFilter();
      }

      /// Project file type
      void initStartFileAndFilter()
      {
         try
         {
            if (dalmatian)
            {
               defaultExt = ".dlmtn";
               filter = "Dalmation project | *" + defaultExt;
            }
            else
            {
               defaultExt = ".crg";
               filter = "Corgy project | *" + defaultExt;
            }

            var args = Environment.GetCommandLineArgs();
            if ((args.Length>1)&&(Path.GetExtension(args[1]) == defaultExt))
            {
               openSaveEvents.SelectedProjectFile = args[1];
               configReader.OpenProject(openSaveEvents.SelectedProjectFile);
            }
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message, "Exeption",
               MessageBoxButton.OK, MessageBoxImage.Error);
         }
      }

      void openProject()
      {
         try
         {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = filter;
            openFileDialog.DefaultExt = defaultExt;

            if(openFileDialog.ShowDialog() == true)
            {
               openSaveEvents.SelectedProjectFile = openFileDialog.FileName;
               configReader.OpenProject(openSaveEvents.SelectedProjectFile);
            }
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message, "Exeption",
               MessageBoxButton.OK, MessageBoxImage.Error);
         }
      }

      void saveProject()
      {
         try
         {
            if (openSaveEvents.SelectedProjectFile != OpenSaveEvents.DefaultProjectFileName)
               configReader.SaveProject(openSaveEvents.SelectedProjectFile);
            else
               saveAsProject();
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message, "Exeption",
               MessageBoxButton.OK, MessageBoxImage.Error);
         }
      }

      void saveAsProject()
      {
         try
         {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = filter;
            saveFileDialog.DefaultExt = defaultExt;
            saveFileDialog.FileName = Path.GetFileName(openSaveEvents.SelectedProjectFile);

            if (saveFileDialog.ShowDialog() == true)
            {
               openSaveEvents.SelectedProjectFile = saveFileDialog.FileName;
               configReader.SaveProject(openSaveEvents.SelectedProjectFile);
            }
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message, "Exeption",
               MessageBoxButton.OK, MessageBoxImage.Error);
         }
      }

      void createProject()
      {
         SaveFileDialog saveFileDialog = new SaveFileDialog();
         saveFileDialog.Filter = filter;
         saveFileDialog.DefaultExt = defaultExt;
         saveFileDialog.FileName = "NewFile";

         if(saveFileDialog.ShowDialog() == true)
         {
            openSaveEvents.SelectedProjectFile = saveFileDialog.FileName;
            dataGrid.Data.Clear();
         }
      }

      void AddFolder()
      {
         try
         {
            string[] allfolders = Directory.GetDirectories(folderData.XmlAddFolder);

            /// Слабое место с конструктором по строке, нужно переделать.
            foreach (var folder in allfolders)
            {
               string[] row = new string[7];
               row[0] = folder;
               row[1] = folderData.XmlInSample;
               row[2] = "Group1";
               row[3] = "";
               row[4] = "";
               row[5] = "";
               row[6] = "";

               dataGrid.addItem(row);
            }

         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message, "Exeption",
               MessageBoxButton.OK, MessageBoxImage.Error);
         }
      }

      void SubfolderSettingsStart()
      {
         if (dalmatian)
         {
            var sWindow = new View.DalmatianSubfolder(folderData);
            sWindow.ShowDialog();
         }
         else
         {
            var sWindow = new View.SubfoldersSettings(folderData);
            sWindow.ShowDialog();
         }
         foreach (var item in dataGrid.Data)
         {
            item.udpateStates(folderData);
         }
      }

   }
}
