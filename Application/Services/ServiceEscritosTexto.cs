using DataAccess;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Services
{
    public class ServiceEscritosTexto : IServiceEscritosTexto
    {
        private readonly DbContext _Context;

        public ServiceEscritosTexto(IAbstractFactory factory)
        {
            _Context = factory.Create(Context.InMemoryDbContex);
        }

        public List<EscritosTexto> GetEscritosTextos()
        {
            return _Context.Set<EscritosTexto>().ToList();
        }

        public EscritosTexto GetEscritosTextoById(int escritoTextoID)
        {
            return _Context.Set<EscritosTexto>().Where(e => e.Id.Equals(escritoTextoID)).FirstOrDefault();
        }

        public void SetEscritoTexto(EscritosTexto escritosTexto)
        {
            _Context.Set<EscritosTexto>().Add(escritosTexto);
            _Context.SaveChanges();
        }
    }
}
