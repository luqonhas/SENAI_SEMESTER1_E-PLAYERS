using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using E_Players_Models.Models;
using System;
using System.Collections.Generic;

namespace E_Players_MVC.Controllers
{
    // localhost:5001/Login
    [Route ("Login")]
    public class LoginController : Controller
    {
        Jogador jogadorModels = new Jogador();

        [TempData]
        public string Mensagem { get; set; }

        public IActionResult Index(){
            return View();
        }

        [Route("Logar")]
        public IActionResult Logar(IFormCollection form)
        {
            // Lemos todos os arquivos do CSV
            List<string> csv = jogadorModels.ReadAllLinesCSV("Database/jogador.csv");

            // Verificamos se as informações passadas existe na lista de string
            var logado = 
            csv.Find(
                x => 
                x.Split(";")[2] == form["Email"] && 
                x.Split(";")[3] == form["Senha"]
            );

            // Redirecionamos o usuário logado caso encontrado
            if(logado != null){
                // para salvar a informação do nome na sessão:
                HttpContext.Session.SetString("nomeDoLogin", logado.Split(";")[1]);

                return LocalRedirect("~/");
            }

            Mensagem = "Tem algo errado aí, tente novamente...";
            return LocalRedirect("~/Login");
        }

        [Route ("Logout")]
        public IActionResult Logout(){
            HttpContext.Session.Remove("nomeDoLogin"); 
            
            return LocalRedirect("~/");
        }
    }
}