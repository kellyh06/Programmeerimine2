using KooliProjekt.Controllers;
using KooliProjekt.Models;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using System.Threading.Tasks;
using KooliProjekt.Data;

namespace KooliProjekt.Tests.Controllers
{
    public class MusicTracksControllerTests
    {
        private readonly MusicTracksController _controller;
        private readonly Mock<IMusicTrackService> _mockMusicTrackService;

        public MusicTracksControllerTests()
        {
            _mockMusicTrackService = new Mock<IMusicTrackService>();
            _controller = new MusicTracksController(_mockMusicTrackService.Object);
        }

        // Test Create (GET) action
        [Fact]
        public void Create_ReturnsViewResult()
        {
            // Act
            var result = _controller.Create();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        // Test Details (GET) action (when id is null)
        [Fact]
        public async Task Details_ReturnsNotFoundResult_WhenIdIsNull()
        {
            // Act
            var result = await _controller.Details(null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        // Test Details (GET) action (when music track not found)
        [Fact]
        public async Task Details_ReturnsNotFoundResult_WhenMusicTrackNotFound()
        {
            // Arrange
            _mockMusicTrackService.Setup(service => service.Get(It.IsAny<int>())).ReturnsAsync((MusicTrack)null);

            // Act
            var result = await _controller.Details(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        // Test Details (GET) action (when music track found)
        [Fact]
        public async Task Details_ReturnsViewResult_WithMusicTrack()
        {
            // Arrange
            var musicTrack = new MusicTrack { Id = 1, Title = "Test Title", Artist = "Test Artist", Year = 2022, Pace =  3};
            _mockMusicTrackService.Setup(service => service.Get(It.IsAny<int>())).ReturnsAsync(musicTrack);

            // Act
            var result = await _controller.Details(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<MusicTrack>(viewResult.Model);
            Assert.Equal(musicTrack.Id, model.Id);
        }

        // Test Edit (GET) action (when id is null)
        [Fact]
        public async Task Edit_ReturnsNotFoundResult_WhenIdIsNull()
        {
            // Act
            var result = await _controller.Edit(null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        // Test Edit (GET) action (when music track not found)
        [Fact]
        public async Task Edit_ReturnsNotFoundResult_WhenMusicTrackNotFound()
        {
            // Arrange
            _mockMusicTrackService.Setup(service => service.Get(It.IsAny<int>())).ReturnsAsync((MusicTrack)null);

            // Act
            var result = await _controller.Edit(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        // Test Edit (GET) action (when music track found)
        [Fact]
        public async Task Edit_ReturnsViewResult_WithMusicTrack()
        {
            // Arrange
            var musicTrack = new MusicTrack { Id = 1, Title = "Test Title", Artist = "Test Artist", Year = 2022, Pace = 2 };
            _mockMusicTrackService.Setup(service => service.Get(It.IsAny<int>())).ReturnsAsync(musicTrack);

            // Act
            var result = await _controller.Edit(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<MusicTrack>(viewResult.Model);
            Assert.Equal(musicTrack.Id, model.Id);
        }

        // Test Delete (GET) action (when id is null)
        [Fact]
        public async Task Delete_ReturnsNotFoundResult_WhenIdIsNull()
        {
            // Act
            var result = await _controller.Delete(null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        // Test Delete (GET) action (when music track not found)
        [Fact]
        public async Task Delete_ReturnsNotFoundResult_WhenMusicTrackNotFound()
        {
            // Arrange
            _mockMusicTrackService.Setup(service => service.Get(It.IsAny<int>())).ReturnsAsync((MusicTrack)null);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        // Test Delete (GET) action (when music track found)
        [Fact]
        public async Task Delete_ReturnsViewResult_WithMusicTrack()
        {
            // Arrange
            var musicTrack = new MusicTrack { Id = 1, Title = "Test Title", Artist = "Test Artist", Year = 2022, Pace = 3 };
            _mockMusicTrackService.Setup(service => service.Get(It.IsAny<int>())).ReturnsAsync(musicTrack);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<MusicTrack>(viewResult.Model);
            Assert.Equal(musicTrack.Id, model.Id);
        }

        // Test Create (POST) action
        [Fact]
        public async Task Create_Post_ReturnsRedirectToAction_WhenModelIsValid()
        {
            // Arrange
            var musicTrack = new MusicTrack { Id = 1, Title = "Test Title", Artist = "Test Artist", Year = 2022, Pace = 4 };
            _mockMusicTrackService.Setup(service => service.Save(It.IsAny<MusicTrack>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Create(musicTrack);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Create_Post_ReturnsViewResult_WhenModelIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Title", "Required");
            var musicTrack = new MusicTrack();

            // Act
            var result = await _controller.Create(musicTrack);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        // Test Delete (POST) action
        [Fact]
        public async Task DeleteConfirmed_Post_ReturnsRedirectToAction_WhenMusicTrackIsDeleted()
        {
            // Arrange
            var musicTrack = new MusicTrack { Id = 1, Title = "Test Title", Artist = "Test Artist", Year = 2022, Pace = 3 };
            _mockMusicTrackService.Setup(service => service.Get(It.IsAny<int>())).ReturnsAsync(musicTrack);
            _mockMusicTrackService.Setup(service => service.Delete(It.IsAny<int>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteConfirmed(1);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task DeleteConfirmed_Post_ReturnsNotFound_WhenMusicTrackIsNotFound()
        {
            // Arrange
            _mockMusicTrackService.Setup(service => service.Get(It.IsAny<int>())).ReturnsAsync((MusicTrack)null);

            // Act
            var result = await _controller.DeleteConfirmed(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
