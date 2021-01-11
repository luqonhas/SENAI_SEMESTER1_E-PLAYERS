using System.Collections.Generic;
using E_Players_Models.Models;

namespace E_Players_Models.Interfaces
{
    public interface IEquipe
    {
        void Create(Equipe novaEquipe);
        List<Equipe> ReadAllLines();
        void Update(Equipe atualizarEquipe);
        void Delete(int apagarPeloId);
    }
}