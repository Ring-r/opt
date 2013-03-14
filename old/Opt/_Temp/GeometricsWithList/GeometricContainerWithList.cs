using System;
using System.Collections.Generic;

namespace Opt.Geometrics.GeometricContainers
{
    /// <summary>
    /// Контейнер, основаный на List.
    /// </summary>
    [Serializable]
    public class ContainerWithList<Type> : GeometricWithPole
    {
        #region Скрытые поля и свойства.
        protected List<Point> list_points;
        protected List<IteratorWithList<Point>> list_iterators;
        #endregion

        #region Открытые поля и свойства.
        public IteratorWithList<Point> Iterator
        {
            get
            {
                return list_iterators[0];
            }
        }
        #endregion

        #region PolygonWithList(...)
        public ContainerWithList()
        {
            list_points = new List<Point>(1);
            list_points.Add(null);

            list_iterators = new List<IteratorWithList<Point>>(1);
            list_iterators.Add(new IteratorWithList<Point>(list_iterators, list_points, 0, false));
        }
        #endregion
    }
}
