using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using ArionLibrary.Config;
using Exhibition.Models;
using Exhibition.Views.Pages;

namespace Exhibition.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        private DispatcherTimer safetyCheckTimer;

        private DispatcherTimer uiTimer;

        private App theApp = null;

        private Thread getImage1 = null;
        private Thread getImage2 = null;
        private Thread run = null;

        private List<ProgramInfoDataSource> progs = new List<ProgramInfoDataSource> { };

        private bool mustQuit = false;

        // Для доступа к выделенной программе
        private int curidx;
        //List<Step> steps = new List<Step>();

        public MainWindow(App ref_app)
        {
            theApp = ref_app;
            InitializeComponent();

            //modbus = new MModbusTCP(theApp);

            //modbus.Show();
            //modbus.Owner = this;
        }

        private void mainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MainManager.Config = new CConfig();
            MainManager.Config.Load();

            safetyCheckTimer = new DispatcherTimer();
            safetyCheckTimer.Interval = TimeSpan.FromMilliseconds(200);
            safetyCheckTimer.IsEnabled = true;

            uiTimer = new DispatcherTimer();
            uiTimer.Interval = TimeSpan.FromMilliseconds(500);
            uiTimer.IsEnabled = true;

            FrameManipulator.Navigate(new ManipulatorsPage());
            FrameXray.Navigate(new XrayPage());
            FramePrograms.Navigate(new ProgramsPage());

            Camera1Frame.Navigate(new Video1Page());
            Camera2Frame.Navigate(new Video2Page());
        }

        private void mainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mustQuit = true;
        }

        private void mainWindow_Closed(object sender, EventArgs e)
        {
            Threads.VideoThread1.Abort();
            Threads.VideoThread2.Abort();

            Threads.VideoRider1.Close();
            Threads.VideoRider2.Close();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private bool _isFullscreen1;
        private bool _isFullscreen2;

        private void Camera1Frame_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (_isFullscreen1)
            {
                Grid.SetRowSpan(Camera1Frame, 1);
                Camera2Frame.Visibility = Visibility.Visible;
                _isFullscreen1 = false;
            }
            else
            {
                Grid.SetRowSpan(Camera1Frame, 2);
                Camera2Frame.Visibility = Visibility.Collapsed;
                _isFullscreen1 = true;
            }
        }

        private void Camera2Frame_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(_isFullscreen2)
            {
                Camera1Frame.Visibility = Visibility.Visible;
                Grid.SetRow(Camera2Frame, 1);
                Grid.SetRowSpan(Camera2Frame, 1);
                _isFullscreen2 = false;
            }
            else
            {
                Camera1Frame.Visibility = Visibility.Collapsed;
                Grid.SetRow(Camera2Frame, 0);
                Grid.SetRowSpan(Camera2Frame, 2);
                _isFullscreen2 = true;
            }
        }
    }
}
