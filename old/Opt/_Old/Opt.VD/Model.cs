using System;
using Opt.GeometricObjects;

namespace Opt.Model
{
    public static class ExtendedDistance
    {
        public static double Calc(Circle circle_i, Circle circle_j)
        {
            double dx = circle_j.X - circle_i.X;
            double dy = circle_j.Y - circle_i.Y;
            return Math.Sqrt(dx * dx + dy * dy) - circle_j.R - circle_i.R;
        }
        public static double Calc(Circle circle, StripLine strip_line)
        {
            return (strip_line.PX - circle.X) * strip_line.VY - (strip_line.PY - circle.Y) * strip_line.VX - circle.R;
        }
        public static double Calc(StripLine strip_line, Circle circle)
        {
            return (strip_line.PX - circle.X) * strip_line.VY - (strip_line.PY - circle.Y) * strip_line.VX - circle.R;
        }
        public static double Calc(StripLine strip_line_i, StripLine strip_line_j)
        {
            if (strip_line_i.VX * strip_line_j.VY - strip_line_i.VY * strip_line_j.VX != 0)
                return (strip_line_i.PX - strip_line_j.PX) * strip_line_i.VY - (strip_line_i.PY - strip_line_j.PY) * strip_line_i.VX;
            else
                return double.PositiveInfinity; // !!!Надо проверять направленны ли они в одну сторону или в разные!!!
        }
    }
    public static class PlacingPoint
    {
        public static Point Calc(Circle circle_i, Circle circle_j)
        {
            Vector vector = new Vector(circle_j.X - circle_i.X, circle_j.Y - circle_i.Y);

            double d = Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);

            if (circle_j.R + circle_i.R < d)
                return null;

            vector /= d; // Заменить на метод, не изменяющий ссылки объекта.

            Vector vector_ = new Vector(-vector.Y, vector.X);

            double a = (d - (circle_j.R * circle_j.R - circle_i.R * circle_i.R) / d) / 2;
            double h = Math.Sqrt(circle_i.R * circle_i.R - a * a);

            vector *= a; // Заменить на метод, не изменяющий ссылки объекта.
            vector_ *= h; // Заменить на метод, не изменяющий ссылки объекта.

