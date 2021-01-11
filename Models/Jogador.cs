using System.Collections.Generic;
using System.IO;
using E_Players_Models.Interfaces;

namespace E_Players_Models.Models
{
    public class Jogador:EPlayersBase, IJogador
    {
        public int IdJogador { get; set; }
        public string Nome { get; set; }
        public int IdEquipe { get; set; }
        
        private const string PATH = "Database/jogador.csv";
        public Jogador(){
            CreateFolderAndFile(PATH);
        }

        public string PrepararCSV(Jogador prepararLinhas){
            return $"{prepararLinhas.IdJogador};{prepararLinhas.Nome};{prepararLinhas.IdEquipe}";
        }

        public void Create(Jogador novoJogador)
        {
            string[] linhas = {PrepararCSV(novoJogador)};
            File.AppendAllLines(PATH, linhas);
        }

        public List<Jogador> ReadAllLines()
        {
            List<Jogador> jogadores = new List<Jogador>();
            string[] linhas = File.ReadAllLines(PATH);

            foreach(var item in linhas){
                string[] linha = item.Split(";");

                // Criar o objeto equipe:
                Jogador jogador = new Jogador();

                // Alimentar o objeto equipe:
                jogador.IdJogador = int.Parse(linha[0]);
                jogador.Nome = linha[1];
                jogador.IdEquipe = int.Parse(linha[2]);

                // Adicionar o objeto equipe na lista de equipes:
                jogadores.Add(jogador);
            }

            return jogadores;
        }

        public void Update(Jogador atualizarJogadores)
        {
            List<string> linhasParaAtualizar = ReadAllLinesCSV(PATH);

            // removerá a linha que tenha o código a ser alterado:
            linhasParaAtualizar.RemoveAll(x => x.Split(";")[0] == atualizarJogadores.IdJogador.ToString());

            // adiciona a linha alterada no final do arquivo com o mesmo código:
            linhasParaAtualizar.Add(PrepararCSV(atualizarJogadores));

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