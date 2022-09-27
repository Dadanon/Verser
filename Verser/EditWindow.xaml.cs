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
            TitleRow.Resources.Add(MainWindow.style.TargetType, MainWindow.style);
            AuthorRow.Resources.Add(MainWindow.style.TargetType, MainWindow.style);
            TextRow.Resources.Add(MainWindow.style.TargetType, MainWindow.style);
            db = new ApplicationContext();
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            OpenClose.ChangeWindow(this, new MainWindow());
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (TitleRow.Text != defaultTexts[0] && AuthorRow.Text != defaultTexts[1] && TextRow.Text != defaultTexts[2])
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

        private void editW_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FocusManager.SetFocusedElement(editW, editW);
            Keyboard.ClearFocus();
        }
        private void TestIfTextNotEmpty(TextBox tb, string text)
        {
            if (string.IsNullOrEmpty(tb.Text)) tb.Text = text;
        }
        private void TitleRow_LostFocus(object sender, RoutedEventArgs e)
        {
            TestIfTextNotEmpty(sender as TextBox, defaultTexts[0]);
        }

        private void AuthorRow_LostFocus(object sender, RoutedEventArgs e)
        {
            TestIfTextNotEmpty(sender as TextBox, defaultTexts[1]);
        }

        private void TextRow_LostFocus(object sender, RoutedEventArgs e)
        {
            TestIfTextNotEmpty(sender as TextBox, defaultTexts[2]);
        }
        private void TestIfTextIsDefault(TextBox tb, string text)
        {
            if (tb.Text == text) tb.Text = "";
        }
        private void TitleRow_GotFocus(object sender, RoutedEventArgs e)
        {
            TestIfTextIsDefault(sender as TextBox, defaultTexts[0]);
        }
        private void AuthorRow_GotFocus(object sender, RoutedEventArgs e)
        {
            TestIfTextIsDefault(sender as TextBox, defaultTexts[1]);
        }
        private void TextRow_GotFocus(object sender, RoutedEventArgs e)
        {
            TestIfTextIsDefault(sender as TextBox, defaultTexts[2]);
        }
    }
}
