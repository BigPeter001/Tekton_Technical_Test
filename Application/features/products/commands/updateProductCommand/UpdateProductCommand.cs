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
        public required string Name { get; set; }
        public required string StatusName { get; set; }
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
        //Inyección consumo PorcentajesApiClient mockapi
        private readonly IHttpClientFactory _httpClientFactory;
        //Inyección Memory cache estándar
        private readonly IMemoryCache _memoryCache;

        public UpdateProductCommandHandler(IRepositoryAsync<Product> repositoryAsync, IMapper mapper, IHttpClientFactory httpClientFactory, IMemoryCache memoryCache)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
            _memoryCache = memoryCache;
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
                InsertarEstadosEnCache();
                producto.Name = request.Name;
                producto.StatusName = RecuperarEstadoDeCache(request.StatusName);
                producto.Stock = request.Stock;
                producto.Description = request.Description;
                producto.Price = request.Price;
                producto.Discount = ConsumirMockApiPorcentajesById(request.Id).Result;                
                producto.FinalPrice = (producto.Price * (100 - producto.Discount)) / 100; ;

                await _repositoryAsync.UpdateAsync(producto);

                return new Response<int>(producto.Id);
            }
        }

        private string RecuperarEstadoDeCache(string _statusKey)
        {
            string? statusName;
            //Recuperar estado de la caché
            if (_memoryCache.TryGetValue(_statusKey, out var resultado))
            {
                statusName = resultado.ToString();
            }
            else
            {
                statusName = "Cache no existe o finalizada";
            }

            return statusName;
        }

        /// <summary>
        /// Este método inserta los estados a la caché.
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        private void InsertarEstadosEnCache()
        {
            // Almacenar en caché
            _memoryCache.Set("1", "Active", new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) // Configurar la expiración
            });
            _memoryCache.Set("0", "Inactive", new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) // Configurar la expiración
            });
        }

        private async Task<double> ConsumirMockApiPorcentajesById(int _id)
        {
            double descuento = 0;
            //Consumo PorcentajesApiClient mockapi
            var client = _httpClientFactory.CreateClient("PorcentajesApiClient");
            var responsePorcentajesById = await client.GetAsync("porcentajesById/" + _id);

            if (responsePorcentajesById.IsSuccessStatusCode)
            {
                var content = await responsePorcentajesById.Content.ReadAsStringAsync();
                var porcentaje = JsonSerializer.Deserialize<Percentaje>(content);
                descuento = Convert.ToDouble(porcentaje.porcentaje);
            }
            
            return descuento;
        }

    }
}
