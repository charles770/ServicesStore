using ServicesStore.Api.Basket.RemoteModel;
using System;
using System.Threading.Tasks;

namespace ServicesStore.Api.Basket.RemoteInterface
{
    public interface IBooksService
    {
        Task<(bool res, RemoteBook book, string error)> GetBook(Guid BookId);
    }
}
