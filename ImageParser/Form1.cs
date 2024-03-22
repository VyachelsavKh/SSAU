using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageParser
{
    public partial class Form1 : Form
    {
        private Bitmap _bitmap1;
        private Bitmap _bitmap2;
        private Bitmap _bitmap3;
        private Bitmap _bitmap4;

        int _F_value;
        double _A_value;
        double _B_value;
        double[][] _M_values;

        int _F2_value;
        int _L_value;
        int _R_value;

        class YIQ
        {
            public int Y, I, Q;

            public YIQ(int y = 0, int i = 0, int q = 0)
            {
                Y = y;
                I = i;
                Q = q;
            }

            public void FromARGB(Color col)
            {
                Y = (int)(0.3 * col.R + 0.59 * col.G + 0.11 * col.B);
                I = (int)(0.6 * col.R - 0.27 * col.G - 0.33 * col.B);
                Q = (int)(0.21 * col.R - 0.52 * col.G + 0.31 * col.B);
            }

            public Color ToARGB()
            {
                int R = (int)(Y + 0.95 * I + 0.62 * Q);
                int G = (int)(Y - 0.27 * I - 0.65 * Q);
                int B = (int)(Y - 1.1 * I + 1.7 * Q);

                R = Math.Max(0, Math.Min(255, R));
                G = Math.Max(0, Math.Min(255, G));
                B = Math.Max(0, Math.Min(255, B));

                return Color.FromArgb(255, R, G, B);
            }
        }

        public Form1()
        {
            InitializeComponent();
            setPictureSizes();

            _F2_value = 1;
            _L_value = _R_value = 0;

            _F_value = 1;
            _A_value = _B_value = 0;
            _M_values = new double[3][];
            _M_values[0] = new double[3];
            _M_values[1] = new double[3];
            _M_values[2] = new double[3];

            for(int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    _M_values[i][j] = 0;
        }

        private void setPictureSizes()
        {
            _bitmap1 = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            _bitmap2 = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            _bitmap3 = new Bitmap(pictureBox3.Width, pictureBox3.Height);
            _bitmap4 = new Bitmap(pictureBox4.Width, pictureBox4.Height);
        }

        private void pictureBox1_DragDrop(object sender, DragEventArgs e)
        {
            var data = e.Data.GetData(DataFormats.FileDrop);
            if (data != null)
            {
                var fileNames = data as string[];

                if (fileNames.Length > 0)
                {
                    pictureBox1.Image = Image.FromFile(fileNames[0]);
                    _bitmap1 = (Bitmap)pictureBox1.Image;
                }
            }
        }

        private void pictureBox1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.AllowDrop = true;
        }

        private void Gray_button_Click(object sender, EventArgs e)
        {
            int midColor;

            for(int i = 0; i < _bitmap1.Width; i++)
            {
                for (int j = 0; j < _bitmap1.Height; j++)
                {
                    midColor = (int)(0.3 * _bitmap1.GetPixel(i, j).R);
                    midColor += (int)(0.59 * _bitmap1.GetPixel(i, j).G);
                    midColor += (int)(0.11 * _bitmap1.GetPixel(i, j).B);

                    if (i < _bitmap2.Width && j < _bitmap2.Height)
                        _bitmap2.SetPixel(i, j, Color.FromArgb(255, midColor, midColor, midColor));
                }
            }

            pictureBox2.Image = _bitmap2;
        }

        private void F_value_TextChanged(object sender, EventArgs e)
        {
            TextBox F = (TextBox)sender;

            if (!int.TryParse(F.Text, out _F_value))
            {
                F.Text = "1";
                _F_value = 1;
            }

            if (_F_value < 1)
            {
                F.Text = "1";
                _F_value = 1;
            }
            if (_F_value > 2)
            {
                F.Text = "2";
                _F_value = 2;
            }
        }

        private void A_value_TextChanged(object sender, EventArgs e)
        {
            TextBox A = (TextBox)sender;

            if (!double.TryParse(A.Text, out _A_value))
            {
                A.Text = "0";
                _A_value = 0;
            }
        }

        private void B_value_TextChanged(object sender, EventArgs e)
        {
            TextBox B = (TextBox)sender;

            if (!double.TryParse(B.Text, out _B_value))
            {
                B.Text = "0";
                _A_value = 0;
            }
        }

        private void M_value_TextChanged(object sender, EventArgs e)
        {
            TextBox M = (TextBox)sender;

            double out_val;

            if (!double.TryParse(M.Text, out out_val))
            {
                M.Text = "0";
                _A_value = 0;
            }

            _M_values[(M.TabIndex - 8) / 3][(M.TabIndex - 8) % 3] = out_val;
        }

        private void Filter_button_Click(object sender, EventArgs e)
        {
            Bitmap bitmap;

            if (_F_value == 1)
                bitmap = _bitmap1;
            else 
                bitmap = _bitmap2;

            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    if (i < _bitmap4.Width && j < _bitmap4.Height)
                        _bitmap4.SetPixel(i, j, Filter(i, j));
                }
            }

            pictureBox4.Image = _bitmap4;
        }

        private Color Filter(int x, int y)
        {
            Bitmap bitmap;
            /*
            if (_F_value == 1)
                bitmap = _bitmap1;
            else*/
                bitmap = _bitmap2;

            Color new_color;

            int new_Y = 0;
            int cur_Y = 0;
            
            for(int i = -1; i <= 1; i++)
                for(int j = -1; j <= 1; j++)
                {
                    if (x + i >= 0 && x + i < bitmap.Width &&
                        y + j >= 0 && y + j < bitmap.Height)
                        new_color = bitmap.GetPixel(x + i, y + j);
                    else
                        new_color = bitmap.GetPixel(x, y);

                    cur_Y = (int)(0.3 * new_color.R + 0.59 * new_color.G + 0.11 * new_color.B);

                    new_Y += (int)(_A_value + _B_value * cur_Y * _M_values[i + 1][j + 1]);
                }

            new_Y = Math.Min(255, Math.Max(0, new_Y));

            return Color.FromArgb(new_Y, new_Y, new_Y);
        }

        private void F2_value_TextChanged(object sender, EventArgs e)
        {
            TextBox F = (TextBox)sender;

            if (!int.TryParse(F.Text, out _F2_value))
            {
                F.Text = "1";
                _F2_value = 1;
            }

            if (_F_value < 1)
            {
                F.Text = "1";
                _F2_value = 1;
            }
            if (_F_value > 2)
            {
                F.Text = "2";
                _F2_value = 2;
            }
        }

        private void L_value_TextChanged(object sender, EventArgs e)
        {
            TextBox L = (TextBox)sender;

            if (!int.TryParse(L.Text, out _L_value))
            {
                L.Text = "0";
                _L_value = 0;
            }

            if (_F_value < 0)
            {
                L.Text = "0";
                _L_value = 0;
            }
            if (_F_value > 255)
            {
                L.Text = "255";
                _L_value = 255;
            }
        }

        private void R_value_TextChanged(object sender, EventArgs e)
        {
            TextBox R = (TextBox)sender;

            if (!int.TryParse(R.Text, out _R_value))
            {
                R.Text = "0";
                _R_value = 0;
            }

            if (_F_value < 0)
            {
                R.Text = "0";
                _R_value = 0;
            }
            if (_F_value > 255)
            {
                R.Text = "255";
                _R_value = 255;
            }
        }

        private void Prepare_button_Click(object sender, EventArgs e)
        {
            Bitmap bitmap;
            /*
            if (_F_value == 1)
                bitmap = _bitmap1;
            else*/
                bitmap = _bitmap2;

            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    if (i < _bitmap3.Width && j < _bitmap3.Height)
                        _bitmap3.SetPixel(i, j, Prepare(i, j));
                }
            }

            pictureBox3.Image = _bitmap3;
        }

        private Color Prepare(int x, int y)
        {
            Bitmap bitmap;

            /*
            if (_F2_value == 1)
                bitmap = _bitmap1;
            else*/
                bitmap = _bitmap2;

            Color cur_color;

            if (x >= 0 && x < bitmap.Width &&
                y >= 0 && y < bitmap.Height)
                cur_color = bitmap.GetPixel(x, y);
            else
                cur_color = Color.Black;

            int new_Y = (int)(0.3 * cur_color.R + 0.59 * cur_color.G + 0.11 * cur_color.B);

            if (new_Y > _L_value && new_Y < _R_value)
            {
                new_Y = 255;
            }

            return Color.FromArgb(new_Y, new_Y, new_Y);
        }
    }
}
