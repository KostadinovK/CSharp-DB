using System.Collections.Generic;
using AutoMapper;
using ProductShop.Dtos.Export;
using ProductShop.Dtos.Import;
using ProductShop.Models;

namespace ProductShop
{
    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            //Import
            this.CreateMap<ImportUserDTO, User>();
            this.CreateMap<ImportProductDTO, Product>();
            this.CreateMap<ImportCategoryDTO, Category>();
            this.CreateMap<ImportCategoryProductDTO, CategoryProduct>();

            //Export
            
            //Product
            this.CreateMap<Product, ExportProductInRangeDTO>()
                .ForMember(x => x.BuyerName, y => y.MapFrom(s => s.Buyer.FirstName + " " + s.Buyer.LastName));
        }
    }
}
