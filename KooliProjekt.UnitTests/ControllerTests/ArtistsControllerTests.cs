﻿using KooliProjekt.Controllers;
using KooliProjekt.Models;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using System.Threading.Tasks;
using KooliProjekt.Data;

namespace KooliProjekt.Tests.Controllers
{
    public class ArtistsControllerTests
    {
        private readonly ArtistsController _controller;
        private readonly Mock<IArtistService> _mockArtistService;

        public ArtistsControllerTests()
        {
            _mockArtistService = new Mock<IArtistService>();
            _controller = new ArtistsController(_mockArtistService.Object);
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

        // Test Details (GET) action (when artist not found)
        [Fact]
        public async Task Details_ReturnsNotFoundResult_WhenArtistNotFound()
        {
            // Arrange
            _mockArtistService.Setup(service => service.Get(It.IsAny<int>())).ReturnsAsync((Artist)null);

            // Act
            var result = await _controller.Details(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        // Test Details (GET) action (when artist found)
        [Fact]
        public async Task Details_ReturnsViewResult_WithArtist()
        {
            // Arrange
            var artist = new Artist { Id = 1, Name = "Test Artist" };
            _mockArtistService.Setup(service => service.Get(It.IsAny<int>())).ReturnsAsync(artist);

            // Act
            var result = await _controller.Details(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Artist>(viewResult.Model);
            Assert.Equal(artist.Id, model.Id);
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

        // Test Edit (GET) action (when artist not found)
        [Fact]
        public async Task Edit_ReturnsNotFoundResult_WhenArtistNotFound()
        {
            // Arrange
            _mockArtistService.Setup(service => service.Get(It.IsAny<int>())).ReturnsAsync((Artist)null);

            // Act
            var result = await _controller.Edit(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        // Test Edit (GET) action (when artist found)
        [Fact]
        public async Task Edit_ReturnsViewResult_WithArtist()
        {
            // Arrange
            var artist = new Artist { Id = 1, Name = "Test Artist" };
            _mockArtistService.Setup(service => service.Get(It.IsAny<int>())).ReturnsAsync(artist);

            // Act
            var result = await _controller.Edit(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Artist>(viewResult.Model);
            Assert.Equal(artist.Id, model.Id);
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

        // Test Delete (GET) action (when artist not found)
        [Fact]
        public async Task Delete_ReturnsNotFoundResult_WhenArtistNotFound()
        {
            // Arrange
            _mockArtistService.Setup(service => service.Get(It.IsAny<int>())).ReturnsAsync((Artist)null);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        // Test Delete (GET) action (when artist found)
        [Fact]
        public async Task Delete_ReturnsViewResult_WithArtist()
        {
            // Arrange
            var artist = new Artist { Id = 1, Name = "Test Artist" };
            _mockArtistService.Setup(service => service.Get(It.IsAny<int>())).ReturnsAsync(artist);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Artist>(viewResult.Model);
            Assert.Equal(artist.Id, model.Id);
        }

        // Test Create (POST) action
        [Fact]
        public async Task Create_Post_ReturnsRedirectToAction_WhenModelIsValid()
        {
            // Arrange
            var artist = new Artist { Id = 1, Name = "Test Artist" };
            _mockArtistService.Setup(service => service.Save(It.IsAny<Artist>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Create(artist);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Create_Post_ReturnsViewResult_WhenModelIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Name", "Required");
            var artist = new Artist();

            // Act
            var result = await _controller.Create(artist);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        // Test Delete (POST) action
        [Fact]
        public async Task DeleteConfirmed_Post_ReturnsRedirectToAction_WhenArtistIsDeleted()
        {
            // Arrange
            var artist = new Artist { Id = 1, Name = "Test Artist" };
            _mockArtistService.Setup(service => service.Get(It.IsAny<int>())).ReturnsAsync(artist);
            _mockArtistService.Setup(service => service.Delete(It.IsAny<int>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteConfirmed(1);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task DeleteConfirmed_Post_ReturnsNotFound_WhenArtistIsNotFound()
        {
            // Arrange
            _mockArtistService.Setup(service => service.Get(It.IsAny<int>())).ReturnsAsync((Artist)null);

            // Act
            var result = await _controller.DeleteConfirmed(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
