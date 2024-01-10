using Application.features.products.commands.deleteProductCommand;
using Application.features.products.commands.insertProductCommand;
using Application.features.products.commands.updateProductCommand;
using Application.features.products.queries.getProductAll;
using Application.features.products.queries.getProductById;
using Azure.Core;
using Domain.entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Http;
using System.Text.Json;

namespace WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiController]

    public class ProductController : BaseApiController
    {

        //GET api/<controller>/5

        /// <summary>
        /// Recupera todos los productos de la BD por filtros utilizando los patrones CQRS, Mediator
        /// y Repository.
        /// </summary>

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllProducts([FromQuery] GetAllProductsParameters filter)
        {
            return Ok(await Mediator.Send(new GetAllProductsQuery 
            { 
                PageNumber = filter.PageNumber, 
                PageSize = filter.PageSize, 
                Name = filter.Name, 
                StatusName = filter.StatusName, 
                Description = filter.Description
            }));
        }

        //GET api/<controller>/5

        /// <summary>
        /// Recupera un producto a la BD por Id utilizando los patrones CQRS, Mediator
        /// y Repository.
        /// </summary>

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProdcutById(int id)
        {
            return Ok(await Mediator.Send(new GetProdcutByIdQuery { Id = id }));
        }

        //POST api/<controller>

        /// <summary>
        /// Inserta un producto a la BD utilizando los patrones CQRS, Mediator
        /// y Repository.
        /// </summary>

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> InsertProduct(InsertProductCommand command)
        {
            InsertarEstadosEnCache();

            command.StatusName = RecuperarEstadoDeCache(command.StatusName);

            command.Discount = await ConsumirMockApiPorcentajesById( 2);

            //Se cálcula el FinalPrice
            command.FinalPrice = (command.Price * (100 - command.Discount)) / 100;
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Este método inserta los estados a la caché.
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        private void InsertarEstadosEnCache()
        {
            // Almacenar en caché
            MemoryCache.Set("1", "Active", new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) // Configurar la expiración
            });
            MemoryCache.Set("0", "Inactive", new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) // Configurar la expiración
            });
        }

        private async Task<double> ConsumirMockApiPorcentajesById(int _id)
        {
            double descuento = 0;
            //Consumo PorcentajesApiClient mockapi
            var client = HttpClientFactory.CreateClient("PorcentajesApiClient");
            var responsePorcentajesById = await client.GetAsync("porcentajesById/" + _id);

            if (responsePorcentajesById.IsSuccessStatusCode)
            {
                var content = await responsePorcentajesById.Content.ReadAsStringAsync();
                var porcentaje = JsonSerializer.Deserialize<Percentaje>(content);
                descuento = Convert.ToDouble(porcentaje.porcentaje);
            }

            return descuento;
        }

        private string RecuperarEstadoDeCache(string _statusKey)
        {
            string? statusName;
            //Recuperar estado de la caché
            if (MemoryCache.TryGetValue(_statusKey, out var resultado))
            {
                statusName = resultado.ToString();
            }
            else
            {
                statusName = "Cache no existe o finalizada";
            }

            return statusName;
        }



        //PUT api/<controller>/5

        /// <summary>
        /// Actualiza un producto a la BD utilizando los patrones CQRS, Mediator
        /// y Repository.
        /// </summary>

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductCommand command)
        {
            if (id != command.Id) 
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        //DELETE api/<controller>/5

        /// <summary>
        /// Elimina un producto a la BD utilizando los patrones CQRS, Mediator
        /// y Repository.
        /// </summary>

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            return Ok(await Mediator.Send(new DeleteProductCommand { Id = id }));
        }

    }
}
