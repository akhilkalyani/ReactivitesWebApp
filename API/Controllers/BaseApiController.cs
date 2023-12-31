using Application.Core;
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
        protected ActionResult HandleResult<T>(Result<T> result)
        {
            if(result==null)return NotFound();
            if(result.IsSuccess && result.Value!=null)
                return Ok(result.Value);
            if(result.IsSuccess && result.Value==null)
                return NotFound();
            return BadRequest(result.Error);
        }
    }
}