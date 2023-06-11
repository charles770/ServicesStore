using System;

namespace ServicesStore.Api.Basket.RemoteModel
{
    public class RemoteBook
    {
        public Guid? BookShopItemId { get; set; }
        public string Title { get; set; }
        public DateTime? PublishDate { get; set; }
        public Guid? AuthorBook { get; set; }
    }
}
