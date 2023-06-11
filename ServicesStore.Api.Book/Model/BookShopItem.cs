using System;

namespace ServicesStore.Api.Book.Model
{
    public class BookShopItem
    {
        public Guid? BookShopItemId { get; set; }
        public string Title { get; set; }
        public DateTime? PublishDate { get; set; }
        public Guid? AuthorBook { get; set; }
               

    }
}
