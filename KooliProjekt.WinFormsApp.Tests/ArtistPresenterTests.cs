using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using KooliProjekt.WinFormsApp;
using KooliProjekt.WinFormsApp.Api;

namespace KooliProjekt.WinFormsApp.Tests
{
    public class ArtistPresenterTests
    {
        private readonly Mock<ITodoView> _mockView;
        private readonly Mock<IApiClient> _mockApiClient;
        private readonly ArtistPresenter _presenter;

        public ArtistPresenterTests()
        {
            _mockView = new Mock<ITodoView>();
            _mockApiClient = new Mock<IApiClient>();
            _presenter = new ArtistPresenter(_mockView.Object, _mockApiClient.Object);
        }

        [Fact]
        public void UpdateView_WithNull_ResetsViewProperties()
        {
            _presenter.UpdateView(null);

            _mockView.VerifySet(v => v.Name = string.Empty, Times.Once);
            _mockView.VerifySet(v => v.Id = 0, Times.Once);
        }

        [Fact]
        public void UpdateView_WithArtist_SetsViewProperties()
        {
            var artist = new Artist { Id = 123, Name = "Test Artist" };
            _presenter.UpdateView(artist);

            _mockView.VerifySet(v => v.Id = 123, Times.Once);
            _mockView.VerifySet(v => v.Name = "Test Artist", Times.Once);
        }

        [Fact]
        public async Task Load_SetsArtistsFromApiClient_WhenNoErrors()
        {
            var artistsFromApi = new List<Artist>
            {
                new Artist { Id = 1, Name = "Artist 1" },
                new Artist { Id = 2, Name = "Artist 2" }
            };

            var result = new Result<List<Artist>>
            {
                Value = artistsFromApi
            };

            _mockApiClient.Setup(api => api.List()).ReturnsAsync(result);

            await _presenter.Load();

            _mockView.VerifySet(v => v.Artists = artistsFromApi, Times.Once);
        }

        [Fact]
        public async Task Load_DoesNotSetArtists_WhenErrors()
        {
            var errorResult = new Result<List<Artist>>();
            errorResult.AddError("List", "Failed to load artists");

            _mockApiClient.Setup(api => api.List()).ReturnsAsync(errorResult);

            await _presenter.Load();

            _mockView.VerifySet(v => v.Artists = It.IsAny<List<Artist>>(), Times.Never);
        }

        [Fact]
        public async Task Save_CallsApiClientSave_AndReloads_WhenNoErrors()
        {
            var artist = new Artist { Id = 1, Name = "Artist 1" };
            var artistsFromApi = new List<Artist> { artist };
            var loadResult = new Result<List<Artist>> { Value = artistsFromApi };
            var saveResult = new Result(); // edukas salvestus, ilma vigadeta

            _mockApiClient.Setup(api => api.Save(artist)).ReturnsAsync(saveResult);
            _mockApiClient.Setup(api => api.List()).ReturnsAsync(loadResult);

            await _presenter.Save(artist);

            _mockApiClient.Verify(api => api.Save(artist), Times.Once);
            _mockApiClient.Verify(api => api.List(), Times.Once);
            _mockView.VerifySet(v => v.Artists = artistsFromApi, Times.Once);
        }

        [Fact]
        public async Task Save_DoesNotReload_WhenSaveHasErrors()
        {
            var artist = new Artist { Id = 1, Name = "Artist 1" };
            var errorResult = new Result();
            errorResult.AddError("Save", "Failed to save artist");

            _mockApiClient.Setup(api => api.Save(artist)).ReturnsAsync(errorResult);

            await _presenter.Save(artist);

            _mockApiClient.Verify(api => api.Save(artist), Times.Once);
            _mockApiClient.Verify(api => api.List(), Times.Never);
            _mockView.VerifySet(v => v.Artists = It.IsAny<List<Artist>>(), Times.Never);
        }

        [Fact]
        public async Task Delete_CallsApiClientDelete_AndReloads_WhenNoErrors()
        {
            int artistId = 5;
            var artistsFromApi = new List<Artist>();
            var loadResult = new Result<List<Artist>> { Value = artistsFromApi };
            var deleteResult = new Result(); // edukas kustutus, ilma vigadeta

            _mockApiClient.Setup(api => api.Delete(artistId)).ReturnsAsync(deleteResult);
            _mockApiClient.Setup(api => api.List()).ReturnsAsync(loadResult);

            await _presenter.Delete(artistId);

            _mockApiClient.Verify(api => api.Delete(artistId), Times.Once);
            _mockApiClient.Verify(api => api.List(), Times.Once);
            _mockView.VerifySet(v => v.Artists = artistsFromApi, Times.Once);
        }

        [Fact]
        public async Task Delete_DoesNotReload_WhenDeleteHasErrors()
        {
            int artistId = 5;
            var errorResult = new Result();
            errorResult.AddError("Delete", "Failed to delete artist");

            _mockApiClient.Setup(api => api.Delete(artistId)).ReturnsAsync(errorResult);

            await _presenter.Delete(artistId);

            _mockApiClient.Verify(api => api.Delete(artistId), Times.Once);
            _mockApiClient.Verify(api => api.List(), Times.Never);
            _mockView.VerifySet(v => v.Artists = It.IsAny<List<Artist>>(), Times.Never);
        }
    }
}
