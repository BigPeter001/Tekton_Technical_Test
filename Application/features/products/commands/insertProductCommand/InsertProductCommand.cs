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
        public  string Name { get; set; }
        public  string StatusName { get; set; }
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
        //Inyeccion del logger
        private readonly ILogger<InsertProductCommandHandler> _logger;

        public InsertProductCommandHandler(IRepositoryAsync<Product> repositoryAsync, IMapper mapper, ILogger<InsertProductCommandHandler> logger)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
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

            _logger.LogInformation("Inicio del request");
            // Inicia el cronómetro
            Stopwatch stopwatch = Stopwatch.StartNew();           

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

        
    }

}
