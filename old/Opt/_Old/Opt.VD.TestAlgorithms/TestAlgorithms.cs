using System.Collections.Generic;
using Opt.Geometrics.Extentions;
using Opt.Geometrics.Geometrics2d;
using Opt.Geometrics.Geometrics2d.Temp;

namespace Opt
{
    namespace VD
    {
        public class TestAlgorithms
        {
            public static Vertex<Circle, DeloneCircle> Близжайшая_вершина(VD<Circle, DeloneCircle> vd, Circle data)
            {
                return Близжайшая_вершина(vd.Близжайшая_тройка(data, false)[0], data);
            }
            public static Vertex<Circle, DeloneCircle> Близжайшая_вершина(Triple<Circle, DeloneCircle> triple, Circle data)
            {
                double prev_distance = CircleExt.Расширенное_расстояние(data, triple.Vertex.Prev.Data);
                double next_distance = CircleExt.Расширенное_расстояние(data, triple.Vertex.Next.Data);
                Vertex<Circle, DeloneCircle> minimal_vertex;
                double minimal_distance;
                if (prev_distance < next_distance)
                {
                    minimal_vertex = triple.Vertex.Prev;
                    minimal_distance = prev_distance;
                }
                else
                {
                    minimal_vertex = triple.Vertex.Next;
                    minimal_distance = next_distance;
                }

                if (minimal_distance > CircleExt.Расширенное_расстояние(data, triple.Vertex.Data))
                    return triple.Vertex;

                return minimal_vertex;
            }

            public static Triple<Circle, DeloneCircle> Минимальновозможная_тройка(VD<Circle, DeloneCircle> vd, Circle data)
            {
                Triple<Circle, DeloneCircle> temp_triple = vd.NextTriple(vd.NullTriple);
                while (temp_triple.Delone_Circle.R < data.R)
                    temp_triple = vd.NextTriple(temp_triple);

                Triple<Circle, DeloneCircle> minimal_triple = temp_triple;
                while (temp_triple != vd.NullTriple)
                {
                    if (minimal_triple.Delone_Circle.R > temp_triple.Delone_Circle.R && temp_triple.Delone_Circle.R > data.R)
                        minimal_triple = temp_triple;
                    temp_triple = vd.NextTriple(temp_triple);
                }
                return minimal_triple;
            }

            public static List<Point2d> Точки_плотного_размещения(VD<Circle, DeloneCircle> vd, Circle data)
            {
                List<Point2d> points = new List<Point2d>();

                Triple<Circle, DeloneCircle> triple = vd.NextTriple(vd.NullTriple);
                while (triple != vd.NullTriple)
                {
                    points.AddRange(Точки_плотного_размещения(triple, data));
                    triple = vd.NextTriple(triple);
                }

                return points;
            }
            public static List<Point2d> Точки_плотного_размещения(Triple<Circle, DeloneCircle> triple, Circle data)
            {
                List<Point2d> points = new List<Point2d>();

                if (double.IsInfinity(triple.Delone_Circle.R))
                {
                    Vertex<Circle, DeloneCircle> vertex = triple.Vertex;
                    while (vertex.Data != null)
                        vertex = vertex.Next;
                    if (CircleExt.Расширенное_расстояние(vertex.Next.Data, vertex.Prev.Data) <= 2 * data.R)
                        points.Add(CircleExt.Точка_пересечения_границ(new Circle() { R = vertex.Next.Data.R + data.R, X = vertex.Next.Data.X, Y = vertex.Next.Data.Y }, new Circle() { R = vertex.Prev.Data.R + data.R, X = vertex.Prev.Data.X, Y = vertex.Prev.Data.Y }));
                }
                else
                {
                    if (triple.Delone_Circle.R >= data.R)
                    {
                        Vertex<Circle, DeloneCircle> vertex = triple.Vertex;
                        do
                        {

                            if (CircleExt.Расширенное_расстояние(vertex.Next.Data, vertex.Prev.Data) <= 2 * data.R)
                            {
                                if (double.IsInfinity(vertex.Cros.Delone_Circle.R))
                                {
                                    StripLine strip_line = new StripLine() { PX = vertex.Next.Data.X, PY = vertex.Next.Data.Y, VX = vertex.Prev.Data.X - vertex.Next.Data.X, VY = vertex.Prev.Data.Y - vertex.Next.Data.Y };
                                    if (PlaneExt.Расширенное_расстояние(strip_line, vertex.Delone_Circle.Center) < 0)
                                        points.Add(CircleExt.Точка_пересечения_границ(new Circle() { R = vertex.Next.Data.R + data.R, X = vertex.Next.Data.X, Y = vertex.Next.Data.Y }, new Circle() { R = vertex.Prev.Data.R + data.R, X = vertex.Prev.Data.X, Y = vertex.Prev.Data.Y }));
                                }
                                else
                                {
                                    StripLine strip_line = new StripLine() { PX = vertex.Next.Data.X, PY = vertex.Next.Data.Y, VX = vertex.Prev.Data.X - vertex.Next.Data.X, VY = vertex.Prev.Data.Y - vertex.Next.Data.Y };
                                    double r = PlaneExt.Расширенное_расстояние(strip_line, vertex.Delone_Circle.Center);
                                    double rr = PlaneExt.Расширенное_расстояние(strip_line, vertex.Cros.Delone_Circle.Center);

                                    if (r * rr < 0)
                                        points.Add(CircleExt.Точка_пересечения_границ(new Circle() { R = vertex.Next.Data.R + data.R, X = vertex.Next.Data.X, Y = vertex.Next.Data.Y }, new Circle() { R = vertex.Prev.Data.R + data.R, X = vertex.Prev.Data.X, Y = vertex.Prev.Data.Y }));
                                    else
                                        if (vertex.Cros.Delone_Circle.R < data.R)
                                            points.Add(CircleExt.Точка_пересечения_границ(new Circle() { R = vertex.Next.Data.R + data.R, X = vertex.Next.Data.X, Y = vertex.Next.Data.Y }, new Circle() { R = vertex.Prev.Data.R + data.R, X = vertex.Prev.Data.X, Y = vertex.Prev.Data.Y }));
                                }
                            }
                            vertex = vertex.Next;
                        } while (vertex != triple.Vertex);
                    }
                }

                return points;
            }
        }
    }
}
