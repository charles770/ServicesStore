using System;

namespace ServicesStore.Api.Gateway.BookRemote
{
    public class BookModelRemote
    {
        public Guid? BookShopItemId { get; set; }
        public string Title { get; set; }
        public DateTime? PublishDate { get; set; }
        public Guid? AuthorBook { get; set; }
        public AuthorModelRemote AuthorData { get; set; }
    }
}
