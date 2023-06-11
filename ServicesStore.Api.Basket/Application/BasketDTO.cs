using System;
using System.Collections.Generic;

namespace ServicesStore.Api.Basket.Application
{
    public class BasketDTO
    {
        public int BasketId { get; set; }
        public DateTime? CreationDate { get; set; }
        public List<BasketDetailDTO> ProductList { get; set; }
    }
}
