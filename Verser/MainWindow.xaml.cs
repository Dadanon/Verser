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
                    Button n = new Button();
                    n.Margin = new Thickness(10);
                    n.Name = "Button" + poem.Id.ToString();
                    n.Content = poem.Title;
                    n.Click += new RoutedEventHandler(this.LinkClick);
                    TitleView.Children.Add(n);
                }
            }
        }
        private void ButtonNew_Click(object sender, RoutedEventArgs e)
        {
            OpenClose.ChangeWindow(this, new EditWindow());
        }

        private string[] SearchDataById(object sender, RoutedEventArgs e)
        {
            string[] data = new string[2];
            string text = "";
            string newTitle = "";
            string poemId = (sender as Button).Name;
            string search = $"SELECT * FROM poems WHERE Id = {poemId.Substring(6)}";
            SQLiteConnection m_db = new SQLiteConnection("DefaultConnection");
            m_db.Open();
            SQLiteCommand command = new SQLiteCommand(search, m_db);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                text += reader["Text"] + "\n";
                newTitle += reader["Title"] + " - " + reader["Author"];
            }
            reader.Close();
            m_db.Close();
            data[0] = newTitle;
            data[1] = text;
            return data;

        }
        private void LinkClick(object sender, RoutedEventArgs e)
        {
            string[] data = SearchDataById(sender, e);
            PoemWindow ne = new PoemWindow();
            ne.TitleRow.Text = data[0];
            ne.TextRow.Text = data[1];
            OpenClose.ChangeWindow(this, ne);
        }
    }
}
