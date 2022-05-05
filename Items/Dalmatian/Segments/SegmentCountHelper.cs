using GUI.Items.Framework.MatlabProcessor;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Items.Dalmatian.Segments
{
   class SegmentCountHelper : IMatlab
   {
      SegmentCountHelper() : base("3dCountHelper")
      { }

      public int CountSegment(Segment segment)
      {
         return -1;
      }
      protected override void MatlabThread() { }
      protected override void PopulateBuffer(ExcelWorksheet worksheet) { }


   }
}
