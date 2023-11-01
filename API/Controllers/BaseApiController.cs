using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController:ControllerBase
    {
       private IMediator _mediator;
        public IMediator Mediator
        {
            get
            {
                if(_mediator==null)
                {
                    _mediator=HttpContext.RequestServices.GetService<IMediator>();
                }
                return _mediator;
            }
        }
    }
}