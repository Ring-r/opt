using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeloneCircleCalculator
{
	public class Calculator
	{
		/// <summary>
		/// Gets or sets the circle_i.
		/// </summary>
		/// <value>
		/// The circle_i.
		/// </value>
		public Circle Circle_i { get; set; }

		/// <summary>
		/// Gets or sets the circle_j.
		/// </summary>
		/// <value>
		/// The circle_j.
		/// </value>
		public Circle Circle_j { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="DeloneCircleCalculator.Calculator"/> class.
		/// </summary>
		/// <param name='objects'>
		/// Objects.
		/// </param>
		/// <param name='dimension'>
		/// Dimension.
		/// </param>
		public Calculator (Object[] objects, Int32 dimension=2)
		{
			Circle circle = null;
			List<Double[]> slae = new List<Double[]> (dimension + 1);
			for (int i = 0; i < objects.Length; i++) {
				if (objects [i] is Circle)
				if (circle == null)
					circle = (Circle)objects [i];
				else {
					Circle circle_temp = (Circle)objects [i];
					Double[] functiom_elements = new Double[dimension + 2];
					Double d = 0;
					for (int j = 0; j < dimension; j++) {
						functiom_elements [j] = circle_temp.P [j] - circle.P [j];
						functiom_elements [dimension + 1] += functiom_elements [j] * functiom_elements [j];
					}
					functiom_elements [dimension] = circle_temp.R - circle.R;
					functiom_elements [dimension + 1] = functiom_elements [2] * functiom_elements [2] - functiom_elements [dimension + 1];
					slae.Add (functiom_elements);
				}
				else {
					Polyplane polyplane_temp = (Polyplane)objects [i];
					Double[] functiom_elements = new Double[4];
					for (int j = 0; j < dimension; j++) {
						functiom_elements [j] = polyplane_temp.N [j];
						functiom_elements [dimension + 1] += polyplane_temp.N [j] * polyplane_temp.P [j];
					}
					functiom_elements [dimension] = -1;
					functiom_elements [dimension + 1] = -functiom_elements [dimension + 1];
					slae.Add (functiom_elements);
				}
			}

			if (circle != null) {
				#region Решение СЛАУ при неизвестном R.
				Double k = slae [0] [0] * slae [1] [1] - slae [1] [0] * slae [0] [1];
				Double ax = (slae [0] [2] * slae [1] [1] - slae [1] [2] * slae [0] [1]) / k;
				Double bx = (slae [0] [3] * slae [1] [1] - slae [1] [3] * slae [0] [1]) / k;
				Double ay = -(slae [0] [2] * slae [1] [0] - slae [1] [2] * slae [0] [0]) / k;
				Double by = -(slae [0] [3] * slae [1] [0] - slae [1] [3] * slae [0] [0]) / k;
				#endregion

				#region Решение квадратичного уравнения.
				Double A = ax * ax + ay * ay - 1;
				Double B = ax * bx + ay * by;
				Double C = bx * bx + by * by;
				Double D = A * C - B * B;

                Circle_i = new Circle();
				Circle_i.R = (-B + Math.Sqrt (D)) / A;
				for (int i = 0; i < dimension; i++)
					Circle_i.P [i] = -(slae [i] [dimension] * Circle_i.R + slae [i] [dimension + 1]);

                Circle_j = new Circle();
				Circle_j.R = (-B - Math.Sqrt (A * C - B * B)) / A;
				for (int i = 0; i < dimension; i++)
					Circle_j.P [i] = -(slae [i] [dimension] * Circle_j.R + slae [i] [dimension + 1]);
				#endregion
			} else {
				//!!!Решение СЛАУ!!!
			}

			for (int i = 0; i < dimension; i++) {
				Circle_i.P [i] += circle.P [i];
				Circle_j.P [i] += circle.P [i];
			}

			Circle_i.R -= circle.R;
			Circle_j.R -= circle.R;

			//!!!Выбор правильного круга из двух!!!
		}
	}
}
