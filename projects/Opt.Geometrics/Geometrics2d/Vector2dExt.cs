using System;
using Opt.Geometrics.Geometrics2d;

namespace Opt.Geometrics.Extentions
{
    /// <summary>
    /// Расширения для вектора.
    /// </summary>
    public static class Vector2dExt
    {
        /// <summary>
        /// Получить перпендикулярный вектор.
        /// </summary>
        /// <param name="vector_this">Вектор.</param>
        /// <param name="isRight">true - если происходит поворот по часовой стрелке и false - если против.</param>
        /// <returns>Перпендикулярный вектор.</returns>
        public static Vector2d _I_(this Vector2d vector_this, bool isRight)
        {
            if (isRight)
                return new Vector2d() { X = vector_this.Y, Y = -vector_this.X };
            else
                return new Vector2d() { X = -vector_this.Y, Y = vector_this.X };
        }
    }
}
