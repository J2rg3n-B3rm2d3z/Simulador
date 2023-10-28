using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;

namespace Simulador.Screens.AssemblerFiles
{
    internal class sintaxis
    {
        private List<string> dataFile = new List<string>();
        public sintaxis(List<string> dataFile) 
        {
            this.dataFile=dataFile;
        }

        public void setDataFile(List<string> dataFile) {
            this.dataFile = dataFile;
        }


        public Boolean SintaxisResult()
        {
            
            if (this.dataFile.Count == 0) return false;


            for (int i = 0; i < this.dataFile.Count; i++)
            {
                dataFile[i] = this.dataFile[i].ToLower();
            }

            if (this.dataFile.Count(x => x == ".data") <= 0 || this.dataFile.Count(x => x == ".data") > 1) return false;
            if (this.dataFile.Count(x => x == ".code") <= 0 || this.dataFile.Count(x => x == ".code") > 1) return false;
            if (this.dataFile.Count(x => x == "halt") <= 0) return false;
            
            int index = dataFile.IndexOf(".data");
            List<string> dataFileData = dataFile.GetRange(index, dataFile.Count - index);
            List<string> dataFileData1 = dataFile.GetRange(index, dataFile.Count - index);

            foreach (var item in dataFileData)
            {
                /**/
                if (!item.Contains(".data"))
                {
                    
                    string[] d = item.Split(' ');
                    
                    dataFileData1.Add(d[0]);
                }
            }
            /*Recorrer info dentro de .code y .data*/

            /*dataFile.RemoveAll(string.IsNullOrEmpty);*/
            bool InCode = false;
            //List<string> banderas = new List<string>();

            foreach (string data in this.dataFile)
            {
                if (data.Contains(".code"))
                {
                    InCode = true;
                }
                else if(data.Contains(".data"))
                {
                    InCode = false;
                }

                if(InCode)
                {
                    string[] dataSplit = data.Split(' ');
                    if (dataSplit.Length > 1)
                    {
                        if (dataSplit[0].Contains("move") || dataSplit[0].Contains("add") ||
                            dataSplit[0].Contains("sub") || dataSplit[0].Contains("mul") ||
                            dataSplit[0].Contains("div") || dataSplit[0].Contains("mod"))
                        {
                            if (dataSplit.Length < 3) return false;

                            if (!string.IsNullOrEmpty(dataSplit[1]) && !string.IsNullOrEmpty(dataSplit[2]))
                            {
                                
                                
                                if (dataSplit[1].EndsWith(","))
                                {
                                    
                                    dataSplit[1] = dataSplit[1].Replace(",", "");
                                    dataSplit[1] = dataSplit[1].Replace(" ", "");
                                    
                                    if (Data.Data.registros.Count(x => x.NumReg.ToLower().Contains(dataSplit[1])) == 1 || dataFileData.Count(x => x.Contains(dataSplit[1])) == 1)
                                    {

                                       

                                        if (!Data.Data.registros.Any(x => x.NumReg.ToLower().Contains(dataSplit[2])) && !dataFileData1.Contains(dataSplit[2])) return false;
                                        
                                        
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else if (dataSplit[0].Contains("inc") || dataSplit[0].Contains("dec") ||
                            dataSplit[0].Contains("in") || dataSplit[0].Contains("out"))
                        {
                            if (!string.IsNullOrEmpty(dataSplit[1]))
                            {
                                
                                if (!Data.Data.registros.Any(x => x.NumReg.ToLower().Contains(dataSplit[1])) && !dataFileData1.Contains(dataSplit[1])) return false;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else if(dataSplit[0].Contains("jmp") || dataSplit[0].Contains("jpn") ||
                            dataSplit[0].Contains("jpc") || dataSplit[0].Contains("jpo") || dataSplit[0].Contains("jpz"))
                        {
                            if (!string.IsNullOrEmpty(dataSplit[1]))
                            {
                                if (dataFile.Count(x => x.Contains(dataSplit[1]+":"))!=1) return false;
                                
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    else if (dataSplit.Length == 1 && !data.Contains(".code") && !dataSplit[0].Contains("halt"))
                    {
                        if (!dataSplit[0].EndsWith(":")) return false;
                    }
                }
                else if(!data.Contains(".data") && !data.Contains("halt"))
                {
                    /*MessageBox.Show(data);*/
                    string[] dataSplit = data.Split(' ');
                    
                    if (dataSplit.Length != 2) return false;
                    if (!int.TryParse(dataSplit[1], out int number)) return false;

                    if (Data.Data.registros.Count(x => x.NumReg.ToLower().Contains(dataSplit[0])) >= 1) return false;
                    if (Data.Data.instrucciones.Count(x => x.Nombre.ToLower().Equals(dataSplit[0])) >= 1) return false;
                }
            }

            return true;
        }
    }
}
