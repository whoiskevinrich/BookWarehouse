using AutoMapper;
using BookWarehouse.Core.Domain;
using BookWarehouse.Web.Models;

namespace BookWarehouse.Web
{
    public class AutomapperConfig : Profile
    {
        protected override void Configure()
        {
            CreateMap<Warehouse, WarehouseModel>();
            CreateMap<Title, TitleModel>();
            CreateMap<InventoryItem, InventoryItemModel>();

            CreateMap<WarehouseModel, Warehouse>();
            CreateMap<TitleModel, Title>();
            CreateMap<InventoryItemModel, InventoryItem>();
        }
    }
}
