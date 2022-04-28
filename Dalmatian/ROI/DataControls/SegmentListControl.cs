using System.Collections.Generic;
using OfficeOpenXml;
using System.IO;
using System.ComponentModel;

namespace Dalmatian.ROI
{

   class SegmentListControl : GUI.Items.Framework.ConfigItem
   {
      private const string CellChanel = "All Cells";
      static string StringProcessing(string s)
      {
         s = GUI.Items.Framework.ConfigReader.delete_symbol(s, ':');
         s = s.Replace('\\', 'l');
         return s;
      }

      public static SegmentListControl SegmentListControlCreate(string sampleName)
      {
         var slc = new SegmentListControl(StringProcessing(sampleName));
         slc.segmentsList.Add(new CellSegment(CellChanel));
         return slc;
      }

      public SegmentListControl(string sampleName) 
         : base(sampleName)
      {
      }
      public override void UpdateName(string newName)
      {
         worksheetName = StringProcessing(newName);
      }
      public override List<GUI.Items.Framework.ConfigItem> SaveConfig(ExcelWorksheet worksheet)
      {
         var column = 1;

         foreach (var item in segmentsList)
         {
            var row = 1;
            worksheet.Cells[row, column].Value = item.SaveName();
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
            segmentsList = new BindingList<Segment>();

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
                  item.AddPoint(worksheet.Cells[row, i].Value.ToString());
                  row++;
               }
               segmentsList.Add(item);
            }
         }
         return null;
      }
      public void CountAll()
      {
         foreach (var item in segmentsList)
         {
            item.Count(segmentsList[0].Get2DPoints());
         }
      }
      public int Export(ExcelWorksheet worksheet, int row, int col)
      {
         CountAll();
         int count = 0;

         foreach (var item in segmentsList)
         {
            worksheet.Cells[row, col].Value = item.Name;
            worksheet.Cells[row, col + 1].Value = item.CellNumber;
            row++;
            count++;
         }

         return count;
      }

      public BindingList<Segment> segmentsList = new BindingList<Segment>();
   }
}
