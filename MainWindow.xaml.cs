using Simulador.Screens;
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

namespace Simulador
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _count = 0;
        private Estructura_Arquitectura estructura_Arquitectura = new Estructura_Arquitectura();
        private Acerca_de_Nosotros acerca_De_Nosotros = new Acerca_de_Nosotros();
        private Editor_Texto editor_Texto = new Editor_Texto();
        public MainWindow()
        {
            InitializeComponent();
            MyFrame.NavigationService.Navigate(acerca_De_Nosotros);
            ResizeMode = ResizeMode.NoResize;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            _count++;
            //boton1.Content = _count.ToString();
           

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.NavigationService.Navigate(acerca_De_Nosotros);
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            MyFrame.NavigationService.Navigate(estructura_Arquitectura);
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            MyFrame.NavigationService.Navigate(editor_Texto);
        }

        private void MyFrame_Navigated(object sender, NavigationEventArgs e)
        {
            
        }
    }
}
