using Application.features.products.commands.updateProductCommand;
using Application.interfaces;
using Application.wrappers;
using AutoMapper;
using Domain.entities;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.features.products.commands.deleteProductCommand
{
    public class DeleteProductCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }

        public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Response<int>>
        {
            //Innyección de patron Repository 
            private readonly IRepositoryAsync<Product> _repositoryAsync;

            public DeleteProductCommandHandler(IRepositoryAsync<Product> repositoryAsync)
            {
                _repositoryAsync = repositoryAsync;
            }

            /// <summary>
            /// Este método realiza se encaraga de manejar la inserrción del command
            /// a la base de datos.
            /// </summary>
            /// <param name="request">request de tipo InsertProductCommand.</param>
            /// <param name="cancellationToken">request de tipo CancellationToken.</param>
            /// <returns>devuelve objeto de tipo Response<int> 
            /// con la respuesta de la ejecución del command.</returns>
            public async Task<Response<int>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                var producto = await _repositoryAsync.GetByIdAsync(request.Id);

                if (producto == null)
                {
                    throw new KeyNotFoundException($"Resgistro no encontrado con el id {request.Id}");
                }
                else
                {
                    await _repositoryAsync.DeleteAsync(producto);

                    return new Response<int>(producto.Id);
                }
            }

           
        }

    }
}
