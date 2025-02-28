using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using KooliProjekt.Data.Repository;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Moq;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class ArtistServiceTests
    {
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly Mock<IArtistRepository> _repositoryMock;
        private readonly ArtistService _artistService;

        public ArtistServiceTests()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _repositoryMock = new Mock<IArtistRepository>();
            _artistService = new ArtistService(_uowMock.Object);

            _uowMock.SetupGet(r => r.ArtistRepository)
                    .Returns(_repositoryMock.Object);
        }

        [Fact]
        public async Task List_should_return_list_of_artists()
        {
            // Arrange
            var results = new List<Artist>
            {
                new Artist { Id = 1 },
                new Artist { Id = 2 }
            };
            var pagedResult = new PagedResult<Artist> { Results = results };
            _repositoryMock.Setup(r => r.List(It.IsAny<int>(), It.IsAny<int>(),null))
                           .ReturnsAsync(pagedResult);

            // Act
            var result = await _artistService.List(1, 10);

            // Assert
            Assert.Equal(pagedResult, result);
        }
    }
}