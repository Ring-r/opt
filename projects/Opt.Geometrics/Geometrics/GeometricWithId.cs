using System;

namespace Opt.Geometrics.Geometrics
{
    public interface IGeometricWithId
    {
        /// <summary>
        /// Возвращает и устанавливает некоторый идентификатор геометрического объекта.
        /// </summary>
        int Id { get; set; }
    }
}
