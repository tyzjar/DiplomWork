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
   public class Segment : GUI.Items.Framework.ViewModelBase
   {
      public static Point ScalePoint(Point p, Double scale)
      {
         return new Point(p.X / scale, p.Y / scale);
      }
      public Segment(string name)
      {
         Name = name;
      }
      public void AddPoint(double x, double y)
      {
         orderPoints.Add(new Point(x,y));
      }
      public void AddPoint(Point p)
      {
         orderPoints.Add(p);
      }
      public void AddPoint(string p)
      {
         var a = p.Split(';');
         if (a.Length == 2)
         {
            orderPoints.Add(new Point(Convert.ToDouble(a[0]),
               Convert.ToDouble(a[1])));
         }
      }
      public void AddPoints(List<Point> newPoints)
      {
         orderPoints.AddRange(newPoints);
      }
      public Viewbox DrawSegment(double w, double h, double scale)
      {
         Path newSegment = new Path();
         gGroup = new GeometryGroup();
         newSegment.StrokeStartLineCap = PenLineCap.Round;
         newSegment.StrokeEndLineCap = PenLineCap.Round;
         newSegment.StrokeThickness = 1;
         newSegment.Stroke = Brushes.White;

         newSegment.Width = w;
         newSegment.Height = h;

         newSegment.Data = gGroup;
         pathBox.Child = newSegment;


         if (orderPoints.Count > 0)
         {
            var start = orderPoints.GetEnumerator();

            if (start.MoveNext())
            {
               var x1 = start;
               var x2 = x1;
               while (x2.MoveNext())
               {
                  gGroup.Children.Add(new LineGeometry(
                     ScalePoint(x1.Current,scale), ScalePoint(x2.Current, scale)));
                  x1 = x2;
               }
               gGroup.Children.Add(new LineGeometry(
                  ScalePoint(x1.Current, scale), ScalePoint(start.Current, scale)));
            }
         }

         return pathBox;
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
         OnPropertyChanged("CellCount");
      }

      // Check that Point is in figure
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
      public GeometryGroup gGroup;
      public Viewbox pathBox = new Viewbox();
      public List<Point> orderPoints = new List<Point>();
      private int cellCount = 0;
   }
}
