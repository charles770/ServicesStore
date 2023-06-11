using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Collections;
using System.Collections.Generic;
using ServicesStore.Api.Basket.Persistence;
using ServicesStore.Api.Basket.Model;

namespace ServicesStore.Api.Basket.Application
{
    public class Creator
    {
        public class Execute : IRequest
        {
            public DateTime? CreationDate { set; get; }
            public List<string> BookItemList { get; set; }

        }

        public class ExecuteValidation : AbstractValidator<Execute>
        {
            public ExecuteValidation()
            {
                RuleFor(x => x.CreationDate).NotEmpty();
                //RuleFor(x => x.BookItemList).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Execute>
        {
            public readonly BasketContext _context;

            public Handler(BasketContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                var basketSession = new BasketSession
                {
                    CreationDate = request.CreationDate
                };

                _context.BasketSession.Add(basketSession);
                var retVal = await _context.SaveChangesAsync();


                if (retVal == 0)
                {
                    throw new Exception("It was not possible to insert the session of the basket");
                }
                int id = basketSession.BasketSessionId;

                foreach (var bookItem in request.BookItemList)
                {
                    var datailSession = new BasketSessionDetail
                    {
                        CreationDate = DateTime.Now,
                        BasketSessionId = id,
                        Product = bookItem
                    };

                    _context.BasketSessionDetail.Add(datailSession);
                }

                retVal = await _context.SaveChangesAsync();

                if (retVal > 0)
                {

                    return Unit.Value;
                }

                throw new Exception("It was not possible to insert the products to the basket");

            }

        }
    }
}
