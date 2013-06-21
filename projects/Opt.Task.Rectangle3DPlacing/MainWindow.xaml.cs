using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Threading;
using Microsoft.Win32;

namespace Rectangle3DPlacing
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Random random = new Random();

        private Rect[] rects;
        private Region region;
        private Placing placing;

        private DiffuseMaterial material_r = new DiffuseMaterial(new SolidColorBrush(Color.FromArgb(255, 255, 255, 0)));
        private DiffuseMaterial material_rect = new DiffuseMaterial(new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)));
        private DiffuseMaterial material_region = new DiffuseMaterial(new SolidColorBrush(Color.FromArgb(255, 255, 0, 0)));

        private DispatcherTimer dispatcherTimer;

        private int geometry_index = -1;
        private MeshGeometry3D mesh_geometry_3d_rect;
        private MeshGeometry3D mesh_geometry_3d_region;

        private string filename = null;

        public MainWindow()
        {
            InitializeComponent();

            R.Dim = 0;
            rects = new Rect[0];
            region = new Region();

            CreateUserInterface();

            placing = new Placing(rects, region);
            placing.Find();

            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);

            #region Создание трёхмерного объекта размещения.
            mesh_geometry_3d_rect = new MeshGeometry3D();
            #region Вершины единичного куба.
            mesh_geometry_3d_rect.Positions.Add(new Point3D(0, 0, 0)); // 0
            mesh_geometry_3d_rect.Positions.Add(new Point3D(1, 0, 0)); // 1
            mesh_geometry_3d_rect.Positions.Add(new Point3D(0, 1, 0)); // 2
            mesh_geometry_3d_rect.Positions.Add(new Point3D(1, 1, 0)); // 3

            mesh_geometry_3d_rect.Positions.Add(new Point3D(0, 0, 1)); // 4
            mesh_geometry_3d_rect.Positions.Add(new Point3D(1, 0, 1)); // 5
            mesh_geometry_3d_rect.Positions.Add(new Point3D(0, 1, 1)); // 6
            mesh_geometry_3d_rect.Positions.Add(new Point3D(1, 1, 1)); // 7
            #endregion
            #region Грани единичного куба.
            mesh_geometry_3d_rect.TriangleIndices.Add(0); mesh_geometry_3d_rect.TriangleIndices.Add(2); mesh_geometry_3d_rect.TriangleIndices.Add(1);
            mesh_geometry_3d_rect.TriangleIndices.Add(1); mesh_geometry_3d_rect.TriangleIndices.Add(2); mesh_geometry_3d_rect.TriangleIndices.Add(3);
            mesh_geometry_3d_rect.TriangleIndices.Add(0); mesh_geometry_3d_rect.TriangleIndices.Add(4); mesh_geometry_3d_rect.TriangleIndices.Add(2);
            mesh_geometry_3d_rect.TriangleIndices.Add(2); mesh_geometry_3d_rect.TriangleIndices.Add(4); mesh_geometry_3d_rect.TriangleIndices.Add(6);

            mesh_geometry_3d_rect.TriangleIndices.Add(0); mesh_geometry_3d_rect.TriangleIndices.Add(1); mesh_geometry_3d_rect.TriangleIndices.Add(4);
            mesh_geometry_3d_rect.TriangleIndices.Add(1); mesh_geometry_3d_rect.TriangleIndices.Add(5); mesh_geometry_3d_rect.TriangleIndices.Add(4);
            mesh_geometry_3d_rect.TriangleIndices.Add(1); mesh_geometry_3d_rect.TriangleIndices.Add(7); mesh_geometry_3d_rect.TriangleIndices.Add(5);
            mesh_geometry_3d_rect.TriangleIndices.Add(1); mesh_geometry_3d_rect.TriangleIndices.Add(3); mesh_geometry_3d_rect.TriangleIndices.Add(7);

            mesh_geometry_3d_rect.TriangleIndices.Add(4); mesh_geometry_3d_rect.TriangleIndices.Add(5); mesh_geometry_3d_rect.TriangleIndices.Add(6);
            mesh_geometry_3d_rect.TriangleIndices.Add(7); mesh_geometry_3d_rect.TriangleIndices.Add(6); mesh_geometry_3d_rect.TriangleIndices.Add(5);
            mesh_geometry_3d_rect.TriangleIndices.Add(2); mesh_geometry_3d_rect.TriangleIndices.Add(6); mesh_geometry_3d_rect.TriangleIndices.Add(3);
            mesh_geometry_3d_rect.TriangleIndices.Add(3); mesh_geometry_3d_rect.TriangleIndices.Add(6); mesh_geometry_3d_rect.TriangleIndices.Add(7);
            #endregion
            #endregion

            #region Создание трёхмерной области размещения.
            mesh_geometry_3d_region = new MeshGeometry3D();
            #region Вершины единичного куба.
            mesh_geometry_3d_region.Positions.Add(new Point3D(0, 0, 0)); // 0
            mesh_geometry_3d_region.Positions.Add(new Point3D(1, 0, 0)); // 1
            mesh_geometry_3d_region.Positions.Add(new Point3D(0, 1, 0)); // 2
            mesh_geometry_3d_region.Positions.Add(new Point3D(1, 1, 0)); // 3

            mesh_geometry_3d_region.Positions.Add(new Point3D(0, 0, 1)); // 4
            mesh_geometry_3d_region.Positions.Add(new Point3D(1, 0, 1)); // 5
            mesh_geometry_3d_region.Positions.Add(new Point3D(0, 1, 1)); // 6
            mesh_geometry_3d_region.Positions.Add(new Point3D(1, 1, 1)); // 7
            #endregion
            #region Грани единичного куба.
            mesh_geometry_3d_region.TriangleIndices.Add(1); mesh_geometry_3d_region.TriangleIndices.Add(2); mesh_geometry_3d_region.TriangleIndices.Add(0);
            mesh_geometry_3d_region.TriangleIndices.Add(3); mesh_geometry_3d_region.TriangleIndices.Add(2); mesh_geometry_3d_region.TriangleIndices.Add(1);
            mesh_geometry_3d_region.TriangleIndices.Add(2); mesh_geometry_3d_region.TriangleIndices.Add(4); mesh_geometry_3d_region.TriangleIndices.Add(0);
            mesh_geometry_3d_region.TriangleIndices.Add(6); mesh_geometry_3d_region.TriangleIndices.Add(4); mesh_geometry_3d_region.TriangleIndices.Add(2);

            mesh_geometry_3d_region.TriangleIndices.Add(4); mesh_geometry_3d_region.TriangleIndices.Add(1); mesh_geometry_3d_region.TriangleIndices.Add(0);
            mesh_geometry_3d_region.TriangleIndices.Add(4); mesh_geometry_3d_region.TriangleIndices.Add(5); mesh_geometry_3d_region.TriangleIndices.Add(1);
            mesh_geometry_3d_region.TriangleIndices.Add(5); mesh_geometry_3d_region.TriangleIndices.Add(7); mesh_geometry_3d_region.TriangleIndices.Add(1);
            mesh_geometry_3d_region.TriangleIndices.Add(7); mesh_geometry_3d_region.TriangleIndices.Add(3); mesh_geometry_3d_region.TriangleIndices.Add(1);

            mesh_geometry_3d_region.TriangleIndices.Add(6); mesh_geometry_3d_region.TriangleIndices.Add(5); mesh_geometry_3d_region.TriangleIndices.Add(4);
            mesh_geometry_3d_region.TriangleIndices.Add(5); mesh_geometry_3d_region.TriangleIndices.Add(6); mesh_geometry_3d_region.TriangleIndices.Add(7);
            mesh_geometry_3d_region.TriangleIndices.Add(3); mesh_geometry_3d_region.TriangleIndices.Add(6); mesh_geometry_3d_region.TriangleIndices.Add(2);
            mesh_geometry_3d_region.TriangleIndices.Add(7); mesh_geometry_3d_region.TriangleIndices.Add(6); mesh_geometry_3d_region.TriangleIndices.Add(3);
            #endregion
            #endregion

            text_box_dim.Text = R.Dim.ToString();

            Visualize();
        }

        private void ReadFromFile(string filename)
        {
            StreamReader sr = new StreamReader(filename);

            R.Dim = int.Parse(sr.ReadLine());

            region = Region.Parse(sr.ReadLine());

            int n = int.Parse(sr.ReadLine());
            rects = new Rect[n];
            int i;
            for (i = 0; i < n && !sr.EndOfStream; i++)
                rects[i] = Rect.Parse(sr.ReadLine());
            for (; i < n; i++)
                rects[i] = new Rect();
            sr.Close();
        }

        private void WriteToFile(string filename)
        {
            StreamWriter sw = new StreamWriter(filename);
            sw.WriteLine(Rect.Dim);
            sw.WriteLine(region.ToString());
            sw.WriteLine(rects.Length);
            for (int i = 0; i < rects.Length; i++)
                sw.WriteLine(rects[i].ToString());
            sw.Close();
        }


        private void CreateUserInterface()
        {
            #region Область размещения.
            grid_region.Children.Clear();
            grid_region.ColumnDefinitions.Clear();

            for (int i = 0; i < R.Dim; i++)
            {
                grid_region.ColumnDefinitions.Add(new ColumnDefinition() { SharedSizeGroup = "dimension" });

                TextBox text_box = new TextBox();
                text_box.Text = region.Size(i).ToString();                
                text_box.SetValue(Grid.ColumnProperty, i);
                if (region.Freez(i))
                    text_box.Background = Brushes.Red;
                else
                    text_box.Background = Brushes.White;
                text_box.Tag = i;
                text_box.KeyDown += new System.Windows.Input.KeyEventHandler(TextBoxRegion_KeyDown);
                grid_region.Children.Add(text_box);
            }
            #endregion

            #region Объекты размещения.
            for (int i = 0; i < rects.Length; i++)
            {
                grid_rects.RowDefinitions.Add(new RowDefinition());

                Grid grid = new Grid();
                grid_rects.Children.Add(grid);
                grid.SetValue(Grid.RowProperty, i);
                grid.Tag = i;
                grid.MouseEnter += new System.Windows.Input.MouseEventHandler(GridRect_MouseEnter);
                for (int j = 0; j < R.Dim; j++)
                {
                    grid.ColumnDefinitions.Add(new ColumnDefinition() { SharedSizeGroup = "dimension" });

                    Grid grid_ = new Grid();
                    grid.Children.Add(grid_);
                    grid_.SetValue(Grid.ColumnProperty, j);
                    grid_.ColumnDefinitions.Add(new ColumnDefinition() { SharedSizeGroup = "value" });
                    grid_.ColumnDefinitions.Add(new ColumnDefinition() { SharedSizeGroup = "value" });
                    grid_.ColumnDefinitions.Add(new ColumnDefinition() { SharedSizeGroup = "value" });

                    TextBox text_box;

                    text_box = new TextBox();
                    text_box.Text = rects[i].Min(j).ToString();
                    text_box.IsReadOnly = true;
                    text_box.SetValue(Grid.ColumnProperty, 0);
                    grid_.Children.Add(text_box);

                    text_box = new TextBox();
                    text_box.Text = rects[i].Size(j).ToString();
                    //text_box.IsReadOnly = true;
                    text_box.SetValue(Grid.ColumnProperty, 1);
                    text_box.Tag = j;
                    text_box.KeyDown += new System.Windows.Input.KeyEventHandler(TextBoxRect_KeyDown);
                    grid_.Children.Add(text_box);

                    text_box = new TextBox();
                    text_box.Text = rects[i].Max(j).ToString();
                    text_box.IsReadOnly = true;
                    text_box.SetValue(Grid.ColumnProperty, 2);
                    grid_.Children.Add(text_box);
                }
            }
            #endregion
        }

        private void TextBoxRegion_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                int i = (int)(sender as TextBox).Tag;
                try
                {
                    double size=double.Parse((sender as TextBox).Text);
                    if (size < 0)
                    {
                        region.Freez(i, false);
                        region.Size(i, 0);
                    }
                    else
                    {
                        region.Freez(i, false);
                        region.Size(i, size);
                        region.Freez(i, true);
                    }

                    CreateUserInterface();

                    placing = new Placing(rects, region);
                    placing.Find();

                    Visualize();
                }
                catch
                {
                    MessageBox.Show("Введены неправильные данные!");
                }
            }
        }

        private void GridRect_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            geometry_index = (int)(sender as Grid).Tag;

            Visualize();
        }

        private void TextBoxRect_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                if (0 <= geometry_index && geometry_index < rects.Length)
                {
                    int j = (int)(sender as TextBox).Tag;
                    try
                    {
                        double size = double.Parse((sender as TextBox).Text);

                        if (size > 0)
                        {
                            rects[geometry_index].Size(j, size);

                            placing = new Placing(rects, region);
                            placing.Find();

                            Visualize();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Введены неправильные данные!");
                    }
                }
            }
        }

        private void Visualize()
        {
            #region Рисование
            #region Определение смещение центра композиции.
            double[] offset = new double[R.Dim];
            for (int i = 0; i < Rect.Dim; i++)
                offset[i] = region.Size(i) / 2;
            #endregion

            model_3d_group.Children.Clear();

            GeometryModel3D gm;
            Transform3DGroup tg;

            #region Создание трёхмерной области размещения.
            gm = new GeometryModel3D(mesh_geometry_3d_region, material_region);

            tg = new Transform3DGroup();
            if (R.Dim >= 3)
            {
                tg.Children.Add(new ScaleTransform3D(region.Size(0), region.Size(1), region.Size(2)));
                tg.Children.Add(new TranslateTransform3D(region.Min(0) - offset[0], region.Min(1) - offset[1], region.Min(2) - offset[2]));
            }
            gm.Transform = tg;

            model_3d_group.Children.Add(gm);
            #endregion

            #region Корректировка осей координат.
            if (R.Dim >= 3)
            {
                ssl_x.Points[0] = new Point3D(-offset[0], -offset[1], -offset[2]);
                ssl_x.Points[1] = new Point3D(-offset[0] + 1.5 * region.Size(0), -offset[1], -offset[2]);

                ssl_y.Points[0] = new Point3D(-offset[0], -offset[1], -offset[2]);
                ssl_y.Points[1] = new Point3D(-offset[0], -offset[1] + 1.5 * region.Size(1), -offset[2]);

                ssl_z.Points[0] = new Point3D(-offset[0], -offset[1], -offset[2]);
                ssl_z.Points[1] = new Point3D(-offset[0], -offset[1], -offset[2] + 1.5 * region.Size(2));
            }
            #endregion

            foreach (Rect rect in rects)
            {
                #region Создание трёхмерного объекта.
                gm = new GeometryModel3D(mesh_geometry_3d_rect, material_rect);

                tg = new Transform3DGroup();
                if (R.Dim >= 3)
                {
                    tg.Children.Add(new ScaleTransform3D(rect.Size(0), rect.Size(1), rect.Size(2)));
                    tg.Children.Add(new TranslateTransform3D(rect.Min(0) - offset[0], rect.Min(1) - offset[1], rect.Min(2) - offset[2]));
                }
                gm.Transform = tg;

                model_3d_group.Children.Add(gm);
                #endregion
            }

            #region Создание трёхмерного объекта.
            if (0 <= geometry_index && geometry_index < rects.Length)
            {
                gm = new GeometryModel3D(mesh_geometry_3d_rect, material_r);

                Rect rect = rects[geometry_index];

                tg = new Transform3DGroup();
                if (R.Dim >= 3)
                {
                    tg.Children.Add(new ScaleTransform3D(rect.Size(0), rect.Size(1), rect.Size(2)));
                    tg.Children.Add(new TranslateTransform3D(rect.Min(0) - offset[0], rect.Min(1) - offset[1], rect.Min(2) - offset[2]));
                }
                gm.Transform = tg;

                model_3d_group.Children.Add(gm);
            }
            #endregion
            #endregion

            #region Заполнение таблиц.
            #region Область размещения.
            for (int i = 0; i < R.Dim; i++)
                (grid_region.Children[i] as TextBox).Text = region.Size(i).ToString();
            #endregion

            #region Объекты размещения.
            for (int i = 0; i < rects.Length; i++)
            {
                Grid grid = grid_rects.Children[i] as Grid;

                for (int j = 0; j < R.Dim; j++)
                {
                    ((grid.Children[j] as Grid).Children[0] as TextBox).Text = rects[i].Min(j).ToString();
                    ((grid.Children[j] as Grid).Children[0] as TextBox).Background = null;
                    ((grid.Children[j] as Grid).Children[1] as TextBox).Text = rects[i].Size(j).ToString();
                    ((grid.Children[j] as Grid).Children[1] as TextBox).Background = null;
                    ((grid.Children[j] as Grid).Children[2] as TextBox).Text = rects[i].Max(j).ToString();
                    ((grid.Children[j] as Grid).Children[2] as TextBox).Background = null;
                }
            }
            #endregion

            if (0 <= geometry_index && geometry_index < rects.Length)
            {
                Grid grid = grid_rects.Children[geometry_index] as Grid;

                for (int j = 0; j < R.Dim; j++)
                {
                    ((grid.Children[j] as Grid).Children[0] as TextBox).Background = Brushes.Yellow;
                    ((grid.Children[j] as Grid).Children[1] as TextBox).Background = Brushes.Yellow;
                    ((grid.Children[j] as Grid).Children[2] as TextBox).Background = Brushes.Yellow;
                }
            }
            #endregion

            Title = "Значение функции цели - " + region.ObjFunc().ToString();
        }


        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            if (dispatcherTimer.IsEnabled)
            {
                dispatcherTimer.Stop();
                (sender as Button).Content = "Запустить расчёт";
            }
            else
            {
                dispatcherTimer.Start();
                (sender as Button).Content = "Остановить расчёт";
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            placing.Mix();
            if (placing.FindBetter())
                Visualize();
        }

        private void MenuItemLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog(this) == true)
            {
                try
                {
                    ReadFromFile(ofd.FileName);
                    filename = ofd.FileName;
                }
                catch
                {
                    MessageBox.Show("Неправильный формат файла!");
                }

                text_box_dim.Text = R.Dim.ToString();

                CreateUserInterface();

                placing = new Placing(rects, region);
                placing.Find();

                Visualize();
            }
        }

        private void MenuItemSave_Click(object sender, RoutedEventArgs e)
        {
            if (filename != null)
                WriteToFile(filename);
            else
                MenuItemSaveAs_Click(null, null);
        }

        private void MenuItemSaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog(this) == true)
            {
                try
                {
                    WriteToFile(sfd.FileName);
                    filename = sfd.FileName;
                }
                catch
                {
                    MessageBox.Show("Ошибка записи в файл!");
                }
            }
        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}