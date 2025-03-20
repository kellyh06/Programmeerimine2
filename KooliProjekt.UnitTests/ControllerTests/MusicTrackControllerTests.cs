using KooliProjekt.Controllers;
using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace KooliProjekt.Tests.Controllers
{
    public class MusicTracksControllerTests
    {
        private readonly MusicTracksController _controller;
        private readonly Mock<IMusicTrackService> _serviceMock;

        public MusicTracksControllerTests()
        {
            _serviceMock = new Mock<IMusicTrackService>();
            _controller = new MusicTracksController(_serviceMock.Object, new Mock<ILogger<MusicTracksController>>().Object);
        }

        [Fact]
        public async Task Index_ReturnsViewResult_WithMusicTracks()
        {
            var mockData = new PagedResult<MusicTrack>
            {
                Results = new List<MusicTrack>
                {
                    new MusicTrack { Title = "Test Track", Artist = "Test Artist", Year = 2025, Pace = 5 }
                }
            };

            _serviceMock.Setup(s => s.List(1, 10, null)).ReturnsAsync(mockData);

            var result = await _controller.Index(1);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<MusicTracksIndexModel>(viewResult.Model);
            Assert.Single(model.Data.Results);
        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenIdIsNull()
        {
            var result = await _controller.Details(null);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsViewResult_WithMusicTrack()
        {
            var track = new MusicTrack { Id = 1, Title = "Test Track", Artist = "Test Artist", Year = 2025, Pace = 4 };
            _serviceMock.Setup(s => s.Get(1)).ReturnsAsync(track);

            var result = await _controller.Details(1);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<MusicTrack>(viewResult.Model);
            Assert.Equal(track.Id, model.Id);
        }

        [Fact]
        public async Task Create_Post_ReturnsRedirectToActionResult_WhenModelIsValid()
        {
            var track = new MusicTrack { Id = 1, Title = "New Track", Artist = "New Artist", Year = 2025, Pace = 3 };

            var result = await _controller.Create(track);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        [Fact]
        public async Task Edit_ReturnsNotFound_WhenIdIsNull()
        {
            var result = await _controller.Edit(null);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_ReturnsViewResult_WithMusicTrack()
        {
            var track = new MusicTrack { Id = 1, Title = "Test Track", Artist = "Test Artist", Year = 2025, Pace = 2 };
            _serviceMock.Setup(s => s.Get(1)).ReturnsAsync(track);

            var result = await _controller.Edit(1);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<MusicTrack>(viewResult.Model);
            Assert.Equal(track.Id, model.Id);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenIdIsNull()
        {
            var result = await _controller.Delete(null);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsViewResult_WithMusicTrack()
        {
            var track = new MusicTrack { Id = 1, Title = "Test Track", Artist = "Test Artist", Year = 2025, Pace = 4 };
            _serviceMock.Setup(s => s.Get(1)).ReturnsAsync(track);

            var result = await _controller.Delete(1);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<MusicTrack>(viewResult.Model);
            Assert.Equal(track.Id, model.Id);
        }

        [Fact]
        public async Task Delete_Post_ReturnsRedirectToActionResult_WhenSuccessful()
        {
            var track = new MusicTrack { Id = 1, Title = "Test Track", Artist = "Test Artist", Year = 2025, Pace = 4 };
            _serviceMock.Setup(s => s.Get(1)).ReturnsAsync(track);

            var result = await _controller.DeleteConfirmed(1);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }
    }
}
