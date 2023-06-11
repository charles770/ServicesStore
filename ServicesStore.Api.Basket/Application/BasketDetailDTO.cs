using ServicesStore.Api.Basket.Model;
using System.Collections.Generic;
using System;

namespace ServicesStore.Api.Basket.Application
{
    public class BasketDetailDTO
    {

        public Guid? BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime? PublishDate { get; set; }

    }
}
