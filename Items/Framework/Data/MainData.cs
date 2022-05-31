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

         openSaveEvents.InstructionOpenEvent += openProject;
         openSaveEvents.InstructionSaveEvent += saveProject;
         openSaveEvents.InstructionSaveAsEvent += saveAsProject;
         openSaveEvents.InstructionCreateEvent += createProject;
         openSaveEvents.SubfolderSettingsEvent += subfolderSettingsStart;
         openSaveEvents.InstructionAddEvent += addFolderStart;
         folderData.AddFolderEvent += addFolder;

         initStartMainData();
      }

      void loadProjectFromSelectedFile()
      {
         configReader.OpenProject(openSaveEvents.SelectedProjectFile);
      }

      /// Project file type
      void initStartMainData()
      {
         try
         {
            if (dalmatian)
            {
               defaultExt = ".dlmtn";
               filter = "Dalmation project | *" + defaultExt;
               folderData.CellCountSubfolder = "";
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
               loadProjectFromSelectedFile();
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
               loadProjectFromSelectedFile();
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

      void addFolder()
      {
         try
         {
            string[] allfolders = Directory.GetDirectories(folderData.AddFolderText);

            /// Слабое место с конструктором по строке, нужно переделать.
            foreach (var folder in allfolders)
            {
               string[] row = new string[2];
               row[0] = folder;
               row[1] = folderData.InSampleText;

               dataGrid.addItem(row);
            }

         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message, "Exeption",
               MessageBoxButton.OK, MessageBoxImage.Error);
         }
      }

      void subfolderSettingsStart()
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
            item.udpateStates();
         }
      }
      void addFolderStart()
      {
         try
         {
            var sWindow = new View.AddFolderWindow(folderData, dalmatian);
            sWindow.ShowDialog();
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message, "Exeption",
               MessageBoxButton.OK, MessageBoxImage.Error);
         }
      }

   }
}
