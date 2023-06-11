using FluentValidation;
using MediatR;
using ServicesStore.Api.Book.Model;
using ServicesStore.Api.Book.Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ServicesStore.Api.Book.Application
{
    public class Creator
    {
        public class Execute : IRequest
        {
            public string Title { get; set; }
            public DateTime? PublishDate { get; set; }
            public Guid? AuthorBook { get; set; }

        }

        public class ExecuteValidation : AbstractValidator<Execute>
        {
            public ExecuteValidation()
            {
                RuleFor(x => x.Title).NotEmpty();
                RuleFor(x => x.PublishDate).NotEmpty();
                RuleFor(x => x.AuthorBook).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Execute>
        {

            public readonly BookShopContext _context;

            public Handler(BookShopContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                var book = new BookShopItem
                {
                    Title = request.Title,
                    AuthorBook = request.AuthorBook,
                    PublishDate = request.PublishDate
                };

                _context.BookShopItem.Add(book);
                var value = await _context.SaveChangesAsync();
                if (value > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("It was not possible to insert the book");

            }
        }
    }
}
