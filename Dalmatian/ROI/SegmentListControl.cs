using System.Collections.Generic;
using OfficeOpenXml;
using System.IO;

namespace Dalmatian.ROI
{
   class SegmentListControl : GUI.Items.Framework.ConfigItem
   {
      private const string CellChanel = "All Cells";
      public static SegmentListControl SegmentListControlCreate(string sampleName)
      {
         sampleName = GUI.Items.Framework.ConfigReader.delete_symbol(sampleName, '\\');
         sampleName = GUI.Items.Framework.ConfigReader.delete_symbol(sampleName, ':');
         var slc = new SegmentListControl(sampleName);
         slc.segmentsList.Add(new CellSegment(CellChanel));

         return slc;
      }

      public SegmentListControl(string sampleName) 
         : base(sampleName)
      {
      }

      public override void UpdateName(string newName)
      {
         newName = GUI.Items.Framework.ConfigReader.delete_symbol(newName, '\\');
         newName = GUI.Items.Framework.ConfigReader.delete_symbol(newName, ':');
         worksheetName = newName;
      }

      public override List<GUI.Items.Framework.ConfigItem> SaveConfig(ExcelWorksheet worksheet)
      {
         var column = 1;

         foreach (var item in segmentsList)
         {
            var row = 1;
            worksheet.Cells[row, column].Value = item.NameWithCount();
            row++;

            foreach (var p in item.ConvertToStrings())
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
         if ((worksheet != null) && (worksheet.Dimension != null))
         {
            segmentsList = new List<Segment>();

            for (int i = 1; i <= worksheet.Dimension.End.Column; i++)
            {
               var row = 1;
               Segment item;

               if (i == 1)
               {
                  item = new CellSegment(worksheet.Cells[row, i].Value.ToString());
               }
               else
               {
                  item = new FigureSegment(worksheet.Cells[row, i].Value.ToString());
               }

               row++;

               while ((row <= worksheet.Dimension.End.Row)&&(worksheet.Cells[row, i].Value != null))
               {
                 // System.Windows.MessageBox.Show(worksheet.Cells[row, i].Value.ToString());
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
