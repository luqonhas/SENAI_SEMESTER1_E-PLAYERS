using System.Collections.Generic;
using System.IO;
using E_Players_Models.Interfaces;

namespace E_Players_Models.Models
{
    public class Noticia:EPlayersBase, INoticia
    {
        public int IdNoticia { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }
        public string Imagem { get; set; }   

        private const string PATH = "Database/noticia.csv";

        
        public Noticia(){
            CreateFolderAndFile(PATH);
        }

        public string PrepararCSV(Noticia prepararLinhas){
            return $"{prepararLinhas.IdNoticia};{prepararLinhas.Titulo};{prepararLinhas.Texto};{prepararLinhas.Imagem}";
        }

        public void Create(Noticia novaNoticia)
        {
            string[] linhas = {PrepararCSV(novaNoticia)};
            File.AppendAllLines(PATH, linhas);
        }

        public List<Noticia> ReadAllLines()
        {
            List<Noticia> noticias = new List<Noticia>();
            string[] linhas = File.ReadAllLines(PATH);

            foreach(var item in linhas){
                string[] linha = item.Split(";");

                // Criar o objeto equipe:
                Noticia noticia = new Noticia();

                // Alimentar o objeto equipe:
                noticia.IdNoticia = int.Parse(linha[0]);
                noticia.Titulo = linha[1];
                noticia.Texto = linha[2];
                noticia.Imagem = linha[3];

                // Adicionar o objeto equipe na lista de equipes:
                noticias.Add(noticia);
            }

            return noticias;
        }

        public void Update(Noticia atualizarNoticias)
        {
            List<string> linhasParaAtualizar = ReadAllLinesCSV(PATH);

            // removerá a linha que tenha o código a ser alterado:
            linhasParaAtualizar.RemoveAll(x => x.Split(";")[0] == atualizarNoticias.IdNoticia.ToString());

            // adiciona a linha alterada no final do arquivo com o mesmo código:
            linhasParaAtualizar.Add(PrepararCSV(atualizarNoticias));

            // reescreve o csv com as alterações:
            ReWriteCSV(PATH, linhasParaAtualizar);
        }

        public void Delete(int apagarPeloId)
        {
            List<string> linhasParaAtualizar = ReadAllLinesCSV(PATH);

            // removerá a linha que tenha o código a ser alterado:
            linhasParaAtualizar.RemoveAll(x => x.Split(";")[0] == apagarPeloId.ToString());

            // reescreve o csv com as alterações:
            ReWriteCSV(PATH, linhasParaAtualizar);
        }
    }
}