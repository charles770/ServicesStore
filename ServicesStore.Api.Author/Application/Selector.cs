using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ServicesStore.Api.Author.Model;
using ServicesStore.Api.Author.Persistance;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ServicesStore.Api.Author.Application
{
    public class Selector
    {
        public class AuthorList : IRequest<List<AuthorDTO>>
        {
        }

        public class Handler: IRequestHandler<AuthorList,List<AuthorDTO>>
        {

            public readonly AuthorContext _context;
            private readonly IMapper _mapper;

            public Handler(AuthorContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<List<AuthorDTO>> Handle(AuthorList request, CancellationToken cancellationToken)
            {
                var authors= await _context.AuthorBooks.ToListAsync();
                var authorDto = _mapper.Map<List<AuthorBook>, List<AuthorDTO>>(authors);
                return authorDto;
            }
        }
    }
}
