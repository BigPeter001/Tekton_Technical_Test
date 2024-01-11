using Application.exceptions;
using Application.features.products.commands.insertProductCommand;
using Application.interfaces;
using Application.wrappers;
using AutoMapper;
using Domain.entities;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.features.products.commands.updateProductCommand
{
    public class UpdateProductCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public  string Name { get; set; }
        public  string StatusName { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public double FinalPrice { get; set; }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Response<int>>
    {
        //Innyección de patron Repository 
        private readonly IRepositoryAsync<Product> _repositoryAsync;
        //Inyección Mapeo automático
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IRepositoryAsync<Product> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;

        }

        /// <summary>
        /// Este método realiza se encaraga de manejar la inserrción del command
        /// a la base de datos.
        /// </summary>
        /// <param name="request">request de tipo InsertProductCommand.</param>
        /// <param name="cancellationToken">request de tipo CancellationToken.</param>
        /// <returns>devuelve objeto de tipo Response<int> 
        /// con la respuesta de la ejecución del command.</returns>
        public async Task<Response<int>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var producto = await _repositoryAsync.GetByIdAsync(request.Id);

            if (producto == null)
            {
                throw new KeyNotFoundException($"Resgistro no encontrado con el id {request.Id}");
            }
            else 
            {
                producto.Name = request.Name;
                producto.StatusName = request.StatusName;
                producto.Stock = request.Stock;
                producto.Description = request.Description;
                producto.Price = request.Price;
                producto.Discount = request.Discount;                
                producto.FinalPrice = (producto.Price * (100 - producto.Discount)) / 100; ;

                await _repositoryAsync.UpdateAsync(producto);

                return new Response<int>(producto.Id);
            }
        }

    }
}
