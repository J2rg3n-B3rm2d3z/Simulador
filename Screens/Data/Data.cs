using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Simulador.Screens.Data
{
    static class Data
    {
        public static Boolean initialization = false;
        static int cantReg = 8;                                                // Cantidad de Registros
        static int cantIns = 16;                                               // Cantidad de instrucciones
        static int cantMem = 256;                                               // Cantidad de Memoria

        public static Registro[] registros = new Registro[cantReg];
        public static Instruccion[] instrucciones = new Instruccion[cantIns];         // Instruccines de bloque fijo
        public static Memoria[] memoria = new Memoria[cantMem];


        public static void Constructor()
        {
            for (int i = 0; i < cantReg; i++)                                   // Genera los registros necesarios para el procesador
            {
                registros[i] = new Registro
                {
                    NumReg = $"R{i}",                                       // Le da su numero de registro
                    Valor = "0000000000000000"                              // Le da un valor al registro
                };                        // Agrega el registro creado al DataGrig
            }

            instrucciones = new Instruccion[]
            {
                new Instruccion { Nombre = "ADD", Valor = "0000" },
                new Instruccion { Nombre = "SUB", Valor = "0001" },
                new Instruccion { Nombre = "DIV", Valor = "0010" },
                new Instruccion { Nombre = "MOD", Valor = "0011" },
                new Instruccion { Nombre = "MUL", Valor = "0100" },
                new Instruccion { Nombre = "MOVE", Valor = "0101" },
                new Instruccion { Nombre = "INC", Valor = "0110" },           // Incrementa en 1 el registro seleccionado INC [Registro]
                new Instruccion { Nombre = "DEC", Valor = "0111" },           // Decrementa en 1 el registro seleccionado DEC [Registro]
                new Instruccion { Nombre = "JMP", Valor = "1000" },           // Va a la direccion o etiqueta si la bandera de signo es negativo BRN [Etiqueta]
                new Instruccion { Nombre = "JPN", Valor = "1001" },           //  Va a la direccion o etiqueta si la bandera de signo es negativo BRN [Etiqueta]
                new Instruccion { Nombre = "JPC", Valor = "1010" },            // Obtiene los datos de entrada IN [Registro donde se guarda]
                new Instruccion { Nombre = "JPO", Valor = "1011" },           // Muestra los datos seleciionado OUT [Registro que se quiere mostrar]
                new Instruccion { Nombre = "JPZ", Valor = "1100" },          // Terminar el programa
                new Instruccion { Nombre = "HALT", Valor = "1101" },           // Son excepciones que muestran error
                new Instruccion { Nombre = "IN", Valor = "1110" },           // Son excepciones que muestran error
                new Instruccion { Nombre = "OUT", Valor = "1111" },           // Son excepciones que muestran error
            };

            for (int i = 0; i < cantMem; i++)
            {
                memoria[i] = new Memoria
                {
                    NumMem = i,
                    Valor = "0000000000000000"
                };
            }

            initialization = true;
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
    }


}
