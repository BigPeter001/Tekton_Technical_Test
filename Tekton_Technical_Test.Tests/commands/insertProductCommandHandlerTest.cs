using Application.DTOs;
using Application.features.products.commands.insertProductCommand;
using Application.features.products.commands.updateProductCommand;
using Application.features.products.queries.getProductById;
using Application.interfaces;
using Application.wrappers;
using AutoMapper;
using Domain.entities;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.features.products.queries.getProductById.GetProdcutByIdQuery;

namespace Tekton_Technical_Test.Tests.commands
{
    public class insertProductCommandHandlerTest
    {
        private readonly Mock<IRepositoryAsync<Product>> _repositoryAsync;
        //Inyección Mapeo automático
        private readonly IMapper _mapper;
        //Inyeccion del logger
        private readonly ILogger<InsertProductCommandHandler> _logger;

        public insertProductCommandHandlerTest()
        {
            _repositoryAsync = new Mock<IRepositoryAsync<Product>>();
            var httpClientFactory = new Mock<IHttpClientFactory>();
            _logger = new Mock<ILogger<InsertProductCommandHandler>>().Object;
            
        }

        [Fact]
        public async Task Handle_Should_ReturnOK()
        {
            //Arrange
            var insertCommand = new InsertProductCommand();
            insertCommand.Name = "Product name";
            insertCommand.Description = "Product description";
            insertCommand.Stock = 100;
            insertCommand.Discount = 10;
            insertCommand.Price = 600;


            var insertCommandHandler = new InsertProductCommandHandler(
                _repositoryAsync.Object,
                _mapper,
                _logger
                );

            //Act
            Response<int> result = await insertCommandHandler.Handle(insertCommand, default);
            //Assert
            result.Data.Should().BeGreaterThan(0);
        }


        [Fact]
        public async Task Update_Handle_Should_ReturnOK()
        {
            //Arrange
            var updateCommand = new UpdateProductCommand();
            updateCommand.Name = "Product name";
            updateCommand.Description = "Product description";
            updateCommand.Stock = 100;
            updateCommand.Discount = 10;
            updateCommand.Price = 600;


            var updateCommandHandler = new UpdateProductCommandHandler(
                _repositoryAsync.Object,
                _mapper
                );

            //Act
            Response<int> result = await updateCommandHandler.Handle(updateCommand, default);
            //Assert
            result.Data.Should().BeGreaterThan(0);
        }


        [Fact]
        public async Task GetProdcutById_Handle_Should_ReturnOK()
        {
            //Arrange
            var getProdcutByIdQuery = new GetProdcutByIdQuery();
           
            var getProdcutByIdQueryHandler = new GetProdcutByIdQueryHandler(
                _repositoryAsync.Object,
                _mapper
                );

            //Act
            Response<ProductDto> result = await getProdcutByIdQueryHandler.Handle(getProdcutByIdQuery, default);
            //Assert
            result.Data.Id.Should().BeGreaterThan(0);
        }

    }
}
