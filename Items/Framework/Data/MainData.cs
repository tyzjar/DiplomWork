/// <summary>
/// Контейнер всех данных для процессов
/// </summary>

using OfficeOpenXml;
using Microsoft.Win32;
using System.IO;
using System;
using System.Windows;

namespace GUI.Items.Framework.Data
{
   public class MainData : ViewModelBase
   {
      public FolderData folderData;
      public DataGrid.DataGrid dataGrid;
      public OpenSaveEvents openSaveEvents;
      public ConfigReader configReader;

      public MainData()
      {
         /// Основные элементы хранилища
         folderData = new FolderData();
         dataGrid = new DataGrid.DataGrid(folderData);

         openSaveEvents = new OpenSaveEvents();
         configReader = new ConfigReader();

         ///Связывание элементов событий для их обработки.
         configReader.AddItem(folderData);
         configReader.AddItem(dataGrid);
         openSaveEvents.InstructionSaveEvent += saveXML;
         openSaveEvents.InstructionSaveAsEvent += saveAsXML;
         openSaveEvents.InstructionCreateEvent += CreateXML;
         openSaveEvents.SubfolderSettingsEvent += SubfolderSettingsStart;
         openSaveEvents.InstructionOpenEvent += openXML;
         folderData.AddFolderEvent += AddFolder;
      }

      void openXML()
      {
         OpenFileDialog openFileDialog = new OpenFileDialog();
         openFileDialog.Filter = "Instruction File | *.xlsx";
         openFileDialog.DefaultExt = "xlsx";

         if(openFileDialog.ShowDialog() == true)
         {
            openSaveEvents.SelectedXmlFile = openFileDialog.FileName;
            configReader.OpenXml(openSaveEvents.SelectedXmlFile);
         }
      }

      void saveXML()
      {
         try
         {
            configReader.SaveXml(openSaveEvents.SelectedXmlFile);
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message, "Exeption",
               MessageBoxButton.OK, MessageBoxImage.Error);
         }
      }

      void saveAsXML()
      {
         try
         {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Instruction File | *.xlsx";
            saveFileDialog.DefaultExt = "xlsx";
            saveFileDialog.FileName = Path.GetFileName(openSaveEvents.SelectedXmlFile);

            if (saveFileDialog.ShowDialog() == true)
            {
               openSaveEvents.SelectedXmlFile = saveFileDialog.FileName;
               configReader.SaveXml(openSaveEvents.SelectedXmlFile);
            }
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message, "Exeption",
               MessageBoxButton.OK, MessageBoxImage.Error);
         }
      }

      void CreateXML()
      {
         SaveFileDialog saveFileDialog = new SaveFileDialog();
         saveFileDialog.Filter = "Instruction File | *.xlsx";
         saveFileDialog.DefaultExt = "xlsx";
         saveFileDialog.FileName = Path.GetFileName("NewFile.xlsx");

         if(saveFileDialog.ShowDialog() == true)
         {
            openSaveEvents.SelectedXmlFile = saveFileDialog.FileName;
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
         var sWindow = new View.SubfoldersSettings(folderData);
         sWindow.ShowDialog();
         foreach (var item in dataGrid.Data)
         {
            item.udpateStates(folderData);
         }
      }

   }
}
