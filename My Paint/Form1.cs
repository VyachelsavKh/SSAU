using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My_Paint
{
    public partial class Form1 : Form
    {
        private enum PaintMode { Pen, Line, Circle, Rectangle, Fill_common, Fill_modify, Bezie };
        private enum MouseState { MouseDown, MouseUp };

        private PaintMode _paintMode;
        private MouseState _mouseState;

        private Color _backGroundColor;

        private Color _paintColor;
        private int _thickness;
        private int _colorError;

        private Bitmap _bitmap;
        private Graphics _graphics;
        private Pen _pen;

        private List<Point> _points;

        public Form1()
        {
            InitializeComponent();

            setPictureSize();

            setColor(Color.Black);
            setThickness(1);
            setCaps();

            _backGroundColor = Color.White;
            ClearImage();

            _points = new List<Point>(2);

            _paintMode = PaintMode.Pen;
            _mouseState = MouseState.MouseUp;

            _colorError = 10;
        }

        private void setColor(Color color)
        {
            _paintColor = color;
            Mixed_color_button.BackColor = color;
            RColor_value.Text = color.R.ToString();
            GColor_value.Text = color.G.ToString();
            BColor_value.Text = color.B.ToString();

            if (_pen == null)
            {
                _pen = new Pen(color);
            }
            else
            {
                _pen.Color = color;
            }
        }

        private void setThickness(int thickness)
        {
            _thickness = thickness;
            _pen.Width = _thickness;
        }

        private void setCaps()
        {
            if (_pen == null)
            {
                setColor(Color.Black);
            }

            _pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            _pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
        }

        private void setPictureSize()
        {
            _bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            _graphics = Graphics.FromImage(_bitmap);
        }

        private void ClearImage()
        {
            _graphics.Clear(_backGroundColor);

            pictureBox.Image = _bitmap;
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            _mouseState = MouseState.MouseDown;

            if (_paintMode == PaintMode.Pen)
            {
                _points.Clear();
            }
            if (_paintMode == PaintMode.Line)
            {
                _points.Clear();
                _points.Add(new Point(e.X, e.Y));
            }
            if (_paintMode == PaintMode.Rectangle)
            {
                _points.Clear();
                _points.Add(new Point(e.X, e.Y));
            }
            if (_paintMode == PaintMode.Circle)
            {
                _points.Clear();
                _points.Add(new Point(e.X, e.Y));
            }
            if (_paintMode == PaintMode.Fill_common)
            {
                _points.Clear();
                _points.Add(new Point(e.X, e.Y));
                FillCommon();
            }
            if (_paintMode == PaintMode.Fill_modify)
            {
                _points.Clear();
                _points.Add(new Point(e.X, e.Y));
                FillModify();
            }
            if (_paintMode == PaintMode.Bezie)
            {
                _points.Add(new Point(e.X, e.Y));
            }
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (_paintMode == PaintMode.Pen && _mouseState == MouseState.MouseDown)
            {
                _points.Add(new Point(e.X, e.Y));

                if (_points.Count >= 2)
                {
                    _graphics.DrawLines(_pen, _points.ToArray());
                    pictureBox.Image = _bitmap;
                    _points.Add(new Point(e.X, e.Y));
                }
            }
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            _mouseState = MouseState.MouseUp;

            if (_paintMode == PaintMode.Pen)
            {
                _points.Clear();
            }
            if (_paintMode == PaintMode.Line)
            {
                DrawLine(_points[0], new Point(e.X, e.Y));
            }
            if (_paintMode == PaintMode.Rectangle)
            {
                DrawRectangle(_points[0], new Point(e.X, e.Y));
            }
            if (_paintMode == PaintMode.Circle)
            {
                DrawCircle(_points[0], new Point(e.X, e.Y));
            }
        }

        private void Thickness_bar_Scroll(object sender, EventArgs e)
        {
            TrackBar thicknessBar = (TrackBar)sender;

            setThickness(thicknessBar.Value);
        }

        private void Color_button_Click(object sender, EventArgs e)
        {
            Button colorButton = (Button)sender;

            setColor(colorButton.BackColor);
        }

        private void Mixed_color_button_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                setColor(colorDialog.Color);
            }
        }

        private void RColor_value_TextChanged(object sender, EventArgs e)
        {
            TextBox RColor = (TextBox)sender;

            try
            {
                int col = byte.Parse(RColor.Text);
                setColor(Color.FromArgb(255, col, _paintColor.G, _paintColor.B));
            }
            catch
            {
                RColor.Text = "0";
            }
        }

        private void GColor_value_TextChanged(object sender, EventArgs e)
        {
            TextBox GColor = (TextBox)sender;

            try
            {
                int col = byte.Parse(GColor.Text);
                setColor(Color.FromArgb(255, _paintColor.R, col, _paintColor.B));
            }
            catch
            {
                GColor.Text = "0";
            }
        }

        private void BColor_value_TextChanged(object sender, EventArgs e)
        {
            TextBox BColor = (TextBox)sender;

            try
            {
                int col = byte.Parse(BColor.Text);
                setColor(Color.FromArgb(255, _paintColor.R, _paintColor.G, col));
            }
            catch
            {
                BColor.Text = "0";
            }
        }

        private void Clear_button_Click(object sender, EventArgs e)
        {
            ClearImage();
        }

        private void Pen_button_Click(object sender, EventArgs e)
        {
            _paintMode = PaintMode.Pen;
        }

        private void Line_button_Click(object sender, EventArgs e)
        {
            _paintMode = PaintMode.Line;
        }

        private void Circle_button_Click(object sender, EventArgs e)
        {
            _paintMode = PaintMode.Circle;
        }

        private void Rectangle_button_Click(object sender, EventArgs e)
        {
            _paintMode = PaintMode.Rectangle;
        }

        private void Fill_button_Click(object sender, EventArgs e)
        {
            _paintMode = PaintMode.Fill_common;
        }

        private void FillModify_button_Click(object sender, EventArgs e)
        {
            _paintMode = PaintMode.Fill_modify;
        }

        private void Bezie_button_Click(object sender, EventArgs e)
        {
            _paintMode = PaintMode.Bezie;

            if (_points.Count > 1)
            {
                DrawBezie();

                _points.Clear();
            }
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            SaveImage();
        }

        private void DrawLine(Point p1, Point p2)
        {
            if (Math.Abs(p1.X - p2.X) > Math.Abs(p1.Y - p2.Y)) //Двигаемся по X
            {
                if (p1.X > p2.X)
                {
                    (p1, p2) = (p2, p1);
                }

                double a = (double)(p2.Y - p1.Y) / (p2.X - p1.X);
                double b = (double)(p2.X * p1.Y - p1.X * p2.Y) / (p2.X - p1.X);

                for (int x = p1.X; x <= p2.X; x++)
                {
                    SetPixel(new Point(x, (int)(a * x + b)));
                }
            }
            else //Двигаемся по Y
            {
                if (p1.Y > p2.Y)
                {
                    (p1, p2) = (p2, p1);
                }

                double a = (double)(p2.X - p1.X) / (p2.Y - p1.Y);
                double b = (double)(p1.X * p2.Y - p2.X * p1.Y) / (p2.Y - p1.Y);

                for (int y = p1.Y; y <= p2.Y; y++)
                {
                    SetPixel(new Point((int)(y*a + b), y));
                }
            }
        }

        private void DrawRectangle(Point p1, Point p2)
        { 
            Point p3 = new Point(p1.X, p2.Y);
            Point p4 = new Point(p2.X, p1.Y);

            DrawLine(p1, p3);
            DrawLine(p3, p2);
            DrawLine(p2, p4);
            DrawLine(p1, p4);
        }

        private void DrawParametr(Point center, double R1, double R2, double f1, double f2)
        {
            const int stepsCount = 360;
            double fi;

            Point p1, p2;

            p1 = new Point(center.X + (int)R1, center.Y);

            for (int i = 0; i <= stepsCount; i++)
            {
                fi = 2 * Math.PI / stepsCount * i;

                p2 = new Point(
                    center.X + (int)(R1 * Math.Cos(f1 * fi)), 
                    center.Y + (int)(R2 * Math.Sin(f2 * fi)));

                DrawLine(p1, p2);

                p1 = p2;
            }
        }

        private void DrawCircle(Point p1, Point p2)
        {
            double R = Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));

            DrawParametr(p1, R, R, 1, 1);
        }

        private void FillCommon()
        {
            Color inner_col = GetPixelColor(_points.Last());

            while (_points.Count != 0 && _points.Count <= 10000)
            {
                Point cur = _points.Last();
                Color col;
                _points.Remove(cur);

                col = GetPixelColor(cur);
                if (cmpColors(col, inner_col))
                {
                    SetPixel(cur);
                }

                Point newPoint = new Point(cur.X + 1, cur.Y);
                col = GetPixelColor(newPoint);
                if (cmpColors(col, inner_col))
                {
                    _points.Add(newPoint);
                }

                newPoint = new Point(cur.X - 1, cur.Y);
                col = GetPixelColor(newPoint);
                if (cmpColors(col, inner_col))
                {
                    _points.Add(newPoint);
                }

                newPoint = new Point(cur.X, cur.Y + 1);
                col = GetPixelColor(newPoint);
                if (cmpColors(col, inner_col))
                {
                    _points.Add(newPoint);
                }

                newPoint = new Point(cur.X, cur.Y - 1);
                col = GetPixelColor(newPoint);
                if (cmpColors(col, inner_col))
                {
                    _points.Add(newPoint);
                }
            }
        }

        class pair<T, U>
        {
            public T first;
            public U second;

            public pair(T a, U b)
            {
                first = a;
                second = b;
            }
        }

        private void FillModify()
        {
            Color inner_color = GetPixelColor(_points.Last());

            do
            {
                Point cur_point = _points.Last();
                _points.Remove(cur_point);

                pair <int, int> borders = FillModifyLine(cur_point, inner_color);

                Color col_left;
                Color col_right;

                int cur_y = cur_point.Y + 1;

                col_right = GetPixelColor(new Point(borders.second, cur_y));

                if (cmpColors(col_right, inner_color))
                {
                    _points.Add(new Point(borders.second, cur_y));
                }

                for (int cur_x = borders.second - 1; cur_x > borders.first - 1; cur_x--)
                {
                    col_left = GetPixelColor(new Point(cur_x - 1, cur_y));
                    col_right = GetPixelColor(new Point(cur_x, cur_y));

                    if (cmpColors(col_right, inner_color) && !cmpColors(col_left, inner_color))
                    {
                        _points.Add(new Point(cur_x - 1, cur_y));
                    }
                }

                cur_y = cur_point.Y - 1;

                col_right = GetPixelColor(new Point(borders.second, cur_y));

                if (cmpColors(col_right, inner_color))
                {
                    _points.Add(new Point(borders.second, cur_y));
                }

                for (int cur_x = borders.second - 1; cur_x > borders.first - 1; cur_x--)
                {
                    col_left = GetPixelColor(new Point(cur_x - 1, cur_y));
                    col_right = GetPixelColor(new Point(cur_x, cur_y));

                    if (cmpColors(col_right, inner_color) && !cmpColors(col_left, inner_color))
                    {
                        _points.Add(new Point(cur_x - 1, cur_y));
                    }
                }


            } while (_points.Count != 0);
        }

        private pair<int, int> FillModifyLine(Point p, Color inner_color)
        {
            int x_left, x_right;
            int cur_x = p.X;
            int cur_y = p.Y;

            Point cur_point = new Point(cur_x, cur_y);
            Color cur_color = GetPixelColor(cur_point);

            while(cmpColors(cur_color, inner_color))
            {
                SetPixel(cur_point);

                cur_x--;

                cur_point = new Point(cur_x, cur_y);
                cur_color = GetPixelColor(cur_point);
            }

            x_left = cur_x;

            cur_x = p.X + 1;

            cur_point = new Point(cur_x, cur_y);
            cur_color = GetPixelColor(cur_point);

            while (cmpColors(cur_color, inner_color))
            {
                SetPixel(cur_point);

                cur_x++;

                cur_point = new Point(cur_x, cur_y);
                cur_color = GetPixelColor(cur_point);
            }

            x_right = cur_x;

            return new pair<int, int>(x_left, x_right);
        }

        private void DrawLines(List<Point> points)
        {
            for(int i = 1; i < points.Count; i++)
            {
                DrawLine(points[i], points[i - 1]);
            }
        }

        private void DrawBezie()
        {
            List<Point> bezie_points = new List<Point>(2);
            List<Point> cur_points;
            List<Point> new_points = new List<Point>(2);

            const int max_steps = 100;

            for (int t = 0; t <= max_steps; t++)
            {
                double k = (double)t / max_steps;

                cur_points = _points.GetRange(0, _points.Count);

                while (cur_points.Count != 1)
                {
                    new_points.Clear();

                    for (int i = 1; i < cur_points.Count; i++)
                    {
                        int x1 = cur_points[i - 1].X;
                        int y1 = cur_points[i - 1].Y;
                        int x2 = cur_points[i].X;
                        int y2 = cur_points[i].Y;

                        Point p = new Point((int)(x1 + k * (x2 - x1)), (int)(y1 + k * (y2 - y1)));

                        new_points.Add(p);
                    }

                    cur_points = new_points.GetRange(0, new_points.Count);
                }

                bezie_points.Add(cur_points[0]);
            }

            DrawLines(bezie_points);
        }

        private void PrintDot(Point p)
        {
            
        }

        private void SetPixel(Point p)
        {
            if (p.X >= 0 & p.Y >= 0 & p.X < pictureBox.Width & p.Y < pictureBox.Height)
            {
                _bitmap.SetPixel(p.X, p.Y, _paintColor);
                pictureBox.Image = _bitmap;
            }
        }

        private Color GetPixelColor(Point p)
        {
            if (p.X >= 0 & p.Y >= 0 & p.X < pictureBox.Width & p.Y < pictureBox.Height)
            {
                return _bitmap.GetPixel(p.X, p.Y);
            }

            return Color.Empty;
        }

        private bool cmpColors(Color c1, Color c2)
        {
            int error = Math.Abs(c1.R - c2.R);
            error += Math.Abs(c1.G - c2.G);
            error += Math.Abs(c1.B - c2.B);

            return error <= _colorError;
        }

        private void SaveImage()
        {
            saveFileDialog.Filter = "JPG(*.JPG|)*.jpg";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (pictureBox.Image != null)
                {
                    pictureBox.Image.Save(saveFileDialog.FileName);
                }
            }
        }
    }
}