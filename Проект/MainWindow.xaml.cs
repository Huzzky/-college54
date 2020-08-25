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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Проект
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// Бычков Владислав Петрович 1ИСП11-10
    /// или уже 2ИСП11-10
    public partial class MainWindow : Window
    {
        static public int sw_roll = 0;
        public MainWindow()
        {
            InitializeComponent();
        }
        enum Roll
        {
            Zauch = 1,
            Uchitel,
            Admin
        }

        private void login_stud_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (log_tectbox.Text.Length > 0 && password_tectbox.Password.Length > 0)
                {
                    demo_studentEntities bd_stud = new demo_studentEntities();
                    var table_with_logins = bd_stud.logins;
                    /*Administrator administrator = new Administrator();
                    Student stud = new Student();
                    Zavuch zav = new Zavuch();*/
                    var result = table_with_logins.Where(i => i.login_name == log_tectbox.Text && i.password == password_tectbox.Password);
                    if (result.Count() == 1)
                    {
                        sw_roll = (int)result.FirstOrDefault().role;
                        Zavuch zavuch = new Zavuch();
                        zavuch.ShowDialog();
                        /*this.Close();
                        sw_roll = (int)result.FirstOrDefault().role;
                        switch (sw_roll)
                        {
                            case (byte)Roll.Zauch:
                                zav.ShowDialog();
                                break;
                            case (byte)Roll.Uchitel:
                                stud.ShowDialog();
                                break;
                            case (byte)Roll.Admin:
                                administrator.ShowDialog();
                                break;
                        }*/
                        MessageBox.Show(string.Format("Готово"),"Готово" ,MessageBoxButton.OK);
                    }
                    else
                        MessageBox.Show(string.Format("Не найдено такого пользователя\n\nЛогин - {0} Пароль - {1}",
                            log_tectbox.Text, password_tectbox.Password),
                            "Предупреждение!", MessageBoxButton.OK, MessageBoxImage.Warning);

                }
                else
                    MessageBox.Show("Поля 'Логин' и 'Пароль' - должны быть полностью заполнены!", "Ошибка!",
                        MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
