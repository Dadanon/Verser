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
        string[] defaultTexts = { "Enter a title:", "Enter an author:", "Enter a text:" };
        public EditWindow()
        {
            InitializeComponent();
            db = new ApplicationContext();
            TitleRow.Text = "Enter a title:";
            TitleRow.GotFocus += new RoutedEventHandler(RemoveText);
            TitleRow.LostFocus += new RoutedEventHandler(AddText);

            AuthorRow.Text = "Enter an author:";
            AuthorRow.GotFocus += new RoutedEventHandler(RemoveText);
            AuthorRow.LostFocus += new RoutedEventHandler(AddText);

            TextRow.Text = "Enter a text:";
            TextRow.GotFocus += new RoutedEventHandler(RemoveText);
            TextRow.LostFocus += new RoutedEventHandler(AddText);
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            OpenClose.ChangeWindow(this, new MainWindow());
        }

        private void PlaceholderText(TextBox tb, string text)
        {
            tb.Text = text;

            tb.GotFocus += new RoutedEventHandler(RemoveText);
            tb.LostFocus += new RoutedEventHandler(AddText);
        }

        public void RemoveText(object sender, RoutedEventArgs e)
        {
            TextBox t = sender as TextBox;
            if (t.Name == "TitleRow" && t.Text == "Enter a title:" || t.Name == "AuthorRow" && t.Text == "Enter an author:" || t.Name == "TextRow" && t.Text == "Enter a text:")
            {
                t.Text = "";
            }
        }

        public void AddText(object sender, RoutedEventArgs e)
        {
            TextBox t = sender as TextBox;
            if (string.IsNullOrWhiteSpace(t.Text))
            {
                if (t.Name == "TitleRow") t.Text = "Enter a title:";
                else if (t.Name == "AuthorRow") t.Text = "Enter an author:";
                else if (t.Name == "TextRow") t.Text = "Enter a text:";
            }
                
            
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (TitleRow.Text != "Enter a title:" && AuthorRow.Text != "Enter an author:" && TextRow.Text != "Enter a text:")
            {
                Poem poem = new Poem(TitleRow.Text, AuthorRow.Text, TextRow.Text);
                db.Poems.Add(poem);
                db.SaveChanges();
                OpenClose.ChangeWindow(this, new MainWindow());
            }
            else
            {
                MessageBox.Show("Enter non-default values :)");
            }
        }
    }
}
