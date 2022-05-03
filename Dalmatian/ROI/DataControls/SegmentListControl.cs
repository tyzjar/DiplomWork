using System.Collections.Generic;
using OfficeOpenXml;
using System.IO;
using System.ComponentModel;

namespace Dalmatian.ROI
{

   class SegmentListControl : GUI.Items.Framework.Data.DataGrid.SegmentsList
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
      {}

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
