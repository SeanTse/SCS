using demoCore.Core;
using System.Windows.Media.Imaging;
using System;
using System.Windows.Input;
using System.IO;
using System.Windows.Controls;

namespace demoCore.ViewModel
{
    public class ImageViewer : ObservableObjects
    {
        private BitmapSource image;
        private double scale;
        private string name;
        private string mouse_position;

        #region Constructor
        public ImageViewer()
        {
            CmdOpenImage = new RelayCommand<Control>(new Action<Control>(OpenImage));
        }
        #endregion

        #region Properties
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
        public double MinimumScale { get; set; }

        public string MousePosition
        {
            get => mouse_position;
            set
            {
                mouse_position = value;
                OnPropertyChanged("MousePosition");
            }
        }

        public ICommand CmdOpenImage { get; set; }
        #endregion

        #region Command methods

        public void OpenImage(Control ctl)
        {
            Microsoft.Win32.OpenFileDialog openFileDlg = new();
            openFileDlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDlg.Filter = "image|*.png;*.jpg;*.tiff|All|*.*";
            if ((bool)openFileDlg.ShowDialog())
            {
                ImageName = openFileDlg.FileName;
                Image = OpenLocalImage(openFileDlg.FileName);
                MinimumScale = Scale = FittingScale(ctl.ActualWidth, ctl.ActualHeight);
            }
        }
        #endregion

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



        private static BitmapSource OpenLocalImage(string path)
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
            catch (Exception)
            {
                return null;
            }
        }
    }
}
