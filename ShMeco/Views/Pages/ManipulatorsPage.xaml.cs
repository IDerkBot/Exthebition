using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using ArionControlLibrary;
using ArionLibrary.Controllers;
using ArionLibrary.Config;
using Exhibition.Models;

namespace Exhibition.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для ManipulatorsPage.xaml
    /// </summary>
    public partial class ManipulatorsPage : Page
    {
        #region Variables

        private ushort[] errors1;                   // Ошибка привода
        private ushort[] errors2;                   // Ошибка привода
        private ushort[] errors3;                   // Ошибка привода
        private ushort[] actualPosition1;           //актуальная позиция привода
        private ushort[] actualPosition2;           //актуальная позиция привода
        private ushort[] actualPosition3;           //актуальная позиция привода
        private const int UpdDelay = 300;
        private const ushort Function = 1;
        private int zadPos1;                        // задаваемая позиция до преобразования
        private int zadPos2;                        // задаваемая позиция до преобразования
        private int zadPos3;                        // задаваемая позиция до преобразования
        private ushort positionH1 = 0;              // Позиция для передачи в привод
        private ushort positionL1 = 0;              // Позиция для передачи в привод
        private ushort positionH2 = 0;              // Позиция для передачи в привод
        private ushort positionL2 = 0;              // Позиция для передачи в привод
        private ushort positionH3 = 0;              // Позиция для передачи в привод
        private ushort positionL3 = 0;              // Позиция для передачи в привод
        private const int NomSpeed1 = 300;          // номинальная скорость привода
        private const int NomSpeed2 = 300;          // номинальная скорость привода
        private const int NomSpeed3 = 300;          // номинальная скорость привода

        private ushort speed1;                      // Вычисляемая скорость  для привода
        private ushort speed2;                      // Вычисляемая скорость  для привода
        private ushort speed3;                      // Вычисляемая скорость  для привода
        private int speedRatio = 20;                // default 
        //private int pos1;
        //private int pos2;
        //private int pos3;

        private const string IpAddress = "192.168.88.140";   // default
        private const int TcpPort = 502;                    // default

        private const ushort Acc = 2500;            // Ускорение привода
        private const ushort Dec = 1500;            // Замедление привода
        private const ushort SusTime = 1;
        private const ushort SpecParam = 16;
        //private readonly MotorController motor1 = new MotorController(1, IpAddress, TcpPort);
        private readonly MotorController motor1 = new MotorController(1, IpAddress, TcpPort);
        private readonly MotorController motor2 = new MotorController(2, true);
        private readonly MotorController motor3 = new MotorController(3, true);
        private bool wrMotAll;
        private bool wrMot1, wrMot2, wrMot3;
        private bool compare;

        public static string ConfigFile = AppDomain.CurrentDomain.BaseDirectory + @"Config_eko.ini";
        IniFile ini = new IniFile(ConfigFile);

        TcpClient tcpClient;                                //Create a new TcpClient object.
        //ModbusIpMaster master;                              //

        private DispatcherTimer UpdTimer = new DispatcherTimer();
        private App theApp = null;
        private CConfig Config;

        #endregion

        #region Window

        public ManipulatorsPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            StartUpdate();

            motor1.Homing();
            motor2.Homing();
            motor3.Homing();
        }

        #endregion

        #region Buttons

        private void BtnMove_OnClick(object sender, MouseButtonEventArgs e)
        {
            if (!(sender is Button btn)) return;

            switch (btn.Name)
            {
                case "Yplus": ChangePosition(1, true); break;
                case "Yminus": ChangePosition(1, false); break;

                case "Uplus": ChangePosition(1, true); break;
                case "Uminus": ChangePosition(1, false); break;

                case "Vplus": ChangePosition(1, true); break;
                case "Vminus": ChangePosition(1, false); break;
            }
        }

        private void BtnStop_OnClick(object sender, MouseButtonEventArgs e)
        {
            wrMot1 = false;
            wrMot2 = false;
            wrMot3 = false;

            if (!(sender is Button btn)) return;

            switch (btn.Name)
            {
                case "Yplus": motor1.EStop(); break;
                case "Yminus": motor1.EStop(); break;

                case "Uplus": motor2.EStop(); break;
                case "Uminus": motor2.EStop(); break;

                case "Vplus": motor3.EStop(); break;
                case "Vminus": motor3.EStop(); break;
            }
        }

        private void BtnZero_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button btn)) return;
            switch (btn.Name)
            {
                case "Y0": motor1.Homing(); wrMot1 = true; break;
                case "U0": motor2.Homing(); wrMot2 = true; break;
                case "V0": motor3.Homing(); wrMot3 = true; break;
            }
        }
        private void SpeedPlus_OnClick(object sender, RoutedEventArgs e) => ChangeSpeed(speedRatio + 1);
        private void SpeedMinus_OnClick(object sender, RoutedEventArgs e) => ChangeSpeed(speedRatio - 1);
        private void BtnSpeed1_OnClick(object sender, RoutedEventArgs e) => ChangeSpeed(int.Parse((sender as Button)?.Content.ToString() ?? string.Empty));
        private void BtnSpeed2_OnClick(object sender, RoutedEventArgs e) => ChangeSpeed(int.Parse((sender as Button)?.Content.ToString() ?? string.Empty));
        private void BtnSpeed3_OnClick(object sender, RoutedEventArgs e) => ChangeSpeed(int.Parse((sender as Button)?.Content.ToString() ?? string.Empty));

        #endregion

        /// <summary>
        /// Получение данных
        /// </summary>
        /// <param name="errors">Ошибки</param>
        /// <param name="actualPos">Позиция</param>
        /// <param name="motor">Движок</param>
        /// <param name="multiply">множитель</param>
        /// <param name="delimiter">делитель</param>
        /// <returns>Получаем актуальную позицию</returns>
        private int GetData(ref ushort[] errors, ref ushort[] actualPos, MotorController motor, int multiply, int delimiter)
        {
            errors = motor.GetErrors();
            actualPos = motor.GetActualPosition();
            var result = (actualPos[0] << 16 | actualPos[1]) * multiply / delimiter;
            return result;
        }

        #region Timer

        private void UpdTimer_Tick(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                try
                {
                    int ap1 = GetData(ref errors1, ref actualPosition1, motor1, 110, 400000);
                    int ap2 = GetData(ref errors2, ref actualPosition2, motor2, 360, 2592000);
                    int ap3 = GetData(ref errors3, ref actualPosition3, motor3, 360, 1080000);

                    ActPos1.ChangeContentAsync(ap1);
                    ActPos2.ChangeContentAsync(ap2);
                    ActPos3.ChangeContentAsync(ap3);
                    //this.Dispatcher.BeginInvoke(new Action(() => lbl_Err.Content = Errors_1[0].ToString()));

                    if (compare)
                    {
                        //if (Actualpos_1[0] == PositionL_1) Dispatcher.BeginInvoke(new Action(() => statY.Content = "V")); else Dispatcher.BeginInvoke(new Action(() => statY.Content = "X"));
                        //if (Actualpos_2[0] == PositionL_2) Dispatcher.BeginInvoke(new Action(() => statU.Content = "V")); else Dispatcher.BeginInvoke(new Action(() => statU.Content = "X"));
                        //if (Actualpos_3[0] == PositionL_3) Dispatcher.BeginInvoke(new Action(() => statV.Content = "V")); else Dispatcher.BeginInvoke(new Action(() => statV.Content = "X"));
                    }
                    else compare = false;
                }
                catch
                {
                    //this.Dispatcher.BeginInvoke(new Action(() => lbl_Err.Content = "Нет связи"));
                }

                Config = MainManager.Config;
                BtnSpeed1.ChangeContentAsync(Config.ProgramSettings.Button1Velocity);
                BtnSpeed2.ChangeContentAsync(Config.ProgramSettings.Button2Velocity);
                BtnSpeed3.ChangeContentAsync(Config.ProgramSettings.Button3Velocity);
            }).Start();
        }

        public void StartUpdate()
        {
            UpdTimer.Interval = TimeSpan.FromMilliseconds(UpdDelay);
            UpdTimer.Tick += UpdTimer_Tick;
            UpdTimer.IsEnabled = true;
        }

        #endregion

        private void ChangePosition(int slave, bool isPositive)
        {
            try
            {
                switch (slave)
                {
                    case 1:
                        zadPos1 = isPositive ? 500 : 0;
                        wrMot1 = true;
                        motor1.GoToPosition(zadPos1, 400000 / 110);
                        wrMotAll = false;
                        break;
                    case 2:
                        zadPos2 = isPositive ? 120 : 0;
                        wrMot2 = true;
                        motor2.GoToPosition(zadPos2, 2592000 / 360);
                        break;
                    case 3:
                        zadPos3 = isPositive ? 360 : 0;
                        wrMot3 = true;
                        motor3.GoToPosition(zadPos3, 1080000 / 360);
                        break;
                }

                wrMotAll = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ChangeSpeed(int value)
        {
            speedRatio = value;
            SpeedDesk.ChangeContentAsync($"{speedRatio}%");

            motor1.SetSpeed(speedRatio);
            motor2.SetSpeed(speedRatio);
            motor2.SetSpeed(speedRatio);
        }
    }

    class IniFile
    {
        private readonly string path;

        [DllImport("kernel32")]
        static extern long WritePrivateProfileString(string section, string key, string value, string filePath);

        [DllImport("kernel32")]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);
        public IniFile(string IniPath)
        {
            path = new FileInfo(IniPath).FullName.ToString();
        }
        public string ReadINI(string Section, string Key)
        {
            var RetVal = new StringBuilder(255);
            GetPrivateProfileString(Section, Key, "", RetVal, 255, path);
            return RetVal.ToString();
        }
        public void WriteINI(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, path);
        }
        public void DeleteKey(string Section, string Key)
        {
            WriteINI(Section, Key, null);
        }
        public void DeleteSection(string Section = null)
        {
            WriteINI(Section, null, null);
        }
        public bool KeyExistsINI(string Section, string Key)
        {
            return ReadINI(Section, Key).Length > 0;
        }
    }
}