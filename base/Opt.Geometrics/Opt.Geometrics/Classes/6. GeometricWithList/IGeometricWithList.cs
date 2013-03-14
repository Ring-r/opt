using System;

namespace Opt.Geometrics
{
    /// <summary>
    /// Интерфейс геометрического объекта с полюсом.
    /// </summary>
    /// <typeparam name="ElementType"></typeparam>
    public interface IGeometricWithList<ElementType>
    {
        #region Открытые поля и свойства.
        /// <summary>
        /// Получает или устанавливает элемент контейнера по индексу.
        /// </summary>
        /// <param name="index">Индекс.</param>
        /// <returns>Элемент контейнера с заданным индексом.</returns>
        ElementType this[int index] { get; set; }
        /// <summary>
        /// Возвращает количество элементов, которые содержаться в контейнере. Возвращает -1, если контейнер пуст.
        /// </summary>
        int Count { get; }
        #endregion

        #region Открытые методы.
        /// <summary>
        /// Добавляет элемент в контайнер.
        /// </summary>
        /// <param name="element">Элемент.</param>
        void Add(ElementType element);
        #endregion
    }
}
