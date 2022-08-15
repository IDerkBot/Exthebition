using System;
using System.Collections.Generic;
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
using ArionLibrary.User;

namespace Exhibition.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        private UsersManager.USER_LOGIN_RESULT _result = UsersManager.USER_LOGIN_RESULT.ABORT;
        public AuthorizationWindow()
        {
            InitializeComponent();

            UsersManager.Load();
            CbUsers.ItemsSource = UsersManager.GetUserList();
        }

        public new UsersManager.USER_LOGIN_RESULT Show()
        {
            ShowDialog();
            return _result;
        }

        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var login = (CbUsers.SelectedItem as User)?.Login;
            if (UsersManager.Login(login, PbPassword.Password))
            {
                _result = UsersManager.USER_LOGIN_RESULT.SUCCESS;
                Close();
            }
        }
    }
}
