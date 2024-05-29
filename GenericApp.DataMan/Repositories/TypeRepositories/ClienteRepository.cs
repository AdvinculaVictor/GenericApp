using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericApp.Domain.Entities;
using GenericApp.Domain.Repositories;

namespace GenericApp.DataMan.Repositories.TypeRepositories
{
    public class ClienteRepository : GenericRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(GenericAppDBContext context) : base(context) { }
    }
}