using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace demoCore.Model
{
    public class Photo
    {
        public static BitmapSource OpenLocalImage(string path)
        {
            try
            {
                BitmapImage bi = new();
                bi.BeginInit();
                bi.CacheOption = BitmapCacheOption.OnLoad;
                using (Stream strm = new MemoryStream(File.ReadAllBytes(path)))
                {
                    bi.StreamSource = strm;
                    bi.EndInit();
                    bi.Freeze();
                }
                return bi;
            }
            catch(Exception)
            {
                return null;
            }
        }
    }
}
