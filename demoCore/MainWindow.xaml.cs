using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace demoCore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Point LastPosition;
        private bool isPressed;
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
            Btn_maximize.Content = WindowState == WindowState.Maximized ? "\xE923" : "\xE922";

        }
        private void Viewer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            iv.MinimumScale = iv.FittingScale(mViewer.ActualWidth, mViewer.ActualHeight);
            if (iv.Scale < iv.MinimumScale)
            {
                iv.Scale = iv.MinimumScale;
            }
        }

        private void ZoomImage(object sender, MouseWheelEventArgs e)
        {
            if (iv.Image != null)
            {
                Point pointToImage = e.GetPosition(mImage);
                Point pointToViewer = e.GetPosition(mViewer);
                double delta = e.Delta * 0.001;
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
        }

        private void MoveImage(object sender, MouseEventArgs e)
        {
            if (iv.Image != null)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    if (isPressed)
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

        private void mViewer_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(mImage!=null)
            {
                Point doubleClick = e.GetPosition(mImage);
                HitTestResult result = VisualTreeHelper.HitTest(mImage, doubleClick);
                if(result != null)
                {
                    iv.PickPosition = doubleClick;
                }
            }
        }
    }



}