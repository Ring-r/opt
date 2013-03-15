using System;

namespace Opt.Geometrics.Geometrics2d
{
    /// <summary>
    /// Геометрический объект в двухмерном пространстве.
    /// </summary>
    [Serializable]
    public abstract class Geometric2d
    {
        /// <summary>
        /// Возвращает размерность пространства, в котором задан геометрический объект.
        /// </summary>
        public int Dim
        {
            get
            {
                return 2;
            }
        }
    }
}
