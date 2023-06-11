using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Newtonsoft.Json;
using ServicesStore.Api.Author.Application;
using ServicesStore.Api.Author.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServicesStore.Api.Author.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Creator.Execute data)
        {
            return await _mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<AuthorDTO>>> GetAll()
        {

            return await _mediator.Send(new Selector.AuthorList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDTO>> GetAuthor(string id)
        {
            var response = await _mediator.Send(new FilterSelector.Author() { AuthorGuid = id });

            if (response is null)
                return NotFound(JsonConvert.DeserializeObject($"{{\"error\":\"The author {id} wasn't found\"}}"));

            return response;
        }


    }
}
