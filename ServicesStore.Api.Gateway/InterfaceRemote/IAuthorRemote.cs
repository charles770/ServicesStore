using ServicesStore.Api.Gateway.BookRemote;
using System.Threading.Tasks;
using System;

namespace ServicesStore.Api.Gateway.InterfaceRemote
{
    public interface IAuthorRemote
    {
        Task<(bool result, AuthorModelRemote author, string errorMessage)> GetAuthor(Guid AuthorId);
    }
}
