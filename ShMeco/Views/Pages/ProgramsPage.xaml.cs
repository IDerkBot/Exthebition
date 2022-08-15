using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;
using ArionLibrary.Controllers;
using ArionLibrary.User;
using Exhibition.Models;
using Exhibition.Views.Windows;

namespace Exhibition.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProgramsPage.xaml
    /// </summary>
    public partial class ProgramsPage : Page
    {
        // Для доступа к индексу из потока
        private int _idx;
        private bool _stop;
        private Thread _run;
        private bool isAuth;

        public ProgramsPage()
        {
            InitializeComponent();
            ManagerProgram.ReadPrograms();
            CbPrograms.ItemsSource = ManagerProgram.Programs;
        }

        #region Buttons

        /// <summary>
        /// Открытие списка программ
        /// </summary>
        private void BtnPrograms_Click(object sender, RoutedEventArgs e) {
            if(CheckAccessUser())
                new ProgramsWindow().ShowDialog();
        }
        private void BtnRunCycle_Click(object sender, RoutedEventArgs e) => StartCycle();
        private void BtnStopCycle_Click(object sender, RoutedEventArgs e) => StopCycle();

        private void BtnAuth_OnClick(object sender, RoutedEventArgs e)
        {
            if (isAuth == false)
            {
                if (new AuthorizationWindow().Show() == UsersManager.USER_LOGIN_RESULT.SUCCESS)
                {
                    BtnAuth.Content = "Выйти";
                    isAuth = true;
                    TbWelcome.Text = $"Добро пожаловать, {UsersManager.CurrentUser.Fullname}";
                }
                return;
            }
            UsersManager.Logout();
            BtnAuth.Content = "Авторизация";
            isAuth = false;
            TbWelcome.Text = $"Доступ к некоторым функциям запрещен";
        }

        private void BtnUsers_OnClick(object sender, RoutedEventArgs e)
        {
            if (CheckAccessUser())
                new UsersWindow().ShowDialog();
        }

        #endregion

        #region Commands

        /// <summary>
        /// Роботизированный цикл
        /// </summary>
        private void StartCycle()
        {
            BtnCycleStart.IsEnabled = _stop = false;

            _idx = CbPrograms.SelectedIndex;
            if (_idx >= ManagerProgram.Programs.Count) return;

            _run = new Thread(Run);
            _run.SetApartmentState(ApartmentState.STA);
            _run.Name = "Acquire video 2";
            _run.Start();
        }

        /// <summary>
        /// Полная остановка цикла
        /// </summary>
        private void StopCycle()
        {
            BtnCycleStart.IsEnabled = _stop = true;
        }

        /// <summary>
        /// Функция роботизированного цикла, запускаемое в отдельном потоке
        /// </summary>
        private void Run()
        {
            for (int i = 0; i < ManagerProgram.Programs[_idx].Steps.Count && !_stop; ++i)
            {
                //theApp.modbus.getToPosition = false;
                //XRayControllerRs232.SendKv(ManagerProgram.Programs[_idx].Steps[i].KV);
                //XRayControllerRs232.SendMa(ManagerProgram.Programs[_idx].Steps[i].MA);
                //XRayControllerRs232.SendTime(ManagerProgram.Programs[_idx].Steps[i].Timing);
                //theApp.modbus.Run(progs[idx].steps[i].Y, progs[idx].steps[i].U, progs[idx].steps[i].Y);

                int j = 0;
                //while (!theApp.modbus.getToPosition && ++j < 30)
                //    Thread.Sleep(1000);
            }
            BtnCycleStart.Dispatcher.BeginInvoke(new Action(() => BtnCycleStart.IsEnabled = true));
        }

        #endregion

        private bool CheckAccessUser()
        {
            if (UsersManager.CurrentUser.Level < 2)
            {
                MessageBox.Show("У вас нет доступа!");
                return false;
            }
            return true;
        }

        private void BtnSettings_OnClick(object sender, RoutedEventArgs e)
        {
            new SettingsWindow().ShowDialog();
        }
    }
}
