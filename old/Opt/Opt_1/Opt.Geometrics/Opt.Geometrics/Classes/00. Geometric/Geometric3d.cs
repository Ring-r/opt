using System;

namespace Opt.Geometrics
{
    /// <summary>
    /// Геометрический объект в трёхмерном пространстве.
    /// </summary>
    [Serializable]
    public abstract class Geometric3d
    {
        /// <summary>
        /// Возвращает размерность пространства, в котором задан геометрический объект.
        /// </summary>
        public int Dim
        {
            get
            {
                return 3;
            }
        }
    }
}
