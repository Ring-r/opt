using System;

namespace Opt.Geometrics
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
