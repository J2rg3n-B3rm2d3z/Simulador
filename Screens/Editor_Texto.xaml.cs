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
using System.Data.SqlTypes;

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
        Assembler assembler = new Assembler(new List<string>());
        private Editor_Execute editor_Execute;

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
                MyFrame.Content = null;
                /*dataFile = File.ReadAllText(openFileDialog.FileName);*/
                /*txtEditor.Text = File.ReadAllText(openFileDialog.FileName);*/

                string CurrentFilePath = openFileDialog.FileName;
                richTextEditor.Document = new FlowDocument(new Paragraph(new Run(File.ReadAllText(CurrentFilePath))));



            }
        }

        

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

            /*BoxInput inputBox = new BoxInput();
            bool? result = inputBox.ShowDialog();*/

            char letra = 'z';
            int dr = (int)letra - 96;
            /*string operando2 = Data.Data.memoria[2].Valor.Substring(6, 11);*/

            /*MessageBox.Show(Data.Data.memoria[2].Valor.Substring(10,6));*/
            /*int dr = (int)letra;*/

            /*string subBinary = "";
            foreach (char letra in letra1)
            {
                string binario6Bits = Convert.ToString((int)letra, 2).PadLeft(8, '0');
                subBinary += binario6Bits.Substring(0, 2);
            }

            if (subBinary.Length < 12)
            {
                string binario6Bits = Convert.ToString(int.Parse(subBinary), 2).PadLeft(12, '0');
                subBinary = binario6Bits;
            }

            subBinary = subBinary.Substring(0, 12);

            */

            string textData = new TextRange(richTextEditor.Document.ContentStart, richTextEditor.Document.ContentEnd).Text;

            MessageBox.Show(textData);
            Sintaxis.setDataFile(new List<string>());
            Sintaxis.setDataFile(new List<string>(textData.ToLower().Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)));
            assembler.setDataFile(new List<string>(textData.ToLower().Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)));
            bool d = Sintaxis.SintaxisResult();
            if (d)
            {

                
                MessageBox.Show("Sintaxis Correcta");
                assembler.SuccessAssembler();

                
            }
            else
            {
                MessageBox.Show("Sintaxis incorrecta");
            }
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            editor_Execute = new Editor_Execute();
            MyFrame.NavigationService.Navigate(editor_Execute );
            
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            editor_Execute.acciones();
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            MyFrame.Content = null;
        }
    }
}
