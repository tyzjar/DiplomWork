using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dalmatian.ROI
{
   class Dot
   {
      public Dot() { }
      public Dot(double x_, double y_)
      {
         x = x_;
         y = y_;
      }

      public double x = 0;
      public double y = 0;
   }

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
      public void AddDots(List<Dot> newDots)
      {
         orderDots.AddRange(newDots);
      }

      public void Count(List<Dot> cellDots)
      {
         UnorderMap searchTable = new UnorderMap();
         orderDots.ForEach((Dot a) => { searchTable.Add(a.x, a.y); });

         int count = 0;

         foreach (var cell in cellDots)
         {
            if (checkDot(searchTable[cell.x], cell.y))
            {
               count++;
            }
         }

         cellCount = count;
      }

      // Check that dot is in contur
      private bool checkDot(List<double> l, double y)
      {
         if (l.Count > 0)
         {
            // Numbers of y above and below dot
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

      private List<Dot> orderDots = new List<Dot>();
      private int cellCount;
   }
}
