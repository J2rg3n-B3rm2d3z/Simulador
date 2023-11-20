using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlTypes;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Simulador.Screens
{
    /// <summary>
    /// Lógica de interacción para Editor_Execute.xaml
    /// </summary>
    public partial class Editor_Execute : Page
    {
        Uri arrowActive = new Uri("/Recursos/arrow.png", UriKind.Relative);
        Uri arrowDesactive = new Uri("/Recursos/play-removebg-preview.png", UriKind.Relative);
        Uri ledOff = new Uri("/Recursos/led_off.png", UriKind.Relative);
        Uri ledOn = new Uri("/Recursos/led_on.png", UriKind.Relative);
        string ultimoResultado = "";
        bool acarreo = false;
        bool overflow = false;

        public bool n = false;


        /*ObservableCollection<Data.Data.Registro> items = new ObservableCollection<Data.Data.Registro>();*/

        public Editor_Execute()
        {

            Data.Data.pc = 0;

            n = false;

            InitializeComponent();
            if (!Data.Data.initialization)
            {
                Data.Data.Constructor();
            }

            Data.Data.intruccion_init();

            int cantReg = 8;                                                // Cantidad de Registros
            int cantIns = 16;                                               // Cantidad de instrucciones
            int cantMem = 64;                                               // Cantidad de Memoria
            /* Registro[] registros = new Registro[cantReg];
             Instruccion[] instrucciones = new Instruccion[cantIns];         // Instruccines de bloque fijo
             Memoria[] memoria = new Memoria[cantMem];*/



            /*for (int i=0; i<cantReg; i++)                                   // Genera los registros necesarios para el procesador
            {
                *//*registros[i] = new Registro 
                {
                    NumReg = $"R{i}",                                       // Le da su numero de registro
                    Valor = "0000000000000000"                              // Le da un valor al registro
                };*//*

                DataGridReg.Items.Add(Data.Data.registros[i]);                        // Agrega el registro creado al DataGrig
            }*/

            /*DataGridReg.ItemsSource = Data.Data.registros;*/

            DataGridIns.Items.Clear();
            DataGridReg.Items.Clear();
            DataGridMem.Items.Clear();

            foreach (var registros in Data.Data.registros)
            {
                /*items.Add(registros);*/
                DataGridReg.Items.Add(registros);
            }

            /*instrucciones = new Instruccion[]
            {
                new Instruccion { Nombre = "ADD", Valor = "0000" },
                new Instruccion { Nombre = "SUB", Valor = "0001" },
                new Instruccion { Nombre = "DIV", Valor = "0010" },
                new Instruccion { Nombre = "MOD", Valor = "0011" },
                new Instruccion { Nombre = "MUL", Valor = "0100" },
                new Instruccion { Nombre = "MOV", Valor = "0101" },
                new Instruccion { Nombre = "INC", Valor = "0110" },           // Incrementa en 1 el registro seleccionado INC [Registro]
                new Instruccion { Nombre = "DEC", Valor = "0111" },           // Decrementa en 1 el registro seleccionado DEC [Registro]
                new Instruccion { Nombre = "JMP", Valor = "1000" },           
                new Instruccion { Nombre = "JPN", Valor = "1001" },           // Va a la direccion o etiqueta si la bandera de signo es negativo BRN [Etiqueta]
                new Instruccion { Nombre = "JPC", Valor = "1010" },            // Obtiene los datos de entrada IN [Registro donde se guarda]
                new Instruccion { Nombre = "JPO", Valor = "1011" },           // Muestra los datos seleciionado OUT [Registro que se quiere mostrar]
                new Instruccion { Nombre = "JPZ", Valor = "1100" },          // Terminar el programa
                new Instruccion { Nombre = "HALT", Valor = "1101" },           // Son excepciones que muestran error
                new Instruccion { Nombre = "IN", Valor = "1110" },           // Son excepciones que muestran error
                new Instruccion { Nombre = "OUT", Valor = "1111" },           // Son excepciones que muestran error
            };*/

            foreach (var instruccion in Data.Data.instrucciones)
            {
                DataGridIns.Items.Add(instruccion);
            }

            for (int i = 0; i < cantMem; i++)
            {
                
                DataGridMem.Items.Add(Data.Data.memoria[i]);
            }

            



            foreach (var variables in Data.Data.instruccionsAuxiliar)
            {
                DataGridMem.Items.Add(variables);
            }


            /*incDec();*/

            /*Task task = pcMemoryIR();*/

            /*Task task1 = accionMove();*/

            /*Task task1 = addPC();*/


        }

        /*new Instruccion { Nombre = "ADD", Valor = "0000" }, add r1,r2
          new Instruccion { Nombre = "SUB", Valor = "0001" }, sub r1,r2
          new Instruccion { Nombre = "DIV", Valor = "0010" }, div r1,r2
          new Instruccion { Nombre = "MOD", Valor = "0011" }, mod r1,r2
          new Instruccion { Nombre = "MUL", Valor = "0100" }, mul r1,r2
          new Instruccion { Nombre = "MOVE", Valor = "0101" }, move r1,r2
          new Instruccion { Nombre = "INC", Valor = "0110" }, inc r1          
          new Instruccion { Nombre = "DEC", Valor = "0111" }, dec r1          
          new Instruccion { Nombre = "JMP", Valor = "1000" }, jmp salto          
          new Instruccion { Nombre = "JPN", Valor = "1001" }, .          
          new Instruccion { Nombre = "JPC", Valor = "1010" }, .           
          new Instruccion { Nombre = "JPO", Valor = "1011" }, .          
          new Instruccion { Nombre = "JPZ", Valor = "1100" }, .         
          new Instruccion { Nombre = "HALT", Valor = "1101" }, terminar           
          new Instruccion { Nombre = "IN", Valor = "1110" }, entreda          
          new Instruccion { Nombre = "OUT", Valor = "1111" }, salida
        */

        public async Task acciones()
        {

            string instruccionAux = Data.Data.memoria[Data.Data.pc].Valor.Substring(12, 4);
            string instruccion = Data.Data.memoria[Data.Data.pc].Valor.Substring(0, 4);

            if (n)
                return;

            if (instruccionAux.Contains("1111"))
            {
                await addPC();
                Data.Data.pc += 1;
                return;
            }
            else
            {
                await pcMemoryIR();
            }

            if (instruccion.Contains("0000") || instruccion.Contains("0001") ||
                instruccion.Contains("0010") || instruccion.Contains("0011") || instruccion.Contains("0100"))
            {
                await operation();

            }
            else if (instruccion.Contains("0101"))
            {

                await accionMove();
            }

            else if (instruccion.Contains("0110") || instruccion.Contains("0111"))
            {

                await incDec();
            }
            else if (instruccion.Contains("1000") || instruccion.Contains("1001") ||
                instruccion.Contains("1010") || instruccion.Contains("1011") || instruccion.Contains("1100"))
            {
                await jump();
            }
            else if (instruccion.Contains("1110"))
            {
                await IO();
            }
            else if (instruccion.Contains("1111"))
            {
                await Output();
            }


            if (instruccion.Contains("1101"))
            {

                MessageBox.Show("La Ejecucion a Terminado");
                Data.Data.pc = 0;

                n = true;
            }
            else
            {
                Data.Data.pc += 1;
            }

        }

        public class Instruccion
        {
            public string Nombre { get; set; }
            public string Valor { get; set; }
        }

        public class Registro
        {
            public string NumReg { get; set; }
            public string Valor { get; set; }
        }

        public class Memoria
        {
            public int NumMem { get; set; }
            public string Valor { get; set; }
        }

        public static int ConvertirBinarioAEntero(string binario)
        {
            int resultado = 0;

            for (int i = 0; i < binario.Length; i++)
            {
                char bit = binario[binario.Length - 1 - i]; // Leemos los bits de derecha a izquierda

                if (bit == '1')
                {
                    resultado += (int)Math.Pow(2, i);
                }
            }

            return resultado;
        }

        public static string ConvertirEnteroABinario(int numero)
        {
            if (numero == 0)
            {
                return "0";
            }

            string binario = "";

            while (numero > 0)
            {
                int residuo = numero % 2;
                binario = residuo.ToString() + binario;
                numero = numero / 2;
            }

            return binario;
        }

        /*pc -> bus interno
bus -> mar
mar -> bus
bus -> memory
memory -> bus
bus -> mdr
mdr -> bus (intruccion)
bus -> ir
mdr -? bus (primer operando)*/

        private async Task pcMemoryIR()
        {
            string binario6Bits = Convert.ToString(Data.Data.pc, 2).PadLeft(6, '0');
            txtPC.Text = binario6Bits;

            await Task.Delay(1000);
            barPC.Fill = Brushes.Red;
            barPC.Stroke = Brushes.Red;
            imgPC1.Source = new BitmapImage(arrowActive);

            await Task.Delay(1000);

            barMAR.Fill = Brushes.Red;
            barMAR.Stroke = Brushes.Red;
            imgMAR.Source = new BitmapImage(arrowActive);

            await Task.Delay(1000);

           

            barPC.Fill = Brushes.Black;
            barPC.Stroke = Brushes.Black;
            imgPC1.Source = new BitmapImage(arrowDesactive);

            txtMAR.Text = binario6Bits;

            await Task.Delay(1000);

            barMARBus.Fill = Brushes.Red;
            barMARBus.Stroke = Brushes.Red;

            barMAR.Fill = Brushes.Black;  
            barMAR.Stroke = Brushes.Black;
            imgMAR.Source = new BitmapImage(arrowDesactive);

            await Task.Delay(1000);

            barMemoryMAR.Fill = Brushes.Red;
            barMemoryMAR.Stroke = Brushes.Red;
            imgMemoryMAR.Source = new BitmapImage(arrowActive);

            barMARBus.Fill = Brushes.Black;
            barMARBus.Stroke = Brushes.Black;

            await addPC();

            await Task.Delay(1000);

            barMemoriaMDR.Fill = Brushes.Red;
            barMemoriaMDR.Stroke = Brushes.Red;

            imgMemoryMDR.Source = new BitmapImage(arrowActive);

            barMemoryMAR.Fill = Brushes.Black;
            barMemoryMAR.Stroke = Brushes.Black;
            imgMemoryMAR.Source = new BitmapImage(arrowDesactive);

            await Task.Delay(1000);

            barBusMDR.Fill = Brushes.Red;
            barBusMDR.Stroke = Brushes.Red;

            imgMDR3.Source = new BitmapImage(arrowActive);

            await Task.Delay(1000);

            txtMDR.Text = Data.Data.memoria[Data.Data.pc].Valor;

            barMemoriaMDR.Fill = Brushes.Black;   
            barMemoriaMDR.Stroke = Brushes.Black;

            imgMemoryMDR.Source = new BitmapImage(arrowDesactive);

            await Task.Delay(1000);

            barMDR.Fill = Brushes.Red;
            barMDR.Stroke = Brushes.Red;

            imgMDR1.Source = new BitmapImage(arrowActive);

            await Task.Delay(1000);

            barBusMDR.Fill = Brushes.Black;
            barBusMDR.Stroke = Brushes.Black;

            imgMDR3.Source = new BitmapImage(arrowDesactive);

            await Task.Delay(1000);

            barBusIR.Fill = Brushes.Red;
            barBusIR.Stroke = Brushes.Red;

            imgArrowBusIR2.Source = new BitmapImage(arrowActive);

            barMDR.Fill = Brushes.Black;
            barMDR.Stroke = Brushes.Black;

            imgMDR1.Source = new BitmapImage(arrowDesactive);
            
            await Task.Delay(1000);

            txtIR.Text = Data.Data.memoria[Data.Data.pc].Valor;

            barBusIR.Fill = Brushes.Black;
            barBusIR.Stroke = Brushes.Black;

            imgArrowBusIR2.Source = new BitmapImage(arrowDesactive);

        }

        private async Task accionMove()
        {
            string operando2 = Data.Data.memoria[Data.Data.pc].Valor.Substring(10,6);
            string operando1 = Data.Data.memoria[Data.Data.pc].Valor.Substring(4, 6);

            

            await Task.Delay(1000);

            if (operando2.Substring(1,1).Contains("1"))
            {
                barBusIR.Fill = Brushes.Red;
                barBusIR.Stroke = Brushes.Red;
                imgArrowBusIR1.Source = new BitmapImage(arrowActive);

                await Task.Delay(1000);

                barBusIR.Fill = Brushes.Black;
                barBusIR.Stroke = Brushes.Black;
                imgArrowBusIR2.Source = new BitmapImage(arrowDesactive);

                await Task.Delay(1000);

                barMAR.Fill = Brushes.Red;
                barMAR.Stroke = Brushes.Red;
                imgMAR.Source = new BitmapImage(arrowActive);

                barBusIR.Fill = Brushes.Black;
                barBusIR.Stroke = Brushes.Black;
                imgArrowBusIR1.Source = new BitmapImage(arrowDesactive);

                await Task.Delay(1000);

                txtMAR.Text = operando2;

                barMARBus.Fill = Brushes.Red;
                barMARBus.Stroke = Brushes.Red;

                await Task.Delay(1000);

                barMAR.Fill = Brushes.Black;
                barMAR.Stroke = Brushes.Black;
                imgMAR.Source = new BitmapImage(arrowDesactive);

                await Task.Delay(1000);

                barMemoryMAR.Fill = Brushes.Red;
                barMemoryMAR.Stroke = Brushes.Red;
                imgMemoryMAR.Source = new BitmapImage(arrowActive);


                await Task.Delay(1000);

                barMARBus.Fill = Brushes.Black;
                barMARBus.Stroke = Brushes.Black;

                barMemoriaMDR.Fill = Brushes.Red;
                barMemoriaMDR.Stroke = Brushes.Red;

                
                imgMemoryMDR.Source = new BitmapImage(arrowActive);

                await Task.Delay(1000);

                barMemoryMAR.Fill = Brushes.Black;
                barMemoryMAR.Stroke = Brushes.Black;
                imgMemoryMAR.Source = new BitmapImage(arrowDesactive);

                await Task.Delay(1000);

                barBusMDR.Fill = Brushes.Red;
                barBusMDR.Stroke = Brushes.Red;
                
                imgMDR3.Source = new BitmapImage(arrowActive);

                await Task.Delay(1000);

                barMemoriaMDR.Fill = Brushes.Black;
                barMemoriaMDR.Stroke = Brushes.Black;

                imgMemoryMDR.Source = new BitmapImage(arrowDesactive);

                await Task.Delay(1000);

                int index = Data.Data.instruccionsAuxiliar.FindIndex(x => x.Nombre.Equals(operando2));
                string info = Data.Data.instruccionsAuxiliar[index].Valor;

                txtMDR.Text = info;

                await Task.Delay(1000);

                barMDR.Fill = Brushes.Red;
                barMDR.Stroke = Brushes.Red;

                imgMDR1.Source = new BitmapImage(arrowActive);
                
                await Task.Delay(1000);

                barBusMDR.Fill = Brushes.Black;
                barBusMDR.Stroke = Brushes.Black;

                imgMDR3.Source = new BitmapImage(arrowDesactive);

                await Task.Delay(1000);

                barRegisBus.Fill = Brushes.Red;
                barRegisBus.Stroke = Brushes.Red;

                imgArrowRegisBus2.Source = new BitmapImage(arrowActive);

                await Task.Delay(1000);

                barMDR.Fill = Brushes.Black;
                barMDR.Stroke = Brushes.Black;

                imgMDR1.Source = new BitmapImage(arrowDesactive);

                await Task.Delay(1000);

                
                Data.Data.registros[Convert.ToInt32(operando1,2)].Valor = info;

                DataGridReg.Items.Clear();
                foreach (var registros in Data.Data.registros)
                {
                    /*items.Add(registros);*/
                    DataGridReg.Items.Add(registros);
                }

                /*items[Convert.ToInt32(operando1, 2)].Valor = operando2;*/

                await Task.Delay(1000);

                barRegisBus.Fill = Brushes.Black;
                barRegisBus.Stroke = Brushes.Black;

                imgArrowRegisBus2.Source = new BitmapImage(arrowDesactive);

            }
            else
            {
                barBusIR.Fill = Brushes.Red;
                barBusIR.Stroke = Brushes.Red;
                imgArrowBusIR1.Source = new BitmapImage(arrowActive);

                await Task.Delay(1000);

                barRegisBus.Fill = Brushes.Red;
                barRegisBus.Stroke = Brushes.Red;

                imgArrowRegisBus2.Source = new BitmapImage(arrowActive);

                await Task.Delay(1000);
                barBusIR.Fill = Brushes.Black;
                barBusIR.Stroke = Brushes.Black;
                imgArrowBusIR1.Source = new BitmapImage(arrowDesactive);

                await Task.Delay(1000);

                imgArrowRegisBus2.Source = new BitmapImage(arrowDesactive);

                imgArrowRegisBus1.Source = new BitmapImage(arrowActive);

                await Task.Delay(1000);

                imgArrowRegisBus1.Source = new BitmapImage(arrowDesactive);

                imgArrowRegisBus2.Source = new BitmapImage(arrowActive);

                await Task.Delay(100);

                Data.Data.registros[Convert.ToInt32(operando1, 2)].Valor = Data.Data.registros[Convert.ToInt32(operando2, 2)].Valor;

                DataGridReg.Items.Clear();

                foreach (var registros in Data.Data.registros)
                {
                    DataGridReg.Items.Add(registros);
                }

                barRegisBus.Fill = Brushes.Black;
                barRegisBus.Stroke = Brushes.Black;
                imgArrowRegisBus2.Source = new BitmapImage(arrowDesactive);

            }
        }

        private async Task addPC()
        {
            await Task.Delay(1000);
            txtY.Text = "00000001";

            barPC.Fill = Brushes.Red;
            barPC.Stroke = Brushes.Red;
            imgPC1.Source = new BitmapImage(arrowActive);

            await Task.Delay(1000);

            barAluX1.Fill = Brushes.Red;
            barAluX1.Stroke = Brushes.Red;

            barAlux2.Fill = Brushes.Red;
            barAlux2.Stroke = Brushes.Red;

            imgArrowAluX2.Source = new BitmapImage(arrowActive);

            await Task.Delay(1000);

            barPC.Fill = Brushes.Black;
            barPC.Stroke = Brushes.Black;
            imgPC1.Source = new BitmapImage(arrowDesactive);

            await Task.Delay(1000);

            barAluZ.Fill = Brushes.Red;
            barAluZ.Stroke = Brushes.Red;
            await Task.Delay(1000);
            

            txtZ.Text = Convert.ToString(Data.Data.pc + 1,2).PadLeft(8,'0');

            await Task.Delay(1000);

            barAluX1.Fill = Brushes.Black;
            barAluX1.Stroke = Brushes.Black;

            barAlux2.Fill = Brushes.Black;
            barAlux2.Stroke = Brushes.Black;

            imgArrowAluX2.Source = new BitmapImage(arrowDesactive);

            await Task.Delay(1000);

            barAluZ.Fill = Brushes.Black;
            barAluZ.Stroke = Brushes.Black;

            barZBus.Fill = Brushes.Red;
            barZBus.Stroke = Brushes.Red;
            imgArrowZBus.Source = new BitmapImage(arrowActive);

            await Task.Delay(1000);

            barPC.Fill = Brushes.Red;
            barPC.Stroke = Brushes.Red;
            imgPC2.Source = new BitmapImage(arrowActive);

            await Task.Delay(1000);
            barZBus.Fill = Brushes.Black;
            barZBus.Stroke = Brushes.Black;
            imgArrowZBus.Source = new BitmapImage(arrowDesactive);

            await Task.Delay(1000);

            txtPC.Text = Convert.ToString(Data.Data.pc + 1, 2).PadLeft(8, '0');

            await Task.Delay(1000);
            barPC.Fill = Brushes.Black;
            barPC.Stroke = Brushes.Black;
            imgPC2.Source = new BitmapImage(arrowDesactive);

        }


        private  bool HasCarry(string binaryA, string binaryB)
        {
            int maxLength = Math.Max(binaryA.Length, binaryB.Length);
            int carry = 0;

            for (int i = 0; i < maxLength; i++)
            {
                int bitA = (i < binaryA.Length) ? binaryA[binaryA.Length - 1 - i] - '0' : 0;
                int bitB = (i < binaryB.Length) ? binaryB[binaryB.Length - 1 - i] - '0' : 0;

                int sum = bitA + bitB + carry;

                if (sum > 1)
                {
                    carry = 1;
                }
                else
                {
                    carry = 0;
                }
            }

            return (carry == 1);
        }

        public static bool HasCarryInBinaryMultiplication(string binaryA, string binaryB)
        {
            string product = Convert.ToString(Convert.ToInt32(binaryA, 2) * Convert.ToInt32(binaryB, 2), 2);

            // Verificar si hay un acarreo observando el bit más alto del resultado
            return product.Length > Math.Max(binaryA.Length, binaryB.Length) && product[0] == '1';
        }

        


        private async Task operation()
        {
            string operando2 = Data.Data.memoria[Data.Data.pc].Valor.Substring(10, 6);
            string operando1 = Data.Data.memoria[Data.Data.pc].Valor.Substring(4, 6);
            string instruccion = Data.Data.memoria[Data.Data.pc].Valor.Substring(0, 4);


            if (operando2.Substring(1, 1).Contains("1"))
            {
                barBusIR.Fill = Brushes.Red;
                barBusIR.Stroke = Brushes.Red;
                imgArrowBusIR1.Source = new BitmapImage(arrowActive);

                await Task.Delay(1000);

                barBusIR.Fill = Brushes.Black;
                barBusIR.Stroke = Brushes.Black;
                imgArrowBusIR2.Source = new BitmapImage(arrowDesactive);

                await Task.Delay(1000);

                barMAR.Fill = Brushes.Red;
                barMAR.Stroke = Brushes.Red;
                imgMAR.Source = new BitmapImage(arrowActive);

                barBusIR.Fill = Brushes.Black;
                barBusIR.Stroke = Brushes.Black;
                imgArrowBusIR1.Source = new BitmapImage(arrowDesactive);

                await Task.Delay(1000);

                txtMAR.Text = operando2;

                barMARBus.Fill = Brushes.Red;
                barMARBus.Stroke = Brushes.Red;

                await Task.Delay(1000);

                barMAR.Fill = Brushes.Black;
                barMAR.Stroke = Brushes.Black;
                imgMAR.Source = new BitmapImage(arrowDesactive);

                await Task.Delay(1000);

                barMemoryMAR.Fill = Brushes.Red;
                barMemoryMAR.Stroke = Brushes.Red;
                imgMemoryMAR.Source = new BitmapImage(arrowActive);


                await Task.Delay(1000);

                barMARBus.Fill = Brushes.Black;
                barMARBus.Stroke = Brushes.Black;

                barMemoriaMDR.Fill = Brushes.Red;
                barMemoriaMDR.Stroke = Brushes.Red;


                imgMemoryMDR.Source = new BitmapImage(arrowActive);

                await Task.Delay(1000);

                barMemoryMAR.Fill = Brushes.Black;
                barMemoryMAR.Stroke = Brushes.Black;
                imgMemoryMAR.Source = new BitmapImage(arrowDesactive);

                await Task.Delay(1000);

                barBusMDR.Fill = Brushes.Red;
                barBusMDR.Stroke = Brushes.Red;

                imgMDR3.Source = new BitmapImage(arrowActive);

                await Task.Delay(1000);

                barMemoriaMDR.Fill = Brushes.Black;
                barMemoriaMDR.Stroke = Brushes.Black;

                imgMemoryMDR.Source = new BitmapImage(arrowDesactive);

                await Task.Delay(1000);

                int index = Data.Data.instruccionsAuxiliar.FindIndex(x => x.Nombre.Equals(operando2));
                string info = Data.Data.instruccionsAuxiliar[index].Valor;

                txtMDR.Text = info;

                await Task.Delay(1000);

                barMDR.Fill = Brushes.Red;
                barMDR.Stroke = Brushes.Red;

                imgMDR1.Source = new BitmapImage(arrowActive);

                await Task.Delay(1000);

                barBusMDR.Fill = Brushes.Black;
                barBusMDR.Stroke = Brushes.Black;

                imgMDR3.Source = new BitmapImage(arrowDesactive);

                await Task.Delay(1000);

                /* barRegisBus.Fill = Brushes.Red;
                 barRegisBus.Stroke = Brushes.Red;

                 imgArrowRegisBus2.Source = new BitmapImage(arrowActive);*/
                barAluY.Fill = Brushes.Red;
                barAluY.Stroke = Brushes.Red;

                barY.Fill = Brushes.Red;
                barY.Stroke = Brushes.Red;

                imgArrowAluY2.Source = new BitmapImage(arrowActive);



                await Task.Delay(1000);

                txtY.Text = info;

                barMDR.Fill = Brushes.Black;
                barMDR.Stroke = Brushes.Black;

                imgMDR1.Source = new BitmapImage(arrowDesactive);

                await Task.Delay(1000);

                barAluY.Fill = Brushes.Black;
                barAluY.Stroke = Brushes.Black;

                barY.Fill = Brushes.Black;
                barY.Stroke = Brushes.Black;

                imgArrowAluY2.Source = new BitmapImage(arrowDesactive);

                await Task.Delay(1000);

                barBusIR.Fill = Brushes.Red;
                barBusIR.Stroke = Brushes.Red;
                imgArrowBusIR1.Source = new BitmapImage(arrowActive);

                await Task.Delay(1000);

                barRegisBus.Fill= Brushes.Red;
                barRegisBus.Stroke = Brushes.Red;

                imgArrowRegisBus2.Source = new BitmapImage(arrowDesactive);                

                await Task.Delay(1000);

                barBusIR.Fill = Brushes.Black;
                barBusIR.Stroke = Brushes.Black;
                imgArrowBusIR2.Source = new BitmapImage(arrowDesactive);
                imgArrowBusIR1.Source = new BitmapImage(arrowDesactive);

                await Task.Delay(1000);

                imgArrowRegisBus2.Source = new BitmapImage(arrowDesactive);

                await Task.Delay(1000);

                imgArrowRegisBus1.Source = new BitmapImage(arrowActive);

                await Task.Delay(1000);

                barAluX1.Fill = Brushes.Red;
                barAluX1.Stroke = Brushes.Red;

                barAlux2.Fill = Brushes.Red;
                barAlux2.Stroke = Brushes.Red;

                imgArrowAluX2.Source = new BitmapImage(arrowActive);

                await Task.Delay(1000);

                barRegisBus.Fill = Brushes.Black;
                barRegisBus.Stroke = Brushes.Black;

                imgArrowRegisBus1.Source = new BitmapImage(arrowDesactive);

                await Task.Delay(1000);

                barAluZ.Stroke = Brushes.Red;
                barAluZ.Fill = Brushes.Red;

                await Task.Delay(1000);

                barAluX1.Fill = Brushes.Black;
                barAluX1.Stroke = Brushes.Black;

                barAlux2.Fill = Brushes.Black;
                barAlux2.Stroke = Brushes.Black;

                imgArrowAluX2.Source = new BitmapImage(arrowDesactive);

                await Task.Delay(1000);



                if (instruccion.Equals("0000"))
                {
                    txtZ.Text =Convert.ToString( (Convert.ToInt32(info, 2) + Convert.ToInt32(Data.Data.registros[Convert.ToInt32(operando1, 2)].Valor, 2)),2).PadLeft(6,'0');
                    if (HasCarry(info, Data.Data.registros[Convert.ToInt32(operando1, 2)].Valor))
                        acarreo = true;
                    else 
                        acarreo = false;
                }
                else if (instruccion.Equals("0001"))
                {
                    txtZ.Text = ( Convert.ToInt32(Data.Data.registros[Convert.ToInt32(operando1, 2)].Valor, 2) - Convert.ToInt32(info, 2)).ToString();
                    acarreo = false;
                }
                else if (instruccion.Equals("0010"))
                {
                    acarreo = false;
                    try
                    {
                        txtZ.Text = (Convert.ToInt32(Data.Data.registros[Convert.ToInt32(operando1, 2)].Valor, 2) / Convert.ToInt32(info, 2)).ToString();
                    }
                    catch (Exception ex)
                    {
                        ultimoResultado = "";
                        overflow = true;
                        return; 
                    }
                    
                }
                else if (instruccion.Equals("0011"))
                {
                    txtZ.Text = ( Convert.ToInt32(Data.Data.registros[Convert.ToInt32(operando1, 2)].Valor, 2) % Convert.ToInt32(info, 2)).ToString();
                    acarreo = false;
                }
                else if (instruccion.Equals("0100"))
                {
                    acarreo = false;
                    try
                    {
                        txtZ.Text = (Convert.ToInt32(info, 2) * Convert.ToInt32(Data.Data.registros[Convert.ToInt32(operando1, 2)].Valor, 2)).ToString();
                        
                        if (HasCarryInBinaryMultiplication(info, Data.Data.registros[Convert.ToInt32(operando1, 2)].Valor))
                            acarreo = true;
                        
                            
                    }
                    catch (Exception ex)
                    {
                        ultimoResultado = "";
                        overflow = true;
                        return;
                    }
                }

                overflow = false;


                ultimoResultado = txtZ.Text;

                await Task.Delay(1000);

                barZBus.Fill = Brushes.Red;
                barZBus.Stroke = Brushes.Red;

                imgArrowZBus.Source = new BitmapImage(arrowActive);

                await Task.Delay(1000);

                barAluZ.Stroke = Brushes.Black;
                barAluZ.Fill = Brushes.Black;

                await Task.Delay(1000);

                barMDR.Fill = Brushes.Red;
                barMDR.Stroke = Brushes.Red;
                imgMDR.Source = new BitmapImage(arrowActive);

                await Task.Delay(1000);

                barZBus.Fill = Brushes.Black;
                barZBus.Stroke = Brushes.Black;

                imgArrowZBus.Source = new BitmapImage(arrowDesactive);

                barBusMDR.Fill = Brushes.Red;
                barBusMDR.Stroke = Brushes.Red;

                await Task.Delay(1000);

                barMDR.Fill = Brushes.Black;
                barMDR.Stroke = Brushes.Black;
                imgMDR.Source = new BitmapImage(arrowDesactive);

                barMemoriaMDR.Fill = Brushes.Red;
                barMemoriaMDR.Stroke = Brushes.Red;
                

                await Task.Delay(1000);

                barBusMDR.Fill = Brushes.Black;
                barBusMDR.Stroke = Brushes.Black;

                Data.Data.instruccionsAuxiliar[index].Valor = txtZ.Text;

                await Task.Delay(1000);

                barMemoriaMDR.Fill = Brushes.Black;
                barMemoriaMDR.Stroke = Brushes.Black;

            } else if (operando1.Substring(1, 1).Contains("1"))
            {
                barBusIR.Fill = Brushes.Red;
                barBusIR.Stroke = Brushes.Red;
                imgArrowBusIR1.Source = new BitmapImage(arrowActive);

                await Task.Delay(1000);

                barBusIR.Fill = Brushes.Black;
                barBusIR.Stroke = Brushes.Black;
                imgArrowBusIR2.Source = new BitmapImage(arrowDesactive);

                await Task.Delay(1000);

                barMAR.Fill = Brushes.Red;
                barMAR.Stroke = Brushes.Red;
                imgMAR.Source = new BitmapImage(arrowActive);

                /*barBusIR.Fill = Brushes.Black;
                barBusIR.Stroke = Brushes.Black;
                imgArrowBusIR1.Source = new BitmapImage(arrowDesactive);*/

                await Task.Delay(1000);

                txtMAR.Text = operando1;

                await Task.Delay(1000);

                barMAR.Fill = Brushes.Black;
                barMAR.Stroke = Brushes.Black;
                imgMAR.Source = new BitmapImage(arrowDesactive);

                await Task.Delay(1000);

                barMARBus.Fill = Brushes.Red;
                barMARBus.Stroke = Brushes.Red;

                await Task.Delay(1000);

                barMARBus.Fill = Brushes.Black;
                barMARBus.Stroke = Brushes.Black;

                await Task.Delay(1000);

                barMemoryMAR.Fill = Brushes.Red;
                barMemoryMAR.Stroke = Brushes.Red;
                imgMemoryMAR.Source = new BitmapImage(arrowActive);

                await Task.Delay(1000);
                barMemoriaMDR.Fill = Brushes.Red;
                barMemoriaMDR.Stroke = Brushes.Red;
                imgMemoryMDR.Source = new BitmapImage(arrowActive);

                await Task.Delay(1000);

                barMemoryMAR.Fill = Brushes.Black;
                barMemoryMAR.Stroke = Brushes.Black;
                imgMemoryMAR.Source = new BitmapImage(arrowDesactive);

                await Task.Delay(1000);

                barBusMDR.Fill = Brushes.Red;
                barBusMDR.Stroke = Brushes.Red;
                imgMDR3.Source = new BitmapImage(arrowActive);

                

                await Task.Delay(1000);

                int index = Data.Data.instruccionsAuxiliar.FindIndex(x => x.Nombre.Equals(operando1));
                /*MessageBox.Show(index.ToString());*/

                string info = Data.Data.instruccionsAuxiliar[index].Valor;
                /*MessageBox.Show(info);*/
                txtMDR.Text = info;

                await Task.Delay(1000);
                barBusMDR.Fill = Brushes.Black;
                barBusMDR.Stroke = Brushes.Black;
                imgMDR3.Source = new BitmapImage(arrowDesactive);

                await Task.Delay(1000);

                barMDR.Fill = Brushes.Red;
                barMDR.Stroke = Brushes.Red;
                imgMDR1.Source = new BitmapImage(arrowActive);

                await Task.Delay(1000);

                barMDR.Fill = Brushes.Black;
                barMDR.Stroke = Brushes.Black;
                imgMDR1.Source = new BitmapImage(arrowDesactive);

                await Task.Delay(1000);
                barAluY.Fill = Brushes.Red;
                barAluY.Stroke = Brushes.Red;
                barY.Fill = Brushes.Red;
                barY.Stroke = Brushes.Red;
                imgArrowAluY2.Source = new BitmapImage(arrowActive);

                await Task.Delay(1000);

                txtY.Text = info;

                await Task.Delay(1000);
                barAluY.Fill = Brushes.Black;
                barAluY.Stroke = Brushes.Black;

                barY.Fill = Brushes.Black;
                barY.Stroke = Brushes.Black;

                imgArrowAluY2.Source = new BitmapImage(arrowDesactive);

                await Task.Delay(1000);

                barBusIR.Fill = Brushes.Red;
                barBusIR.Stroke = Brushes.Red;
                imgArrowBusIR1.Source = new BitmapImage(arrowActive);

                await Task.Delay(1000);

                barBusIR.Fill = Brushes.Black;
                barBusIR.Stroke = Brushes.Black;
                imgArrowBusIR2.Source = new BitmapImage(arrowDesactive);
                imgArrowBusIR1.Source = new BitmapImage(arrowDesactive);

                await Task.Delay(1000);


                barRegisBus.Fill = Brushes.Red;
                barRegisBus.Stroke = Brushes.Red;
                imgArrowRegisBus2.Source = new BitmapImage(arrowActive);

                await Task.Delay(1000);

                imgArrowRegisBus2.Source = new BitmapImage(arrowDesactive);

                await Task.Delay(1000);

                imgArrowRegisBus1.Source = new BitmapImage(arrowActive);

                await Task.Delay(1000);

                barRegisBus.Fill = Brushes.Black;
                barRegisBus.Stroke = Brushes.Black;
                imgArrowRegisBus1.Source = new BitmapImage(arrowDesactive);

                await Task.Delay(1000);


                barAluX1.Fill = Brushes.Red;
                barAluX1.Stroke = Brushes.Red;

                barAlux2.Fill = Brushes.Red;
                barAlux2.Stroke = Brushes.Red;

                imgArrowAluX2.Source = new BitmapImage(arrowActive);

                await Task.Delay(1000);

                barAluX1.Fill = Brushes.Black;
                barAluX1.Stroke = Brushes.Black;

                barAlux2.Fill = Brushes.Black;
                barAlux2.Stroke = Brushes.Black;

                imgArrowAluX2.Source = new BitmapImage(arrowDesactive);

                await Task.Delay(1000);

                barAluZ.Stroke = Brushes.Red;
                barAluZ.Fill = Brushes.Red;

                await Task.Delay(1000);


                if (instruccion.Equals("0000"))
                {
                    txtZ.Text = Convert.ToString((Convert.ToInt32(info, 2) + Convert.ToInt32(Data.Data.registros[Convert.ToInt32(operando2, 2)].Valor, 2)), 2).PadLeft(6, '0');
                    
                    if (HasCarry(info, Data.Data.registros[Convert.ToInt32(operando2, 2)].Valor))
                        acarreo = true;
                    else
                        acarreo = false;
                }
                else if (instruccion.Equals("0001"))
                {
                    txtZ.Text = (Convert.ToInt32(info, 2) - Convert.ToInt32(Data.Data.registros[Convert.ToInt32(operando2, 2)].Valor, 2) ).ToString();
                    acarreo = false;
                }
                else if (instruccion.Equals("0010"))
                {
                    acarreo = false;
                    try
                    {
                        txtZ.Text = (Convert.ToInt32(info, 2) / Convert.ToInt32(Data.Data.registros[Convert.ToInt32(operando2, 2)].Valor, 2)).ToString();
                    }
                    catch (Exception ex)
                    {
                        ultimoResultado = "";
                        overflow = true;
                        return;
                    }
                }
                else if (instruccion.Equals("0011"))
                {
                    txtZ.Text = (Convert.ToInt32(info, 2) % Convert.ToInt32(Data.Data.registros[Convert.ToInt32(operando2, 2)].Valor, 2)).ToString();
                    acarreo = false;
                }
                else if (instruccion.Equals("0100"))
                {
                    acarreo = false;
                    try
                    {
                        txtZ.Text = (Convert.ToInt32(info, 2) * Convert.ToInt32(Data.Data.registros[Convert.ToInt32(operando2, 2)].Valor, 2)).ToString();

                        if (HasCarryInBinaryMultiplication(info, Data.Data.registros[Convert.ToInt32(operando2, 2)].Valor))
                            acarreo = true;


                    }
                    catch (Exception ex)
                    {
                        ultimoResultado = "";
                        overflow = true;
                        return;
                    }
                }

                overflow = false;


                ultimoResultado = txtZ.Text;

                await Task.Delay(1000);

                barAluZ.Stroke = Brushes.Black;
                barAluZ.Fill = Brushes.Black;

                await Task.Delay(1000);

                barZBus.Fill = Brushes.Red;
                barZBus.Stroke = Brushes.Red;

                imgArrowZBus.Source = new BitmapImage(arrowActive);

                await Task.Delay(1000);

                barZBus.Fill = Brushes.Black;
                barZBus.Stroke = Brushes.Black;

                imgArrowZBus.Source = new BitmapImage(arrowDesactive);

                await Task.Delay(1000);

                barMDR.Fill = Brushes.Red;
                barMDR.Stroke = Brushes.Red;
                imgMDR.Source = new BitmapImage(arrowActive);

                await Task.Delay(1000);
                txtMDR.Text = txtZ.Text;

                await Task.Delay(1000);
                barMDR.Fill = Brushes.Black;
                barMDR.Stroke = Brushes.Black;
                imgMDR.Source = new BitmapImage(arrowDesactive);

                await Task.Delay(1000);
                barBusMDR.Fill= Brushes.Red;
                barBusMDR.Stroke= Brushes.Red;

                await Task.Delay(1000);
                barBusMDR.Fill = Brushes.Black;
                barBusMDR.Stroke = Brushes.Black;

                await Task.Delay(1000);
                barMemoriaMDR.Fill = Brushes.Red;
                barMemoriaMDR.Stroke = Brushes.Red;
                imgMemoryMDR2.Source = new BitmapImage(arrowActive);

                await Task.Delay(1000);
                barMemoriaMDR.Fill = Brushes.Red;
                barMemoriaMDR.Stroke = Brushes.Red;
                imgMemoryMDR2.Source = new BitmapImage(arrowDesactive);

                Data.Data.instruccionsAuxiliar[index].Valor = txtZ.Text;

            }
            else
            {

                barBusIR.Fill = Brushes.Red;
                barBusIR.Stroke = Brushes.Red;
                imgArrowBusIR1.Source = new BitmapImage(arrowActive);

                await Task.Delay(1000);

                barRegisBus.Fill = Brushes.Red;
                barRegisBus.Stroke = Brushes.Red;

                imgArrowRegisBus2.Source = new BitmapImage(arrowDesactive);

                await Task.Delay(1000);
                barBusIR.Fill = Brushes.Black;
                barBusIR.Stroke = Brushes.Black;
                imgArrowBusIR1.Source = new BitmapImage(arrowDesactive);

                await Task.Delay(1000);

                imgArrowRegisBus2.Source = new BitmapImage(arrowDesactive);

                imgArrowRegisBus1.Source = new BitmapImage(arrowActive);

                await Task.Delay(1000);
                barAluY.Fill = Brushes.Red;
                barAluY.Stroke = Brushes.Red;

                barY.Fill = Brushes.Red;
                barY.Stroke = Brushes.Red;

                imgArrowAluY2.Source = new BitmapImage(arrowActive);

                await Task.Delay(1000);

                imgArrowRegisBus1.Source = new BitmapImage(arrowDesactive);
                barRegisBus.Fill = Brushes.Black;
                barRegisBus.Stroke = Brushes.Black;

                barAluY.Fill = Brushes.Black;
                barAluY.Stroke = Brushes.Black;

                imgArrowAluY2.Source = new BitmapImage(arrowDesactive);

                barY.Fill = Brushes.Black;
                barY.Stroke = Brushes.Black;

                imgArrowRegisBus1.Source = new BitmapImage(arrowDesactive);

                string info = Data.Data.registros[Convert.ToInt32(operando2, 2)].Valor;

                txtY.Text = info;

                await Task.Delay(1000);

                barBusIR.Fill = Brushes.Red;
                barBusIR.Stroke = Brushes.Red;
                imgArrowBusIR2.Source = new BitmapImage(arrowActive);

                await Task.Delay(1000);

                barRegisBus.Fill = Brushes.Red;
                barRegisBus.Stroke = Brushes.Red;

                imgArrowRegisBus2.Source = new BitmapImage(arrowActive);

                await Task.Delay(1000);
                barBusIR.Fill = Brushes.Black;
                barBusIR.Stroke = Brushes.Black;
                imgArrowBusIR2.Source = new BitmapImage(arrowDesactive);
                

                await Task.Delay(1000);

                imgArrowRegisBus2.Source = new BitmapImage(arrowDesactive);

                imgArrowRegisBus1.Source = new BitmapImage(arrowActive);

                await Task.Delay(1000);
                barAluX1.Fill = Brushes.Red;
                barAluX1.Stroke = Brushes.Red;

                barAlux2.Fill = Brushes.Red;
                barAlux2.Stroke = Brushes.Red;

                imgArrowAluX2.Source = new BitmapImage(arrowActive);

                await Task.Delay(1000);

                barRegisBus.Fill = Brushes.Black;
                barRegisBus.Stroke = Brushes.Black;

                imgArrowRegisBus1.Source = new BitmapImage(arrowDesactive);

                await Task.Delay(1000);

                barAluZ.Stroke = Brushes.Red;
                barAluZ.Fill = Brushes.Red;

                await Task.Delay(1000);

                barAluX1.Fill = Brushes.Black;
                barAluX1.Stroke = Brushes.Black;

                barAlux2.Fill = Brushes.Black;
                barAlux2.Stroke = Brushes.Black;

                imgArrowAluX2.Source = new BitmapImage(arrowDesactive);

                await Task.Delay(1000);

                

                if (instruccion.Equals("0000"))
                {
                    int suma = Convert.ToInt32(Data.Data.registros[Convert.ToInt32(operando2, 2)].Valor, 2) + Convert.ToInt32(Data.Data.registros[Convert.ToInt32(operando1, 2)].Valor, 2);
                    txtZ.Text = Convert.ToString((Convert.ToInt32(Data.Data.registros[Convert.ToInt32(operando2, 2)].Valor, 2) + Convert.ToInt32(Data.Data.registros[Convert.ToInt32(operando1, 2)].Valor, 2)), 2).PadLeft(6, '0');

                    
                    
                    if (HasCarry(Data.Data.registros[Convert.ToInt32(operando2, 2)].Valor, Data.Data.registros[Convert.ToInt32(operando1, 2)].Valor))
                        acarreo = true;
                    else
                        acarreo = false;
                }
                else if (instruccion.Equals("0001"))
                {
                    txtZ.Text = (Convert.ToInt32(Data.Data.registros[Convert.ToInt32(operando1, 2)].Valor, 2) - Convert.ToInt32(Data.Data.registros[Convert.ToInt32(operando2, 2)].Valor, 2)).ToString();
                    acarreo = false;
                }
                else if (instruccion.Equals("0010"))
                {
                    acarreo = false;
                    try
                    {
                        txtZ.Text = (Convert.ToInt32(Data.Data.registros[Convert.ToInt32(operando1, 2)].Valor, 2) / Convert.ToInt32(Data.Data.registros[Convert.ToInt32(operando2, 2)].Valor, 2)).ToString();
                    }
                    catch (Exception ex)
                    {
                        ultimoResultado = "";
                        overflow = true;
                        return;
                    }
                }
                else if (instruccion.Equals("0011"))
                {
                    txtZ.Text = (Convert.ToInt32(Data.Data.registros[Convert.ToInt32(operando1, 2)].Valor, 2) % Convert.ToInt32(Data.Data.registros[Convert.ToInt32(operando2, 2)].Valor, 2)).ToString();
                    acarreo = false;
                }
                else if (instruccion.Equals("0100"))
                {
                    acarreo = false;
                    try
                    {
                        txtZ.Text = (Convert.ToInt32(Data.Data.registros[Convert.ToInt32(operando2, 2)].Valor, 2) * Convert.ToInt32(Data.Data.registros[Convert.ToInt32(operando1, 2)].Valor, 2)).ToString();
                        if (HasCarryInBinaryMultiplication(Data.Data.registros[Convert.ToInt32(operando2, 2)].Valor, Data.Data.registros[Convert.ToInt32(operando1, 2)].Valor))
                            acarreo = true;


                    }
                    catch (Exception ex)
                    {
                        ultimoResultado = "";
                        overflow = true;
                        return;
                    }
                }

                overflow = false;


                ultimoResultado = txtZ.Text;

                await Task.Delay(1000);

                barZBus.Fill = Brushes.Red;
                barZBus.Stroke = Brushes.Red;

                imgArrowZBus.Source = new BitmapImage(arrowActive);

                await Task.Delay(1000);

                barAluZ.Stroke = Brushes.Black;
                barAluZ.Fill = Brushes.Black;

                await Task.Delay(1000);

                barRegisBus.Fill = Brushes.Red;
                barRegisBus.Stroke = Brushes.Red;

                imgArrowRegisBus2.Source = new BitmapImage(arrowActive);

                await Task.Delay(1000);
                barZBus.Fill = Brushes.Black;
                barZBus.Stroke = Brushes.Black;

                imgArrowZBus.Source = new BitmapImage(arrowDesactive);

                await Task.Delay(1000);

                Data.Data.registros[Convert.ToInt32(operando2, 2)].Valor = txtZ.Text;

                DataGridReg.Items.Clear();
                foreach (var registros in Data.Data.registros)
                {
            
                    DataGridReg.Items.Add(registros);
                }

                barRegisBus.Fill = Brushes.Black;
                barRegisBus.Stroke = Brushes.Black;

                imgArrowRegisBus2.Source = new BitmapImage(arrowDesactive);

            }

            ultimoResultado = txtZ.Text;

                await Task.Delay(1000);
        }


        private async Task incDec()
        {
            
            string operando1 = Data.Data.memoria[Data.Data.pc].Valor.Substring(4, 6);
            string instruccion = Data.Data.memoria[Data.Data.pc].Valor.Substring(0, 4);


            

            txtY.Text = "000001";

            await Task.Delay(1000);

            barBusIR.Fill = Brushes.Red;
            barBusIR.Stroke = Brushes.Red;
            imgArrowBusIR1.Source = new BitmapImage(arrowActive);

            await Task.Delay(1000);

            barRegisBus.Fill= Brushes.Red;
            barRegisBus.Stroke= Brushes.Red;

            imgArrowRegisBus2.Source= new BitmapImage(arrowActive);

            await Task.Delay(1000);

            barBusIR.Fill = Brushes.Black;
            barBusIR.Stroke = Brushes.Black;
            imgArrowBusIR1.Source = new BitmapImage(arrowDesactive);

            await Task.Delay(1000);

            imgArrowRegisBus1.Source = new BitmapImage(arrowActive);

            imgArrowRegisBus2.Source = new BitmapImage(arrowDesactive);

            await Task.Delay(1000);

            barAluX1.Fill = Brushes.Red;
            barAluX1.Stroke = Brushes.Red;

            barAlux2.Fill = Brushes.Red;
            barAlux2.Stroke = Brushes.Red;

            imgArrowAluX2.Source = new BitmapImage(arrowActive);

            await Task.Delay(1000);

            barRegisBus.Fill = Brushes.Black;
            barRegisBus.Stroke = Brushes.Black;

            imgArrowRegisBus1.Source = new BitmapImage(arrowDesactive);

            await Task.Delay(1000);

            barAluZ.Fill = Brushes.Red;
            barAluZ.Stroke = Brushes.Red;

            await Task.Delay(1000);

            barAluX1.Fill = Brushes.Black;
            barAluX1.Stroke = Brushes.Black;

            barAlux2.Fill = Brushes.Black;
            barAlux2.Stroke = Brushes.Black;

            imgArrowAluX2.Source = new BitmapImage(arrowDesactive);

            
            await Task.Delay(1000);

         
            if (instruccion.Contains("0110"))
            {
                txtZ.Text = Convert.ToString(Convert.ToInt32(Data.Data.registros[Convert.ToInt32(operando1, 2)].Valor, 2) + 1, 2).PadLeft(6, '0').ToString();
            }
            else if(instruccion.Contains("0111"))
            {
                txtZ.Text = Convert.ToString(Convert.ToInt32(Data.Data.registros[Convert.ToInt32(operando1, 2)].Valor, 2) - 1, 2).PadLeft(6, '0').ToString();
            }

            await Task.Delay(1000);

            barZBus.Fill = Brushes.Red;
            barZBus.Stroke = Brushes.Red;

            imgArrowZBus.Source = new BitmapImage(arrowActive);

            await Task.Delay(1000);

            barAluZ.Fill = Brushes.Black;
            barAluZ.Stroke = Brushes.Black;

            await Task.Delay(1000);

            barRegisBus.Stroke = Brushes.Red;
            barRegisBus.Fill = Brushes.Red;

            imgArrowRegisBus2.Source = new BitmapImage(arrowActive);

            await Task.Delay(1000);

            barZBus.Fill = Brushes.Black;
            barZBus.Stroke = Brushes.Black;

            imgArrowZBus.Source = new BitmapImage(arrowDesactive);

            Data.Data.registros[Convert.ToInt32(operando1, 2)].Valor = txtZ.Text;

            DataGridReg.Items.Clear();
            foreach (var registros in Data.Data.registros)
            {
                DataGridReg.Items.Add(registros);
            }

            await Task.Delay(1000);

            barRegisBus.Stroke = Brushes.Black;
            barRegisBus.Fill = Brushes.Black;

            imgArrowRegisBus2.Source = new BitmapImage(arrowDesactive);

        }

        private async Task IO()
        {
            string operando1 = Data.Data.memoria[Data.Data.pc].Valor.Substring(4, 6);
            string instruccion = Data.Data.memoria[Data.Data.pc].Valor.Substring(0, 4);

            await Task.Delay(1000);

            barBusIR.Fill = Brushes.Red;
            barBusIR.Stroke = Brushes.Red;
            imgArrowBusIR1.Source = new BitmapImage(arrowActive);

            await Task.Delay(1000);

            barBusIR.Fill = Brushes.Black;
            barBusIR.Stroke = Brushes.Black;
            imgArrowBusIR1.Source = new BitmapImage(arrowDesactive);

            await Task.Delay(1000);

            barMAR.Fill = Brushes.Red;
            barMAR.Stroke = Brushes.Red;

            imgMAR.Source = new BitmapImage(arrowActive);

            await Task.Delay(1000);
            barMAR.Fill = Brushes.Black;
            barMAR.Stroke = Brushes.Black;

            imgMAR.Source = new BitmapImage(arrowDesactive);

           

            await Task.Delay(1000);

            barMARBus.Fill = Brushes.Red;
            barMARBus.Stroke = Brushes.Red;

            await Task.Delay(1000);

            barMARBus.Fill = Brushes.Black;
            barMARBus.Stroke = Brushes.Black;

            

            await Task.Delay(1000);

            barIO.Stroke = Brushes.Red;
            barIO.Fill = Brushes.Red;

            imgIO.Source = new BitmapImage(arrowActive);

            await Task.Delay(1000);

            if (instruccion.Contains("1110"))
            {
                await Input(operando1);
            }

        }

        private async Task Input(string operation)
        {

            await Task.Delay(1000);

            barIO.Stroke = Brushes.Black;
            barIO.Fill = Brushes.Black;
            imgIO.Source = new BitmapImage(arrowDesactive);

            await Task.Delay(1000);

            barIO1.Fill = Brushes.Red;
            barIO1.Stroke = Brushes.Red;

            barIN1.Fill = Brushes.Red;
            barIN1.Stroke = Brushes.Red;

            imgIN1.Source = new BitmapImage(arrowActive);

            await Task.Delay(1000);

            barIO1.Fill = Brushes.Black;
            barIO1.Stroke = Brushes.Black;

            barIN1.Fill = Brushes.Black;
            barIN1.Stroke = Brushes.Black;

            imgIN1.Source = new BitmapImage(arrowDesactive);

            await Task.Delay(1000);

            barIN2.Fill = Brushes.Red;
            barIN2.Stroke = Brushes.Red;

            barIN3.Fill = Brushes.Red;
            barIN3.Stroke = Brushes.Red;

            await Task.Delay(1000);

            btnSwitch.IsEnabled = true;

            await Task.Delay(1000);

            barIN2.Fill = Brushes.Black;
            barIN2.Stroke = Brushes.Black;

            barIN3.Fill = Brushes.Black;
            barIN3.Stroke = Brushes.Black;

            imgArrowRegisBus2.Source = new BitmapImage(arrowDesactive);

        }

        private async Task Output()
        {

            string operando1 = Data.Data.memoria[Data.Data.pc].Valor.Substring(4, 6);
            string instruccion = Data.Data.memoria[Data.Data.pc].Valor.Substring(0, 4);

            await Task.Delay(1000);
            barBusIR.Fill = Brushes.Red;
            barBusIR.Stroke = Brushes.Red;

            imgArrowBusIR1.Source = new BitmapImage(arrowActive);

            await Task.Delay(1000);

            barRegisBus.Stroke= Brushes.Red;
            barRegisBus.Fill= Brushes.Red;
            imgArrowRegisBus2.Source= new BitmapImage(arrowActive);

            await Task.Delay(1000);

            barBusIR.Fill = Brushes.Black;
            barBusIR.Stroke = Brushes.Black;

            imgArrowBusIR1.Source = new BitmapImage(arrowDesactive);

            await Task.Delay(1000);

            imgArrowRegisBus1.Source = new BitmapImage(arrowActive);

            imgArrowRegisBus2.Source = new BitmapImage(arrowDesactive);

            await Task.Delay(1000);

            barMDR.Fill = Brushes.Red;
            barMDR.Stroke = Brushes.Red;

            imgMDR.Source = new BitmapImage(arrowActive);

            await Task.Delay(1000);

            barRegisBus.Stroke = Brushes.Black;
            barRegisBus.Fill = Brushes.Black;
            imgArrowRegisBus1.Source = new BitmapImage(arrowDesactive);

            await Task.Delay(1000);

            string info = Data.Data.registros[Convert.ToInt32(operando1, 2)].Valor;

            txtMDR.Text = info;

            await Task.Delay(1000);

            barBusMDR.Fill = Brushes.Red;
            barBusMDR.Stroke= Brushes.Red;

            await Task.Delay(1000);

            barMDR.Fill = Brushes.Black;
            barMDR.Stroke = Brushes.Black;

            imgMDR.Source = new BitmapImage(arrowDesactive);

            await Task.Delay(1000);

            barBusMDR.Fill = Brushes.Black;
            barBusMDR.Stroke = Brushes.Black;

            await Task.Delay(1000);

            barIO.Stroke = Brushes.Red;
            barIO.Fill = Brushes.Red;

            imgIO.Source = new BitmapImage(arrowActive);

            await Task.Delay(1000);

            barIO.Stroke = Brushes.Black;
            barIO.Fill = Brushes.Black;

            imgIO.Source = new BitmapImage(arrowDesactive);

            await Task.Delay(1000);

            barIO1.Fill = Brushes.Red;
            barIO1.Stroke = Brushes.Red;

            barIO2.Fill = Brushes.Red;
            barIO2.Stroke = Brushes.Red;

            barIO3.Fill = Brushes.Red;
            barIO3.Stroke = Brushes.Red;

            barOUT1.Fill = Brushes.Red;
            barOUT1.Stroke = Brushes.Red;

            imgOUT1.Source = new BitmapImage(arrowActive);

            await Task.Delay(1000);

            barIO1.Fill = Brushes.Black;
            barIO1.Stroke = Brushes.Black;

            barIO2.Fill = Brushes.Black;
            barIO2.Stroke = Brushes.Black;

            barIO3.Fill = Brushes.Black;
            barIO3.Stroke = Brushes.Black;

            barOUT1.Fill = Brushes.Black;
            barOUT1.Stroke = Brushes.Black;

            imgOUT1.Source = new BitmapImage(arrowDesactive);

            await Task.Delay(1000);

            txtPuerto1.Text = Convert.ToInt32(txtMDR.Text, 2).ToString();

            await Task.Delay(1000);

            barOUT2.Fill = Brushes.Red;
            barOUT2.Stroke = Brushes.Red;

            barOUT3.Fill = Brushes.Red;
            barOUT3.Stroke = Brushes.Red;

            barOUT4.Fill = Brushes.Red;
            barOUT4.Stroke = Brushes.Red;

            barOUT5.Fill = Brushes.Red;
            barOUT5.Stroke = Brushes.Red;

            await Task.Delay(1000);

            barOUT2.Fill = Brushes.Black;
            barOUT2.Stroke = Brushes.Black;

            barOUT3.Fill = Brushes.Black;
            barOUT3.Stroke = Brushes.Black;

            barOUT4.Fill = Brushes.Black;
            barOUT4.Stroke = Brushes.Black;

            barOUT5.Fill = Brushes.Black;
            barOUT5.Stroke = Brushes.Black;

            await Task.Delay(1000);

            if(Convert.ToInt32(txtMDR.Text, 2) == 0)
            {
                imgLed.Source = new BitmapImage(ledOff);
            }else
            {
                imgLed.Source = new BitmapImage(ledOn);
            }

            await Task.Delay(1000);

            barOUT6.Fill = Brushes.Red;
            barOUT6.Stroke = Brushes.Red;

            barOUT7.Fill = Brushes.Red;
            barOUT7.Stroke = Brushes.Red;

            barOUT8.Fill = Brushes.Red;
            barOUT8.Stroke = Brushes.Red;

            barOUT9.Fill = Brushes.Red;
            barOUT9.Stroke = Brushes.Red;

            barOUT10.Fill = Brushes.Red;
            barOUT10.Stroke = Brushes.Red;

            await Task.Delay(1000);

            barOUT6.Fill = Brushes.Black;
            barOUT6.Stroke = Brushes.Black;

            barOUT7.Fill = Brushes.Black;
            barOUT7.Stroke = Brushes.Black;

            barOUT8.Fill = Brushes.Black;
            barOUT8.Stroke = Brushes.Black;

            barOUT9.Fill = Brushes.Black;
            barOUT9.Stroke = Brushes.Black;

            barOUT10.Fill = Brushes.Black;
            barOUT10.Stroke = Brushes.Black;



            MessageBox.Show(Convert.ToInt32(txtMDR.Text, 2).ToString(), "Resultados");



        }

        private async Task jump()
        {
            string instruccion = Data.Data.memoria[Data.Data.pc].Valor.Substring(0, 4);
            string jump = Data.Data.memoria[Data.Data.pc].Valor.Substring(4, 12);

            barBusIR.Fill = Brushes.Red;
            barBusIR.Stroke = Brushes.Red;
            imgArrowBusIR1.Source = new BitmapImage(arrowActive);

            await Task.Delay(1000);

            barBusIR.Fill = Brushes.Black;
            barBusIR.Stroke = Brushes.Black;
            imgArrowBusIR1.Source = new BitmapImage(arrowDesactive);

            await Task.Delay(1000);

            barMAR.Fill = Brushes.Red;
            barMAR.Stroke = Brushes.Red;
            imgMAR.Source = new BitmapImage(arrowActive);

            await Task.Delay(1000);

            txtMAR.Text = jump;

            await Task.Delay(1000);

            barMAR.Fill = Brushes.Black;
            barMAR.Stroke = Brushes.Black;
            imgMAR.Source = new BitmapImage(arrowDesactive);

            await Task.Delay(1000);

            barMARBus.Fill = Brushes.Red;
            barMARBus.Stroke = Brushes.Red;

            await Task.Delay(1000);

            barMARBus.Fill = Brushes.Black;
            barMARBus.Stroke = Brushes.Black;

            await Task.Delay(1000);

            barMemoryMAR.Stroke = Brushes.Red;
            barMemoryMAR.Fill = Brushes.Red;
            imgMemoryMAR.Source = new BitmapImage(arrowActive);
            
            await Task.Delay(1000);

            barMemoryMAR.Stroke = Brushes.Black;
            barMemoryMAR.Fill = Brushes.Black;
            imgMemoryMAR.Source = new BitmapImage(arrowDesactive);

            await Task.Delay(1000);

            barMemoriaMDR.Fill = Brushes.Red;
            barMemoriaMDR.Stroke = Brushes.Red;
            imgMemoryMDR.Source = new BitmapImage(arrowActive);

            await Task.Delay(1000);

            barMemoriaMDR.Fill = Brushes.Black;
            barMemoriaMDR.Stroke = Brushes.Black;
            imgMemoryMDR.Source = new BitmapImage(arrowDesactive);

            await Task.Delay(1000);

            barBusMDR.Fill= Brushes.Red;
            barBusMDR.Stroke= Brushes.Red;
            imgMDR3.Source = new BitmapImage(arrowActive);

            await Task.Delay(1000);

            int index = Data.Data.BanderaMemoria.FindIndex(x => x.Valor.Equals(jump));
            string info = Data.Data.BanderaMemoria[index].NumMem.ToString();

            

            txtMDR.Text = Convert.ToString(int.Parse(info), 2).PadLeft(6, '0'); ;

            await Task.Delay(1000);

            barBusMDR.Fill = Brushes.Black;
            barBusMDR.Stroke = Brushes.Black;
            imgMDR3.Source = new BitmapImage(arrowDesactive);

            await Task.Delay(1000);

            barMDR.Fill = Brushes.Red;
            barMDR.Stroke = Brushes.Red;
            imgMDR1.Source = new BitmapImage(arrowActive);

            await Task.Delay(1000);

            barMDR.Fill = Brushes.Black;
            barMDR.Stroke = Brushes.Black;
            imgMDR1.Source = new BitmapImage(arrowDesactive);

            await Task.Delay(1000);

            barPC.Stroke = Brushes.Red;
            barPC.Fill = Brushes.Red;
            imgPC2.Source = new BitmapImage(arrowActive);

            await Task.Delay(1000);

            if (instruccion.Equals("1000"))
            {
                txtPC.Text = txtMDR.Text;
                Data.Data.pc = Data.Data.BanderaMemoria[index].NumMem-1;
            }
            else if (instruccion.Equals("1001"))
            {
                if (!overflow)
                {
                    if(int.Parse(ultimoResultado) < 0)
                    {
                        txtPC.Text = txtMDR.Text;
                        Data.Data.pc = Data.Data.BanderaMemoria[index].NumMem - 1;
                    }
                }
            }
            else if (instruccion.Equals("1010"))
            {
                if (acarreo)
                {
                    txtPC.Text = txtMDR.Text;
                    Data.Data.pc = Data.Data.BanderaMemoria[index].NumMem - 1;
                }
            }
            else if (instruccion.Equals("1011"))
            {
                if (overflow)
                {
                    txtPC.Text = txtMDR.Text;
                    Data.Data.pc = Data.Data.BanderaMemoria[index].NumMem - 1;
                }
            }
            else if (instruccion.Equals("1100"))
            {
                if (!overflow)
                {
                    if (int.Parse(ultimoResultado) == 0)
                    {
                        txtPC.Text = txtMDR.Text;
                        Data.Data.pc = Data.Data.BanderaMemoria[index].NumMem - 1;
                    }
                }
            }

            

            await Task.Delay(1000);

            barPC.Stroke = Brushes.Black;
            barPC.Fill = Brushes.Black;
            imgPC2.Source = new BitmapImage(arrowDesactive);

           

            

        }


                /*new Instruccion { Nombre = "JMP", Valor = "1000" },           
                new Instruccion { Nombre = "JPN", Valor = "1001" },           
                new Instruccion { Nombre = "JPC", Valor = "1010" },            
                new Instruccion { Nombre = "JPO", Valor = "1011" },           
                new Instruccion { Nombre = "JPZ", Valor = "1100" }, */








        private void DataGridIns_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private async void btnSwitch_Click(object sender, RoutedEventArgs e)
        {

            await Task.Run(async () =>
            {
                await outtAsync();
            });
           

        }

        private async Task outtAsync()
        {
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                barIN4.Fill = Brushes.Red;
                barIN4.Stroke = Brushes.Red;

                barIN5.Fill = Brushes.Red;
                barIN5.Stroke = Brushes.Red;

                barIN6.Fill = Brushes.Red;
                barIN6.Stroke = Brushes.Red;

                barIN7.Fill = Brushes.Red;
                barIN7.Stroke = Brushes.Red;

                barIN8.Fill = Brushes.Red;
                barIN8.Stroke = Brushes.Red;
            });
            await Task.Delay(1000);


            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                barIN4.Fill = Brushes.Black;
                barIN4.Stroke = Brushes.Black;

                barIN5.Fill = Brushes.Black;
                barIN5.Stroke = Brushes.Black;

                barIN6.Fill = Brushes.Black;
                barIN6.Stroke = Brushes.Black;

                barIN7.Fill = Brushes.Black;
                barIN7.Stroke = Brushes.Black;

                barIN8.Fill = Brushes.Black;
                barIN8.Stroke = Brushes.Black;
            });

            await Task.Delay(1000);

            bool? result = true;
            BoxInput inputBox = null;
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                inputBox = new BoxInput();
                result = inputBox.ShowDialog();
            });

            await Application.Current.Dispatcher.InvokeAsync(() =>
            {

                barIN4.Fill = Brushes.Red;
                barIN4.Stroke = Brushes.Red;

                barIN5.Fill = Brushes.Red;
                barIN5.Stroke = Brushes.Red;

                barIN6.Fill = Brushes.Red;
                barIN6.Stroke = Brushes.Red;

                barIN7.Fill = Brushes.Red;
                barIN7.Stroke = Brushes.Red;

                barIN8.Fill = Brushes.Red;
                barIN8.Stroke = Brushes.Red;
            });
            await Task.Delay(1000);

            await Application.Current.Dispatcher.InvokeAsync(() =>
            {

                barIN4.Fill = Brushes.Black;
                barIN4.Stroke = Brushes.Black;

                barIN5.Fill = Brushes.Black;
                barIN5.Stroke = Brushes.Black;

                barIN6.Fill = Brushes.Black;
                barIN6.Stroke = Brushes.Black;

                barIN7.Fill = Brushes.Black;
                barIN7.Stroke = Brushes.Black;

                barIN8.Fill = Brushes.Black;
                barIN8.Stroke = Brushes.Black;
            });

            await Task.Delay(1000);

            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                barIN2.Fill = Brushes.Red;
                barIN2.Stroke = Brushes.Red;

                barIN3.Fill = Brushes.Red;
                barIN3.Stroke = Brushes.Red;
            });

            await Task.Delay(1000);

            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                btnSwitch.IsEnabled = false;
            });
            await Task.Delay(1000);

            await Application.Current.Dispatcher.InvokeAsync(() =>
            {

                barIN2.Fill = Brushes.Black;
                barIN2.Stroke = Brushes.Black;

                barIN3.Fill = Brushes.Black;
                barIN3.Stroke = Brushes.Black;

            });

            await Task.Delay(1000);


            if (result == true)
            {
                string userInput = "no contenido";

                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    userInput = inputBox.InputText;
                });
                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    txtPuerto0.Text = userInput;
                });
                await Task.Delay(1000);

                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    barIO1.Fill = Brushes.Red;
                    barIO1.Stroke = Brushes.Red;

                    barIN1.Fill = Brushes.Red;
                    barIN1.Stroke = Brushes.Red;

                    imgIN1.Source = new BitmapImage(arrowActive);
                });

                await Task.Delay(1000);

                await Application.Current.Dispatcher.InvokeAsync(() =>
                {

                    barIO1.Fill = Brushes.Black;
                    barIO1.Stroke = Brushes.Black;

                    barIN1.Fill = Brushes.Black;
                    barIN1.Stroke = Brushes.Black;

                    imgIN1.Source = new BitmapImage(arrowDesactive);
                });

                await Task.Delay(1000);

                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    barBusMDR.Fill = Brushes.Red;
                    barBusMDR.Stroke = Brushes.Red;
                    imgMDR3.Source = new BitmapImage(arrowActive);
                });

                await Task.Delay(1000);
                await Application.Current.Dispatcher.InvokeAsync(() =>
                {

                    barIO.Stroke = Brushes.Black;
                    barIO.Fill = Brushes.Black;

                    imgIO.Source = new BitmapImage(arrowDesactive);
                });


                await Task.Delay(1000);

                await Application.Current.Dispatcher.InvokeAsync(() =>
                {

                    txtMDR.Text = txtPuerto0.Text;

                    barMDR.Fill = Brushes.Red;
                    barMDR.Stroke = Brushes.Red;

                    imgMDR1.Source = new BitmapImage(arrowActive);
                });

                await Task.Delay(1000);

                await Application.Current.Dispatcher.InvokeAsync(() =>
                {

                    barBusMDR.Fill = Brushes.Black;
                    barBusMDR.Stroke = Brushes.Black;
                    imgMDR3.Source = new BitmapImage(arrowDesactive);
                });

                await Task.Delay(1000);
                await Application.Current.Dispatcher.InvokeAsync(() =>
                {

                    barRegisBus.Stroke = Brushes.Red;
                    barRegisBus.Fill = Brushes.Red;

                    imgArrowRegisBus2.Source = new BitmapImage(arrowActive);
                });

                await Task.Delay(1000);
                await Application.Current.Dispatcher.InvokeAsync(() =>
                {

                    barMDR.Fill = Brushes.Black;
                    barMDR.Stroke = Brushes.Black;

                    imgMDR1.Source = new BitmapImage(arrowDesactive);
                    
                    string operando = Data.Data.memoria[Data.Data.pc - 1].Valor.Substring(4, 6);
                    Data.Data.registros[Convert.ToInt32(operando, 2)].Valor = txtMDR.Text;


                    DataGridReg.Items.Clear();
                    foreach (var registros in Data.Data.registros)
                    {
                        DataGridReg.Items.Add(registros);
                    }

                });

                await Task.Delay(1000);

                await Application.Current.Dispatcher.InvokeAsync(() =>
                {

                    barRegisBus.Stroke = Brushes.Black;
                    barRegisBus.Fill = Brushes.Black;
                    imgArrowRegisBus2.Source = new BitmapImage(arrowDesactive);

                });
                /**/

            }
        }
    }
}
