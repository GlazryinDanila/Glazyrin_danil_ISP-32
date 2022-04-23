using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace nedvisimost
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DataTable Select(string selectSQL) // функция подключения к базе данных и обработка запросов
        {
            DataTable dataTable = new DataTable("dataBase");                // создаём таблицу в приложении
                                                                            // подключаемся к базе данных
            SqlConnection sqlConnection = new SqlConnection("server=DESKTOP-11AI5DV;Trusted_Connection=Yes;" +
                "DataBase=Nedvisimost;");
            sqlConnection.Open();                                           // открываем базу данных
            SqlCommand sqlCommand = sqlConnection.CreateCommand();          // создаём команду
            sqlCommand.CommandText = selectSQL;                             // присваиваем команде текст
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand); // создаём обработчик
            sqlDataAdapter.Fill(dataTable);                                 // возращаем таблицу с результатом
            return dataTable;
        }
        public MainWindow()
        {
            InitializeComponent();

            CaptchaGenerator();
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            String allowchar = "";
            allowchar = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            allowchar += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,y,z";
            allowchar += "1,2,3,4,5,6,7,8,9,0";
            char[] a = { ',' };
            String[] ar = allowchar.Split(a);
            String pwd = "";
            string temp = "";

            Random r = new Random();
            for (int i = 0; i < 4; i++)
            {
                temp = ar[(r.Next(0, ar.Length))];
                pwd += temp;
            }

            if (captchaGenTextBox.Text != "")
            {
                captchaGenTextBox.Text = null;
            }

            captchaGenTextBox.Text = pwd;
        }
        void CaptchaGenerator()
        {
            char[] chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789".ToCharArray();
            string randomString = "";
            Random ran = new Random();
            for (int i = 0; i < 5; i++)
            {
                randomString += chars[ran.Next(0, chars.Length)];
            }
            captchaGenTextBox.Text = randomString;
        }
        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = "admin";
            string pass = "admin";


            if (loginTextBox.Text == login)
            {
                if (passwordBox.Password == pass)
                {
                    if (captchaTextBox.Text == captchaGenTextBox.Text)
                    {
                       Window2 win2 = new Window2();
                        win2.Owner = this;
                        //    gwin.userLabel.Content = loginTextBox.Text;
                        win2.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Каптча неверная");
                    }
                }
                else
                {
                    MessageBox.Show("Пароль неверный");
                    CaptchaGenerator();
                }
            }
            else
            {
                MessageBox.Show("Логин неверный");
                CaptchaGenerator();
            }


            if (captchaGenTextBox.Text != "" && captchaGenTextBox.Text != captchaTextBox.Text)
            {               
                
            }
            captchaTextBox.Text = null;

        }

        private void showPassword_Checked(object sender, RoutedEventArgs e)
        {
            if (showPassword.IsChecked == true)
            {
                passwordTextBox.Text = passwordBox.Password;
                passwordTextBox.Visibility = Visibility.Visible;
                passwordBox.Visibility = Visibility.Hidden;
            }
            else
            {
                passwordBox.Password = passwordTextBox.Text;
                passwordTextBox.Visibility = Visibility.Hidden;
                passwordBox.Visibility = Visibility.Visible;
            }
        }
    }
}
