using System;

namespace ServicesStore.Api.Basket.Model
{
    public class BasketSessionDetail
    {
        public int BasketSessionDetailId { get; set; }
        public DateTime? CreationDate { get; set; }
        public string Product { get; set; }

        public int BasketSessionId { get; set; }

        public BasketSession BasketSession { get; set; }

    }
}
