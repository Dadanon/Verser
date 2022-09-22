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
    /// Логика взаимодействия для PoemWindow.xaml
    /// </summary>
    public partial class PoemWindow : Window
    {
        public static List<Expander> exps = new List<Expander>();
        public PoemWindow()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            OpenClose.ChangeWindow(this, new MainWindow());
        }
    }
}
