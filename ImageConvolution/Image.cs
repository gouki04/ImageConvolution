using System.Drawing;

namespace ImageConvolution
{
    public class Image
    {
        public Bitmap SrcBitmap;

        public delegate void EdgeHandle(ref int x, ref int y, int width, int height);
        public EdgeHandle EdgeHandler;

        public int Width => SrcBitmap.Width;
        public int Height => SrcBitmap.Height;

        public Image(string file)
        {
            SrcBitmap = new Bitmap(file);
        }

        /// <summary>
        /// 获取指定座标的像素值
        /// x和y支持超出Image的范围
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Pixel GetPixel(int x, int y)
        {
            // 处理边缘溢出像素
            EdgeHandler(ref x, ref y, Width, Height);

            return new Pixel(SrcBitmap.GetPixel(x, y));
        }

        /// <summary>
        /// 使用kernel进行卷积操作
        /// </summary>
        /// <param name="kernel"></param>
        /// <param name="save_file_path"></param>
        public void Process(Kernel kernel, string save_file_path)
        {
            var bitmap = new Bitmap(Width, Height);
            for (var x = 0; x < SrcBitmap.Width; ++x) {
                for (var y = 0; y < SrcBitmap.Height; ++y) {
                    bitmap.SetPixel(x, y, kernel.Convolution(this, x, y).ToColor());
                }
            }

            bitmap.Save(save_file_path);
        }
    }
}
