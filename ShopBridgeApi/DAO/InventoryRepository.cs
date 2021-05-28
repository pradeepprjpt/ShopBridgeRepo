using Microsoft.EntityFrameworkCore;
using ShopBridgeApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopBridgeApi.DAO
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly ShopBridgeContext _shopBridgeContext;
        public InventoryRepository(ShopBridgeContext shopBridgeContext)
        {
            _shopBridgeContext = shopBridgeContext;
        }
        public async Task Add(Inventory request)
        {
            request.CreatedOn = DateTime.Now.Date;
            await _shopBridgeContext.Inventories.AddAsync(request).ConfigureAwait(false);
            await _shopBridgeContext.SaveChangesAsync();
        }

        public async Task<Inventory> Get(string name)
        {
            var inventory = await _shopBridgeContext.Inventories.FirstOrDefaultAsync(i => i.Name == name).ConfigureAwait(false);
            return inventory;
        }
        public async Task<Inventory> Get(int id)
        {
            var inventory = await _shopBridgeContext.Inventories.FirstOrDefaultAsync(i => i.Id == id).ConfigureAwait(false);
            return inventory;
        }

        public async Task<IEnumerable<Inventory>> GetAll()
        {
            var inventories = await _shopBridgeContext.Inventories.ToListAsync().ConfigureAwait(false);
            return inventories;
        }

        public async Task Update(Inventory inventory)
        {
            var dbEntity = await _shopBridgeContext.Inventories.FirstOrDefaultAsync(i => i.Id == inventory.Id).ConfigureAwait(false);
            if (dbEntity == null)
            {
                return;
            }

            dbEntity.Name = inventory.Name;
            dbEntity.Description = inventory.Description;
            dbEntity.Price = inventory.Price;
            dbEntity.Quantity = inventory.Quantity;
            dbEntity.ModifiedBy = inventory.ModifiedBy;
            dbEntity.ModifiedOn = DateTime.Now.Date;

            await _shopBridgeContext.SaveChangesAsync();
        }
        public async Task Delete(Inventory inventory)
        {
            _shopBridgeContext.Inventories.Remove(inventory);
            await _shopBridgeContext.SaveChangesAsync();
        }
    }
}
