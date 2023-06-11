using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using ServicesStore.Api.Basket.Application;

namespace ServicesStore.Api.Basket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {

        private readonly IMediator _mediator;
        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Creator.Execute data)
        {
            return await _mediator.Send(data);
        }
        

        [HttpGet("{id}")]
        public async Task<ActionResult<BasketDTO>> GetBasket(int id)
        {
            var response = await _mediator.Send(new Selector.Basket() { BasketSessionId = id });

            if (response is null)
                return NotFound(JsonConvert.DeserializeObject($"{{\"error\":\"The basket {id} wasn't found\"}}"));

            return response;
        }
       

    }
}
