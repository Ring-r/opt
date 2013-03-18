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
                new Circle() { P = new double[2]{ el1.Center.X, el1.Center.Y}, R = el1.RadiusX },
                new Circle() { P = new double[2]{  el2.Center.X, el2.Center.Y}, R = el2.RadiusX },
                new Circle() { P = new double[2]{  el3.Center.X, el3.Center.Y}, R = el3.RadiusX }
                });

            el4.Center = new Point(calc.Circle_i.P[0], calc.Circle_i.P[1]);
            el4.RadiusX = el4.RadiusY = calc.Circle_i.R;
        }
    }
}
