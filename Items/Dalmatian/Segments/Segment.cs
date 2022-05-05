using Newtonsoft.Json;
using System.Collections.Generic;

namespace GUI.Items.Dalmatian
{
   public class Point3d
   {
      public Point3d() { }
      public Point3d(int x, int y, int z)
      {
         X = x;
         Y = y;
         Z = z;
      }
      public int X = 0;
      public int Y = 0;
      public int Z = 0;
   }

   public class Segment : Framework.ViewModelBase
   {
      public Segment(string name)
      {
         SegmentName = name;
      }

      /// Read json
      public Segment()
      {
         SegmentName = "default name";
      }

      public virtual void Count() { }

      [JsonProperty("name")]
      public string SegmentName { get; set; }
      [JsonProperty("id")]
      public int Id { get; set; } = 0;
      [JsonProperty("children")]
      public List<Segment> Childrens = new List<Segment>();
      public int CellNumber { get; set; } = 0;
   }

   class CellSegment : Segment
   {
      public CellSegment(string name) : base(name)
      { }

      public override void Count()
      { }

      public List<Point3d> Cells = new List<Point3d>();
   }

   class FigureSegment : Segment
   {
      public FigureSegment(string name, Segment cells_) : base(name)
      {
         cells = cells_;
      }

      public override void Count()
      { }

      private Segment cells;
   }
}
