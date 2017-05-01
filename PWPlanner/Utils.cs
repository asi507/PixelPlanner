﻿using System.Drawing;
using System.IO;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PWPlanner
{
    public class Utils
    {
        public static System.Drawing.Bitmap GetCroppedBitmap(TileType type, int X, int Y)
        {

            Rectangle cropRect = new Rectangle(X, Y, 32, 32);
            Bitmap src = TileTypeMethods.GetResourceForType(type);

            Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);

            using (Graphics g = Graphics.FromImage(target))
            {
                g.DrawImage(src, new Rectangle(0, 0, target.Width, target.Height),
                                 cropRect,
                                 GraphicsUnit.Pixel);
            }
            return target;
        }

        public static System.Windows.Controls.Image BitmapToImageControl(Bitmap bmp)
        {

            var bi = new BitmapImage();
            using (var ms = new MemoryStream())
            {
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                ms.Position = 0;

                bi.BeginInit();
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.StreamSource = ms;
                bi.EndInit();
            }

            System.Windows.Controls.Image image = new System.Windows.Controls.Image();
            image.Source = bi;
            return image;
        }

        public static bool IsBitmapVoid(System.Drawing.Bitmap bmp)
        {
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color color = bmp.GetPixel(x, y);
                    if (color.R > 0 && color.G > 0 && color.B > 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static int ARGBColortoInt(System.Windows.Media.Color color)
        {
            return (color.A << 24) | (color.R << 16) | (color.G << 8) | color.B;
        }

        public static System.Windows.Media.Color IntToARGBColor(int value)
        {
            return System.Windows.Media.Color.FromArgb((byte)(value >> 24), (byte)(value >> 16), (byte)(value >> 8), (byte)(value));
        }

    }
}