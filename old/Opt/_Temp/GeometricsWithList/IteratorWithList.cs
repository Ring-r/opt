using System;
using System.Collections.Generic;

namespace Opt.Geometrics.GeometricContainers
{
    public class IteratorWithList<Type>
    {
        #region Скрытые поля и свойства.
        protected List<Type> list_elements;
        protected int index;
        protected List<IteratorWithList<Type>> list_iterators;
        protected bool is_closeable;
        #endregion

        #region Открытые поля и свойства.
        public Type Data
        {
            get
            {
                return list_elements[index]; // Индекс не должен быть отрицательным.
            }
            set
            {
                list_elements[index] = value; // Индекс не должен быть отрицательным.
            }
        }
        #endregion

        #region Iterator(...)
        public IteratorWithList(List<IteratorWithList<Type>> list_iterators, List<Type> list_elements, int index, bool is_closeable)
        {
            this.list_iterators = list_iterators;
            if (is_closeable)
                this.list_iterators.Add(this);

            this.list_elements = list_elements;
            this.index = index;

            CorrectIndex(ref index);
        }
        #endregion

        #region Методы.
        public IteratorWithList<Type> CreateIterator()
        {
            return new IteratorWithList<Type>(list_iterators, list_elements, index, true);
        }

        public void Add(int k, Type element)
        {
            //if (k < 0)
            //    k = -1;
            //else
            //    k = +1;
            list_elements.Insert(index + k, element);
        }
        public void Del(int k)
        {
            //if (k < 0)
            //    k = -1;
            //else
            //    k = +1;
            bool is_free = true;
            for (int i = 0; i < list_iterators.Count || !is_free; i++)
                if (list_iterators[i].index == index + k)
                    is_free = false;
            if (is_free)
                list_elements.RemoveAt(index + k);
        }

        public IteratorWithList<Type> Move(int k)
        {
            //if (k < 0)
            //    k = -1;
            //else
            //    k = +1;
            index += k;
            CorrectIndex(ref index);
            return this;
        }

        public void Close()
        {
            if (is_closeable)
            {
                list_elements = null;
                index = -1;
                list_iterators.Remove(this);
                list_iterators = null;
            }
        }
        public bool IsClose()
        {
            return list_iterators == null;
        }
        #endregion

        #region Вспомотельные методы.
        /// <summary>
        /// Корреляция индекса относительно границ списка.
        /// </summary>
        /// <param name="index">Индекс.</param>
        protected void CorrectIndex(ref int index)
        {
            if (0 < index)
                index = list_elements.Count + index % list_elements.Count; // Проверить!!!
            else
                if (index >= list_elements.Count)
                    index = index % list_elements.Count;
        }
        #endregion
    }
}
