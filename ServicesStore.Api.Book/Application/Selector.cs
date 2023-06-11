using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ServicesStore.Api.Book.Model;
using ServicesStore.Api.Book.Persistence;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ServicesStore.Api.Book.Application
{
    public class Selector
    {
        public class Execute : IRequest<List<BookShopItemDTO>>
        {
        }
        public class Handler : IRequestHandler<Execute, List<BookShopItemDTO>>
        {

            public readonly BookShopContext _context;
            private readonly IMapper _mapper;

            public Handler(BookShopContext context,IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<BookShopItemDTO>> Handle(Execute request, CancellationToken cancellationToken)
            {
                var books = await _context.BookShopItem.ToListAsync();
                return _mapper.Map<List<BookShopItem>,List<BookShopItemDTO>>(books);
            }
        }



    }
}
