using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Dalmatian.ROI
{

   class UnorderMap
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
   class Segment
   {
      public void AddPoint(double x, double y)
      {
         orderPoints.Add(new Point(x,y));
      }
      public void AddPoints(List<Point> newPoints)
      {
         orderPoints.AddRange(newPoints);
      }

      public void Count(List<Point> cellPoints)
      {
         UnorderMap searchTable = new UnorderMap();
         orderPoints.ForEach((Point a) => { searchTable.Add(a.X, a.Y); });

         int count = 0;

         foreach (var cell in cellPoints)
         {
            if (checkPoint(searchTable[cell.X], cell.Y))
            {
               count++;
            }
         }

         cellCount = count;
      }
      public Path DrawSegment()
      {
         Path newSegment = new Path();
         GeometryGroup gGroup = new GeometryGroup();
         newSegment.StrokeStartLineCap = PenLineCap.Round;
         newSegment.StrokeEndLineCap = PenLineCap.Round;
         newSegment.StrokeThickness = 1;
         newSegment.Stroke = Brushes.Black;

         var x1 = orderPoints.GetEnumerator();
         var x2 = x1;
         while (x2.MoveNext())
         {
            gGroup.Children.Add(new LineGeometry(x1.Current, x2.Current));
         }
         gGroup.Children.Add(new LineGeometry(x2.Current, x1.Current));

         newSegment.Data = gGroup;

         return newSegment;
      }

      // Check that Point is in contur
      private bool checkPoint(List<double> l, double y)
      {
         if (l.Count > 0)
         {
            // Numbers of y above and below Point
            int greaterСount = 0;
            int smallerСount = 0;

            foreach (var dy in l)
            {
               if (dy > y)
               {
                  greaterСount++;
               }
               else if (dy < y)
               {
                  smallerСount++;
               }
               else
                  return true;
            }

            if ((greaterСount > 0) && (smallerСount > 0) && (greaterСount % 2 != 0))
               return true;
         }

         return false;
      }
      public string Name { get; set; }
      public int CellCount { get { return cellCount; } }

      private List<Point> orderPoints = new List<Point>();
      private int cellCount;
   }
}
