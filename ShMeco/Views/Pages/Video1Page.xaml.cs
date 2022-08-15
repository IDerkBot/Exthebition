using Exhibition.Models;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Accord.Video.FFMPEG;

namespace Exhibition.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для Video1Page.xaml
    /// </summary>
    public partial class Video1Page : Page
    {
        public Video1Page()
        {
            InitializeComponent();
            Threads.VideoRider1 = new VideoFileReader();
            //Threads.VideoReader.Open("rtsp://admin:admin@192.168.88.20:554");
            Threads.VideoRider1.Open("rtsp://admin:sura450t@192.168.10.22:554/ISAPI/Streaming/Channels/101");

            Threads.VideoThread1 = new Thread(() =>
            {
                while (Threads.VideoRider1.IsOpen)
                {
                    try
                    {
                        Bitmap Frame = Threads.VideoRider1.ReadVideoFrame();
                        Img.Dispatcher.BeginInvoke(new Action(() => Img.Source = ImageSourceFromBitmap(Frame)));
                    }
                    catch
                    {
                        // ignored
                    }
                }
            });

            Threads.VideoThread1.Start();
        }

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);

        public ImageSource ImageSourceFromBitmap(Bitmap bmp)
        {
            try
            {
                var handle = bmp.GetHbitmap();
                try
                {
                    return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                }
                finally { DeleteObject(handle); }
            }
            catch (NullReferenceException)
            {
                var handle = new Bitmap("").GetHbitmap();
                try
                {
                    return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                }
                finally { DeleteObject(handle); }
            }
        }
    }
}
