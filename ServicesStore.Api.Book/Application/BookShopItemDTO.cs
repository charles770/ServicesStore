using System;

namespace ServicesStore.Api.Book.Application
{
    public class BookShopItemDTO
    {
        public Guid? BookShopItemId { get; set; }
        public string Title { get; set; }
        public DateTime? PublishDate { get; set; }
        public Guid? AuthorBook { get; set; }
    }
}
