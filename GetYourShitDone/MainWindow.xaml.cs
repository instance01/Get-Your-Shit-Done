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
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.ComponentModel;
using System.IO;
using System.Collections;

namespace GetYourShitDone
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            Tasks = new List<ITask>();
            InitializeComponent();
            string file = AppDomain.CurrentDomain.BaseDirectory + @"\settings.conf";
            if (File.Exists(file))
            {
                loadAllTasks(file);
            }
        }

        public IList<ITask> Tasks { get; set; }

        public class ITask : INotifyPropertyChanged
        {
            public int percent;

            public string Name { get; set; }
            public int Progress
            {
                get { return this.percent; }
                set
                {
                    percent = value;
                    NotifyPropertyChanged("Progress");
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            public void NotifyPropertyChanged(String info)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(info));
                }
            }
        }


        private void menuitem1_click(object sender, RoutedEventArgs e)
        {
            if (listbox1.SelectedIndex != -1)
            {
                Tasks.RemoveAt(listbox1.SelectedIndex);
                listbox1.Items.RemoveAt(listbox1.SelectedIndex);
            }
        }

        private async void menuitem2_click(object sender, RoutedEventArgs e)
        {
            if (listbox1.SelectedIndex != -1)
            {
                string result = await this.ShowInputAsync("Set new value", "Set new value");
                int res;
                bool isNumeric = int.TryParse(result, out res);
                if (isNumeric)
                {
                    Tasks[listbox1.SelectedIndex].Progress = res;
                }
            }
        }

        private void listbtn_click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            ITask it = button.DataContext as ITask;
            if (it.Progress < 100)
            {
                it.Progress += 5;
            }
        }

        private void listbtn2_click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            ITask it = button.DataContext as ITask;
            if (it.Progress > 0)
            {
                it.Progress -= 5;
            }
        }

        private void textbox1_keydown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (textbox1.Text != "")
                {
                    String s = textbox1.Text;
                    ITask t = new ITask() { Name = s, Progress = 10 };
                    Tasks.Add(t);
                    listbox1.Items.Add(t);
                    e.Handled = true;
                }
            }
        }


        public void saveAllTasks(string file)
        {
            ArrayList list = new ArrayList();
            foreach(ITask it in Tasks){
                list.Add(it.Name + "#" + it.Progress);
            }
            File.WriteAllLines(file, (string[])list.ToArray(typeof(string)));
        }

        public void loadAllTasks(string file)
        {
            string[] str = File.ReadAllLines(file);
            foreach (string str_ in str)
            {
                string[] args = str_.Split('#');
                int progress = 0;
                int.TryParse(args[1], out progress);
                ITask t = new ITask() { Name = args[0], Progress = progress };
                Tasks.Add(t);
                listbox1.Items.Add(t);
            }
        }

        private void base_onclose(object sender, CancelEventArgs e)
        {
            string file = AppDomain.CurrentDomain.BaseDirectory + @"\settings.conf";
            if (File.Exists(file))
            {
                File.Delete(file);
            }
            if (Tasks.Count > 0)
            {
                saveAllTasks(file);
            }
            Application.Current.Shutdown();
        }
    }
}
