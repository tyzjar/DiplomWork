

namespace GUI.Items.Dalmatian
{
   class Segment : Framework.ViewModelBase
   {
      public Segment(string name)
      { }

      public string Name { get; }

      public string name;
      public int id;
   }

   class CellSegment : Segment
   {
      public CellSegment(string name) : base(name)
      { }
   }
}
