using ShopBridgeApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopBridgeApi.DAO
{
    public interface IInventoryRepository
    {
        public Task Add(Inventory inventory);
        public Task Update(Inventory inventory);
        public Task Delete(Inventory inventory);
        public Task<Inventory> Get(string name);
        public Task<Inventory> Get(int id);
        public Task<IEnumerable<Inventory>> GetAll();

    }
}
