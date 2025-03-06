using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KooliProjekt.Data;
using KooliProjekt.Services;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class ArtistServiceTests : ServiceTestBase
    {
        [Fact]
        public async Task Save_should_add_new_list()
        {
            // Arrange
            var service = new ArtistService(DbContext);
            var ArtistList = new Artist { Name = "Test" };

            // Act
            await service.Save(ArtistList);

            // Assert
            var count = DbContext.Artists.Count();
            var result = DbContext.Artists.FirstOrDefault();
            Assert.Equal(1, count);
            Assert.Equal(ArtistList.Name, result.Name);
        }

        [Fact]
        public async Task Delete_should_remove_given_list()

        {
            // Arrange
            var service = new ArtistService(DbContext);
            var artist = new Artist { Name = "Test" };
            DbContext.Artists.Add(artist);
            DbContext.SaveChanges();

            // Act
            await service.Delete(1);

            // Assert
            var count = DbContext.Artists.Count();
            Assert.Equal(0, count);
        }
    }
}
