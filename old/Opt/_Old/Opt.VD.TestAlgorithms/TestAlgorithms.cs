using System;
using System.Collections.Generic;

using Opt.GeometricObjects;
using Opt.GeometricObjects.Extending;

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
                double prev_distance = data.ExtendedDistance(triple.Vertex.Prev.Data);
                double next_distance = data.ExtendedDistance(triple.Vertex.Next.Data);
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

                if (minimal_distance > data.ExtendedDistance(triple.Vertex.Data))
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

            public static List<Point> Точки_плотного_размещения(VD<Circle, DeloneCircle> vd, Circle data)
            {
                List<Point> points = new List<Point>();

                Triple<Circle, DeloneCircle> triple = vd.NextTriple(vd.NullTriple);
                while (triple != vd.NullTriple)
                {
                    points.AddRange(Точки_плотного_размещения(triple, data));
                    triple = vd.NextTriple(triple);
                }

                return points;
            }
            public static List<Point> Точки_плотного_размещения(Triple<Circle, DeloneCircle> triple, Circle data)
            {
                List<Point> points = new List<Point>();

                if (double.IsInfinity(triple.Delone_Circle.R))
                {
                    Vertex<Circle, DeloneCircle> vertex = triple.Vertex;
                    while (vertex.Data != null)
                        vertex = vertex.Next;
                    if (vertex.Next.Data.ExtendedDistance(vertex.Prev.Data) <= 2 * data.R)
                        points.Add(CircleExtending.PointOfIntersection(new Circle() { R = vertex.Next.Data.R + data.R, X = vertex.Next.Data.X, Y = vertex.Next.Data.Y }, new Circle() { R = vertex.Prev.Data.R + data.R, X = vertex.Prev.Data.X, Y = vertex.Prev.Data.Y }, -1));
                }
                else
                {
                    if (triple.Delone_Circle.R >= data.R)
                    {
                        Vertex<Circle, DeloneCircle> vertex = triple.Vertex;
                        do
                        {

                            if (vertex.Next.Data.ExtendedDistance(vertex.Prev.Data) <= 2 * data.R)
                            {
                                if (double.IsInfinity(vertex.Cros.Delone_Circle.R))
                                {
                                    StripLine strip_line = new StripLine(vertex.Next.Data.X, vertex.Next.Data.Y, vertex.Prev.Data.X - vertex.Next.Data.X, vertex.Prev.Data.Y - vertex.Next.Data.Y);
                                    if(strip_line.ExtendedDistance(vertex.Delone_Circle.Center)<0)
                                        points.Add(CircleExtending.PointOfIntersection(new Circle() { R = vertex.Next.Data.R + data.R, X = vertex.Next.Data.X, Y = vertex.Next.Data.Y }, new Circle() { R = vertex.Prev.Data.R + data.R, X = vertex.Prev.Data.X, Y = vertex.Prev.Data.Y }, -1));
                                }
                                else
                                {
                                    StripLine strip_line = new StripLine(vertex.Next.Data.X, vertex.Next.Data.Y, vertex.Prev.Data.X - vertex.Next.Data.X, vertex.Prev.Data.Y - vertex.Next.Data.Y);
                                    double r = strip_line.ExtendedDistance(vertex.Delone_Circle.Center);
                                    double rr = strip_line.ExtendedDistance(vertex.Cros.Delone_Circle.Center);

                                    if (r * rr < 0)
                                        points.Add(CircleExtending.PointOfIntersection(new Circle(vertex.Next.Data.R + data.R, vertex.Next.Data.X, vertex.Next.Data.Y), new Circle(vertex.Prev.Data.R + data.R, vertex.Prev.Data.X, vertex.Prev.Data.Y),-1));
                                    else
                                        if (vertex.Cros.Delone_Circle.R < data.R)
                                            points.Add(CircleExtending.PointOfIntersection(new Circle(vertex.Next.Data.R + data.R, vertex.Next.Data.X, vertex.Next.Data.Y), new Circle(vertex.Prev.Data.R + data.R, vertex.Prev.Data.X, vertex.Prev.Data.Y),-1));
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
