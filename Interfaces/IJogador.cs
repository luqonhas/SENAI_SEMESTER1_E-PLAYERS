using System.Collections.Generic;
using E_Players_Models.Models;

namespace E_Players_Models.Interfaces
{
    public interface IJogador
    {
        void Create(Jogador novoJogador);
        List<Jogador> ReadAllLines();
        void Update(Jogador atualizarJogadores);
        void Delete(int apagarPeloId);
    }
}