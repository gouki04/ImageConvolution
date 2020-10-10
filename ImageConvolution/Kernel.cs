using System;
using System.Collections.Generic;

namespace ImageConvolution
{
    /// <summary>
    /// 卷积核
    /// @see https://en.wikipedia.org/wiki/Kernel_(image_processing)
    /// </summary>
    public class Kernel
    {
        public string Name;
        private float[,] m_Matrix;
        private int a;
        private int b;
        private bool m_IsNormalized = false;
        private float m_NormalizeFactor = 1f;

        private float ValueAt(int dx, int dy)
        {
            return m_Matrix[dx + a, dy + b];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">名字</param>
        /// <param name="mat">卷积矩阵</param>
        /// <param name="need_normalization">是否需要自动归一化</param>
        public Kernel(string name, float[,] mat, bool need_normalization)
        {
            Name = name;
            m_Matrix = mat;
            m_IsNormalized = need_normalization;

            var width = m_Matrix.GetLength(0);
            var height = m_Matrix.GetLength(1);

            a = (int)Math.Floor(width / 2.0f);
            b = (int)Math.Floor(height / 2.0f);

            if (m_IsNormalized) {
                var sum = 0f;
                foreach (var v in mat) {
                    sum += v;
                }

                // 防止除0错误
                if (sum == 0f) {
                    m_NormalizeFactor = 1f;
                }
                else {
                    m_NormalizeFactor = 1f / sum;
                }
            }
        }

        /// <summary>
        /// 对Image的坐标为(x,y)的像素进行卷积操作
        /// </summary>
        /// <param name="image"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Pixel Convolution(Image image, int x, int y)
        {
            var sum = new Pixel();
            for (var dx = -a; dx <= a; ++dx) {
                for (var dy = -b; dy <= b; ++dy) {
                    sum += image.GetPixel(x + dx, y + dy) * ValueAt(dx, dy);
                }
            }

            if (m_IsNormalized) {
                sum *= m_NormalizeFactor;
            }

            return sum;
        }

        public static Kernel EdgeDetection1 = new Kernel("EdgeDetection1", new float[3, 3] {
                {  1f, 0f, -1f },
                {  0f, 0f,  0f },
                { -1f, 0f,  1f },
            }, false);

        public static Kernel EdgeDetection2 = new Kernel("EdgeDetection2", new float[3, 3] {
                {  0f, -1f,  0f },
                { -1f,  4f, -1f },
                {  0f, -1f,  0f },
            }, false);

        public static Kernel EdgeDetection3 = new Kernel("EdgeDetection3", new float[3, 3] {
                { -1f, -1f, -1f },
                { -1f,  8f, -1f },
                { -1f, -1f, -1f },
            }, false);

        public static Kernel Sharpen = new Kernel("Sharpen", new float[3, 3] {
                {  0f, -1f,  0f },
                { -1f,  5f, -1f },
                {  0f, -1f,  0f },
            }, false);

        public static Kernel TopSoble = new Kernel("TopSoble", new float[3, 3] {
                {  1f,  2f,  1f },
                {  0f,  0f,  0f },
                { -1f, -2f, -1f },
            }, false);

        public static Kernel BottomSoble = new Kernel("BottomSoble", new float[3, 3] {
                { -1f, -2f, -1f },
                {  0f,  0f,  0f },
                {  1f,  2f,  1f },
            }, false);

        public static Kernel LeftSoble = new Kernel("LeftSoble", new float[3, 3] {
                { 1f, 0f, -1f },
                { 2f, 0f, -2f },
                { 1f, 0f, -1f },
            }, false);

        public static Kernel RightSoble = new Kernel("RightSoble", new float[3, 3] {
                { -1f, 0f, 1f },
                { -2f, 0f, 2f },
                { -1f, 0f, 1f },
            }, false);

        public static Kernel Emboss = new Kernel("Emboss", new float[3, 3] {
                { -2f, -1f, 0f },
                { -1f,  1f, 1f },
                {  0f,  1f, 2f },
            }, false);

        public static Kernel LaplacianGaussian5x5 = new Kernel("LaplacianGaussian5x5", new float[5, 5] {
                {  0f,  0f, -1f,  0f,  0f },
                {  0f, -1f, -2f, -1f,  0f },
                { -1f, -2f, 16f, -2f, -1f },
                {  0f, -1f, -2f, -1f,  0f },
                {  0f,  0f, -1f,  0f,  0f },
            }, false);

        public static Kernel BoxBlur = new Kernel("BoxBlur", new float[3, 3] {
                { 1f, 1f, 1f },
                { 1f, 1f, 1f },
                { 1f, 1f, 1f },
            }, true);

        public static Kernel GaussianBlur3x3 = new Kernel("GaussianBlur3x3", new float[3, 3] {
                { 1f, 2f, 1f },
                { 2f, 4f, 2f },
                { 1f, 2f, 1f },
            }, true);

        public static Kernel GaussianBlur5x5 = new Kernel("GaussianBlur5x5", new float[5, 5] {
                { 1f,  4f,  6f,  4f, 1f },
                { 4f, 16f, 24f, 16f, 4f },
                { 6f, 24f, 36f, 24f, 6f },
                { 4f, 16f, 24f, 16f, 4f },
                { 1f,  4f,  6f,  4f, 1f },
            }, true);

        public static Kernel UnsharpMasking5x5 = new Kernel("UnsharpMasking5x5", new float[5, 5] {
                { 1f,  4f,    6f,  4f, 1f },
                { 4f, 16f,   24f, 16f, 4f },
                { 6f, 24f, -476f, 24f, 6f },
                { 4f, 16f,   24f, 16f, 4f },
                { 1f,  4f,    6f,  4f, 1f },
            }, true);

        public static List<Kernel> Kernels = new List<Kernel>()
        {
            EdgeDetection1,
            EdgeDetection2,
            EdgeDetection3,
            Sharpen,
            TopSoble,
            BottomSoble,
            LeftSoble,
            RightSoble,
            Emboss,
            LaplacianGaussian5x5,
            BoxBlur,
            GaussianBlur3x3,
            GaussianBlur5x5,
            UnsharpMasking5x5,
        };
    }
}
