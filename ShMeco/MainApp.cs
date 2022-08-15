using System;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using Exhibition.Views.Windows;
// Нужна ли нам информация по сети?

namespace Exhibition
{
    public partial class App : Application
    {

        // Для работы с детектором
        public bool ExitApp = false;

        //public MModbusTCP modbus = null;

        private void App_Startup(object sender, StartupEventArgs e)
        {
            //IPAddress tmp_ip;

            if (MainInit() == false)
            {
                Application.Current?.Shutdown();

                return;
            }

            // Главное окно
            MainWindow ms = new MainWindow(this);

            bool checkConnection = GetIpConfiguration();

            Current.MainWindow = ms;
            ms.Show();

            // Окно манипулятора
            Thread modbusRT = new Thread(() =>
                {
                    try
                    {
                        //modbus = new MModbusTCP(this);
                        //modbus.Show();
                        //modbus.StartUpdate();
                        //modbus.Closed += (sender2, e2) => modbus.Dispatcher.InvokeShutdown();


                        System.Windows.Threading.Dispatcher.Run();
                    }
                    catch (InvalidOperationException ex2)
                    {
                        MessageBox.Show(ex2.Message);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                });

            modbusRT.Name = "Modbus in real time";
            modbusRT.IsBackground = true;
            modbusRT.SetApartmentState(ApartmentState.STA);
            modbusRT.Start();

            

            Dispatcher.Run();
            ExitApp = true;
            
            //modbus.Dispatcher.BeginInvoke(new Action(() => modbus.Close()));

            Thread.Sleep(500);
            Application.Current?.Shutdown();
        }

        private bool MainInit()
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;

            bool b = GetIpConfiguration();

            return true;
        }

        /// <summary>
        /// Legge la configurazione di rete e ritorna se una rete è disponibile (non se è corretta)
        /// </summary>
        /// <returns></returns>
        public bool GetIpConfiguration()
        {
            bool b = false;
            try
            {
                b = NetworkInterface.GetIsNetworkAvailable();
            }
            catch (SystemException ex) { MessageBox.Show(ex.Message); }
            return b;
        }
    }
}
