using System;
using System.Collections.Generic;

namespace Opt.Geometrics.Geometrics2d
{
    /// <summary>
    /// Кольцевой список, основаный на List.
    /// </summary>
    [Serializable]
    public class Geometrics2dWithList<ElementType> : Geometric2dWithPole
        where ElementType : class
    {
        #region Скрытые поля и свойства.

        /// <summary>
        /// Список элементов, которые содержит кольцевой список.
        /// </summary>
        protected List<ElementType> list_elements = new List<ElementType>();

        #endregion

        #region Скрытые методы.
        /// <summary>
        /// Корректирует индекс таким образом, чтобы он не выходил за границы внутреннего списка.
        /// </summary>
        /// <param name="index">Индекс.</param>
        protected void CheckedIndex(ref int index)
        {
            if (list_elements.Count == 0)
            {
                index = -1;
                return;
            }
            if (index >= list_elements.Count)
            {
                index = index % list_elements.Count;
                return;
            }
            if (index < 0)
            {
                index = list_elements.Count + index % list_elements.Count;
                return;
            }
        }
        #endregion

        #region Открытые поля и свойства.
        /// <summary>
        /// Получает или устанавливает элемент кольцевого списка по индексу.
        /// </summary>
        /// <param name="index">Индекс.</param>
        /// <returns>Элемент кольцевого списка с заданным индексом.</returns>
        public ElementType this[int index]
        {
            get
            {
                CheckedIndex(ref index);
                if (index >= 0)
                    return list_elements[index];
                else
                    return null;
            }
            set
            {
                CheckedIndex(ref index);
                if (index >= 0)
                    list_elements[index] = value;
            }
        }
        /// <summary>
        /// Возвращает количество элементов, которые содержаться в кольцевом списке. Возвращает -1, если кольцевой список пуст.
        /// </summary>
        public int Count
        {
            get
            {
                return list_elements.Count;
            }
        }
        #endregion

        #region Открытые методы.
        /// <summary>
        /// Добавляет элемент в контайнер.
        /// </summary>
        /// <param name="element">Элемент.</param>
        public void Add(ElementType element)
        {
            if (list_elements.Count == 0)
                list_elements.Insert(0, element);
            else
                list_elements.Insert(list_elements.Count, element);
        }
        #endregion
    }
}