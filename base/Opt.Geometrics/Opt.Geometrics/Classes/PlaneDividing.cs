using System;
using Opt.Geometrics.Extentions;

namespace Opt.Geometrics.SpecialGeometrics
{
    /// <summary>
    /// Разделяющая в двухмерном пространстве.
    /// </summary>
    public class PlaneDividing
    {
        #region Скрытые поля и свойства.
        protected Polygon.Iterator iterator_plane;
        protected Polygon.Iterator iterator_point;
        #endregion

        #region Открытые поля и свойства.
        public Polygon.Iterator IteratorPlane
        {
            get
            {
                return iterator_plane;
            }
        }
        public Polygon.Iterator IteratorPoint
        {
            get
            {
                return iterator_point;
            }
        }
        #endregion

        #region public PlaneDividing(...)
        public PlaneDividing(Polygon.Iterator iterator_plane, Polygon.Iterator iterator_point)
        {
            this.iterator_plane = iterator_plane;
            this.iterator_point = iterator_point;
        }
        #endregion

        public void Find()
        {
            double ed_curr = -1;
            double ed_next = -1;
            for (int i = 0; i < iterator_plane.Count && ed_curr < 0; i++)
            {
                ed_curr = PlaneExt.Расширенное_расстояние(iterator_plane.Plane(0), iterator_point.Point(0));
                ed_next = PlaneExt.Расширенное_расстояние(iterator_plane.Plane(0), iterator_point.Point(1));
                int k;
                if (ed_curr > ed_next)
                    k = 1;
                else
                {                    
                    ed_curr = ed_next;
                    ed_next = PlaneExt.Расширенное_расстояние(iterator_plane.Plane(0), iterator_point.Point(-1));
                    k = -1;                    
                }
                while (ed_curr >= 0 && ed_curr > ed_next)
                {
                    iterator_point.Move(k);
                    ed_curr = ed_next;
                    ed_next = PlaneExt.Расширенное_расстояние(iterator_plane.Plane(0), iterator_point.Point(0));
                }
                if (ed_curr < 0)
                    iterator_plane.Move(1);
            }
            if (ed_curr < 0)
            {
                Polygon.Iterator t = iterator_plane;
                iterator_plane = iterator_point;
                iterator_point = t;

                for (int i = 0; i < iterator_plane.Count && ed_curr < 0; i++)
                {
                    ed_curr = PlaneExt.Расширенное_расстояние(iterator_plane.Plane(0), iterator_point.Point(0));
                    ed_next = PlaneExt.Расширенное_расстояние(iterator_plane.Plane(0), iterator_point.Point(1));
                    int k;
                    if (ed_curr > ed_next)
                        k = 1;
                    else
                    {
                        ed_curr = ed_next;
                        ed_next = PlaneExt.Расширенное_расстояние(iterator_plane.Plane(0), iterator_point.Point(-1));
                        k = -1;
                        iterator_point.Move(1);
                    }
                    while (ed_curr >= 0 && ed_curr > ed_next)
                    {
                        iterator_point.Move(k);
                        ed_curr = ed_next;
                        ed_next = PlaneExt.Расширенное_расстояние(iterator_plane.Plane(0), iterator_point.Point(0));
                    }
                    if (ed_curr < 0)
                        iterator_plane.Move(1);
                }
            }
        }
    }
}
