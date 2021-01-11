using System.Collections.Generic;
using E_Players_Models.Models;

namespace E_Players_Models.Interfaces
{
    public interface INoticia
    {
        void Create(Noticia novaNoticia);
        List<Noticia> ReadAllLines();
        void Update(Noticia atualizarNoticias);
        void Delete(int apagarPeloId);
    }
}