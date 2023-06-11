using Mapster;
using ServicesStore.Api.Book.Application;
using ServicesStore.Api.Book.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesStore.Api.Book.Tests
{
    public class MappingTest : IRegister
    {       
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<BookShopItem,BookShopItemDTO>();
        }
    }
}
