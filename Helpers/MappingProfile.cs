using AutoMapper;
using Inveny.Collections;
using Inveny.Models;
using Inveny.Requests;

namespace Inveny.Helpers;

public class MappingProfile : Profile
{
  public MappingProfile()
  {
    CreateMap<Supplier, SupplierCollections>();
    CreateMap<SupplierCollections, Supplier>();
    CreateMap<Supplier, SupplierRequest>();
    CreateMap<SupplierRequest, Supplier>();
    // listing relation to Inventory
    CreateMap<Supplier, InventorySupplierCustom>();


    CreateMap<Inventory, InventoryCollection>().ForMember(dest => dest.Supplier, opt => opt.MapFrom(src => src.Supplier));
    CreateMap<InventoryCollection, Inventory>();
    CreateMap<Inventory, InventoryRequest>();
    CreateMap<InventoryRequest, Inventory>();
  }
}