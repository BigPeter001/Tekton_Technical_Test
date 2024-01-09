using Application.DTOs;
using Application.features.products.commands.insertProductCommand;
using AutoMapper;
using Domain.entities;

namespace Application.mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region DTOs
            CreateMap<Product, ProductDto>();
            #endregion

            #region commands
            CreateMap<InsertProductCommand, Product>();
            #endregion
        }
    }
}
