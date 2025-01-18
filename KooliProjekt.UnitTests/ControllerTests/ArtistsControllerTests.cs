﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KooliProjekt.Controllers;
using KooliProjekt.Data;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace KooliProjekt.UnitTests.ControllerTests
{
    public class ArtistsControllerTests
    {
        private readonly Mock<IArtistService> _ArtistServiceMock;
        private readonly ArtistsController _controller;

        public ArtistsControllerTests()
        {
            _ArtistServiceMock = new Mock<IArtistService>();
            _controller = new ArtistsController(_ArtistServiceMock.Object);
        }

        [Fact]
        public async Task Index_should_return_correct_view_with_data()
        {
            // Arrange
            int page = 1;
            var data = new List<Artist>
            {
                new Artist { Id = 1, Title = "Test 1" },
                new Artist { Id = 2, Title = "Test 2" }
            };
            var pagedResult = new PagedResult<Artist> { Results = data };
            _ArtistServiceMock.Setup(x => x.List(page, It.IsAny<int>())).ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.Index(page) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(pagedResult, result.Model);
        }
    }
}