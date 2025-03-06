using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Controllers;
using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Services;
using System.Linq;
using System.Collections.Generic;

public class ShowSchedulesControllerTests
{
    private readonly ShowSchedulesController _controller;
    private readonly Mock<IShowScheduleService> _mockService;
    private readonly ApplicationDbContext _context;

    public ShowSchedulesControllerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new ApplicationDbContext(options);
        _mockService = new Mock<IShowScheduleService>();
        _controller = new ShowSchedulesController(_mockService.Object, _context);
    }

    [Fact]
    public async Task Index_ReturnsViewWithModel()
    {
        var result = await _controller.Index();
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.NotNull(viewResult.Model);
    }

    [Fact]
    public async Task Details_ReturnsNotFound_WhenIdIsNull()
    {
        var result = await _controller.Details(null);
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Create_Post_ValidModel_RedirectsToIndex()
    {
        var showSchedule = new ShowSchedule { Id = 1, date = System.DateTime.Now };
        var result = await _controller.Create(showSchedule);
        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectResult.ActionName);
    }

    [Fact]
    public async Task Edit_Post_InvalidId_ReturnsNotFound()
    {
        var showSchedule = new ShowSchedule { Id = 1, date = System.DateTime.Now };
        var result = await _controller.Edit(2, showSchedule);
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task DeleteConfirmed_RemovesEntityAndRedirects()
    {
        var showSchedule = new ShowSchedule { Id = 1, date = System.DateTime.Now };
        _context.ShowSchedule.Add(showSchedule);
        await _context.SaveChangesAsync();

        var result = await _controller.DeleteConfirmed(1);
        Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal(0, _context.ShowSchedule.Count());
    }
}
