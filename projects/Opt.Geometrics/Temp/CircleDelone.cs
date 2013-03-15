using System;
using Opt.Geometrics.Geometrics2d;

namespace Opt.Geometrics.SpecialGeometrics
{
    /// <summary>
    /// Круг Делоне в двухмерном пространстве.
    /// </summary>
    [Serializable]
    public class CircleDelone2d : Geometric2dWithPoleValue
    {
        #region Скрытые поля и свойства.
        /// <summary>
        /// Вектор нормали.
        /// </summary>
        protected Vector2d normal;
        #endregion

        #region Открытые поля и свойства.
        public Vector2d Normal
        {
            get
            {
                return normal;
            }
            set
            {
                normal = value;
            }
        }
        #endregion

        #region CircleDelone_(...)
        public CircleDelone2d()
        {
            normal = new Vector2d();
        }
        #endregion
    }
}
