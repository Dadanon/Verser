using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Verser
{
    internal static class OpenClose
    {
        public static void ChangeWindow<T, K>(T windowToClose, K windowToOpen)
            where T : Window where K : Window
        {
            windowToOpen.Width = windowToClose.Width;
            windowToOpen.Height = windowToClose.Height;
            windowToOpen.Left = windowToClose.Left;
            windowToOpen.Top = windowToClose.Top;
            windowToClose.Close();
            windowToOpen.Show();
        }
    }
}
