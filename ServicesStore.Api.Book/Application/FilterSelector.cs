using MapsterMapper;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using ServicesStore.Api.Book.Persistence;
using System;
using Microsoft.EntityFrameworkCore;
using ServicesStore.Api.Book.Model;

namespace ServicesStore.Api.Book.Application
{
    public class FilterSelector
    {
        public class BookItem : IRequest<BookShopItemDTO>
        {
            public Guid? BookId { get; set; }
        }

        public class Handler : IRequestHandler<BookItem, BookShopItemDTO>
        {

            public readonly BookShopContext _context;
            private readonly IMapper _mapper;
            public Handler(BookShopContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<BookShopItemDTO> Handle(BookItem request, CancellationToken cancellationToken)
            {
                var book = await _context.BookShopItem.FirstOrDefaultAsync(x => x.BookShopItemId == request.BookId);
                return _mapper.Map<BookShopItem, BookShopItemDTO>(book);

            }

        }
    }
}
