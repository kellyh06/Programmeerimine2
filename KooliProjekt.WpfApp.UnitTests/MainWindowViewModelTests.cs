using KooliProjekt.PublicApi;
using KooliProjekt.WpfApp;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace KooliProjekt.WpfApp.UnitTests
{
    public class MainWindowViewModelTests
    {
        [Fact]
        public async Task Load_Populates_List()
        {
            var mockApiClient = new Mock<IApiClient>();

            var fakeArtists = new List<Artist>
            {
                new Artist { Id = 1, Name = "Artist 1" },
                new Artist { Id = 2, Name = "Artist 2" }
            };

            var fakeResult = new Result<List<Artist>> { Value = fakeArtists };

            mockApiClient.Setup(api => api.List()).ReturnsAsync(fakeResult);

            var vm = new MainWindowViewModel(mockApiClient.Object);

            await vm.Load();

            Assert.Equal(2, vm.Lists.Count);
            Assert.Equal("Artist 1", vm.Lists[0].Name);
        }

        [Fact]
        public void NewCommand_Sets_New_Artist()
        {
            // Arrange
            var vm = new MainWindowViewModel(new Mock<IApiClient>().Object);

            // Act
            vm.NewCommand.Execute(null);

            // Assert
            Assert.NotNull(vm.SelectedItem);
            Assert.Equal(0, vm.SelectedItem.Id); // default value
        }

        [Fact]
        public async Task SaveCommand_Calls_Save_On_ApiClient()
        {
            // Arrange
            var mockApiClient = new Mock<IApiClient>();
            var vm = new MainWindowViewModel(mockApiClient.Object);
            var artist = new Artist { Id = 1, Name = "Saved Artist" };
            vm.SelectedItem = artist;

            // SaveCommand test
            mockApiClient.Setup(a => a.Save(It.IsAny<Artist>()))
                .ReturnsAsync(new Result());

            // List() test
            mockApiClient.Setup(a => a.List())
                .ReturnsAsync(new Result<List<Artist>> { Value = new List<Artist>() });

            // DeleteCommand test
            mockApiClient.Setup(a => a.Delete(It.IsAny<int>()))
                .ReturnsAsync(new Result());


            // Act
            vm.SaveCommand.Execute(null);

            // Assert
            mockApiClient.Verify(a => a.Save(artist), Times.Once);
        }

        [Fact]
        public async Task DeleteCommand_Calls_Delete_And_Removes_From_List()
        {
            // Arrange
            var mockApiClient = new Mock<IApiClient>();
            var artist = new Artist { Id = 5, Name = "To be deleted" };

            var vm = new MainWindowViewModel(mockApiClient.Object);
            vm.Lists.Add(artist);
            vm.SelectedItem = artist;
            vm.ConfirmDelete = _ => true;

            // Act
            vm.DeleteCommand.Execute(null);

            // Assert
            mockApiClient.Verify(a => a.Delete(artist.Id), Times.Once);
            Assert.DoesNotContain(artist, vm.Lists);
            Assert.Null(vm.SelectedItem);
        }
    }
}
