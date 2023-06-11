using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ServicesStore.Api.Author.Model;
using ServicesStore.Api.Author.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static ServicesStore.Api.Author.Application.FilterSelector;

namespace ServicesStore.Api.Author.Application
{
    public class FilterSelector
    {

        public class Author : IRequest<AuthorDTO>
        {
            public string AuthorGuid { get; set; }
        }

        public class Handler : IRequestHandler<Author, AuthorDTO>
        {

            public readonly AuthorContext _context;
            private readonly IMapper _mapper;
            public Handler(AuthorContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<AuthorDTO> Handle(Author request, CancellationToken cancellationToken)
            {
                var author = await _context.AuthorBooks.FirstOrDefaultAsync(x => x.AuthorBookGuid == request.AuthorGuid);
                var authorDto = _mapper.Map<AuthorBook, AuthorDTO>(author);
                return authorDto;
            }
        }

    }
}
