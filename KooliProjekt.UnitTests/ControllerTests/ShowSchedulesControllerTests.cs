using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using KooliProjekt.Controllers;
using KooliProjekt.Services;
using KooliProjekt.Models;
using KooliProjekt.Data;

namespace KooliProjekt.Tests.Controllers
{
    public class ShowSchedulesControllerTests
    {
        private readonly Mock<IShowScheduleService> _mockService;
        private readonly ShowSchedulesController _controller;

        public ShowSchedulesControllerTests()
        {
            _mockService = new Mock<IShowScheduleService>();
            _controller = new ShowSchedulesController(_mockService.Object);
        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenIdIsNull()
        {
            // Act
            var result = await _controller.Details(null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenShowScheduleNotFound()
        {
            // Arrange
            _mockService.Setup(s => s.Get(It.IsAny<int>())).ReturnsAsync((ShowSchedule)null);

            // Act
            var result = await _controller.Details(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsView_WithShowSchedule()
        {
            // Arrange
            var schedule = new ShowSchedule { Id = 1, Date = "2025-02-15" };
            _mockService.Setup(s => s.Get(1)).ReturnsAsync(schedule);

            // Act
            var result = await _controller.Details(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(schedule, viewResult.Model);
        }

        [Fact]
        public void Create_ReturnsView()
        {
            // Act
            var result = _controller.Create();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Edit_ReturnsNotFound_WhenIdIsNull()
        {
            // Act
            var result = await _controller.Edit(null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_ReturnsNotFound_WhenShowScheduleNotFound()
        {
            // Arrange
            _mockService.Setup(s => s.Get(It.IsAny<int>())).ReturnsAsync((ShowSchedule)null);

            // Act
            var result = await _controller.Edit(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_ReturnsView_WithShowSchedule()
        {
            // Arrange
            var schedule = new ShowSchedule { Id = 1, Date = "2025-02-15" };
            _mockService.Setup(s => s.Get(1)).ReturnsAsync(schedule);

            // Act
            var result = await _controller.Edit(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(schedule, viewResult.Model);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenIdIsNull()
        {
            // Act
            var result = await _controller.Delete(null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenShowScheduleNotFound()
        {
            // Arrange
            _mockService.Setup(s => s.Get(It.IsAny<int>())).ReturnsAsync((ShowSchedule)null);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsView_WithShowSchedule()
        {
            // Arrange
            var schedule = new ShowSchedule { Id = 1, Date = "2025-02-15" };
            _mockService.Setup(s => s.Get(1)).ReturnsAsync(schedule);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(schedule, viewResult.Model);
        }
    }
}
