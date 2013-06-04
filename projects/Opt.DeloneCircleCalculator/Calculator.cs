using System;
using System.Collections.Generic;

namespace DeloneCircleCalculator
{
    public class Calculator
    {
        /// <summary>
        /// Get the circle_i.
        /// </summary>
        /// <value>
        /// The circle_i.
        /// </value>
        public Circle Circle_i { get; private set; }

        /// <summary>
        /// Get the circle_j.
        /// </summary>
        /// <value>
        /// The circle_j.
        /// </value>
        public Circle Circle_j { get; private set; }

        /// <summary>
        /// Get the circle.
        /// </summary>
        /// <value>
        /// The circle.
        /// </value>
        public Circle Circle { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeloneCircleCalculator.Calculator"/> class.
        /// </summary>
        /// <param name='objects'>
        /// Objects.
        /// </param>
        /// <param name='dim'>
        /// Dimension.
        /// </param>
        public Calculator(Object[] objects, Int32 dim = 2)
        {
            Circle circle = null;
            List<Double[]> slae = new List<Double[]>(dim + 1);
            for (int i = 0; i < objects.Length; i++)
            {
                Circle circle_temp = objects[i] as Circle;
                if (circle_temp != null)
                {
                    if (circle == null)
                    {
                        circle = circle_temp;
                    }
                    else
                    {
                        Double[] functiom_elements = new Double[dim + 2];
                        for (int j = 0; j < dim; j++)
                        {
                            functiom_elements[j] = circle_temp.Point[j] - circle.Point[j];
                            functiom_elements[dim + 1] += functiom_elements[j] * functiom_elements[j];
                        }
                        functiom_elements[dim] = circle_temp.Value - circle.Value;
                        functiom_elements[dim + 1] = functiom_elements[dim] * functiom_elements[dim] - functiom_elements[dim + 1]; // TODO: Check.
                        slae.Add(functiom_elements);
                    }
                }
                else
                {
                    Polyplane polyplane_temp = objects[i] as Polyplane;
                    Double[] functiom_elements = new Double[dim + 2]; // TODO: Check.
                    for (int j = 0; j < dim; j++)
                    {
                        functiom_elements[j] = polyplane_temp.Vector[j];
                        functiom_elements[dim + 1] += polyplane_temp.Vector[j] * polyplane_temp.Point[j];
                    }
                    functiom_elements[dim] = -1;
                    functiom_elements[dim + 1] = -functiom_elements[dim + 1];
                    slae.Add(functiom_elements);
                }
            }

            if (circle != null)
            {
                #region Решение СЛАУ при неизвестном R. Временный ограниченный вариант.
                Double k = slae[0][0] * slae[1][1] - slae[1][0] * slae[0][1];
                Double ax = (slae[0][2] * slae[1][1] - slae[1][2] * slae[0][1]) / k;
                Double bx = (slae[0][3] * slae[1][1] - slae[1][3] * slae[0][1]) / k;
                Double ay = -(slae[0][2] * slae[1][0] - slae[1][2] * slae[0][0]) / k;
                Double by = -(slae[0][3] * slae[1][0] - slae[1][3] * slae[0][0]) / k;
                #endregion

                #region Решение квадратичного уравнения.
                Double A = ax * ax + ay * ay - 1;
                Double B = ax * bx + ay * by;
                Double C = bx * bx + by * by;
                Double D = A * C - B * B; D = Math.Sqrt(D);

                Circle_i = new Circle();
                Circle_i.Value = (-B + D) / A;
                for (int i = 0; i < dim; i++)
                    Circle_i.Point[i] = -(slae[i][dim] * Circle_i.Value + slae[i][dim + 1]);

                Circle_j = new Circle();
                Circle_j.Value = (-B - D) / A;
                for (int i = 0; i < dim; i++)
                    Circle_j.Point[i] = -(slae[i][dim] * Circle_j.Value + slae[i][dim + 1]);
                #endregion
            }
            else
            {
                //!!!Решение СЛАУ!!!
            }

            for (int i = 0; i < dim; i++)
            {
                Circle_i.Point[i] += circle.Point[i];
                Circle_j.Point[i] += circle.Point[i];
            }

            Circle_i.Value -= circle.Value;
            Circle_j.Value -= circle.Value;

            //!!!Выбор правильного круга из двух!!!
        }
    }
}
