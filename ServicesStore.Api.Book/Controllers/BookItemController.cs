using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServicesStore.Api.Book.Application;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServicesStore.Api.Book.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookItemController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BookItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Creator.Execute data)
        {
            return await _mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<BookShopItemDTO>>> GetAll()
        {
            return await _mediator.Send(new Selector.Execute());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookShopItemDTO>> GetBookItem(string id)
        {
            var response = await _mediator.Send(new FilterSelector.BookItem() { BookId = Guid.Parse(id) });

            if (response is null)
                return NotFound(JsonConvert.DeserializeObject($"{{\"error\":\"The book {id} wasn't found\"}}"));

            return response;
        }
    }
}
