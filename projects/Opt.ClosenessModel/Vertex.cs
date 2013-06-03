using System;

namespace Opt.ClosenessModel
{
    [Serializable]
    public partial class Vertex<DataType>
    {
        #region Скрытые поля и свойства.
        /// <summary>
        /// Ссылка на предыдущий узел в тройке.
        /// </summary>
        protected Vertex<DataType> prev;
        /// <summary>
        /// Ссылка на перекрёстный узел.
        /// </summary>
        protected Vertex<DataType> cros;
        /// <summary>
        /// Ссылка на следующий узел в тройке.
        /// </summary>
        protected Vertex<DataType> next;
        /// <summary>
        /// Ссылка на объект.
        /// </summary>
        protected DataType data;
        /// <summary>
        /// Ссылка на дополнительные данные.
        /// </summary>
        protected SomesClass somes;
        #endregion

        #region Открытые поля и свойства.
        /// <summary>
        /// Возвращает ссылку на предыдущую вершину в тройке.
        /// </summary>
        public Vertex<DataType> Prev
        {
            get
            {
                return this.prev;
            }
        }
        /// <summary>
        /// Возвращает ссылку на перекрёстную вершину.
        /// </summary>
        public Vertex<DataType> Cros
        {
            get
            {
                return this.cros;
            }
        }
        /// <summary>
        /// Возвращает ссылку на следующую вершину в тройке.
        /// </summary>
        public Vertex<DataType> Next
        {
            get
            {
                return this.next;
            }
        }
        /// <summary>
        /// Устанавливает ссылку на объект для узла.
        /// </summary>
        public DataType DataInNode
        {
            set
            {
                Vertex<DataType> vertex_start = this;
                Vertex<DataType> vertex_temp = vertex_start;
                do
                {
                    vertex_temp.data = value;
                    vertex_temp = vertex_temp.next.cros.next;
                } while (vertex_temp != vertex_start);
            }
        }
        /// <summary>
        /// Возвращает или устанавливает ссылку на объект.
        /// </summary>
        public DataType DataInVertex
        {
            get
            {
                return this.data;
            }
            set
            {
                this.data = value;
            }
        }
        /// <summary>
        /// Возвращает ссылку на дополнительные данные.
        /// </summary>
        public SomesClass Somes
        {
            get
            {
                return this.somes;
            }
        }
        /// <summary>
        /// Возвращает перечислитель.
        /// </summary>
        public EnumeratorClass Enumerator
        {
            get
            {
                return new EnumeratorClass(this);
            }
        }
        #endregion

        #region Vertex(...)
        /// <summary>
        /// Конструктор.
        /// </summary>
        protected Vertex()
        {
            this.somes = new SomesClass(this);
        }
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="data_prev"></param>
        /// <param name="data_curr"></param>
        /// <param name="data_next"></param>
        protected Vertex(DataType data_prev, DataType data_curr, DataType data_next)
        {
            // TODO: Передавать один параметр?
            this.prev = new Vertex<DataType>() { next = this, data = data_prev };
            this.data = data_curr;
            this.next = new Vertex<DataType>() { prev = this, data = data_next };

            this.prev.prev = this.next;
            this.next.next = this.prev;

            this.somes = new SomesClass(this);
        }
        #endregion

        /// <summary>
        /// Установление связи с перекрёстным узлом (и наоборот).
        /// </summary>
        /// <param name="cros">Ссылка на перекрёстный узел.</param>
        protected void SetCros(Vertex<DataType> cros)
        {
            this.cros = cros;
            cros.cros = this;
        }

        /// <summary>
        /// Разбиение противоположного ребра (вставка двух новых троек).
        /// </summary>
        /// <param name="data">Вставляемый объект.</param>
        public void BreakCrosBy(DataType data)
        {
            // Создаём две противоположнонаправленные тройки (near and far).
            Vertex<DataType> vertex_n = new Vertex<DataType>(this.next.data, data, this.prev.data) { cros = this };
            Vertex<DataType> vertex_f = new Vertex<DataType>(this.prev.data, data, this.next.data) { cros = this.cros };

            // Устанавливаем связи между созданными тройками.
            vertex_n.prev.SetCros(vertex_f.next);
            vertex_n.next.SetCros(vertex_f.prev);

            // Устанавливаем связи со всем графом.
            vertex_n.cros.cros = vertex_n;
            vertex_f.cros.cros = vertex_f;
        }

        /// <summary>
        /// Переразбиение смежных троек.
        /// </summary>
        public bool Rebuild()
        {
            Vertex<DataType> vertex_i = this;
            Vertex<DataType> vertex_j = this.cros;

            vertex_i.prev.data = vertex_j.data;
            vertex_j.prev.data = vertex_i.data;

            vertex_i.SetCros(vertex_j.next.cros);
            vertex_j.SetCros(vertex_i.next.cros);
            vertex_i.next.SetCros(vertex_j.next);
            vertex_j.next.SetCros(vertex_i.next);

            return true;
        }

        /// <summary>
        /// Создание модели близости по трём объектам.
        /// </summary>
        /// <param name="data_prev">Предыдущий объект в тройке.</param>
        /// <param name="data_curr">Текущий объект в тройке.</param>
        /// <param name="data_next">Следующий объект в тройке.</param>
        /// <returns></returns>
        public static Vertex<DataType> CreateClosenessModel(DataType data_prev, DataType data_curr, DataType data_next)
        {
            Vertex<DataType> vertex_prev = new Vertex<DataType>(data_prev, data_curr, data_next);
            Vertex<DataType> vertex_next = new Vertex<DataType>(data_next, data_curr, data_prev);

            // Установление связей между начальными тройками.
            vertex_prev.prev.SetCros(vertex_next.next);
            vertex_prev.SetCros(vertex_next);
            vertex_prev.next.SetCros(vertex_next.prev);

            return vertex_prev;
        }

        public override string ToString()
        {
            // TODO: Тестовый вариант.
            return this.prev.data.ToString() + " | " + this.data.ToString() + " | " + this.next.data.ToString();
        }
    }
}
