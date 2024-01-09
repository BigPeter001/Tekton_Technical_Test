using Application.DTOs;
using Application.interfaces;
using Application.wrappers;
using AutoMapper;
using Domain.entities;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Application.features.products.queries.getProductById
{
    public class GetProdcutByIdQuery :IRequest<Response<ProductDto>>
    {
        public int Id { get; set; }

        public class GetProdcutByIdQueryHandler : IRequestHandler<GetProdcutByIdQuery, Response<ProductDto>>
        {
            private readonly IRepositoryAsync<Product> _repositoryAsync;
            //Inyección Mapeo automático
            private readonly IMapper _mapper;

            public GetProdcutByIdQueryHandler(IRepositoryAsync<Product> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;              
            }

            public async Task<Response<ProductDto>> Handle(GetProdcutByIdQuery request, CancellationToken cancellationToken)
            {
                var producto = await _repositoryAsync.GetByIdAsync(request.Id);

                if (producto == null)
                {
                    throw new KeyNotFoundException($"Resgistro no encontrado con el id {request.Id}");
                }
                else
                {
                    var dtoProduct = _mapper.Map<ProductDto>(producto);
                    return new Response<ProductDto>(dtoProduct);
                }
            }
        }

    }
}
