using GenFu;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using ServicesStore.Api.Book.Application;
using ServicesStore.Api.Book.Model;
using ServicesStore.Api.Book.Persistence;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xunit;

namespace ServicesStore.Api.Book.Tests
{
    public class BooksServiceTest
    {
        private IEnumerable<BookShopItem> GetTestData()
        {
            A.Configure<BookShopItem>()
                .Fill(x => x.Title).AsArticleTitle()
                .Fill(x => x.BookShopItemId, () => { return Guid.NewGuid(); });

            var list = A.ListOf<BookShopItem>(30);
            list[0].BookShopItemId = Guid.Empty;

            return list;
        }

        private Mock<BookShopContext> CreateContext()
        {
            var dataTest = GetTestData().AsQueryable();
            var dbSet = new Mock<DbSet<BookShopItem>>();
            dbSet.As<IQueryable<BookShopItem>>().Setup(x=>x.Provider).Returns(dataTest.Provider);
            dbSet.As<IQueryable<BookShopItem>>().Setup(x => x.Expression).Returns(dataTest.Expression);
            dbSet.As<IQueryable<BookShopItem>>().Setup(x => x.ElementType).Returns(dataTest.ElementType);
            dbSet.As<IQueryable<BookShopItem>>().Setup(x => x.GetEnumerator()).Returns(dataTest.GetEnumerator());

            dbSet.As<IAsyncEnumerable<BookShopItem>>().Setup(x=>x.GetAsyncEnumerator(new System.Threading.CancellationToken()))
                .Returns(new AsyncEnumerator<BookShopItem>(dataTest.GetEnumerator()));

            dbSet.As<IQueryable<BookShopItem>>().Setup(x=>x.Provider)
                .Returns(new AsyncQueryProvider<BookShopItem>(dataTest.Provider));

            var context= new Mock<BookShopContext>();
            context.Setup(x=>x.BookShopItem).Returns(dbSet.Object);

            return context; 
        }
        
        [Fact]
        public async void GetBookById()
        {
            var mockContext = CreateContext();
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(typeof(MappingTest).Assembly);
            var mockMapper = new Mapper(config);

            FilterSelector.Handler handler = new FilterSelector.Handler(mockContext.Object, mockMapper);

            FilterSelector.BookItem request = new FilterSelector.BookItem();
            request.BookId = Guid.Empty;

            var book = await handler.Handle(request, new System.Threading.CancellationToken());

            Assert.NotNull(book);
            Assert.True(book.BookShopItemId == Guid.Empty);

        }

        [Fact]
        public async void GetBooks()
        {
            //Debugger.Launch();

            //1. Emulate instance EF - BookShopContext
            //mock objects
            var mockContext = CreateContext();

            //2. Emulate IMapper
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(typeof(MappingTest).Assembly);
             
            var mockMapper = new Mapper(config);

            //3. Intance of Handler class
            Selector.Handler handler = new Selector.Handler(mockContext.Object, mockMapper);

            Selector.Execute request = new Selector.Execute();

            var list = await handler.Handle(request,new System.Threading.CancellationToken());

            Assert.True(list.Any());
        }

        [Fact]
        public async void CreateBook()
        {
            //Debugger.Launch(); //For testing and Continuous Integration

            var options = new DbContextOptionsBuilder<BookShopContext>()
                .UseInMemoryDatabase(databaseName: "BookDB")
                .Options;

            var context = new BookShopContext(options);

            Creator.Execute request = new Creator.Execute();
            request.Title = "Test Book";
            request.AuthorBook = Guid.Empty;
            request.PublishDate = DateTime.Now;

            Creator.Handler handler = new Creator.Handler(context);

            var book = await handler.Handle(request, new System.Threading.CancellationToken());
            Assert.True(book!=null);
        }
    
    }
}
