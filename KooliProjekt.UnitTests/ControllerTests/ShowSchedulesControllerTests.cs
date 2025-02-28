using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using KooliProjekt.Controllers;
using KooliProjekt.Models;
using KooliProjekt.Services;
using KooliProjekt.Data;
using KooliProjekt.Search;

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
    public async Task Index_ReturnsView_WithModel()
    {
        // Arrange
        var model = new ShowSchedulesIndexModel();
        _mockService.Setup(s => s.List(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ShowScheduleSearch>()))
            .ReturnsAsync(model.Data);

        // Act
        var result = await _controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.IsType<ShowSchedulesIndexModel>(viewResult.Model);
    }

    [Fact]
    public async Task Details_ReturnsNotFound_WhenIdIsNull()
    {
        var result = await _controller.Details(null);
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Details_ReturnsNotFound_WhenShowScheduleNotFound()
    {
        _mockService.Setup(s => s.Get(It.IsAny<int>())).ReturnsAsync((ShowSchedule)null);
        var result = await _controller.Details(1);
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Details_ReturnsView_WithShowSchedule()
    {
        var schedule = new ShowSchedule { Id = 1, Date = DateTime.Now };
        _mockService.Setup(s => s.Get(1)).ReturnsAsync(schedule);

        var result = await _controller.Details(1);

        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal(schedule, viewResult.Model);
    }

    [Fact]
    public void Create_ReturnsView()
    {
        var result = _controller.Create();
        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public async Task Create_Post_RedirectsToIndex_WhenModelIsValid()
    {
        var schedule = new ShowSchedule { Id = 1, Date = DateTime.Now };
        _mockService.Setup(s => s.Save(schedule)).Returns(Task.CompletedTask);

        var result = await _controller.Create(schedule);

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirect.ActionName);
    }

    [Fact]
    public async Task Edit_ReturnsNotFound_WhenIdIsNull()
    {
        var result = await _controller.Edit(null);
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Edit_ReturnsView_WithShowSchedule()
    {
        var schedule = new ShowSchedule { Id = 1, Date = DateTime.Now };
        _mockService.Setup(s => s.Get(1)).ReturnsAsync(schedule);

        var result = await _controller.Edit(1);

        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal(schedule, viewResult.Model);
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_WhenIdIsNull()
    {
        var result = await _controller.Delete(null);
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsView_WithShowSchedule()
    {
        var schedule = new ShowSchedule { Id = 1, Date = DateTime.Now };
        _mockService.Setup(s => s.Get(1)).ReturnsAsync(schedule);

        var result = await _controller.Delete(1);

        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal(schedule, viewResult.Model);
    }

    [Fact]
    public async Task DeleteConfirmed_RedirectsToIndex_WhenShowScheduleExists()
    {
        var schedule = new ShowSchedule { Id = 1, Date = DateTime.Now };
        _mockService.Setup(s => s.Get(1)).ReturnsAsync(schedule);
        _mockService.Setup(s => s.Delete(1)).Returns(Task.CompletedTask);

        var result = await _controller.DeleteConfirmed(1);

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirect.ActionName);
    }
}
