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

namespace Simulador.Screens
{
    /// <summary>
    /// Lógica de interacción para Estructura_Arquitectura.xaml
    /// </summary>
    public partial class Estructura_Arquitectura : Page
    {
        public Estructura_Arquitectura()
        {
            InitializeComponent();
            int cantReg = 8; // Cantidad de registro en el procesador
            Registro[] registros = new Registro[cantReg];

            for (int i=0; i< cantReg; i++) // Genera los registros necesarios para el procesador
            {
                registros[i] = new Registro 
                {
                    NumReg = $"R{i}", // Le da su numero de registro
                    Valor = (i + 1) * 100 // Le da un valor al registro
                };

                DataGridReg.Items.Add(registros[i]); // Agrega el registro creado al DataGrig
            }
        }

        public class Registro 
        {
            public string NumReg { get; set; }
            public int Valor { get; set; }
        } 
    }
}
