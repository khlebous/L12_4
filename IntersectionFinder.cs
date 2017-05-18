using System;
using System.Collections.Generic;

namespace discs
{
    public struct Point
    {
        public Point(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public double X { get; private set; }
        public double Y { get; private set; }

        public override string ToString()
        {
            return string.Format("[{0};{1}]", X, Y);
        }
        public bool IsRightOf(Point b)
        {
            return (this.X > b.X || (this.X == b.X && this.Y > b.Y));
        }
        public bool Equals(Point p)
        {
            if ((object)p == null)
                return false;
            return (this.X == p.X) && (this.Y == p.Y);
        }
        public double Distance(Point p)
        {
            return Math.Sqrt(Math.Pow(this.X - p.X, 2) + Math.Pow(this.Y - p.Y, 2));
        }
    }

    public enum IntersectionType
    {
        Disjoint,
        Contains,
        IsContained,
        Identical,
        Touches,
        Crosses
    }

    public struct Disk
    {
        public Point Center { get; private set; }
        public double Radius { get; private set; }

        public Disk(Point center, double radius)
        {
            this.Center = center;
            this.Radius = radius;
        }

        public bool Contains(Point p)
        {
            return (p.X - Center.X) * (p.X - Center.X) + (p.Y - Center.Y) * (p.Y - Center.Y) <= Radius * Radius + Program.epsilon;
        }

        /// <summary>
        ///  Funkcja sprawdza wzajemne położenie dwóch kół.
        /// </summary>
        /// <param name="other">drugie koło</param>
        /// <param name="crossingPoints">
        /// Punkty przecięcia obwodów kół, jeśli zwracana jest wartość: Touches albo Crosses.
        /// Pusta tablica wpp.
        /// <returns>
        /// Disjoint - kiedy koła nie mają punktów wspólnych
        /// Contains - kiedy pierwsze koło całkowicie zawiera drugie
        /// IsContained - kiedy pierwsze koło jest całkowicie zawarte w drugim
        /// Identical - kiedy koła pokrywają się
        /// Touches - kiedy koła mają dokładnie jeden punkt wspólny
        /// Crosses - kiedy obwody kół mają dokładnie dwa punkty wspólne
        /// </returns>
        public IntersectionType GetIntersectionType(Disk other, out Point[] crossingPoints)
        {
            double dX = other.Center.X - this.Center.X;
            double dY = other.Center.Y - this.Center.Y;
            double dist2 = dX * dX + dY * dY;
            double dist = Math.Sqrt(dist2);

            /*
             * tu zajmij się wszystkimi przypadkami wzajemnego położenia kół,
             * oprócz Crosses i Touches
             */
            bool k = this.Contains(new Point(10, 7));
            bool lllk = other.Contains(new Point(10, 7));

            crossingPoints = new Point[0];
            if (this.Center.Equals(other.Center) && this.Radius == other.Radius)
                return IntersectionType.Identical;
            double distanceBetweenCentrees = this.Center.Distance(other.Center);
            if (distanceBetweenCentrees > this.Radius + other.Radius)
                return IntersectionType.Disjoint;
            if (distanceBetweenCentrees <= Math.Max(this.Radius,other.Radius) - Math.Min(this.Radius, other.Radius))
            {
                if (this.Radius > other.Radius)
                    return IntersectionType.Contains;
                else
                    return IntersectionType.IsContained;
            }

            // odległość od środka aktualnego koła (this) do punktu P,
            // który jest punktem przecięcia odcinka łączącego środki kół (this i other)
            // z odcinkiem łączącym punkty wspólne obwodów naszych kół
            double a = (this.Radius * this.Radius - other.Radius * other.Radius + dist2) / (2 * dist);

            // odległość punktów przecięcia obwodów do punktu P
            double h = Math.Sqrt(this.Radius * this.Radius - a * a);

            // punkt P
            double px = this.Center.X + (dX * a / dist);
            double py = this.Center.Y + (dY * a / dist);

            /*
             * teraz wiesz już wszystko co potrzebne do rozpoznania położenia Touches
             * zajmij się tym
             */
            if (a == this.Radius)
            {
                crossingPoints = new Point[1];
                crossingPoints[0] = new Point(px, py);
                return IntersectionType.Touches;
            }

            // przypadek Crosses - dwa punkty przecięcia - już jest zrobiony

            double rX = -dY * h / dist;
            double rY = dX * h / dist;

            crossingPoints = new Point[2];
            crossingPoints[0] = new Point(px + rX, py + rY);
            crossingPoints[1] = new Point(px - rX, py - rY);
            return IntersectionType.Crosses;
        }


        /*
         * dopisz wszystkie inne metody, które uznasz za stosowne         
         * 
         */

    }

    static class IntersectionFinder
    {

        public static Point? FindCommonPoint(Disk[] disks)
        {
            /*
             * uzupełnij
             */
            return null;
        }

    }
}
