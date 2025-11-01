using Moq;
using BookStore.Data.Repository;
using BookStore.Data.Context;
using BookStore.Service;

namespace BookStore.Tests
{
    public class BookStoreReposiotryTests
    {
        [Fact]
        public async Task GetBookById_ShouldReturnBook()
        {
            //Arrange
            var mockRepo = new Mock<IBookRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(new Guid()))
                    .ReturnsAsync(new Book { BookId = new Guid(), Title = "Sample", Author = "Sample Author" });

            var service = new BookService(mockRepo.Object); 

            //Act
            var book = await service.GetByIdAsync(new Guid());

            //Assert
            Assert.NotNull(book);
            Assert.Equal("Sample", book?.Title);
        }
    }
}