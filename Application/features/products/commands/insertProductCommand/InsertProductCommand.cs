using Application.interfaces;
using Application.wrappers;
using AutoMapper;
using Domain.entities;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Drawing;
using System.Net.Http;
using System.Text.Json;

namespace Application.features.products.commands.insertProductCommand
{
    public class InsertProductCommand : IRequest<Response<int>>
    {
        public required string Name { get; set; }
        public required string StatusName { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public double FinalPrice { get; set; }
    }

    public class InsertProductCommandHandler : IRequestHandler<InsertProductCommand, Response<int>>
    {
        //Innyección de patron Repository 
        private readonly IRepositoryAsync<Product> _repositoryAsync;
        //Inyección Mapeo automático
        private readonly IMapper _mapper;
        //Inyección consumo PorcentajesApiClient mockapi
        private readonly IHttpClientFactory _httpClientFactory;
        //Inyección Memory cache estándar
        private readonly IMemoryCache _memoryCache;
        //Inyeccion del logger
        private readonly ILogger<InsertProductCommandHandler> _logger;

        public InsertProductCommandHandler(IRepositoryAsync<Product> repositoryAsync, IMapper mapper, IHttpClientFactory httpClientFactory, IMemoryCache memoryCache, ILogger<InsertProductCommandHandler> logger)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        /// <summary>
        /// Este método realiza se encaraga de manejar la inserrción del command
        /// a la base de datos.
        /// </summary>
        /// <param name="request">request de tipo InsertProductCommand.</param>
        /// <param name="cancellationToken">request de tipo CancellationToken.</param>
        /// <returns>devuelve objeto de tipo Response<int> 
        /// con la respuesta de la ejecución del command.</returns>
        public async Task<Response<int>> Handle(InsertProductCommand request, CancellationToken cancellationToken)
        {

            int _idPorcentaje = 1;
            _logger.LogInformation("Inicio del request");
            // Inicia el cronómetro
            Stopwatch stopwatch = Stopwatch.StartNew();

            InsertarEstadosEnCache();

            RecuperarEstadoDeCache(request);

            await ConsumirMockApiPorcentajesById(request, _idPorcentaje);

            //Se cálcula el FinalPrice
            request.FinalPrice = (request.Price * (100 - request.Discount)) / 100;

            //Mapeo automatico del request a la clase producto
            var nuevoRegistro = _mapper.Map<Product>(request);
            //El patrón repositorio inserta el registro en la BD
            var data = await _repositoryAsync.AddAsync(nuevoRegistro);

            // Detiene el cronómetro
            stopwatch.Stop();
            _logger.LogInformation($"Final del request - Tiempo Transcurrido..: {stopwatch.Elapsed}");

            //Devuelve el Id del registro insertado a la BD
            return new Response<int>(data.Id);
        }

        /// <summary>
        /// Este método consume el servicio externo MockApi PorcentajesById.
        /// </summary>
        /// <param name="request">parametro de tipo InsertProductCommand, para actualizar los valores del porcentaje </param>
        ///  <param name="_id">parametro de tipo int del id del porcentaje </param>
        /// <returns></returns>
        private async Task ConsumirMockApiPorcentajesById(InsertProductCommand request, int _id)
        {
            //Consumo PorcentajesApiClient mockapi
            var client = _httpClientFactory.CreateClient("PorcentajesApiClient");
            var responsePorcentajesById = await client.GetAsync("porcentajesById/"+_id);

            if (responsePorcentajesById.IsSuccessStatusCode)
            {
                var content = await responsePorcentajesById.Content.ReadAsStringAsync();
                var porcentaje = JsonSerializer.Deserialize<Percentaje>(content);
                request.Discount = Convert.ToDouble(porcentaje.porcentaje);
            }
            else
            {
                //discount por default
                request.Discount = 0;
            }
        }

        /// <summary>
        /// Este método recupera los estados a la caché y lo actualiza en el objeto request.
        /// </summary>
        /// <param name="request">parametro de tipo InsertProductCommand, para actualizar los valores del status </param>
        /// <returns></returns>
        private void RecuperarEstadoDeCache(InsertProductCommand request)
        {
            //Recuperar estado de la caché
            if (_memoryCache.TryGetValue(request.StatusName, out var resultado))
            {
                request.StatusName = resultado.ToString();
            }
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
    }

}
