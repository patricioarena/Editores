﻿using Application.IFactory;
using Application.IServices;
using AutoMapper;
using Dominio.DTOs;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Application.Services
{
    public class ServiceEscritosTexto : IServiceEscritosTexto
    {
        private readonly DbContext _Context;
        private readonly IAbstractServiceFactory _Service;

        public ServiceEscritosTexto(IAbstractContextFactory Factory, IAbstractServiceFactory service)
        {
            _Context = Factory.CreateContext();
            _Service = service;
        }

        public List<EscritosTexto> GetAllEscritosTextos()
        {
            return _Context.Set<EscritosTexto>().ToList();
        }

        public EscritosTexto GetEscritosTextoById(int escritoTextoID)
        {
            return _Context.Set<EscritosTexto>().Where(e => e.Id.Equals(escritoTextoID)).FirstOrDefault();
        }

        public EscritosTexto GetUltimoEscritosTexto()
        {
            string conn = _Context.Database.GetDbConnection().ConnectionString;
            return new DataAccess.DbManager(conn).ExecuteSingle<EscritosTexto>("dbo.GetUltimoEscritoTexto", null);
        }

        public int SetEscritoTexto(EscritosTextoDto escritosTexto)
        {
            EscritosTexto EscritosTexto = _Service.Mapper().Map<EscritosTexto>(escritosTexto);
            _Context.Set<EscritosTexto>().Add(EscritosTexto);
            return _Context.SaveChanges();
        }

    }
}
