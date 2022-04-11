using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Dalmatian.ROI
{

   public class UnorderMap
   {
      public void Add(double x_, double y_)
      {
         if (!map.ContainsKey(x_))
         {
            var newlist = new List<double>();
            newlist.Add(y_);
            map.Add(x_, newlist);
         }
         else
         {
            map[x_].Add(y_);
         }
      }

      public List<double> this[double x_]
      {
         get { return map[x_]; }
         set { map[x_] = value; }
      }

      Dictionary<double, List<double>> map = new Dictionary<double, List<double>>();
   }

   public abstract class Segment : GUI.Items.Framework.ViewModelBase
   {
      public static Point ScalePoint(Point p, Double scale)
      {
         return new Point(p.X * scale, p.Y * scale);
      }
      public static Point DeScalePoint(Point p, Double scale)
      {
         return new Point(p.X / scale, p.Y / scale);
      }
      public static string PointToString(Point p)
      {
         return p.X.ToString() + delimiter + p.Y.ToString();
      }

      public Segment(string name)
      {
         var a = name.Split(delimiter);
         if (a.Length == 2)
         {
            Name = a[0];
            cellCount = Convert.ToInt32(a[1]);
         }
         else
            Name = name;
      }
      public abstract void AddPoint(double x, double y);
      public abstract void AddPoint(Point p);
      public abstract void AddPoint(string p);
      public abstract void AddPointZ(Point p, double z);
      public abstract string NameWithCount();
      public abstract List<string> ConvertToStrings();
      public abstract Viewbox DrawSegment(double w, double h);
      public abstract void RenderSegment(double w, double h);
      public abstract void Count(List<Point> cellPoints);
      public abstract List<Point> Get2DPoints();
      public string Name { get; set; }
      public int CellNumber { get { return cellCount; } }
      public GeometryGroup gGroup;
      public Viewbox pathBox;
      protected int cellCount = 0;
      public static char delimiter = ';';
   }

   /// ------------------------------------------------------------------------------------------------------------

   public class FigureSegment : Segment
   {
      public FigureSegment(string name) : base(name)
      {
      }
      public override void AddPoint(double x, double y)
      {
         orderPoints.Add(new Point(x, y));
      }
      public override void AddPoint(Point p)
      {
         orderPoints.Add(p);
      }
      public override void AddPoint(string p)
      {
         var a = p.Split(delimiter);
         if (a.Length == 2)
         {
            orderPoints.Add(new Point(Convert.ToDouble(a[0]),
               Convert.ToDouble(a[1])));
         }
      }
      public override void AddPointZ(Point p, double z)
      {
         throw (new GUI.Items.Framework.StandartExceptions("AddPointZ does not overload", true));
      }
      public override string NameWithCount()
      {
         return Name + delimiter + cellCount.ToString();
      }
      public override List<string> ConvertToStrings()
      {
         List<string> str = new List<string>();
         orderPoints.ForEach((Point  p) => { str.Add(PointToString(p)); });
         return str;
      }
      public override Viewbox DrawSegment(double w, double h)
      {
         pathBox = new Viewbox();
         Path newSegment = new Path();
         gGroup = new GeometryGroup();
         newSegment.StrokeStartLineCap = PenLineCap.Round;
         newSegment.StrokeEndLineCap = PenLineCap.Round;
         newSegment.StrokeThickness = 1;
         newSegment.Stroke = Brushes.White;

         newSegment.Width = w;
         newSegment.Height = h;

         RenderSegment(w, h);

         newSegment.Data = gGroup;
         pathBox.Child = newSegment;

         return pathBox;
      }
      public override void RenderSegment(double w, double h)
      {
         if (orderPoints.Count > 0)
         {
            var start = orderPoints.GetEnumerator();

            if (start.MoveNext())
            {
               var x1 = start;
               var x2 = x1;
               while (x2.MoveNext())
               {
                  gGroup.Children.Add(new LineGeometry(x1.Current, x2.Current));
                  x1 = x2;
               }
               gGroup.Children.Add(new LineGeometry(x1.Current, start.Current));
            }
         }
      }
      public override void Count(List<Point> cellPoints)
      {
         //UnorderMap searchTable = new UnorderMap();
         //orderPoints.ForEach((Point a) => { searchTable.Add(a.X, a.Y); });

         int count = 0;

         foreach (var cell in cellPoints)
         {
            if (checkPoint(orderPoints, cell))
            {
               count++;
            }
         }

         cellCount = count;
         OnPropertyChanged("CellNumber");
      }
      public override List<Point> Get2DPoints()
      {
         return orderPoints;
      }
      // Check that Point is in figure
      private bool checkPoint(List<Point> p, Point point)
      {
         bool result = false;
         int j = p.Count - 1;
         for (int i = 0; i < p.Count; i++)
         {
            if ((p[i].Y < point.Y && p[j].Y >= point.Y || p[j].Y < point.Y && p[i].Y >= point.Y) &&
                 (p[i].X + (point.Y - p[i].Y) / (p[j].Y - p[i].Y) * (p[j].X - p[i].X) < point.X))
               result = !result;
            j = i;
         }

         return result;
      }

      private List<Point> orderPoints = new List<Point>();
   }

   /// ------------------------------------------------------------------------------------------------------------

   public class CellSegment : Segment
   {
      public CellSegment(string name) : base(name)
      {
      }
      public override void AddPoint(double x, double y)
      {
         orderPoints.Add(new Point(x, y));
      }
      public override void AddPoint(Point p)
      {
         orderPoints.Add(p);
      }
      public override void AddPoint(string p)
      {
         var a = p.Split(delimiter);
         if (a.Length == 2)
         {
            orderPoints.Add(new Point(Convert.ToDouble(a[0]),
               Convert.ToDouble(a[1])));
         }
      }
      public override void AddPointZ(Point p, double z)
      {
         throw (new GUI.Items.Framework.StandartExceptions("AddPointZ does not overload", true));
      }
      public override string NameWithCount()
      {
         return Name + delimiter + cellCount.ToString();
      }
      public override List<string> ConvertToStrings()
      {
         List<string> str = new List<string>();
         orderPoints.ForEach((Point p) => { str.Add(PointToString(p)); });
         return str;
      }
      public override Viewbox DrawSegment(double w, double h)
      {
         pathBox = new Viewbox();
         Path newSegment = new Path();
         gGroup = new GeometryGroup();
         newSegment.StrokeStartLineCap = PenLineCap.Round;
         newSegment.StrokeEndLineCap = PenLineCap.Round;
         newSegment.StrokeThickness = 1;
         newSegment.Stroke = Brushes.Red;

         newSegment.Width = w;
         newSegment.Height = h;

         RenderSegment(w,h);

         newSegment.Data = gGroup;
         pathBox.Child = newSegment;

         Count(null);
         return pathBox;
      }
      public override void RenderSegment(double w, double h)
      {
         if (orderPoints.Count > 0)
         {
            var x = orderPoints.GetEnumerator();

            while (x.MoveNext())
            {
               gGroup.Children.Add(new LineGeometry(x.Current, x.Current));
            }
         }
      }
      public override void Count(List<Point> cellPoints)
      {
         cellCount = orderPoints.Count;
         OnPropertyChanged(nameof(CellNumber));
      }
      public override List<Point> Get2DPoints()
      {
         return orderPoints;
      }

      private List<Point> orderPoints = new List<Point>();
   }
}
