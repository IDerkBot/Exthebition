using System;
using System.Windows;
using System.Windows.Controls;
using ArionLibrary.Controllers;

namespace Exhibition.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для XrayPage.xaml
    /// </summary>
    public partial class XrayPage : Page
    {
        // xRay. Данные взяты из BHT
        private int kV_Minimum = 20;
        private int kV_Maximum = 160;
        private int kV_Resolution = 1;
        private int kV_Limit = 160;

        private double mA_Minimum = 0.2;
        private double mA_MaximumFine = 10;
        private double mA_MaximumBroad = 20;
        private double mA_Resolution = 0.1;
        private double mA_Maximum = 20;

        private double Watt_Max_FineSpot = 640;
        private double Watt_Max_BroadSpot = 1440;
        private double Actual_Watt_Max = 1440;

        private double kv_updown = 1;   // инкремент и декремент
        private double ma_updown = 0.1;
        private int repeat_click_counter = 0;

        public XrayPage()
        {
            InitializeComponent();
            //DisplayKv.Init(10, 100, 1, 1, XRayControllerRs232.TargetKv, "кВ", "0");
            //DisplayMa.Init(10, 100, 1, 1, XRayControllerRs232.TargetMa, "мА", "0.0");
            //DisplayTime.Init(10, 100, 1, 1, XRayControllerRs232.TargetTime);
        }

        private void DecreaseKV(object sender, RoutedEventArgs e)
        {
            Int32 num = Convert.ToInt32(LblKv.Content);
            if (num > kV_Minimum)
                LblKv.Content = ((num - kv_updown).ToString());
        }

        void IncreaseKV(object sender, RoutedEventArgs e)
        {
            Int32 num = Convert.ToInt32(LblKv.Content);
            if (num < kV_Maximum)
                LblKv.Content = ((num + kv_updown).ToString());
        }

        private void DecreaseMA(object sender, RoutedEventArgs e)
        {
            Double num = Convert.ToDouble(lblmA.Content);
            if (num > mA_Minimum)
                lblmA.Content = (((num - ma_updown)).ToString("0.0"));
        }

        void IncreaseMA(object sender, RoutedEventArgs e)
        {
            Double num = Convert.ToDouble(lblmA.Content);
            if (num < mA_Maximum)
                lblmA.Content = ((num + ma_updown).ToString("0.0"));
        }
    }
}
