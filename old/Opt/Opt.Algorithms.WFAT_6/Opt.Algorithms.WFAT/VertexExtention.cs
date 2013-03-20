using System;
using System.Collections.Generic;
using Opt.ClosenessModel;
using Opt.Geometrics.Geometrics2d;
using Circle = Opt.Geometrics.Geometrics2d.Geometric2dWithPoleValue;

namespace Opt.Algorithms
{
    public static class VertexExtention
    {
        public static void SetCircleDelone(this Vertex<Geometric2d> vertex, Circle circle_delone)
        {
            vertex.Prev.Somes.CircleDelone = circle_delone;
            vertex.Somes.CircleDelone = circle_delone;
            vertex.Next.Somes.CircleDelone = circle_delone;            
        }
        public static List<Vertex<Geometric2d>> GetTriples(this Vertex<Geometric2d> vertex)
        {
            // Поиск всех троек в триангуляции.
            DateTime dt = DateTime.Now;
            List<Vertex<Geometric2d>> list = new List<Vertex<Geometric2d>>();

            vertex.Prev.Somes.LastChecked = dt;
            vertex.Somes.LastChecked = dt;
            vertex.Next.Somes.LastChecked = dt;
            list.Add(vertex);

            GetTriples(list, vertex.Cros, dt);
            return list;
        }
        private static void GetTriples(List<Vertex<Geometric2d>> list, Vertex<Geometric2d> vertex, DateTime dt)
        {
            if (vertex.Somes.LastChecked != dt)
            {
                // Добавляем вершину.
                if (vertex.Somes.CircleDelone.Value != 0)
                    list.Add(vertex);

                // Отмечем все тройки.
                Vertex<Geometric2d> vertex_temp = vertex;
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
