using System;
using System.Collections.Generic;

namespace ServicesStore.Api.Basket.Model
{
    public class BasketSession
    {
        public int BasketSessionId { get; set; }
        public DateTime? CreationDate { get; set;}
        public ICollection<BasketSessionDetail> BasketSessionDetails { get; set; }
    }
}
