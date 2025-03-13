using KooliProjekt.Controllers;
using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace KooliProjekt.Tests
{
    public class MusicTracksControllerTests
    {
        private MusicTracksController GetController(ApplicationDbContext context, IMusicTrackService musicTrackService)
        {
            return new MusicTracksController(context, musicTrackService);
        }

        private ApplicationDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique DB for each test
                .Options;
            return new ApplicationDbContext(options);
        } // ❗ SULEB meetodi õigesti

        [Fact]
        public async Task Index_ReturnsViewResult_WithMusicTracks()
        {
            var context = GetInMemoryDbContext();
            context.MusicTracks.Add(new MusicTrack { Title = "Test Track", Artist = "Test Artist", Year = 2025, Pace = 5 });
            context.SaveChanges();

            var controller = GetController(context, null);
            var result = await controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<PagedResult<MusicTrack>>(viewResult.Model); // Muuda siin
            Assert.Single(model.Results); // Kasuta Results omadust
        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenIdIsNull()
        {
            var context = GetInMemoryDbContext();
            var controller = GetController(context, null);

            var result = await controller.Details(null);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsViewResult_WithMusicTrack()
        {
            var context = GetInMemoryDbContext();
            var musicTrack = new MusicTrack { Id = 1, Title = "Test Track", Artist = "Test Artist", Year = 2025, Pace = 4 };
            context.MusicTracks.Add(musicTrack);
            context.SaveChanges();
            var controller = GetController(context, null);

            var result = await controller.Details(1);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<MusicTrack>(viewResult.Model);
            Assert.Equal(musicTrack.Id, model.Id);
        }

        [Fact]
        public async Task Create_Post_ReturnsRedirectToActionResult_WhenModelIsValid()
        {
            var context = GetInMemoryDbContext();
            var musicTrack = new MusicTrack { Id = 1, Title = "New Track", Artist = "New Artist", Year = 2025, Pace = 3 };
            var controller = GetController(context, null);

            var result = await controller.Create(musicTrack);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        [Fact]
        public async Task Edit_ReturnsNotFound_WhenIdIsNull()
        {
            var context = GetInMemoryDbContext();
            var controller = GetController(context, null);

            var result = await controller.Edit(null);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_ReturnsViewResult_WithMusicTrack()
        {
            var context = GetInMemoryDbContext();
            var musicTrack = new MusicTrack { Id = 1, Title = "Test Track", Artist = "Test Artist", Year = 2025, Pace = 2 };
            context.MusicTracks.Add(musicTrack);
            context.SaveChanges();
            var controller = GetController(context, null);

            var result = await controller.Edit(1);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<MusicTrack>(viewResult.Model);
            Assert.Equal(musicTrack.Id, model.Id);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenIdIsNull()
        {
            var context = GetInMemoryDbContext();
            var controller = GetController(context, null);

            var result = await controller.Delete(null);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsViewResult_WithMusicTrack()
        {
            var context = GetInMemoryDbContext();
            var musicTrack = new MusicTrack { Title = "Test Track", Artist = "Test Artist", Year = 2025, Pace = 4 };
            context.MusicTracks.Add(musicTrack);
            context.SaveChanges();
            var controller = GetController(context, null);

            var result = await controller.Delete(musicTrack.Id);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<MusicTrack>(viewResult.Model);
            Assert.Equal(musicTrack.Id, model.Id);
        }

        [Fact]
        public async Task DeleteConfirmed_RemovesMusicTrack()
        {
            var context = GetInMemoryDbContext();
            var musicTrack = new MusicTrack { Id = 1, Title = "Test Track", Artist = "Test Artist", Year = 2025, Pace = 4 };
            context.MusicTracks.Add(musicTrack);
            context.SaveChanges();
            var controller = GetController(context, null);

            var result = await controller.DeleteConfirmed(1);

            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", ((RedirectToActionResult)result).ActionName);
            Assert.False(context.MusicTracks.Any(mt => mt.Id == 1));
        }
    }
}
