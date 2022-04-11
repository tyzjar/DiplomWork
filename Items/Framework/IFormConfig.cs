using System;
using System.Windows.Controls;
using System.Windows.Input;
using OfficeOpenXml;

namespace GUI.Items.Framework
{
   /// <summary>
   /// Interface for all working forms
   /// Containing binding events for working threads
   /// </summary>
   abstract class IFormConfig : ConfigItem
   {
      public IFormConfig(Framework.Data.MainData mainData_,
         UserControl gridAndProcessPanel_,
         string worksheetName):
         base(worksheetName)
      {
         mainData = mainData_;
         gridAndProcessPanel = gridAndProcessPanel_;
         InitItems();
      }
      public override void UpdateName(string newName)
      {}

      void InitItems()
      {
         loadPanel = new View.LoadScreen();

         Initialize();

         /// events
         //mainData.dataGrid.DataUpdateEvent += DataUpdate;
         mainData.configReader.AddItem(this);
         processor.EventProcessEnd += swapToView;
         processor.EventProcessStart += swapToLoad;

         ProcessorStartCommand = new DelegateCommand((object param) => {
            processor.StartProcess();
         });

         swapToView();
      }

      /// Here should init own components.
      protected abstract void Initialize();
      protected abstract void swapToView();

      public void swapToLoad()
      {
         gridAndProcessPanel.Content = loadPanel;
      }

      #region events
      public ICommand ProcessorStartCommand { get; private set; }
      #endregion

      public Data.MainData mainData;
      public View.LoadScreen loadPanel;
      public UserControl gridAndProcessPanel;
      protected MatlabProcessor.IMatlab processor;
   }
}
