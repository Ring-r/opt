using System.Drawing;
using System.Windows.Forms;

namespace PolygonPlacingTest
{
    public partial class FormSettings : Form
    {
        public class SettingsClass
        {
            protected PointF offset;
            public PointF Offset
            {
                get
                {
                    return offset;
                }
            }

            protected Color color_background;
            public Color ColorBackground
            {
                get
                {
                    return color_background;
                }
            }

            protected Brush brush_strip;
            public Brush BrushStrip
            {
                get
                {
                    return brush_strip;
                }
            }

            protected Pen pen_strip;
            public Pen PenStrip
            {
                get
                {
                    return pen_strip;
                }
            }

            protected Brush brush_strip_used;
            public Brush BrushStripUsed
            {
                get
                {
                    return brush_strip_used;
                }
            }

            protected Pen pen_strip_used;
            public Pen PenStripUsed
            {
                get
                {
                    return pen_strip_used;
                }
            }

            protected float float_cell;
            public float FloatCell
            {
                get
                {
                    return float_cell;
                }
            }

            protected Brush brush_polygon;
            public Brush BrushPolygon
            {
                get
                {
                    return brush_polygon;
                }
            }

            protected Pen pen_polygon;
            public Pen PenPolygon
            {
                get
                {
                    return pen_polygon;
                }
            }

            protected Brush brush_polygon_current;
            public Brush BrushPolygonCurrent
            {
                get
                {
                    return brush_polygon_current;
                }
            }

            protected Pen pen_polygon_current;
            public Pen PenPolygonCurrent
            {
                get
                {
                    return pen_polygon_current;
                }
            }

            protected Brush brush_point;
            public Brush BrushPoint
            {
                get
                {
                    return brush_point;
                }
            }

            protected float float_point;
            public float FloatPoint
            {
                get
                {
                    return float_point;
                }
            }


            public SettingsClass()
            {
                offset = new PointF();
                color_background = Color.White;
                brush_strip = Brushes.Silver;
                pen_strip = Pens.Black;
                brush_strip_used = Brushes.White;
                pen_strip_used = Pens.Yellow;

                float_cell = 10;

                brush_polygon = Brushes.PaleVioletRed;
                pen_polygon = Pens.Black;

                brush_polygon_current = Brushes.Red;
                pen_polygon_current = Pens.Black;

                brush_point = Brushes.Black;

                float_point = 3;
            }
        }
        protected SettingsClass settings = new SettingsClass();
        public SettingsClass Settings
        {
            get
            {
                return settings;
            }
        }

        public FormSettings()
        {
            InitializeComponent();
        }
    }
}
