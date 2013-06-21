using System;

namespace Opt.Geometrics
{
    /// <summary>
    /// Геометрический объект.
    /// </summary>
    [Serializable]
    public abstract class Geometric
    {
        /// <summary>
        /// Возвращает и устанавливает некоторый идентификатор геометрического объекта.
        /// </summary>
        public int Id { get; set; }
    }
}
