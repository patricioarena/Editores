using Dominio.DTOs;
using Dominio.Entities;
using System.Collections.Generic;

namespace Application.IServices
{
    public interface IServiceEscritosTexto
    {
        EscritosTexto GetEscritosTextoById(int escritoTextoID);
        List<EscritosTexto> GetEscritosTextos();
        void SetEscritoTexto(EscritosTextoDto escritosTexto);
    }
}