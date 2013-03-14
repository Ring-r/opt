using System;

namespace Opt.Geometrics.SpecialGeometrics
{
    /// <summary>
    /// Круг Делоне в двухмерном пространстве.
    /// </summary>
    [Serializable]
    public class CircleDelone : Circle
    {
        #region Скрытые поля и свойства.
        /// <summary>
        /// Вектор нормали.
        /// </summary>
        protected Vector normal;
        #endregion

        #region Открытые поля и свойства.
        public Vector Normal
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
        public CircleDelone()
        {
            normal = new Vector();
        }
        #endregion
    }
}