            return circle_i.Center + vector + vector_; // Заменить на метод, не изменяющий ссылки объекта.
        }
        public static Point Calc(Circle circle, StripLine strip_line)
        {
            return null;
        } // !!!Дописать!!!
        public static Point Calc(StripLine strip_line, Circle circle)
        {
            return null;
        } // !!!Дописать!!!
        public static Point Calc(StripLine strip_line_i, StripLine strip_line_j)
        {
            Vector vector_i = strip_line_i.Vector;
            Vector vector_j_ = new Vector(-strip_line_j.VY, strip_line_j.VX);
            double d = vector_i * vector_j_;
            if (d == 0)
                return null;
            else
                return strip_line_i.Point + vector_i * ((strip_line_j.Point - strip_line_i.Point) * vector_j_ / d);
        } // !!Проверить!!
    }
    public static class DeloneCircle
    {
        public static Circle Calc(Circle circle_i, Circle circle_j, Circle circle_k)
        {
            return null;
        }
        public static Circle Calc(StripLine strip_Line_i, Circle circle_j, Circle circle_k)
        {
            return null;
        }
        public static Circle Calc(StripLine strip_Line_i, StripLine strip_line_j, Circle circle_k)
        {
            return null;
        }
        public static Circle Calc(StripLine strip_Line_i, StripLine strip_line_j, StripLine strip_line_k)
        {
            return null;
        }
    }

    public class TripleClass
    {
        protected TripleClass prev;
        protected TripleClass next;

        public void Add(TripleClass triple)
        {
            triple.prev = this;
            triple.next = this.next;
            triple.prev.next = triple;
            triple.next.prev = triple;
        }
        public void Del()
        {
            prev.next = next;
            next.prev = prev;
            prev = this;
            next = this;
        }

        public class Enumerator
        {
            protected TripleClass start;
            protected TripleClass current;
            public TripleClass Curren
            {
                get
                {
                    return current;
                }
            }

            public Enumerator(TripleClass triple)
            {
                start = triple;
                current = triple;
            }

            public bool MoveNext()
            {
                current = current.next;
                return current != start;
            }

            public void Reset()
            {
                current = start;
            }
        }
        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        /// <summary>
        /// Ссылка на круг Делоне.
        /// </summary>
        public Circle Data;

        public VertexClass Vertex;

        public TripleClass()
        {
            prev = this;
            next = this;
        }
        public TripleClass(VertexClass vertex)
        {
            Vertex = vertex;
            VertexClass.Enumerator en_vertexes = Vertex.GetEnumerator();
            do
            {
                en_vertexes.Current.Triple = this;
            } while (en_vertexes.MoveNextInTriple());
        }

        public void Update()
        {
        } // !!!Недописано!!!
    }

    public class VertexClass
    {
        #region Данные.
        /// <summary>
        /// Ссылка на предыдущий узел в тройке.
        /// </summary>
        protected VertexClass prev;
        public VertexClass Prev
        {
            get
            {
                return prev;
            }
        }
        /// <summary>
        /// Ссылка на перекрёстный узел.
        /// </summary>
        protected VertexClass cros;
        public VertexClass Cros
        {
            get
            {
                return cros;
            }
        }
        /// <summary>
        /// Ссылка на следующий узел.
        /// </summary>
        protected VertexClass next;
        public VertexClass Next
        {
            get
            {
                return next;
            }
        }
        /// <summary>
        /// Ссылка на объект.
        /// </summary>
        protected Object data;
        public Object Data
        {
            get
            {
                return data;
            }
            set
            {
                VertexClass vertex_start = this;
                VertexClass vertex_temp = vertex_start;
                do
                {
                    vertex_temp.data = value;
                    vertex_temp = vertex_temp.next.cros.next;
                } while (vertex_temp != vertex_start);
            }
        }
        /// <summary>
        /// Вспомогательная ссылка.
        /// </summary>
        /// <remarks>
        /// Можно использовать для хранения ссылки на текущую тройку либо для хранения ссылки на круг Делоне связанный с данным узлом.
        /// </remarks>
        public TripleClass Triple;
        #endregion

        protected VertexClass()
        {
        }
        protected VertexClass(Object data_prev, Object data_curr, Object data_next)
        {
            prev = new VertexClass() { next = this, data=data_prev };
            data = data_curr;
            next = new VertexClass() { prev = this,data=data_next };

            prev.prev = next;
            next.next = prev;
        }

        /// <summary>
        /// Разбиение противоположного ребра (вставка двух новых троек).
        /// </summary>
        /// <param name="data">Вставляемый объект.</param>
        public void BreakCrosBy(Object data)
        {
            // Создаём две противоположнонаправленные тройки.
            VertexClass vertex_n = new VertexClass(next.data, data, prev.data) { cros = this};
            VertexClass vertex_f = new VertexClass(prev.data, data, next.data) { cros = this.cros };

            // Устанавливаем связи между созданными тройками.
            vertex_n.prev.SetCros(vertex_f.next);
            vertex_n.next.SetCros(vertex_f.prev);

            // Устанавливаем связи со всем графом.
            vertex_n.cros.cros = vertex_n;
            vertex_f.cros.cros = vertex_f;
            
        }

        public delegate bool CheckedBoolFunction(VertexClass vertex);
        public delegate void CheckedVoidFunction(VertexClass vertex);
        /// <summary>
        /// Переразбиение смежных троек.
        /// </summary>
        public bool Rebuild()
        {
            VertexClass vertex = cros;

            prev.data = vertex.data;
            vertex.prev.data = data;
            SetCros(vertex.next.cros);
            vertex.SetCros(next.cros);
            next.SetCros(vertex.next);

            return true;
        }
        /// <summary>
        /// Переразбиение двух соседних троек при невыполнении условия некоторого формирования троек в модели.
        /// </summary>
        /// <param name="ChF">Метод проверяющий правильность выполнения условия.</param>
        /// <returns>Возвращает true, если произошло хотябы одно переразбиение.</returns>
        public bool Rebuild(CheckedBoolFunction ChF)
        {
            bool res = !ChF(this);
            if (res)
                Rebuild();
            return res;

        }

        public class Enumerator
        {
            protected VertexClass start;
            protected VertexClass current;
            public VertexClass Current
            {
                get
                {
                    return current;
                }
            }

            public Enumerator(VertexClass vertex)
            {
                start = vertex;
                current = vertex;
            }

            public bool MoveNextInNode()
            {
                current = current.next.cros.next;
                return current != start;
            }
            public bool MoveNextInNode(CheckedBoolFunction ChF)
            {
                do
                {
                    if (!current.Rebuild(ChF))
                        return true;
                    current = current.next.cros.next;
                } while (current != start);
                return false;
            }
            public bool MoveNextInNode(CheckedBoolFunction ChF, CheckedVoidFunction FF)
            {
                do
                {
                    if (!current.Rebuild(ChF))
                        return true;
                    FF(current);
                    current = current.next.cros.next;
                } while (current != start);
                return false;
            }

            public bool MoveNextInTriple()
            {
                current = current.next;
                return current != start;
            }
            public bool MoveNextInTriple(CheckedBoolFunction ChF)
            {
                do
                {
                    if (!current.Rebuild(ChF))
                        return true;
                    current = current.next;
                } while (current != start);
                return false;
            }
            public bool MoveNextInTriple(CheckedBoolFunction ChF, CheckedVoidFunction FF)
            {
                do
                {
                    if (!current.Rebuild(ChF))
                        return true;
                    FF(current);
                    current = current.next;
                } while (current != start);
                return false;
            }

            public void Reset()
            {
                current = start;
            }
        }
        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        /// <summary>
        /// Установление связи с перекрёстным узлом (и наоборот).
        /// </summary>
        /// <param name="cros_vertex">Ссылка на перекрёстный узел.</param>
        protected void SetCros(VertexClass cros_vertex)
        {
            cros = cros_vertex;
            cros.cros = this;
        }

        public static VertexClass CreateModel(Object data_prev, Object data_curr, Object data_next)
        {
            VertexClass vertex_prev = new VertexClass(data_prev, data_curr, data_next);
            VertexClass vertex_next = new VertexClass(data_next, data_curr, data_prev);

            // Установление связей между начальными тройками.
            vertex_prev.prev.SetCros(vertex_next.next);
            vertex_prev.SetCros(vertex_next);
            vertex_prev.next.SetCros(vertex_next.prev);

            return vertex_prev;
        } // Проверить! Оптимизировать!
        public static VertexClass CreateModel(Object data_prev, Object data_curr, Object data_next, Object data_cros)
        {
            VertexClass vertex = CreateModel(data_prev, data_curr, data_next);
            vertex.Cros.data = data_cros;
            return vertex;
        }

        public static VertexClass Create(Object data_prev, Object data, Object data_next, bool with_null = false)
        {
            throw new NotImplementedException();
        } // Доделать!
        // 1. Создание Model по двум объектам (и неявно заданном объекте на бесконечности). !Создаются две тройки.
        // 2. Создание Model по трём объектам. !Создаются две тройки.
        // 3. Создание Model по трём объектам (и неявно заданном объекте на бесконечности). !Создаются четыре тройки.
        // 4. Создание Model по набору объектов. Количество объектов n0. !Количество троек пока неизвестно.
        // 5. Создание Model по набору объектов (и неявно заданном объекте на бесконечности). Количество объектов n0. !Создаются 2*(n0-1) тройки.
    }

            //    private bool TheoryCheck(VertexClass vertex, Circle circle)
            //{
            //    bool is_exist;
            //    // Проверяется один раз для всей тройки.
            //    if (((vertex.DataExt as TripleClass).Data as Circle).R < circle.R)
            //        is_exist = false;
            //    else
            //    if (vertex.Cros.Triple.Data.R <= circle.R)
            //        is_exist = true;
            //    else
            //        if (MonotonCheck(vertex, circle)) // Проверка монотонности функции. !!!Дописать!!!
            //            is_exist = false;
            //        else
            //            is_exist = (ModelExtending.ED(vertex.Prev.Data, vertex.Next.Data) <= 2 * circle.R);
            //    return is_exist;
            //}

}
