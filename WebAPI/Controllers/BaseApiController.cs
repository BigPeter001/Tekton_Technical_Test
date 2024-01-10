using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]

    public abstract class BaseApiController : ControllerBase
    {

        private IMediator? _mediator;
        protected IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        private  IHttpClientFactory? _httpClientFactory;
        protected IHttpClientFactory? HttpClientFactory => _httpClientFactory ??= HttpContext.RequestServices.GetService<IHttpClientFactory>();

        private  IMemoryCache? _memoryCache;
        protected IMemoryCache? MemoryCache => _memoryCache ??= HttpContext.RequestServices.GetService<IMemoryCache>();

    }
}
