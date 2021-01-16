using System;
using System.IO;
using E_Players_Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Players_MVC.Controllers
{
    // localhost:5001/Equipe
    [Route ("Equipe")]
    public class EquipeController : Controller // a classe Controller (que é da biblioteca AspNetCore) vai trazer vários métodos para poder usar
    {
        Equipe equipeModels = new Equipe();
        
        [Route ("Listar")]
        public IActionResult Index(){ // método que vai definir os recursos do Controller
            // aqui vamos enviar todas as equipes e enviando-as para a View:
            ViewBag.Equipes = equipeModels.ReadAllLines(); // o ViewBag (nesse caso) servirá como um array da lista de Equipes
            return View();
        }
        [Route ("Cadastrar")]
        public IActionResult Cadastrar(IFormCollection formularioDeCadastro){ // vai "aceitar" as informações do "formulário" e envia-las para a tela (View)
            // depois de receber as informações, vamos passa-las para o CSV:
            Equipe novaEquipe = new Equipe();
            novaEquipe.IdEquipe = Int32.Parse(formularioDeCadastro["IdEquipe"]);
            novaEquipe.Nome = formularioDeCadastro["Nome"];
            novaEquipe.Imagem = formularioDeCadastro["Imagem"];

            // início do upload:
            
            // primeiramente, vamos verificar se no tal formulário existe associado ou não:
            if(formularioDeCadastro.Files.Count > 0){ // o Files é uma lista de arquivos que foram enviados pelo formulário || será recebido apenas um arquivo, então o indice será [0] || temos o Count que irá contar, então se o usuário não subir nenhum arquivo, o Count vai ser 0
                // se o usuário anexou um arquivo (ou seja, o Count tem mais que 0):
                var receberArquivo = formularioDeCadastro.Files[0]; // foi criado uma "var" (variável) para receber/armazenar o arquivo (o índice [0])
                var pastaCaminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Equipes"); // essa "var" servirá para armazenar o caminho que essas imagens seguirão || o método Combine fará uma combinação de caminhos || o Combine pegará o diretório atual (o domínio do projeto, onde está rodando) e juntar com o "wwwroot" (onde fica o css, js e lib / e onde ficará a pasta com as imagens)

                if(!Directory.Exists(pastaCaminhoArquivo)){ // aqui será verificado se a pasta existe ou não
                    Directory.CreateDirectory(pastaCaminhoArquivo); // se não existir, será criada
                }

                // aqui juntará a pasta com o arquivo:
                //                                              localhost:5001          + "wwwroot/img/" + pasta Equipes      + arquivo.jpg
                var caminhoPastaArquivo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", pastaCaminhoArquivo, receberArquivo.FileName); // o FileName irá captar o nome do arquivo que o usuário colocar (por exemplo, se o usuário colocar o nome do arquivo como "Logo.jpg", o FileName vai salvar esse nome e enviá-lo junto com o arquivo da imagem para dentro da pasta)

                // o using será utilizado para definir um método de manipulação de arquivo
                using (var manipulacao = new FileStream(caminhoPastaArquivo, FileMode.Create)){ // o FileStream recebe várias sobrecargas, sendo uma delas, o caminho de um determinado arquivo que será manipulado (será possível não só salvar o arquivo, como também deletar, copiar, mover o arquivo) || poderiamos fazer tudo aquilo que está na variável "caminhoPastaArquivo", mas ficará mais fácil acoplar tudo em uma de uma vez só || o "using" está sendo utilizado, pois está sendo trabalhado com arquivo || FileMode é um modo de criação
                    // o arquivo que vamos salvar será o receberArquivo, pois é ele que está com a imagem:
                    // aqui estamos realmente salvando o arquivo:
                    receberArquivo.CopyTo(manipulacao); // será feito uma cópia do arquivo para dentro da "manipulacao" || a variavel "manipulacao" contém o caminho que este arquivo será salvo e o que será feito com ele
                }

                // aqui vamos definir na propriedade que será salva no CSV:
                novaEquipe.Imagem = receberArquivo.FileName; // aqui nós vamos definir o nome que o arquivo (a imagem) virá para dentro da pasta, que no caso será o mesmo nome que o usuário colocar
                
            } // tudo isso acima acontece se o usuário ENVIAR o arquivo... 

            // porém, se o usuário NÃO ENVIAR (o usuário enviou o ID e o nome, mas o arquivo não) o arquivo...
            else{
                // será atribuída uma imagem padrão:
                novaEquipe.Imagem = "padrão.png";
            }

            // fim do upload

            equipeModels.Create(novaEquipe); // vamos criar as linhas no CSV
            ViewBag.Equipes = equipeModels.ReadAllLines(); // vai jogar todas as informações novas pra dentro do ViewBag (tipo um array)

            return LocalRedirect("~/Equipe/Listar"); // vai redirecionar o usuário para uma outra página
        }

        [Route ("{id}")]
        // Excluir Equipes:
        public IActionResult Excluir(int id){
            equipeModels.Delete(id);

            ViewBag.Equipes = equipeModels.ReadAllLines();
            return LocalRedirect("~/Equipe/Listar");
        }
    }
}