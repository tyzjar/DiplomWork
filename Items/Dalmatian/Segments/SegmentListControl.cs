using System.Collections.Generic;
using OfficeOpenXml;
using System.ComponentModel;
using GUI.Items.Framework.Data.DataGrid;

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


      public int Export(ExcelWorksheet worksheet, int row, int col)
      {
         int count = 0;

         foreach (var item in segmentsList)
         {
            worksheet.Cells[row, col].Value = item.SegmentName + " (" + item.Id.ToString() + ")";
            worksheet.Cells[row, col + 1].Value = item.CellNumber;
            row++;
            count++;
         }

         return count;
      }

      public void Reset(List<Segment> ns)
      {
         segmentsList.Clear();
         segmentsList.Add(new CellSegment(CellChanel));

         foreach (var item in ns) 
         {
            segmentsList.Add(item);
         }
      }

      public BindingList<Segment> segmentsList = new BindingList<Segment>();
   }
}
