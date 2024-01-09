using Application.DTOs;
using Application.interfaces;
using Application.specifications;
using Application.wrappers;
using AutoMapper;
using Domain.entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.features.products.queries.getProductAll
{
    public class GetAllProductsQuery : IRequest<PageResponse<List<ProductDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Name { get; set; }
        public string? StatusName { get; set; }
        public string? Description { get; set; }

        public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, PageResponse<List<ProductDto>>>
        {
            private readonly IRepositoryAsync<Product> _repositoryAsync;
            //Inyección Mapeo automático
            private readonly IMapper _mapper;

            public GetAllProductsQueryHandler(IRepositoryAsync<Product> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<PageResponse<List<ProductDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
            {
                var products = await _repositoryAsync.ListAsync(new PagedProductsSpecification(request.PageNumber, request.PageSize, request.Name, request.StatusName, request.Description));
                var productoDto = _mapper.Map<List<ProductDto>>(products);

                return new PageResponse<List<ProductDto>>(productoDto, request.PageNumber, request.PageSize);
            }
        }
    }
}
