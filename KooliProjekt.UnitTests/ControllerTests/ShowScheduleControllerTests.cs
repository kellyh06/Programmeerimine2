using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using KooliProjekt.Controllers;
using KooliProjekt.Models;
using Xunit;
using KooliProjekt.Data;
using KooliProjekt.Services;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Tests.Controllers
{
    public class ShowSchedulesControllerTests
    {
        private readonly ShowSchedulesController _controller;
        private readonly Mock<IShowScheduleService> _serviceMock;

        public ShowSchedulesControllerTests()
        {
            _serviceMock = new Mock<IShowScheduleService>();
            _controller = new ShowSchedulesController(_serviceMock.Object, null);
        }

        // Test 'Create' meetod edukas
        [Fact]
        public async Task Create_ReturnsRedirectToAction_WhenModelIsValid()
        {
            var showSchedule = new ShowSchedule { Id = 1, date = DateTime.Now };

            _serviceMock.Setup(service => service.AddShowScheduleAsync(showSchedule))
                .Returns(Task.CompletedTask); // Simuleeri, et andmed lisatakse

            var result = await _controller.Create(showSchedule);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        // Test 'Create' meetod, kui ModelState on invalid
        [Fact]
        public async Task Create_ReturnsView_WhenModelIsInvalid()
        {
            _controller.ModelState.AddModelError("Date", "Required"); // Lisage validatsiooni viga
            var result = await _controller.Create(new ShowSchedule()) as ViewResult;

            var viewResult = Assert.IsType<ViewResult>(result);
            var isCorrectViewName = string.IsNullOrWhiteSpace(viewResult.ViewName) || result.ViewName == "Create";
            Assert.True(isCorrectViewName);
        }

        // Test 'Edit' meetod, kui 'id' on null
        [Fact]
        public async Task Edit_ReturnsNotFound_WhenIdIsNull()
        {
            var result = await _controller.Edit(null);

            Assert.IsType<NotFoundResult>(result);
        }

        // Test 'Edit' meetod, kui ModelState on valideeritud ja kõik on õige
        [Fact]
        public async Task Edit_ReturnsRedirectToAction_WhenModelStateIsValid()
        {
            var showSchedule = new ShowSchedule { Id = 1, date = DateTime.Now };

            _serviceMock.Setup(service => service.UpdateShowScheduleAsync(1, showSchedule))
                .Returns(Task.CompletedTask); // Simuleeri, et andmed uuendatakse

            var result = await _controller.Edit(1, showSchedule);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        // Test 'Edit' meetod, kus on konkurentsi erand
        [Fact]
        public async Task Edit_ReturnsRedirectToAction_WhenConcurrencyErrorOccurs()
        {
            var showSchedule = new ShowSchedule { Id = 1, date = DateTime.Now };

            _serviceMock.Setup(service => service.UpdateShowScheduleAsync(1, It.IsAny<ShowSchedule>()))
                .Throws(new DbUpdateConcurrencyException()); // Simuleeri konkurentsi viga

            var result = await _controller.Edit(1, showSchedule);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Edit", redirectResult.ActionName);  // Oodatud on "Edit" (mitte "Index")
            Assert.Equal(1, redirectResult.RouteValues["id"]);
        }

        // Test 'Delete' meetod, kui 'id' on null
        [Fact]
        public async Task Delete_ReturnsNotFound_WhenIdIsNull()
        {
            var result = await _controller.Delete(null);

            Assert.IsType<NotFoundResult>(result);
        }

        // Test 'DeleteConfirmed' meetod, kui objekt eemaldatakse
        [Fact]
        public async Task DeleteConfirmed_RemovesShowSchedule()
        {
            var showSchedule = new ShowSchedule { Id = 1, date = DateTime.Now };

            _serviceMock.Setup(service => service.DeleteShowScheduleAsync(1))
                .Returns(Task.CompletedTask); // Simuleeri, et objekt on eemaldatud

            var result = await _controller.DeleteConfirmed(1);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        // Test 'Details' meetod, kui id on null
        [Fact]
        public async Task Details_ReturnsNotFound_WhenIdIsNull()
        {
            var result = await _controller.Details(null);

            Assert.IsType<NotFoundResult>(result);
        }

        // Test 'Details' meetod, kui showSchedule on null
        [Fact]
        public async Task Details_ReturnsNotFound_WhenShowScheduleIsNull()
        {
            _serviceMock.Setup(service => service.GetShowScheduleByIdAsync(1))
                .ReturnsAsync((ShowSchedule)null); // Simuleeri, et andmed puuduvad

            var result = await _controller.Details(1);

            Assert.IsType<NotFoundResult>(result);
        }

        // Test 'Index' meetod, kui leht on tühi
        [Fact]
        public async Task Index_ReturnsViewResult_WithData()
        {
            _serviceMock.Setup(service => service.GetAllShowSchedulesAsync())
                .ReturnsAsync(new[] { new ShowSchedule() }); // Simuleeri, et andmed on olemas

            var result = await _controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult.Model); // Ootame, et mudel oleks olemas
        }
    }
}
