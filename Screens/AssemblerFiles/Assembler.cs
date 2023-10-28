using Simulador.Screens.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Simulador.Screens.AssemblerFiles
{
    internal class Assembler
    {
        private List<string> dataFile = new List<string>();
        public Assembler(List<string> dataFile)
        {
            this.dataFile = dataFile;
        }

        public void setDataFile(List<string> dataFile)
        {
            this.dataFile = dataFile;
        }

        public bool SuccessAssembler()
        {
            if (this.dataFile.Count == 0) return false;

            bool InCode = false;
            string assemblarBinario = "";
            int index = 0;
            foreach (var data in this.dataFile)
            {
                if (data.Contains(".code"))
                {
                    InCode = true;
                }
                else if (data.Contains(".data"))
                {
                    InCode = false;
                }

                if (InCode)
                {
                    string[] dataSplit = data.Split(' ');
                    if (dataSplit.Length > 1) 
                        assemblarBinario = Data.Data.instrucciones.Where(x=> x.Nombre.ToLower() == dataSplit[0]).Select(x => x.Valor).FirstOrDefault();

                    if (dataSplit[0].Contains("halt"))
                    {
                        
                        assemblarBinario = Data.Data.instrucciones.Where(x => x.Nombre.ToLower() == dataSplit[0]).Select(x => x.Valor).FirstOrDefault() + "000000000000";

                    }

                    if (dataSplit[0].Contains("move") || dataSplit[0].Contains("add") ||
                            dataSplit[0].Contains("sub") || dataSplit[0].Contains("mul") ||
                            dataSplit[0].Contains("div") || dataSplit[0].Contains("mod"))
                    {
                        dataSplit[1] = dataSplit[1].Replace(",", "");
                        if (Data.Data.registros.Count(x => x.NumReg.ToLower().Contains(dataSplit[1])) == 1)
                        {
                            string pattern = @"(\D+)(\d+)";
                            Match match = Regex.Match(dataSplit[1], pattern);

                            if (match.Success)
                            {
                                char letra = char.Parse(match.Groups[2].Value);
                                /*MessageBox.Show(letra.ToString());*/
                                string binario6Bits = Convert.ToString(int.Parse(match.Groups[2].Value), 2).PadLeft(6, '0');
                               /* MessageBox.Show(binario6Bits);*/
                                assemblarBinario += binario6Bits;
                            }

                            if (Data.Data.registros.Count(x => x.NumReg.ToLower().Contains(dataSplit[2])) == 1)
                            {

                                match = Regex.Match(dataSplit[2], pattern);

                                if (match.Success)
                                {
                                    char letra = char.Parse(match.Groups[2].Value);
                                    string binario6Bits = Convert.ToString(int.Parse(match.Groups[2].Value), 2).PadLeft(6, '0');
                                   /* MessageBox.Show(binario6Bits);*/
                                    assemblarBinario += binario6Bits;
                                }
                            }
                            else
                            {
                                char letra = char.Parse(dataSplit[2]);

                                string binario6Bits = "1" + Convert.ToString((int)letra - 96, 2).PadLeft(5, '0');
                                /*MessageBox.Show(binario6Bits);*/

                                assemblarBinario += binario6Bits;
                                if(Data.Data.instruccionsAuxiliar.Count(x=>x.Nombre == binario6Bits)<1) 
                                    Data.Data.instruccionsAuxiliar.Add(new Data.Data.Instruccion { Nombre = binario6Bits, Valor = "0000" });

                            }

                        }
                        else
                        {
                            char letra = char.Parse(dataSplit[1]);
                            string binario6Bits = "1" + Convert.ToString((int)letra - 96, 2).PadLeft(5, '0');
                            assemblarBinario += binario6Bits;
                            MessageBox.Show(binario6Bits);

                            if (Data.Data.instruccionsAuxiliar.Count(x => x.Nombre == binario6Bits) < 1)
                                Data.Data.instruccionsAuxiliar.Add(new Data.Data.Instruccion { Nombre = binario6Bits, Valor = "0000" });

                            if (Data.Data.registros.Count(x => x.NumReg.ToLower().Contains(dataSplit[2])) == 1)
                            {
                                dataSplit[1] = dataSplit[1].Replace(",", "");
                                string pattern = @"(\D+)(\d+)";
                                Match match = Regex.Match(dataSplit[2], pattern);

                                if (match.Success)
                                {
                                    
                                    letra = char.Parse(match.Groups[2].Value);
                                    binario6Bits = Convert.ToString(int.Parse(match.Groups[2].Value), 2).PadLeft(6, '0');
                                    assemblarBinario += binario6Bits;
                                    MessageBox.Show(binario6Bits);
                                }

                            }
                            else
                            {
                                letra = char.Parse(dataSplit[2]);
                                binario6Bits = "1" + Convert.ToString((int)letra - 96, 2).PadLeft(5, '0');
                                assemblarBinario += binario6Bits;
                                MessageBox.Show(binario6Bits);

                                if (Data.Data.instruccionsAuxiliar.Count(x => x.Nombre == binario6Bits) < 1)
                                    Data.Data.instruccionsAuxiliar.Add(new Data.Data.Instruccion { Nombre = binario6Bits, Valor = "0000" });
                            }

                        }

                    }
                    else if (dataSplit[0].Contains("inc") || dataSplit[0].Contains("dec") ||
                            dataSplit[0].Contains("in") || dataSplit[0].Contains("out"))
                    {
                        if (Data.Data.registros.Count(x => x.NumReg.ToLower().Contains(dataSplit[1])) == 1)
                        {
                            string pattern = @"(\D+)(\d+)";
                            Match match = Regex.Match(dataSplit[1], pattern);

                            if (match.Success)
                            {
                                char letra = char.Parse(match.Groups[2].Value);
                                string binario6Bits = Convert.ToString(int.Parse(match.Groups[2].Value), 2).PadLeft(6, '0');

                                assemblarBinario += binario6Bits+"000000";
                            }
                        }
                        else 
                        {
                            char letra = char.Parse(dataSplit[2]);
                            string binario6Bits = "1" + Convert.ToString((int)letra - 96, 2).PadLeft(5, '0');
                            assemblarBinario += binario6Bits;

                            if (Data.Data.instruccionsAuxiliar.Count(x => x.Nombre == binario6Bits) < 1)
                                Data.Data.instruccionsAuxiliar.Add(new Data.Data.Instruccion { Nombre = binario6Bits, Valor = "0000" });
                        }
                    }
                    else if (dataSplit[0].Contains("jmp") || dataSplit[0].Contains("jpn") ||
                            dataSplit[0].Contains("jpc") || dataSplit[0].Contains("jpo") || dataSplit[0].Contains("jpz"))
                    {
                        string subBinary = "";
                        foreach (char letra in dataSplit[1])
                        {
                            string binario6Bits = Convert.ToString((int)letra, 2).PadLeft(8, '0');
                            subBinary += binario6Bits.Substring(0, 2);
                        }

                        if(subBinary.Length < 12 || subBinary.Length > 12)
                        {
                            string binario6Bits = Convert.ToString(int.Parse(subBinary), 2).PadLeft(12, '0');
                            subBinary += binario6Bits;
                        }

                        subBinary = subBinary.Substring(0, 12);

                        if (Data.Data.BanderaMemoria.Count(x => x.Valor.Equals(subBinary)) < 1)
                            Data.Data.BanderaMemoria.Add(new Data.Data.Memoria
                                {
                                    NumMem = -1,
                                    Valor = subBinary
                                }
                            );

                        
                        assemblarBinario += subBinary;
                    }
                    else if (dataSplit.Length == 1 && !data.Contains(".code") && !dataSplit[0].Contains("halt"))
                    {
                        dataSplit[0] = dataSplit[0].Replace(":", "");

                        string subBinary = "";
                        foreach (char letra in dataSplit[0])
                        {
                            string binario6Bits = Convert.ToString((int)letra, 2).PadLeft(8, '0');
                            subBinary += binario6Bits.Substring(0, 2);
                        }

                        if (subBinary.Length < 12 || subBinary.Length > 12)
                        {
                            string binario6Bits = Convert.ToString(int.Parse(subBinary), 2).PadLeft(12, '0');
                            subBinary += binario6Bits;
                        }

                        subBinary = subBinary.Substring(0, 12);
                       
                        
                        if (Data.Data.BanderaMemoria.Count(x => x.Valor.Equals(subBinary)) < 1)
                        {
                            Data.Data.BanderaMemoria.Add(new Data.Data.Memoria
                                {
                                    NumMem = index,
                                    Valor = subBinary
                                }
                            );
                        }
                        else
                        {
                            int indexB = Data.Data.BanderaMemoria.FindIndex(x=>x.Valor.Equals(subBinary));
                            Data.Data.BanderaMemoria[indexB].NumMem = index;
                        }
                        assemblarBinario += subBinary+"1111";
                    }

                    if (!data.Contains(".code"))
                    {
                        Data.Data.memoria[index].Valor = assemblarBinario;
                        index++;
                    }


                }
                else if (!InCode)
                {
                    string[] dataSplit = data.Split(' ');

                    if(dataSplit.Length > 1)
                    {
                        char letra = char.Parse(dataSplit[0]);
                        string binario6Bits = "1" + Convert.ToString((int)letra - 96, 2).PadLeft(5, '0');
                    
                        string binarioValor = Convert.ToString(int.Parse(dataSplit[1]),2).PadLeft(6,'0');

                        if (Data.Data.instruccionsAuxiliar.Count(x => x.Nombre == binario6Bits) < 1)
                            Data.Data.instruccionsAuxiliar.Add(new Data.Data.Instruccion { Nombre = binario6Bits, Valor = binarioValor });
                        else
                        {
                            int indexB = Data.Data.instruccionsAuxiliar.FindIndex(x => x.Nombre.Equals(binario6Bits));
                            Data.Data.instruccionsAuxiliar[indexB].Valor = binarioValor;
                        }
                    }
                }

                assemblarBinario = "";
            }

            return true;
        }


    }
}
