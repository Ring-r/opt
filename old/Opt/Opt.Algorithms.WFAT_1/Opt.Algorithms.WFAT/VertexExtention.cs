using System;
using System.Collections.Generic;

using Opt.ClosenessModel;
using Opt.Geometrics;

namespace Opt.Algorithms
{
    public static class VertexExtention
    {
        public static void SetCircleDelone(this Vertex<Geometric> vertex, Circle circle_delone)
        {
            vertex.Prev.Somes.CircleDelone = circle_delone;
            vertex.Somes.CircleDelone = circle_delone;
            vertex.Next.Somes.CircleDelone = circle_delone;            
        }
        public static List<Vertex<Geometric>> GetTriples(this Vertex<Geometric> vertex)
        {
            // Поиск всех троек в триангуляции.
            DateTime dt = DateTime.Now;
            List<Vertex<Geometric>> list = new List<Vertex<Geometric>>();

            vertex.Prev.Somes.LastChecked = dt;
            vertex.Somes.LastChecked = dt;
            vertex.Next.Somes.LastChecked = dt;
            list.Add(vertex);

            GetTriples(list, vertex.Cros, dt);
            return list;
        }
        private static void GetTriples(List<Vertex<Geometric>> list, Vertex<Geometric> vertex, DateTime dt)
        {
            if (vertex.Somes.LastChecked != dt)
            {
                // Добавляем вершину.
                if (vertex.Somes.CircleDelone.Radius != 0)
                    list.Add(vertex);

                // Отмечем все тройки.
                Vertex<Geometric> vertex_temp = vertex;
                do
                {
                    vertex_temp.Somes.LastChecked = dt;
                    vertex_temp = vertex_temp.Next;
                } while (vertex_temp != vertex);

                // Запускаем для отмеченных.
                do
                {
                    GetTriples(list, vertex_temp.Cros, dt);
                    vertex_temp = vertex_temp.Next;
                } while (vertex_temp != vertex);
            }
        }
    }
}
