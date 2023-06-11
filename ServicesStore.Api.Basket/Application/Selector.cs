using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ServicesStore.Api.Basket.Persistence;
using ServicesStore.Api.Basket.RemoteInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ServicesStore.Api.Basket.Application
{
    public class Selector
    {
        public class Basket : IRequest<BasketDTO>
        {
            public int BasketSessionId { get; set; }
        }

        public class Handler : IRequestHandler<Basket, BasketDTO>
        {

            private readonly BasketContext _context;
            private readonly IMapper _mapper;
            private readonly IBooksService _booksService;


            public Handler(BasketContext context, IMapper mapper, IBooksService booksService)
            {
                _context = context;
                _mapper = mapper;
                _booksService = booksService;
            }
            public async Task<BasketDTO> Handle(Basket request, CancellationToken cancellationToken)
            {
                var basket = await _context.BasketSession.FirstOrDefaultAsync(x => x.BasketSessionId == request.BasketSessionId);

                var details=await _context.BasketSessionDetail.Where(x=>x.BasketSessionId==request.BasketSessionId).ToListAsync();

                var basketDTOList = new List<BasketDetailDTO>(); 

                foreach (var detail in details)
                {
                    var response= await _booksService.GetBook(new Guid(detail.Product));

                    if (response.res)
                    {
                        var book = response.book;
                        var basketDetail = new BasketDetailDTO
                        {
                            Title = book.Title,
                            PublishDate=book.PublishDate,
                            BookId=book.BookShopItemId
                        };
                        basketDTOList.Add(basketDetail);
                    }
                }

                var basketSessionDTO = new BasketDTO
                {
                    BasketId=basket.BasketSessionId,
                    CreationDate=basket.CreationDate,
                    ProductList=basketDTOList
                };

                return basketSessionDTO;
            }
        }

    }
}
