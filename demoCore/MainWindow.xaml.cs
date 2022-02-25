using System;
using System.Windows;
using System.Windows.Input;
using demoCore.Model;

namespace demoCore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Point LastPosition;
        private bool isPressed = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Btn_minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Btn_maximize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        private void Btn_close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            btn_maximize.Content = WindowState == WindowState.Maximized ? "\xE923" : "\xE922";

        }
        private void Viewer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            iv.MinimumScale = iv.FittingScale(mViewer.ActualWidth, mViewer.ActualHeight);
            if (iv.Scale < iv.MinimumScale)
            {
                iv.Scale = iv.MinimumScale;
            }
        }

        private void Menu_Open_Image_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDlg = new();
            openFileDlg.InitialDirectory = "C:\\Users\\Sean\\Desktop";
            openFileDlg.Filter = "image|*.png;*.jpg;*.tiff|All|*.*";
            if ((bool)openFileDlg.ShowDialog())
            {
                iv.ImageName = openFileDlg.FileName;
                iv.Image = Photo.OpenLocalImage(openFileDlg.FileName);
                //初始化缩放比例，使画面全部出现在ScrollViewer里
                iv.MinimumScale = iv.Scale = iv.FittingScale(mViewer.ActualWidth, mViewer.ActualHeight);
            }
        }

        private void Border_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            Point pointToImage = e.GetPosition(mImage);
            Point pointToViewer = e.GetPosition(mViewer);
            double step;
            double scaleLevel = iv.Scale / iv.MinimumScale;
            if (scaleLevel <= 1.5)
            {
                step = 0.001;
            }
            else if (scaleLevel is > 1.5 and <= 2)
            {
                step = 0.002;
            }
            else if (scaleLevel is > 2 and <= 3)
            {
                step = 0.003;
            }
            else
            {
                step = 0.005;
            }

            double delta = e.Delta * step;
            if (iv.Scale + delta < iv.MinimumScale)
            {
                iv.Scale = iv.MinimumScale;
                return;
            }
            iv.Scale += delta;
            mViewer.ScrollToHorizontalOffset(pointToImage.X * iv.Scale - pointToViewer.X);
            mViewer.ScrollToVerticalOffset(pointToImage.Y * iv.Scale - pointToViewer.Y);
            e.Handled = true;
        }

        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            Point now = e.GetPosition(mImage);
            now.X = (int)now.X;
            now.Y = (int)now.Y;
            iv.MousePosition = now.ToString();
        }

        private void Border_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                if(isPressed)
                {
                    Point now = e.GetPosition(mViewer);
                    if (now != LastPosition)
                    {
                        mViewer.ScrollToHorizontalOffset(mViewer.HorizontalOffset + LastPosition.X - now.X);
                        mViewer.ScrollToVerticalOffset(mViewer.VerticalOffset + LastPosition.Y - now.Y);
                        LastPosition = now;
                    }
                }
                else
                {
                    LastPosition = e.GetPosition(mViewer);
                    isPressed = true;
                }
            }
            else
            {
                isPressed = false;
            }

        }
    }
}
