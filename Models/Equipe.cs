using System;
using System.IO;
using System.Collections.Generic;
using E_Players_Models.Interfaces;

namespace E_Players_Models.Models
{
    public class Equipe:EPlayersBase, IEquipe
    {
        public int IdEquipe { get; set; }
        public string Nome { get; set; }
        public string Imagem { get; set; }

        private const string PATH = "Database/equipe.csv";

        public Equipe(){
            CreateFolderAndFile(PATH);
        }

        public string PrepararCSV(Equipe prepararLinhas){
            return $"{prepararLinhas.IdEquipe};{prepararLinhas.Nome};{prepararLinhas.Imagem}";
        }

        public void Create(Equipe novaEquipe)
        {
            string[] linhas = {PrepararCSV(novaEquipe)};
            File.AppendAllLines(PATH, linhas);
        }

        public List<Equipe> ReadAllLines()
        {
            List<Equipe> equipes = new List<Equipe>();
            string[] linhas = File.ReadAllLines(PATH);

            foreach(var item in linhas){
                string[] linha = item.Split(";");

                // Criar o objeto equipe:
                Equipe equipe = new Equipe();

                // Alimentar o objeto equipe:
                equipe.IdEquipe = int.Parse(linha[0]);
                equipe.Nome = linha[1];
                equipe.Imagem = linha[2];

                // Adicionar o objeto equipe na lista de equipes:
                equipes.Add(equipe);
            }

            return equipes;
        }

        public void Update(Equipe atualizarEquipe)
        {
            List<string> linhasParaAtualizar = ReadAllLinesCSV(PATH);

            // removerá a linha que tenha o código a ser alterado:
            linhasParaAtualizar.RemoveAll(x => x.Split(";")[0] == atualizarEquipe.IdEquipe.ToString());

            // adiciona a linha alterada no final do arquivo com o mesmo código:
            linhasParaAtualizar.Add(PrepararCSV(atualizarEquipe));

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