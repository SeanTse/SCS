using demoCore.Core;
using System.Windows.Media.Imaging;
using demoCore.Model;
using System;


namespace demoCore.ViewModel
{
    public class ImageViewer : ObservableObjects
    {
        private BitmapSource image;
        private double scale;
        private double min_scale;
        private string name;
        private string mouse_position;

        public BitmapSource Image
        {
            get => image;
            set
            {
                if (image == value)
                {
                    return;
                }
                image = value;
                OnPropertyChanged("Image");
            }
        }

        public string ImageName
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("ImageName");
            }
        }

        public double Scale
        {
            get => scale;
            set
            {
                if (Math.Abs(scale - value) < 0.0001)
                {
                    return;
                }
                scale = value;
                OnPropertyChanged("Scale");
            }
        }

        public double FittingScale(double X, double Y)
        {
            if (Image == null)
            {
                return 1;
            }
            double sY = Y / Image.Height;
            double sX = X / Image.Width;
            return Math.Min(Math.Min(sX, sY), 1);
        }

        public double MinimumScale
        {
            get => min_scale;
            set
            {
                min_scale = value;
                OnPropertyChanged("MinimumScale");
            }
        }

        public string MousePosition
        {
            get => mouse_position;
            set
            {
                mouse_position = value;
                OnPropertyChanged("MousePosition");
            }
        }
    }
}
