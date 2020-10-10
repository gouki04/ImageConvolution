using System.Drawing;

namespace ImageConvolution
{
    /// <summary>
    /// 像素值，为了方便卷积操作，实现了基本的加法和乘法运算
    /// 可以和Color互相转换
    /// </summary>
    public struct Pixel
    {
        public float R;
        public float G;
        public float B;
        public float A;

        public Pixel(float r, float g, float b, float a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public Pixel(Color c)
        {
            R = c.R / 255.0f;
            G = c.G / 255.0f;
            B = c.B / 255.0f;
            A = c.A / 255.0f;
        }

        public Color ToColor()
        {
            return Color.FromArgb(
                MathExtension.Clamp((int)(A * 255.0f), 0, 255),
                MathExtension.Clamp((int)(R * 255.0f), 0, 255),
                MathExtension.Clamp((int)(G * 255.0f), 0, 255),
                MathExtension.Clamp((int)(B * 255.0f), 0, 255));
        }

        public static Pixel operator +(Pixel pa, Pixel pb)
        {
            return new Pixel(pa.R + pb.R, pa.G + pb.G, pa.B + pb.B, pa.A + pb.A);
        }

        public static Pixel operator *(Pixel p, float mul)
        {
            return new Pixel(p.R * mul, p.G * mul, p.B * mul, p.A * mul);
        }

        public static Pixel operator /(Pixel p, float mul)
        {
            return new Pixel(p.R / mul, p.G / mul, p.B / mul, p.A / mul);
        }
    }
}
