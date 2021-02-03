using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OpenCV
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Color OkunanRenk, DonusenRenk;
            int R = 0, G = 0, B = 0;
            Bitmap GirisResmi, CikisResmi;
            GirisResmi = new Bitmap(pictureBox1.Image);
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            int[,] colorR = new int[ResimGenisligi, ResimYuksekligi];
            int[,] colorG = new int[ResimGenisligi, ResimYuksekligi];
            int[,] colorB = new int[ResimGenisligi, ResimYuksekligi];
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            for (int x = 0; x < ResimGenisligi; x++)
            {
                for (int y = 0; y < ResimYuksekligi; y++)
                {
                    OkunanRenk = GirisResmi.GetPixel(x, y);
                    R = OkunanRenk.R;
                    G = OkunanRenk.G;
                    B = OkunanRenk.B;
                    /*Burada Matris Dökülüyor*/
                    colorR[x, y] = OkunanRenk.R;
                    colorG[x, y] = OkunanRenk.G;
                    colorB[x, y] = OkunanRenk.B;
                    DonusenRenk = Color.FromArgb(R, G, B);
                    CikisResmi.SetPixel(x, y, DonusenRenk);
                }
            }
            pictureBox2.Image = CikisResmi;
            var colorRstring = "";
            var colorGstring = "";
            var colorBstring = "";
            for (int i = 0; i < ResimGenisligi; i++)
            {
                for (int j = 0; j < ResimYuksekligi; j++)
                {
                    colorRstring += colorR[i, j] + ",";
                    colorGstring += colorG[i, j] + ",";
                    colorBstring += colorB[i, j] + ",";
                }
            }
            var colorRZigZagList = zigZagMatrix(colorR, ResimGenisligi, ResimYuksekligi);
            var colorGZigZagList = zigZagMatrix(colorG, ResimGenisligi, ResimYuksekligi);
            var colorBZigZagList = zigZagMatrix(colorB, ResimGenisligi, ResimYuksekligi);
            foreach (var item in colorRZigZagList)
            {
                if (item != "0")
                    colorRListView.Items.Add(item);
            }
            foreach (var item in colorGZigZagList)
            {
                if (item != "0")
                    colorGListView.Items.Add(item);
            }
            foreach (var item in colorBZigZagList)
            {
                if (item != "0")
                    colorBListView.Items.Add(item);
            }
        }

        public static List<string> zigZagMatrix(int[,] arr, int n, int m)
        {
            int row = 0, col = 0;
            //var zigzagMatrisstring = "";
            List<string> zigzagmatris = new List<string>();
            bool row_inc = false;

            int mn = Math.Min(m, n);
            for (int len = 1; len <= mn; ++len)
            {
                for (int i = 0; i < len; ++i)
                {

                    zigzagmatris.Add(arr[row, col].ToString());
                    if (i + 1 == len)
                        break;
                    if (row_inc)
                    {
                        ++row;
                        --col;
                    }
                    else
                    {
                        --row;
                        ++col;
                    }
                }

                if (len == mn)
                    break;
                if (row_inc)
                {
                    ++row;
                    row_inc = false;
                }
                else
                {
                    ++col;
                    row_inc = true;
                }
            }
            if (row == 0)
            {
                if (col == m - 1)
                    ++row;
                else
                    ++col;
                row_inc = true;
            }
            else
            {
                if (row == n - 1)
                    ++col;
                else
                    ++row;
                row_inc = false;
            }
            int MAX = Math.Max(m, n) - 1;
            for (int len, diag = MAX; diag > 0; --diag)
            {

                if (diag > mn)
                    len = mn;
                else
                    len = diag;

                for (int i = 0; i < len; ++i)
                {
                    zigzagmatris.Add(arr[row, col].ToString());
                    if (i + 1 == len)
                        break;
                    if (row_inc)
                    {
                        ++row;
                        --col;
                    }
                    else
                    {
                        ++col;
                        --row;
                    }
                }
                if (row == 0 || col == m - 1)
                {
                    if (col == m - 1)
                        ++row;
                    else
                        ++col;

                    row_inc = true;
                }
                else if (col == 0 || row == n - 1)
                {
                    if (row == n - 1)
                        ++col;
                    else
                        ++row;

                    row_inc = false;
                }
            }
            return zigzagmatris;
        }

    }
}
