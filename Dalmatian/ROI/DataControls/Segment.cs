using Newtonsoft.Json;
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

   public class ColorControl
   {
      public ColorControl() { }
      public ColorControl(Color color)
      {
         SetColor(color);
      }
      public ColorControl(string color)
      {
         if (!SetColor(color))
         {
            m_color = Color.FromRgb(255, 255, 255);
         }
      }
      public SolidColorBrush CreateBrush()
      {
         return new SolidColorBrush(m_color);
      }
      public void SetColor(Color color)
      {
         m_color = color;
      }
      public bool SetColor(string color)
      {
         var a = color.Split(delimiter);
         if (a.Length == 3)
         {
            m_color = Color.FromRgb(Convert.ToByte(a[0]),
               Convert.ToByte(a[1]), Convert.ToByte(a[2]));
            return true;
         }
         return false;
      }
      public Color GetColor()
      {
         return m_color;
      }
      public string ConvertToString()
      {
         return m_color.R.ToString() + delimiter + 
            m_color.G.ToString() + delimiter + m_color.B.ToString();
      }

      public static char delimiter = ',';
      private Color m_color;
   }

   [JsonObject(MemberSerialization.OptIn)]
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
         Name = name;
         defaultInit();
      }

      public void AddPoint(double x, double y)
      {
         orderPoints.Add(new Point(x, y));
      }
      public void AddPoint(Point p)
      {
         orderPoints.Add(p);
      }
      public void AddPoint(string p)
      {
         var a = p.Split(delimiter);
         if (a.Length == 2)
         {
            orderPoints.Add(new Point(Convert.ToDouble(a[0]),
               Convert.ToDouble(a[1])));
         }
      }
      public void RemoveLast()
      {
         orderPoints.RemoveAt(orderPoints.Count - 1);
      }
      public void RemoveAll()
      {
         orderPoints.Clear();
      }

      public abstract void defaultInit();
      public abstract void AddAndDrawPoint(Point p);
      public abstract void DeletePoint(Point p);
      public abstract Viewbox DrawSegment(double w, double h);
      public abstract void RenderSegment(double w, double h);
      public abstract void Count(List<Point> cellPoints);
      public abstract List<Point> Get2DPoints();
      public void UpdateStorke(Color color, double thickness)
      {
         ColorView = color;
         Thickness = thickness;
      }


      [JsonProperty]
      public string Name { get; set; }
      [JsonProperty]
      public double Thickness
      {
         get
         {
            return m_thickness;
         }
         set
         {
            if (m_thickness != value)
            {
               m_thickness = value;
               if (pathBox != null)
               {
                  (pathBox.Child as Path).StrokeThickness = m_thickness * m_thickness_scale;
               }
            }
         }
      }
      [JsonProperty]
      public Color ColorView
      {
         get
         {
            return m_color.GetColor();
         }
         set
         {
            m_color.SetColor(value);
            if (pathBox != null)
            {
               (pathBox.Child as Path).Stroke = m_color.CreateBrush();
            }
         }
      }
      public Brush BrushView
      {
         get
         {
            return m_color.CreateBrush();
         }
      }
      public int CellNumber { get { return cellCount; } }
      public GeometryGroup gGroup;
      public Viewbox pathBox;
      public static char delimiter = ';';

      protected ColorControl m_color = new ColorControl(Color.FromRgb(255, 255, 255));
      protected double m_thickness = 3;
      protected double m_thickness_scale = 1;
      [JsonProperty]
      public int cellCount = 0;
      [JsonProperty]
      public List<Point> orderPoints = new List<Point>();
   }

   /// ------------------------------------------------------------------------------------------------------------

   public class FigureSegment : Segment
   {
      public FigureSegment(string name) : base(name)
      {}
      public override void defaultInit()
      {
         m_color = new ColorControl(Color.FromRgb(255, 255, 255));
         m_thickness_scale = 1;
         Thickness = 3;
   }
      public override void AddAndDrawPoint(Point p)
      {
         if (orderPoints.Contains(p))
            return;
         AddPoint(p);
         gGroup.Children.Add(new LineGeometry(p, p));
      }
      public override void DeletePoint(Point p)
      {}
      public override Viewbox DrawSegment(double w, double h)
      {
         pathBox = new Viewbox();
         Path newSegment = new Path();
         gGroup = new GeometryGroup();

         newSegment.StrokeStartLineCap = PenLineCap.Round;
         newSegment.StrokeEndLineCap = PenLineCap.Round;
         newSegment.StrokeThickness = m_thickness * m_thickness_scale;
         newSegment.Stroke = m_color.CreateBrush();

         newSegment.Width = w;
         newSegment.Height = h;

         newSegment.Data = gGroup;
         pathBox.Child = newSegment;

         RenderSegment(w, h);

         return pathBox;
      }
      public override void RenderSegment(double w, double h)
      {
         if (orderPoints.Count > 0)
         {
            var start = orderPoints.GetEnumerator();
            gGroup.Children.Clear();
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
   }

   /// ------------------------------------------------------------------------------------------------------------

   public class CellSegment : Segment
   {
      public CellSegment(string name) : base(name)
      {}
      public override void defaultInit()
      {
         m_color = new ColorControl(Color.FromRgb(255, 0, 0));
         m_thickness_scale = 2;
         Thickness = 3;
      }
      public override void AddAndDrawPoint(Point p)
      {
         if (orderPoints.Contains(p))
            return;

         AddPoint(p);
         drawPoints.Add(p, new LineGeometry(p, p));
         gGroup.Children.Add(drawPoints[p]);
      }
      public override void DeletePoint(Point p)
      {
         var r = 5;
         var left = p.X - Thickness - r;
         var right = p.X + Thickness + r;
         var top = p.Y - Thickness - r;
         var bot = p.Y + Thickness + r;
         for (int i = 0; i < orderPoints.Count; i++)
         {
            var point = orderPoints[i];
            if ((left < point.X) && (point.X < right) && (top < point.Y) && (point.Y < bot))
            {
               gGroup.Children.Remove(drawPoints[orderPoints[i]]);
               drawPoints.Remove(orderPoints[i]);
               orderPoints.RemoveAt(i);
               i--;
            }
         }
         Count(null);
      }
      public override Viewbox DrawSegment(double w, double h)
      {
         pathBox = new Viewbox();
         Path newSegment = new Path();
         gGroup = new GeometryGroup();
         drawPoints.Clear();

         newSegment.StrokeStartLineCap = PenLineCap.Round;
         newSegment.StrokeEndLineCap = PenLineCap.Round;
         newSegment.StrokeThickness = m_thickness * m_thickness_scale;
         newSegment.Stroke = m_color.CreateBrush();

         newSegment.Width = w;
         newSegment.Height = h;

         RenderSegment(w, h);

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
               if (!drawPoints.ContainsKey(x.Current))
               {
                  drawPoints.Add(x.Current, new LineGeometry(x.Current, x.Current));
                  gGroup.Children.Add(drawPoints[x.Current]);
               }
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
      public Dictionary<Point, Geometry> drawPoints = new Dictionary<Point, Geometry>();
   }
}
