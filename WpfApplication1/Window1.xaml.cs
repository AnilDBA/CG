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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();                        
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();            
            Window nt = new MainWindow();
            nt.ShowDialog();
            
        }

        private void Window_LayoutUpdated(object sender, EventArgs e)
        {            
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            MessageBox.Show(commonClass.UserName);
            tabControl1.Width = this.ActualWidth-20;
        }
    }
}
