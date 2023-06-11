using FluentValidation;
using MediatR;
using ServicesStore.Api.Author.Model;
using ServicesStore.Api.Author.Persistance;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ServicesStore.Api.Author.Application
{
    public class Creator
    {
        public class Execute : IRequest
        {

            public string Name { set; get; }
            public string LastName { set; get; }
            public DateTime? BirthDate { set; get; }

        }

        public class ExecuteValidation : AbstractValidator<Execute>
        {
            public ExecuteValidation() {
                RuleFor(x=>x.Name).NotEmpty();
                RuleFor(x => x.LastName).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Execute>
        {
            public readonly AuthorContext _context;

            public Handler(AuthorContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                var authorBook = new AuthorBook
                {
                    Name = request.Name,
                    LastName = request.LastName,
                    BirthDate = request.BirthDate,
                    AuthorBookGuid = Guid.NewGuid().ToString()
                };

                _context.AuthorBooks.Add(authorBook);
                var retVal= await _context.SaveChangesAsync();
                if (retVal > 0)
                {

                    return Unit.Value;
                }

                throw new Exception("It was not possible to insert the author of the book");

            }
        }
    }
}
