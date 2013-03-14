using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
                new Circle() { X = el1.Center.X, Y = el1.Center.Y, R = el1.RadiusX },
                new Circle() { X = el2.Center.X, Y = el2.Center.Y, R = el2.RadiusX },
                new Circle() { X = el3.Center.X, Y = el3.Center.Y, R = el3.RadiusX }
                });

            el4.Center = new Point(calc.Circle_i.X, calc.Circle_i.Y);
            el4.RadiusX = el4.RadiusY = calc.Circle_i.R;
        }
    }
}
