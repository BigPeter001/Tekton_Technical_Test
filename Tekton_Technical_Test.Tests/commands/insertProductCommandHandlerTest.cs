using Application.features.products.commands.insertProductCommand;
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

namespace Tekton_Technical_Test.Tests.commands
{
    public class insertProductCommandHandlerTest
    {
        private readonly Mock<IRepositoryAsync<Product>> _repositoryAsync;
        //Inyección Mapeo automático
        private readonly Mock<IMapper> _mapper;
        //Inyeccion del logger
        private readonly ILogger<InsertProductCommandHandler> _logger;

        public insertProductCommandHandlerTest()
        {
            _mapper = new Mock<IMapper>();
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
                _mapper.Object,
                _logger
                );

            //Act
            Response<int> result = await insertCommandHandler.Handle(insertCommand, default);
            //Assert
            result.Data.Should().BeGreaterThan(0);
        }
    }
}
