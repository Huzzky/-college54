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

namespace Проект
{
    /// <summary>
    /// Логика взаимодействия для Zavuch.xaml
    /// </summary>
    public partial class Zavuch : Window
    {
        public demo_studentEntities bd_stud = new demo_studentEntities();
        public bool flag_add = false;
        public bool flag_edit = false;
        public Zavuch()
        {
            InitializeComponent();
        }
        private void load_greed(object sender, RoutedEventArgs e)
        {
            var mem = bd_stud.members;
            var prep = bd_stud.prepod;
            var classt = bd_stud.class_tab;
            var classt1 = bd_stud.class_tab;
            var grade_c1 = bd_stud.grade;
            var pol = bd_stud.pol_tab;
            var pol1 = bd_stud.pol_tab;
            var stat = bd_stud.studnow_tab;
            var result_pr = prep.Where(k => k.id > 0);
            var result = mem.Where(i => i.id > 0);

            gride_zav.ItemsSource = prep.ToList();

            gridee.ItemsSource = mem.ToList();
            combobox1.ItemsSource = classt1.ToList();
            pol_gr1.ItemsSource = pol1.ToList();
            gradebox.ItemsSource = grade_c1.ToList();
            gradebox1.ItemsSource = grade_c1.ToList();
            gradebox2.ItemsSource = grade_c1.ToList();
            star_gr.ItemsSource = stat.ToList();
            combobox.ItemsSource = classt.ToList();
            pol_gr.ItemsSource = pol.ToList();

            if (radio_1.IsChecked == true)
            {
                if (MainWindow.sw_roll == 2)
                    group_box.Visibility = Visibility.Collapsed;
                if (MainWindow.sw_roll == 1)
                    group_box.Visibility = Visibility.Visible;

                add_stud.Visibility = Visibility.Visible;
                gride_zav.Visibility = Visibility.Collapsed;
                gridee.Visibility = Visibility.Visible;
                label_9.Visibility = Visibility.Collapsed;
                label_8.Visibility = Visibility.Visible;
            }

            if (MainWindow.sw_roll == 1)
                radio_2.IsChecked = true;
            if (radio_2.IsChecked == true)
            {
                add_stud.Visibility = Visibility.Collapsed;
                gridee.Visibility = Visibility.Collapsed;
                gride_zav.Visibility = Visibility.Visible;
                label_9.Visibility = Visibility.Visible;
                label_8.Visibility = Visibility.Collapsed;
            }

        }
        private void end_edit(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (flag_add == false && flag_edit == true)
            {
                bd_stud.SaveChanges();
                flag_edit = false;
            }
        }
        private void row_edit(object sender, DataGridRowEditEndingEventArgs e)
        {
            var memb = bd_stud.members;
            var prepod = bd_stud.prepod;
            if (flag_add == true)
            {
                if (radio_1.IsChecked == true)
                {
                    memb.Add(gridee.SelectedItem as members);
                }
                if (radio_2.IsChecked == true)
                {
                    prepod.Add(gride_zav.SelectedItem as prepod);
                }
                bd_stud.SaveChanges();
                flag_add = false;
            }
        }
        private void key_down(object sender, KeyEventArgs e)
        {
            var memb = bd_stud.members;
            var pers = bd_stud.person;
            if (e.Key == Key.Delete && flag_edit != true)
            {
                if (radio_1.IsChecked == true)
                {
                    if (gridee.SelectedItem == null)
                        return;
                    if (MessageBox.Show("Информация по : " + (gridee.SelectedItem as members).fname +
                        " " + (gridee.SelectedItem as members).sname +
                        " " + (gridee.SelectedItem as members).fatherName +
                        " будет удалена!", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                    {
                        gridee.ItemsSource = memb.ToList();
                        return;
                    }
                    var numbb = (gridee.SelectedItem as members).id;
                    int numbbb = (int)(gridee.SelectedItem as members).id_person;
                    var result = memb.Where(i => i.id == numbb).ToList();
                    var result1 = pers.Where(i => i.id == numbbb).ToList();
                    if (result.Count == 1 && result1.Count == 1)
                    {
                        pers.Remove(result1.FirstOrDefault() as person);
                        if (memb.Remove(result.FirstOrDefault() as members) != null)
                        {
                            bd_stud.SaveChanges();
                            gridee.ItemsSource = memb.ToList();
                            gridee.Focus();
                            gridee.SelectedItem = e.InputSource;
                        }
                    }
                }
                if (radio_2.IsChecked == true)
                {
                    var prepod = bd_stud.prepod;
                    if (gride_zav.SelectedItem == null)
                        return;
                    var numbb = (gride_zav.SelectedItem as prepod).id;
                    var result11 = prepod.Where(j => j.id == numbb).ToList();
                    if (result11.Count == 1)
                    {
                        if (MessageBox.Show("Информация по : " + (gride_zav.SelectedItem as prepod).fname +
                         " " + (gride_zav.SelectedItem as prepod).sname +
                         " " + (gride_zav.SelectedItem as prepod).fatherName +
                         " будет удалена!", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        {
                            if (prepod.Remove(result11.FirstOrDefault() as prepod) != null)
                            {
                                bd_stud.SaveChanges();
                                gride_zav.ItemsSource = prepod.ToList();
                                gridee.Focus();
                            }
                        }
                    }
                }
            }
        }

        private void new_item(object sender, InitializingNewItemEventArgs e)
        {
            flag_add = true;
        }
        private void begin_edit(object sender, DataGridBeginningEditEventArgs e)
        {
            if (flag_add != true)
                flag_edit = true;
        }
        private void add_student(object sender, RoutedEventArgs e)
        {

        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (radio_1.IsChecked == true)
            {
                gridee.ItemsSource = bd_stud.members.ToList();
                poisk_fam.Text = "";
                poisk_nam.Text = "";
                combobox_1.Text = "";
            }
            if (radio_2.IsChecked == true)
            {
                gride_zav.ItemsSource = bd_stud.prepod.ToList();
                poisk_fam.Text = "";
                poisk_nam.Text = "";
                combobox_1.Text = "";
            }
        }
        private void key_call(object sender, RoutedEventArgs e)
        {
            gridee.Visibility = Visibility.Collapsed;
        }
        private void exit_key(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void radio11(object sender, RoutedEventArgs e)
        {
            gride_zav.Visibility = Visibility.Collapsed;
            gridee.Visibility = Visibility.Visible;
            add_stud.Visibility = Visibility.Visible;
            label_9.Visibility = Visibility.Collapsed;
            label_8.Visibility = Visibility.Visible;
        }
        private void radio22(object sender, RoutedEventArgs e)
        {
            add_stud.Visibility = Visibility.Collapsed;
            gridee.Visibility = Visibility.Collapsed;
            gride_zav.Visibility = Visibility.Visible;
            label_9.Visibility = Visibility.Visible;
            label_8.Visibility = Visibility.Collapsed;
        }
        private void load_combo(object sender, RoutedEventArgs e)
        {
            var classt = bd_stud.class_tab;
            var result = classt.Where(i => i.id > 0).ToList();
            combobox_1.ItemsSource = classt.ToList();
        }
        private void load_class(object sender, RoutedEventArgs e)
        {
            var class_aa = bd_stud.class_tab;
            combobox_1.ItemsSource = class_aa.ToList();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (radio_1.IsChecked == true)
            {
                var memb = bd_stud.members;
                var result = memb.Where(i => i.fname.Contains(poisk_fam.Text) &&
                                             i.sname.Contains(poisk_nam.Text) &&
                                             i.class_tab.@class.Contains(combobox_1.Text)).ToList();
                if (result.Count() != 0)
                    gridee.ItemsSource = result.ToList();
                else
                    MessageBox.Show("Не найден студент с такими параметрами !", "Предупреждение!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            if (radio_2.IsChecked == true)
            {
                var prepod = bd_stud.prepod;
                var result = prepod.Where(i => i.fname.Contains(poisk_fam.Text) &&
                                                       i.sname.Contains(poisk_nam.Text) &&
                                                       i.class_tab.@class.Contains(combobox_1.Text)).ToList();
                if (result.Count() != 0)
                    gride_zav.ItemsSource = result.ToList();
                else
                    MessageBox.Show("Не найден учитель с такими параметрами !", "Предупреждение!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
       
    }
}
