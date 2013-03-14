using System;

namespace Opt.ClosenessModel.Old
{
    #region Старый вариант.
    ///// <summary>
    ///// Класс вспомогательных данных (тройка) для вершины модели близости.
    ///// </summary>
    ///// <typeparam name="DataType">Класс данных, который содержиться в модели близости.</typeparam>
    //public class Triple<DataType>
    //{
    //    #region Поля, свойства и методы класса NodeTwoWay.
    //    /// <summary>
    //    /// Ссылка на круг Делоне.
    //    /// </summary>
    //    public Object Data { get; set; }
    //    protected Triple<DataType> prev;
    //    protected Triple<DataType> next;

    //    public void Add(Triple<DataType> triple)
    //    {
    //        triple.prev = this;
    //        triple.next = this.next;
    //        triple.prev.next = triple;
    //        triple.next.prev = triple;
    //    }
    //    public void Del()
    //    {
    //        prev.next = next;
    //        next.prev = prev;
    //        prev = this;
    //        next = this;
    //    }

    //    public class Enumerator
    //    {
    //        protected Triple<DataType> start;
    //        protected Triple<DataType> current;
    //        public Triple<DataType> Curren
    //        {
    //            get
    //            {
    //                return current;
    //            }
    //        }

    //        public Enumerator(Triple<DataType> triple)
    //        {
    //            start = triple;
    //            current = triple;
    //        }

    //        public bool MoveNext()
    //        {
    //            current = current.next;
    //            return current != start;
    //        }

    //        public void Reset()
    //        {
    //            current = start;
    //        }
    //    }
    //    public Enumerator GetEnumerator()
    //    {
    //        return new Enumerator(this);
    //    }
    //    #endregion

    //    #region Скрытые поля и свойства.
    //    /// <summary>
    //    /// Вершина, связанная со вспомогательными данными.
    //    /// </summary>
    //    protected Vertex<DataType> vertex;
    //    #endregion

    //    #region Triple(...)
    //    public Triple()
    //    {
    //        prev = this;
    //        next = this;
    //    }
    //    public Triple(Vertex<DataType> vertex)
    //    {
    //        this.vertex = vertex;

    //        //this.vertex.Prev.Somes = this;
    //        //this.vertex.Somes = this;
    //        //this.vertex.Next.Somes = this;
    //    }
    //    #endregion

    //    public void Update()
    //    {
    //    } // !!!Недописано!!!
    //}
    #endregion

    #region Старый вариант.
    ///// <summary>
    ///// Разбиение тройки на три новые.
    ///// </summary>
    ///// <param name="data">Ссылка на объект, который является общим для новых троек.</param>
    ///// <remarks>Новые тройки связаны между собой.</remarks>
    //public void Break(Object data)
    //{
    //    Triple<Object, CircleDelone> triple_pcp = new Triple<Object, CircleDelone>(this);
    //    Triple<Object, CircleDelone> triple_ncn = new Triple<Object, CircleDelone>(this);

    //    // Получаем разбиение текущей тройки.
    //    triple_pcp.vertex.prev.data = data;
    //    triple_pcp.vertex.cros = vertex.prev;
    //    triple_pcp.vertex.next.cros = triple_ncn.vertex.prev;
    //    vertex.prev.cros = triple_pcp.vertex;
    //    vertex.data = data;
    //    vertex.next.cros = triple_ncn.vertex;
    //    triple_ncn.vertex.prev.cros = triple_pcp.vertex.next;
    //    triple_ncn.vertex.cros = vertex.next;
    //    triple_ncn.vertex.next.data = data;

    //    // Создаем ссылки из соседних троек.
    //    triple_pcp.vertex.prev.cros.cros = triple_pcp.vertex.prev;
    //    vertex.cros.cros = vertex;
    //    triple_ncn.vertex.next.cros.cros = triple_ncn.vertex.next;
    //}
    #endregion
}
