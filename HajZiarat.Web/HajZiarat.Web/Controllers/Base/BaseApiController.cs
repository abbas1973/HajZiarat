using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace HajZiarat.Web.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    public partial class BaseApiController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
