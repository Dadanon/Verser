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
using System.Collections.ObjectModel;

namespace Verser
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int startCount = 0;
        public int endCount = 0;
        ApplicationContext db;
        List<Poem> poems;
        public static TextBlock tb = new TextBlock();
        public List<string> listDuplicate;
        public List<string> text;
        public static int expCounter = 0;
        public List<Expander> expanders = new List<Expander>();

        public MainWindow()
        {
            InitializeComponent();
            db = new ApplicationContext();

            if (db != null)
            {
                poems = db.Poems.ToList();
                byte[] colorValues = TitleColorValues();

                foreach (Poem poem in poems)
                {
                    Button n = new Button();
                    n.Name = "button" + poem.Id.ToString();
                    n.Background = new SolidColorBrush(Color.FromRgb(colorValues[0], colorValues[1], colorValues[2]));
                    n.Margin = new Thickness(10);
                    SetTitleTextProps(n, poem);
                    n.Click += new RoutedEventHandler(this.LinkClick);
                    TitleView.Children.Add(n);
                }
            }
        }
        private byte[] TitleColorValues()
        {
            byte[] values = new byte[3];
            Random rand = new Random();

            for (int i = 0; i < 3; i++)
            {
                values[i] = (byte)(rand.Next(60, 190));
            }
            return values;
        }
        private void SetTitleTextProps(Button button, Poem poem)
        {
            TextBlock tb = new TextBlock();
            Run bold = new Run();
            bold.Text = poem.Author + "\n\n";
            bold.FontWeight = FontWeights.Bold;
            bold.FontSize = 16;
            tb.Inlines.Add(bold);
            Run normal = new Run();
            normal.Text = poem.Title;
            normal.FontSize = 14;
            tb.Inlines.Add(normal);
            tb.TextWrapping = TextWrapping.Wrap;
            tb.TextAlignment = TextAlignment.Center;
            tb.Margin = new Thickness(0, 5, 0, 5);
            button.Content = tb;
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
            SQLiteConnection m_db = new SQLiteConnection("Data Source = poemsDb.db");
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
        private TextBlock BaseTextBlock(object sender, RoutedEventArgs e)
        {
            text = SearchDataById(sender, e)[1].Split('\n').ToList();
            text.RemoveAll(x => string.IsNullOrWhiteSpace(x));

            TextBlock tt = new TextBlock();
            tt.Margin = new Thickness(0, 10, 0, 0);
            tt.Name = "TitleRow";

            if ((text.Count - startCount) > 3)
            {
                endCount += 4;

                for (int i = startCount; i < endCount; i++)
                {
                    tt.Text += text[i] + "\n";
                }
                startCount += 4;
            }
            else
            {
                endCount += text.Count - startCount;

                for (int i = startCount; i < endCount; i++)
                {
                    tt.Text += text[i] + "\n";
                }
            }
            return tt;
        }

        private void CollapseChildren(object sender, RoutedEventArgs e)
        {
            Expander ex = e.Source as Expander;

            if (PoemWindow.exps.Count > 0)
            {
                for (int i = PoemWindow.exps.IndexOf(ex); i < PoemWindow.exps.Count; i++)
                {
                    PoemWindow.exps[i].IsExpanded = false;
                }
            }
        }

        private TextBlock ExpandableTextBlock(object sender, RoutedEventArgs e)
        {
            TextBlock etb = BaseTextBlock(sender, e);

            if ((text.Count - startCount) > 3)
            {
                Expander ep = new Expander();
                ep.Margin = new Thickness(0, 10, 0 , 0);
                ep.Name = "exp" + expanders.Count().ToString();
                expanders.Add(ep);
                ep.Collapsed += new RoutedEventHandler(CollapseChildren);
                ep.Content = ExpandableTextBlock(sender, e);
                etb.Inlines.Add(ep);

            }
            else if ((text.Count - startCount) > 0)
            {
                Expander ex = new Expander();
                ex.Margin = new Thickness(0, 10, 0, 10);
                TextBlock addy = new TextBlock();
                addy.Margin = new Thickness(0, 10, 0, 0);

                for (int i = startCount; i < text.Count; i++)
                {
                    addy.Text += text[i] + "\n";
                }
                ex.Content = addy;

                if (!etb.Text.Equals(addy.Text))
                {
                    ex.Name = "exp" + expanders.Count().ToString();
                    expanders.Add(ex);
                    etb.Inlines.Add(ex);
                }  
            }
            return etb;
        }
        private void LinkClick(object sender, RoutedEventArgs e)
        {
            if (this.Cursor != Cursors.Pen)
            {
                string[] data = SearchDataById(sender, e);
                PoemWindow ne = new PoemWindow();
                ne.TitleRow.Text = data[0];
                TextBlock ttt = ExpandableTextBlock(sender, e);
                ttt.Margin = new Thickness(10);
                ne.TextView.Content = ttt;
                PoemWindow.exps = expanders;
                OpenClose.ChangeWindow(this, ne);
            }
            else
            {
                ButtonDelete_Click(sender, e);
                this.Cursor = Cursors.Arrow;
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbr = MessageBox.Show("Do you want to delete this poem?", "Important Decision", MessageBoxButton.YesNo);
            switch (mbr)
            {
                case MessageBoxResult.Yes:
                    int poemId;
                    bool success = int.TryParse((sender as Button).Name.Substring(6), out poemId);
                    Poem poemsInDb = db.Poems.First(x => x.Id == poemId);
                    db.Poems.Remove(poemsInDb);
                    db.SaveChanges();
                    TitleView.Children.Remove(sender as Button);
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }
        private void ChangeCursor(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Pen;
        }

        private void Window_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.Cursor == Cursors.Pen)
                this.Cursor = Cursors.Arrow;
        }
    }
}
