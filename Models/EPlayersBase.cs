using System.Collections.Generic;
using System.IO;

namespace E_Players_Models.Models
{
    public class EPlayersBase
    {
        public void CreateFolderAndFile(string path){
            string folder = path.Split("/")[0];

            if(!Directory.Exists(folder)){
                Directory.CreateDirectory(folder);
            }

            if(!File.Exists(path)){
                File.Create(path).Close();
            }
        }

        public List<string> ReadAllLinesCSV(string path){
            List<string> linhasLidas = new List<string>();
            
            // using = vai abrir e fechar determinado tipo de arquivo ou conexão
            // StreamReader = vai ler as informações do CSV
            using (StreamReader file = new StreamReader(path)){
                string linha;
                while((linha = file.ReadLine()) != null){
                    linhasLidas.Add(linha);
                }
            }
            return linhasLidas;
        }
        public void ReWriteCSV(string path, List<string> linhasLidas){
            // StreamWriter = vai escrever nos arquivos:
            using (StreamWriter output = new StreamWriter(path)){
                foreach (var item in linhasLidas){
                    output.Write(item + '\n');
                }
            }
        }
    }
}