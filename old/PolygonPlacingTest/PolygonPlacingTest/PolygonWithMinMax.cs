using System;

using Opt.Geometrics;

namespace PolygonPlacingTest
{
    public class PolygonWithMinMax : Polygon
    {
        protected Point min = new Point();
        public double MinX
        {
            get
            {
                return min.X;
            }
        }
        public double MinY
        {
            get
            {
                return min.Y;
            }
        }

        protected Point max = new Point();
        public double MaxX
        {
            get
            {
                return  max.X;
            }
        }
        public double MaxY
        {
            get
            {
                return max.Y;
            }
        }

        protected Point center = new Point();
        public double CenterX
        {
            get
            {
                return pole.X + center.X;
            }
        }
        public double CenterY
        {
            get
            {
                return pole.Y + center.Y;
            }
        }

        public void Check()
        {
            // TODO: Сделать проверку выпуклости многоугольника. Выдать ошибку.

            // TODO: Сделать проверку обхода точек против часовой стрелки. Исправить.

            #region Поиск прямоугольной оболочки.
            min.X = float.PositiveInfinity;
            min.Y = float.PositiveInfinity;
            max.X = float.NegativeInfinity;
            max.Y = float.NegativeInfinity;

            for (int i = 0; i < list_elements.Count; i++)
            {
                if (min.X > list_elements[i].X)
                    min.X = list_elements[i].X;
                if (min.Y > list_elements[i].Y)
                    min.Y = list_elements[i].Y;
                if (max.X < list_elements[i].X)
                    max.X = list_elements[i].X;
                if (max.Y < list_elements[i].Y)
                    max.Y = list_elements[i].Y;
            }
            #endregion

            #region Поиск центра многоугольника. !Установка полюса в центр.
            center.X = 0;
            center.Y = 0;
            for (int i = 0; i < list_elements.Count; i++)
            {
                center.X += list_elements[i].X;
                center.Y += list_elements[i].Y;
            }
            center.X /= list_elements.Count;
            center.Y /= list_elements.Count;
            #endregion

        }
    }
}
