using Newtonsoft.Json;
using System.Collections.Generic;
using System.Windows;

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
      public bool CheckId(int Id)
      {
         if (this.Id == Id)
            return true;

         foreach (var child in Childrens)
         {
            if (child.CheckId(Id))
               return true;
         }
         return false;
      }

      public virtual void Count() 
      {
         CellNumber = Cells.Count;
         OnPropertyChanged("CellNumber");
      }
      public virtual void AddPoint(int x, int y, int z) 
      {
         Cells.Add(new Point3d(x, y, z));
         CellNumber++;
         OnPropertyChanged("CellNumber");
      }
      public virtual void RemoveAll() 
      {
         Cells.Clear();
         CellNumber = 0;
         OnPropertyChanged("CellNumber");
      }
      public Segment Clone()
      {
         var ns = new Segment();
         ns.SegmentName = SegmentName;
         ns.Id = Id;

         foreach(var item in Childrens)
            ns.Childrens.Add(item.Clone());

         return ns;
      }

      [JsonProperty("name")]
      public string SegmentName { get; set; }
      [JsonProperty("id")]
      public int Id { get; set; } = 0;
      [JsonProperty("children")]
      public List<Segment> Childrens = new List<Segment>();
      public int CellNumber { get; set; } = 0;
      public List<Point3d> Cells = new List<Point3d>();
    }

   class CellSegment : Segment
   {
      public CellSegment(string name) : base(name)
      { }
   }

   class FigureSegment : Segment
   {
      public FigureSegment(string name) : base(name)
      {
      }
   }
}
