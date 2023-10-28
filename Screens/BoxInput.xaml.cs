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

namespace Simulador.Screens
{
    /// <summary>
    /// Lógica de interacción para BoxInput.xaml
    /// </summary>
    public partial class BoxInput : Window
    {
        public BoxInput()
        {
            InitializeComponent();
        }

        public string InputText { get; private set; }

       

        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {
            InputText = txtInput.Text;
            DialogResult = true;
            Close();
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
