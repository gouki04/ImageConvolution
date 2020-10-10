using System;

namespace ImageConvolution
{
    public static class EdgeHandlers
    {
        /// <summary>
        /// The nearest border pixels are conceptually extended as far as necessary to 
        /// provide values for the convolution. Corner pixels are extended in 90° wedges. 
        /// Other edge pixels are extended in lines.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public static void Extend(ref int x, ref int y, int width, int height)
        {
            x = MathExtension.Clamp(x, 0, width - 1);
            y = MathExtension.Clamp(y, 0, height - 1);
        }

        /// <summary>
        /// The image is conceptually wrapped (or tiled) and values are taken from the 
        /// opposite edge or corner.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public static void Wrap(ref int x, ref int y, int width, int height)
        {
            x = MathExtension.Fmod(x, width - 1);
            y = MathExtension.Fmod(y, height - 1);
        }

        /// <summary>
        /// The image is conceptually mirrored at the edges. For example, 
        /// attempting to read a pixel 3 units outside an edge reads one 3 units 
        /// inside the edge instead.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public static void Mirror(ref int x, ref int y, int width, int height)
        {
            if (x < 0) {
                x = Math.Abs(x);
            }
            else if (x > width - 1) {
                x = (width - 1) - x + (width - 1);
            }

            if (y < 0) {
                y = Math.Abs(y);
            }
            else if (y > height - 1) {
                y = (height - 1) - y + (height - 1);
            }
        }
    }
}
