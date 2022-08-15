using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ArionLibrary.Config;
using ArionLibrary.User;
using Exhibition.Models;

namespace Exhibition.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public CConfig Config;

        public SettingsWindow()
        {
            InitializeComponent();

            Config = MainManager.Config;
            SetData();
        }

        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            GetData();

            MainManager.Config = Config;
            MainManager.Config.Save();
            Close();
        }

        public void SetData()
        {
            //ClSpeed1.Init(0, 100, 1, 1, Config.ProgramSettings.Button1Velocity);
            //ClSpeed2.Init(0, 100, 1, 1, Config.ProgramSettings.Button2Velocity);
            //ClSpeed3.Init(0, 100, 1, 1, Config.ProgramSettings.Button3Velocity);
        }

        private void GetData()
        {
            Config.ProgramSettings.Button1Velocity = (int)ClSpeed1.Value;
            Config.ProgramSettings.Button2Velocity = (int)ClSpeed2.Value;
            Config.ProgramSettings.Button3Velocity = (int)ClSpeed3.Value;
        }
    }
}
