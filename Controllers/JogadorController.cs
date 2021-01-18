using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using E_Players_Models.Models;
using System;

namespace E_Players_MVC.Controllers
{
    // localhost:5001/Jogador
    [Route ("Jogador")]
    public class JogadorController:Controller
    {
        Jogador jogadorModels = new Jogador();
        Equipe equipeModels = new Equipe();

        // localhost:5001/Equipe/Listar
        [Route ("Listar")]
        public IActionResult Index(){ // método que vai definir os recursos do Controller
            ViewBag.Equipes = equipeModels.ReadAllLines();
            // aqui vamos enviar todas as equipes e enviando-as para a View:
            ViewBag.Jogadores = jogadorModels.ReadAllLines(); // o ViewBag (nesse caso) servirá como um array da lista de Equipes
            return View();
        }

        [Route ("Cadastrar")]
        public IActionResult Cadastrar(IFormCollection formularioDeCadastro){ // vai "aceitar" as informações do "formulário" e envia-las para a tela (View)
            // depois de receber as informações, vamos passa-las para o CSV:
            Jogador novoJogador = new Jogador();
            novoJogador.IdJogador = Int32.Parse(formularioDeCadastro["IdJogador"]);
            novoJogador.Nome = formularioDeCadastro["Nome"];
            novoJogador.Email = formularioDeCadastro["Email"];
            novoJogador.Senha = formularioDeCadastro["Senha"];

            jogadorModels.Create(novoJogador); // vamos criar as linhas no CSV 
            ViewBag.Jogadores = jogadorModels.ReadAllLines(); // vai jogar todas as informações novas pra dentro do ViewBag (tipo um array)

            return LocalRedirect("~/Jogador/Listar"); // vai redirecionar o usuário para uma outra página
        }

        // Excluir Jogadores:
        [Route ("{id}")]
        public IActionResult Excluir(int id){
            jogadorModels.Delete(id);

            ViewBag.Jogadores = jogadorModels.ReadAllLines();
            return LocalRedirect("~/Jogador/Listar");
        }
    }
}