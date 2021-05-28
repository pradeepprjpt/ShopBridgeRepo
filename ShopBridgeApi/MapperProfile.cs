using AutoMapper;
using ShopBridgeApi.Models;
using ShopBridgeApi.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridgeApi
{
    public class MapperProfile :Profile
    {
        public MapperProfile()
        {
            CreateMap<InventoryAddRequest, Inventory>();
            CreateMap<InventoryUpdateRequest, Inventory>();
        }
    }
}
