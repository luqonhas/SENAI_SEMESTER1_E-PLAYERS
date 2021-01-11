using System.Collections.Generic;
using E_Players_Models.Models;

namespace E_Players_Models.Interfaces
{
    public interface IPartida
    {
        void Create(Partida novaPartida);
        List<Partida> ReadAllLines();
        void Update(Partida atualizarPartidas);
        void Delete(int apagarPeloId);
    }
}