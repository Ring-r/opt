using System.Windows;
using Opt.Geometrics.Geometrics2d;
using Opt.Geometrics.Geometrics2d.Extentions;

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

            Geometric2dWithPointScalar circle = Geometric2dExt.Круг_Делоне(
                    new Circle2d() { X = el1.Center.X, Y = el1.Center.Y, R = el1.RadiusX },
                    new Circle2d() { X = el2.Center.X, Y = el2.Center.Y, R = el2.RadiusX },
                    new Circle2d() { X = el3.Center.X, Y = el3.Center.Y, R = el3.RadiusX }
                );


            el4.Center = new Point(circle.Point.X, circle.Point.Y);
            el4.RadiusX = el4.RadiusY = circle.Scalar;
        }
    }
}
