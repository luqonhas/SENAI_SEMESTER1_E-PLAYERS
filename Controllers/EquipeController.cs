using System;
using E_Players_Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Players_MVC.Controllers
{
    public class EquipeController : Controller // a classe Controller (que é da biblioteca AspNetCore) vai trazer vários métodos para poder usar
    {
        Equipe equipeModels = new Equipe();
        public IActionResult Index(){ // método que vai definir os recursos do Controller
            // aqui vamos enviar todas as equipes e enviando-as para a View:
            ViewBag.Equipes = equipeModels.ReadAllLines(); // o ViewBag (nesse caso) servirá como um array da lista de Equipes
            return View();
        }
        public IActionResult Cadastrar(IFormCollection formularioDeCadastro){ // vai "aceitar" as informações do "formulário" e envia-las para a tela (View)
            // depois de receber as informações, vamos passa-las para o CSV:
            Equipe novaEquipe = new Equipe();
            novaEquipe.IdEquipe = Int32.Parse(formularioDeCadastro["IdEquipe"]);
            novaEquipe.Nome = formularioDeCadastro["Nome"];
            novaEquipe.Imagem = formularioDeCadastro["Imagem"];

            equipeModels.Create(novaEquipe); // vamos criar as linhas no CSV 
            ViewBag.Equipes = equipeModels.ReadAllLines(); // vai jogar todas as informações novas pra dentro do ViewBag (tipo um array)

            return LocalRedirect("~/Equipe"); // vai redirecionar o usuário para uma outra página
        }
    }
}