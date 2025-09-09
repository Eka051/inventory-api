using Inventory_api.src.Core.Entities;

namespace Inventory_api.Application.Interfaces
{
    public interface IWarehouseRepository
    {
        Task<IEnumerable<Warehouse>> GetAll();
        Task<Warehouse?> GetById(int id);
        Task<IEnumerable<Warehouse>> GetByName(string name);
        Task AddAsync(Warehouse warehouse);
        void Update(Warehouse warehouse);
        void Delete(int id);
    }
}
