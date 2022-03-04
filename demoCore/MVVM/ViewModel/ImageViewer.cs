using demoCore.Core;
using System.Windows.Media.Imaging;
using System;
using System.Windows.Input;
using demoCore.Util;
using System.Windows.Controls;
using System.Windows;

namespace demoCore.MVVM.ViewModel
{
    public class ImageViewer : ObservableObjects
    {
        private BitmapSource current_image;
        private double scale;
        private string name;
        private Point pick_position;

        public ImageViewer()
        {
            CmdExploreImage = new RelayCommand<Control>(new Action<Control>(ExploreImage));
        }


        public BitmapSource Image
        {
            get => current_image;
            set
            {
                if (current_image == value)
                {
                    return;
                }
                current_image = value;
                OnPropertyChanged("Image");
            }
        }

        public string ImageName
        {
            get => name;
            internal set
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
        public double MinimumScale { get; set; }

        public Point PickPosition
        {
            get => pick_position;
            set
            {
                pick_position = value;
                OnPropertyChanged("PickPosition");
            }
        }

        public ICommand CmdExploreImage { get; private set; }

        private void ExploreImage(Control container)
        {
            Microsoft.Win32.OpenFileDialog openFileDlg = new();
            openFileDlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDlg.Filter = "image|*.png;*.jpg;*.tiff|All|*.*";
            if ((bool)openFileDlg.ShowDialog())
            {
                ImageName = openFileDlg.FileName;
                Image = Utilities.OpenLocalImage(openFileDlg.FileName);
                MinimumScale = Scale = FittingScale(container.ActualWidth, container.ActualHeight);
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
    }
}
