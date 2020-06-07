using Dominio.Entities;
using System.Collections.Generic;

namespace Application.Services
{
    public interface IServiceEscritosTexto
    {
        EscritosTexto GetEscritosTextoById(int escritoTextoID);
        List<EscritosTexto> GetEscritosTextos();
        void SetEscritoTexto(EscritosTexto escritosTexto);
    }
}