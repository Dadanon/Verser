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
using System.Data.SQLite;

namespace Verser
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ApplicationContext db;
        public MainWindow()
        {
            InitializeComponent();
            db = new ApplicationContext();
            if (db != null)
            {
                List<Poem> poems = db.Poems.ToList();
                foreach (Poem poem in poems)
                {
                    TextBlock n = new TextBlock();
                    n.Text = poem.Title;
                    TitleView.Children.Add(n);
                }
            }
        }
        private void ButtonNew_Click(object sender, RoutedEventArgs e)
        {
            EditWindow editWindow = new EditWindow();
            editWindow.Width = this.Width;
            editWindow.Height = this.Height;
            editWindow.Left = this.Left;
            editWindow.Top = this.Top;
            this.Close();
            editWindow.Show();
        }
    }
}
