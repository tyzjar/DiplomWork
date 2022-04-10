using System.Collections.Generic;
using OfficeOpenXml;
using System.IO;

namespace Dalmatian.ROI
{
   class SegmentListControl : GUI.Items.Framework.ConfigItem
   {
      public static SegmentListControl SegmentListControlCreate(string sampleName)
      {
         sampleName = GUI.Items.Framework.ConfigReader.delete_symbol(sampleName, '\\');
         sampleName = GUI.Items.Framework.ConfigReader.delete_symbol(sampleName, ':');
         return new SegmentListControl(sampleName);
      }

      public SegmentListControl(string sampleName) 
         : base(sampleName)
      {
      }
      public override List<GUI.Items.Framework.ConfigItem> SaveConfig(ExcelWorksheet worksheet)
      {
         var column = 1;

         foreach (var item in segmentsList)
         {
            var row = 1;
            worksheet.Cells[row, column].Value = item.Name;
            row++;

            foreach (var p in item.orderPoints)
            {
               worksheet.Cells[row, column].Value = p;
               row++;
            }

            column++;
         }

         return null;
      }
      public override List<GUI.Items.Framework.ConfigItem> LoadConfig(ExcelWorksheet worksheet)
      {
         //SamplesSegments.Clear();
         if ((worksheet != null) && (worksheet.Dimension != null))
         {
            for (int i = 1; i <= worksheet.Dimension.End.Column; i++)
            {
               var row = 1;
               var item = new Segment(worksheet.Cells[row, i].Value.ToString());
               row++;

               while (row <= worksheet.Dimension.End.Row)
               {
                  //System.Windows.MessageBox.Show(worksheet.Cells[row, i].Value.ToString());
                  item.AddPoint(worksheet.Cells[row, i].Value.ToString());
                  row++;
               }
              // System.Windows.MessageBox.Show(item.orderPoints.Count.ToString());
               segmentsList.Add(item);
            }
         }
         return null;
      }

      public List<Segment> segmentsList = new List<Segment>();
   }
}
