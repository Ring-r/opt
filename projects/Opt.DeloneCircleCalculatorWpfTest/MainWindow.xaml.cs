using System;
using System.Windows;

using DeloneCircleCalculator;

namespace DeloneCircleCalculatorWpfTest
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Calculator calc = new Calculator(
                new Object[]
                {
                new Circle() { Point = new double[2]{ el1.Center.X, el1.Center.Y}, Value = el1.RadiusX },
                new Circle() { Point = new double[2]{  el2.Center.X, el2.Center.Y}, Value = el2.RadiusX },
                new Circle() { Point = new double[2]{  el3.Center.X, el3.Center.Y}, Value = el3.RadiusX }
                });

            el4.Center = new Point(calc.Circle_i.Point[0], calc.Circle_i.Point[1]);
            el4.RadiusX = el4.RadiusY = calc.Circle_i.Value;
        }
    }
}
