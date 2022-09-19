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

namespace Verser
{
    /// <summary>
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        ApplicationContext db;
        public EditWindow()
        {
            InitializeComponent();
            db = new ApplicationContext();
        }

        private void ToBase()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Width = this.Width;
            mainWindow.Height = this.Height;
            mainWindow.Left = this.Left;
            mainWindow.Top = this.Top;
            this.Close();
            mainWindow.Show();
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            ToBase();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Poem poem = new Poem(TitleRow.Text, AuthorRow.Text, TextRow.Text);
            db.Poems.Add(poem);
            db.SaveChanges();
            ToBase();
        }
    }
}
