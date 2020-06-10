using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.IFactory
{
    public interface IAbstractServiceFactory
    {
        public IMapper Mapper();
        public ILogger Logger();
    }
}
