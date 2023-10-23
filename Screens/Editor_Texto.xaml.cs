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
using Microsoft.Win32;
using System.IO;
using System.Drawing.Imaging;
using Simulador.Screens.AssemblerFiles;

namespace Simulador.Screens
{
    /// <summary>
    /// Lógica de interacción para Editor_Texto.xaml
    /// </summary>
    public partial class Editor_Texto : Page
    {

        string dataFile = "";
        List<string> dataSplit;
        sintaxis Sintaxis = new sintaxis(new List<string>());

        public Editor_Texto()
        {
            InitializeComponent();
        }
        
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Assembler files (*.asm)|*.asm";
            if (openFileDialog.ShowDialog() == true)
            {
                dataFile = File.ReadAllText(openFileDialog.FileName);
                txtEditor.Text = dataFile;
                
                Sintaxis.setDataFile(new List<string>(dataFile.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)));
            }
        }

        

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            bool d = Sintaxis.SintaxisResult();
            if (d)
            {
                MessageBox.Show("Sintaxis Correcta");
            }
            else
            {
                MessageBox.Show("Sintaxis incorrecta");
            }
        }
    }
}
