using System.Collections.Generic;
using OfficeOpenXml;
using System.ComponentModel;

namespace GUI.Items.Dalmatian
{
   class SegmentListControl : Framework.Data.DataGrid.SegmentsList
   {
      private const string CellChanel = "All Cells";

      public static SegmentListControl SegmentListControlCreate(string sampleName)
      {
         var slc = new SegmentListControl();
         slc.segmentsList.Add(new CellSegment(CellChanel));
         return slc;
      }

      public SegmentListControl()
         : base()
      { }

      public void CountAll()
      {
         foreach (var item in segmentsList)
         {
            item.Count();
         }
      }
      public int Export(ExcelWorksheet worksheet, int row, int col)
      {
         CountAll();
         int count = 0;

         //foreach (var item in segmentsList)
         //{
         //   worksheet.Cells[row, col].Value = item.Name;
         //   worksheet.Cells[row, col + 1].Value = item.CellNumber;
         //   row++;
         //   count++;
         //}

         return count;
      }

      public BindingList<Segment> segmentsList = new BindingList<Segment>();
   }
}
