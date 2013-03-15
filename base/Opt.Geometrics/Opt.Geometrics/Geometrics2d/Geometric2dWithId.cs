﻿using System;

namespace Opt.Geometrics.Geometrics2d
{
    /// <summary>
    /// Геометрический объект в двухмерном пространстве с идентификатором.
    /// </summary>
    [Serializable]
    public abstract class Geometric2dWithId : Geometric2d
    {
        /// <summary>
        /// Некоторый идентификатор геометрического объекта.
        /// </summary>
        protected int id;
        /// <summary>
        /// Возвращает и устанавливает некоторый идентификатор геометрического объекта.
        /// </summary>
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                this.id = value;
            }
        }
    }
}
