using System;
using System.Collections.Generic;
using Opt.Geometrics.Extentions;
using Opt.Geometrics.Geometrics2d;

namespace Opt.ClosenessModel.Extentions
{
    [Serializable]
    public class ClosenessModelExt<VertexDataType, DataType>
        where DataType : Geometric2d
    {
        // TODO: Test code. Enumerator? MoveNearest?
        public static Vertex<VertexDataType> Nearest(Vertex<VertexDataType> vertex, DataType data)
        {
            Vertex<VertexDataType> minim_vertex = vertex;
            double minim_dist = GeometricExt.Расширенное_расстояние(minim_vertex.Somes.CircleDelone, data);

            double temp_dist = GeometricExt.Расширенное_расстояние(minim_vertex.Cros.Somes.CircleDelone, data);
            if (minim_dist > temp_dist)
            {
                minim_vertex = minim_vertex.Cros;
                minim_dist = temp_dist;
            }
            bool is_end = false;
            while (!is_end)
            {
                is_end = true;
                double prev_dist = GeometricExt.Расширенное_расстояние(minim_vertex.Prev.Cros.Somes.CircleDelone, data);
                double next_dist = GeometricExt.Расширенное_расстояние(minim_vertex.Next.Cros.Somes.CircleDelone, data);
                if (prev_dist < next_dist && prev_dist < minim_dist)
                {
                    minim_dist = prev_dist;
                    minim_vertex = minim_vertex.Prev.Cros;
                    is_end = false;
                }
                if (next_dist < prev_dist && next_dist < minim_dist)
                {
                    minim_dist = next_dist;
                    minim_vertex = minim_vertex.Next.Cros;
                    is_end = false;
                }
            }
            return minim_vertex;
        }

        public static List<Vertex<VertexDataType>> NearestFullPath(Vertex<VertexDataType> vertex, DataType data)
        {
            List<Vertex<VertexDataType>> res = new List<Vertex<VertexDataType>>(); // Изменить при поиске только последней тройки.

            Vertex<VertexDataType> minim_vertex = vertex;
            double minim_dist = GeometricExt.Расширенное_расстояние(minim_vertex.Somes.CircleDelone, data);

            res.Add(minim_vertex);

            double temp_dist = GeometricExt.Расширенное_расстояние(minim_vertex.Cros.Somes.CircleDelone, data);
            if (minim_dist > temp_dist)
            {
                minim_vertex = minim_vertex.Cros;
                minim_dist = temp_dist;
                res.Add(minim_vertex);
            }
            bool is_end = false;
            while (!is_end)
            {
                is_end = true;
                double prev_dist = GeometricExt.Расширенное_расстояние(minim_vertex.Prev.Cros.Somes.CircleDelone, data);
                double next_dist = GeometricExt.Расширенное_расстояние(minim_vertex.Next.Cros.Somes.CircleDelone, data);
                if (prev_dist < next_dist && prev_dist < minim_dist)
                {
                    minim_dist = prev_dist;
                    minim_vertex = minim_vertex.Prev.Cros;
                    res.Add(minim_vertex);
                    is_end = false;
                }
                if (next_dist < prev_dist && next_dist < minim_dist)
                {
                    minim_dist = next_dist;
                    minim_vertex = minim_vertex.Next.Cros;
                    res.Add(minim_vertex);
                    is_end = false;
                }
            }
            return res;
        }

        public static List<Vertex<VertexDataType>> GetTriples(Vertex<VertexDataType> vertex, bool isWidth = true)
        {
            // Поиск всех троек в триангуляции.
            DateTime dt = DateTime.Now;
            List<Vertex<VertexDataType>> list = new List<Vertex<VertexDataType>>();

            vertex.Prev.Somes.LastChecked = dt;
            vertex.Somes.LastChecked = dt;
            vertex.Next.Somes.LastChecked = dt;
            list.Add(vertex);

            if (isWidth)
            {
                GetTriplesWidth(list, vertex.Cros, dt);
            }
            else
            {
                GetTriplesDeep(list, vertex.Cros, dt);
            }
            return list;
        }
        private static void GetTriplesDeep(List<Vertex<VertexDataType>> list, Vertex<VertexDataType> vertex, DateTime dt)
        {
            if (vertex.Somes.LastChecked != dt)
            {
                // Добавляем вершину.
                list.Add(vertex);

                // Отмечем и запускаем для отмеченной тройки.
                Vertex<VertexDataType> vertex_temp = vertex;
                do
                {
                    vertex_temp.Somes.LastChecked = dt;
                    GetTriplesDeep(list, vertex_temp.Cros, dt);
                    vertex_temp = vertex_temp.Next;
                } while (vertex_temp != vertex);
            }
        }
        private static void GetTriplesWidth(List<Vertex<VertexDataType>> list, Vertex<VertexDataType> vertex, DateTime dt)
        {
            if (vertex.Somes.LastChecked != dt)
            {
                // Добавляем вершину.
                list.Add(vertex);

                // Отмечем все тройки.
                Vertex<VertexDataType> vertex_temp = vertex;
                do
                {
                    vertex_temp.Somes.LastChecked = dt;
                    vertex_temp = vertex_temp.Next;
                } while (vertex_temp != vertex);

                // Запускаем для отмеченных.
                do
                {
                    GetTriplesWidth(list, vertex_temp.Cros, dt);
                    vertex_temp = vertex_temp.Next;
                } while (vertex_temp != vertex);
            }
        }
    }
}