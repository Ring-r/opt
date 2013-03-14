using System;

namespace Opt.Geometrics.Extentions
{
    /// <summary>
    /// Расширения для вектора.
    /// </summary>
    public static class VectorExt
    {
        /// <summary>
        /// Получить перпендикулярный вектор.
        /// </summary>
        /// <param name="vector_this">Вектор.</param>
        /// <param name="по_часовой_стрелке">true - если происходит поворот по часовой стрелке и fakse - если против.</param>
        /// <returns>Перпендикулярный вектор.</returns>
        public static Vector _I_(this Vector vector_this, bool по_часовой_стрелке)
        {
            if (по_часовой_стрелке)
                return new Vector() { X = vector_this.Y, Y = -vector_this.X };
            else
                return new Vector() { X = -vector_this.Y, Y = vector_this.X };
        }
    }
}
