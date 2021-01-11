using System;
using System.Collections.Generic;
using System.IO;
using E_Players_Models.Interfaces;

namespace E_Players_Models.Models
{
    public class Partida : EPlayersBase, IPartida
    {
        public Partida(int idPartida, int idEquipe1, int idJogador1, int idEquipe2, int idJogador2, DateTime horarioInicio, DateTime horarioTermino)
        {
            this.IdPartida = idPartida;
            this.IdEquipe1 = idEquipe1;
            this.IdJogador1 = idJogador1;
            this.IdEquipe2 = idEquipe2;
            this.IdJogador2 = idJogador2;
            this.HorarioInicio = horarioInicio;
            this.HorarioTermino = horarioTermino;

        }
        public int IdPartida { get; set; }
        public int IdEquipe1 { get; set; }
        public int IdJogador1 { get; set; }
        public int IdEquipe2 { get; set; }
        public int IdJogador2 { get; set; }
        public DateTime HorarioInicio { get; set; }
        public DateTime HorarioTermino { get; set; }

        private const string PATH = "Database/partida.csv";
        public Partida()
        {
            CreateFolderAndFile(PATH);
        }

        public string PrepararCSV(Partida prepararLinhas)
        {
            return $"{prepararLinhas.IdPartida};{prepararLinhas.IdEquipe1};{prepararLinhas.IdJogador1};{prepararLinhas.IdEquipe2};{prepararLinhas.IdJogador2};{prepararLinhas.HorarioInicio};{prepararLinhas.HorarioTermino}";
        }

        public void Create(Partida novaPartida)
        {
            string[] linhas = { PrepararCSV(novaPartida) };
            File.AppendAllLines(PATH, linhas);
        }

        public List<Partida> ReadAllLines()
        {
            List<Partida> partidas = new List<Partida>();
            string[] linhas = File.ReadAllLines(PATH);

            foreach(var item in linhas){
                string[] linha = item.Split(";");

                // Criar o objeto equipe:
                Partida partida = new Partida();

                // Alimentar o objeto equipe:
                partida.IdPartida = int.Parse(linha[0]);
                partida.IdEquipe1 = int.Parse(linha[1]);
                partida.IdJogador1 = int.Parse(linha[2]);
                partida.IdEquipe2 = int.Parse(linha[3]);
                partida.IdJogador2 = int.Parse(linha[4]);
                partida.HorarioInicio = DateTime.Parse(linha[5]);
                partida.HorarioTermino = DateTime.Parse(linha[6]);

                // Adicionar o objeto equipe na lista de equipes:
                partidas.Add(partida);
            }

            return partidas;
        }

        public void Update(Partida atualizarPartidas)
        {
            List<string> linhasParaAtualizar = ReadAllLinesCSV(PATH);

            // removerá a linha que tenha o código a ser alterado:
            linhasParaAtualizar.RemoveAll(x => x.Split(";")[0] == atualizarPartidas.IdPartida.ToString());

            // adiciona a linha alterada no final do arquivo com o mesmo código:
            linhasParaAtualizar.Add(PrepararCSV(atualizarPartidas));

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