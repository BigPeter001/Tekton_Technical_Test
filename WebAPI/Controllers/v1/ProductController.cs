using Application.features.products.commands.deleteProductCommand;
using Application.features.products.commands.insertProductCommand;
using Application.features.products.commands.updateProductCommand;
using Application.features.products.queries.getProductAll;
using Application.features.products.queries.getProductById;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            return Ok(await Mediator.Send(command));
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
