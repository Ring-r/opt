using System;

namespace Opt.Geometrics
{
    /// <summary>
    /// Геометрический объект в двухмерном пространстве.
    /// </summary>
    [Serializable]
    public abstract class Geometric
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

        /// <summary>
        /// Некоторый идентификатор геометрического объекта.
        /// </summary>
        protected int id;
        /// <summary>
        /// Возвращает и устанавливает некоторый идентификатор геометрического объекта.
        /// </summary>
        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        /// <summary>
        /// Возвращает строку-информацию об объекте.
        /// </summary>
        /// <returns>Строка.</returns>
        public override string ToString()
        {
            return string.Format("{0}", id);
        }
    }
}
