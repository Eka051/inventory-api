using Inventory_api.src.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_api.Application.Interfaces
{
    internal interface IWarehouseRepository
    {
        Task<IEnumerable<Warehouse>> GetAll();
        Task<Warehouse> GetById(int id);
        Task<IEnumerable<Warehouse>> GetByName(string name);
        Task AddAsync(Warehouse warehouse);
        void Update(Warehouse warehouse);
        void Delete(int id);
    }
}
