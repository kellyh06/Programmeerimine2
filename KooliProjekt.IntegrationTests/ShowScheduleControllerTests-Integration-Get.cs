using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using KooliProjekt.Data;
using KooliProjekt.IntegrationTests.Helpers;
using KooliProjekt.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace KooliProjekt.IntegrationTests
{
    [Collection("Sequential")]
    public class ShowScheduleControllerTests_Integration_Get : TestBase
    {
        private readonly HttpClient _client;
        private readonly ApplicationDbContext _dbContext;

        public ShowScheduleControllerTests_Integration_Get()
        {
            var options = new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            };

            _client = Factory.CreateClient(options);
            _dbContext = Factory.Services.GetRequiredService<ApplicationDbContext>();
        }

        private ApplicationDbContext GetDbContext()
        {
            var scope = Factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            dbContext.ShowSchedule.RemoveRange(dbContext.ShowSchedule);
            dbContext.SaveChanges();

            return dbContext;
        }

        [Fact]
        public async Task Index_should_return_success()
        {
            using var response = await _client.GetAsync("/ShowSchedules");
            response.EnsureSuccessStatusCode();
        }

        [Theory]
        [InlineData("/ShowSchedules/Details")]
        [InlineData("/ShowSchedules/Details/100")]
        [InlineData("/ShowSchedules/Delete")]
        [InlineData("/ShowSchedules/Delete/100")]
        [InlineData("/ShowSchedules/Edit")]
        [InlineData("/ShowSchedules/Edit/100")]
        public async Task Should_return_notfound(string url)
        {
            using var response = await _client.GetAsync(url);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Details_should_return_notfound_when_showSchedule_was_not_found()
        {
            using var response = await _client.GetAsync("/ShowSchedules/Details/100");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Details_should_return_success_when_showSchedule_was_found()
        {
            using var dbContext = GetDbContext();

            var showSchedule = new ShowSchedule { date = DateTime.UtcNow };
            dbContext.ShowSchedule.Add(showSchedule);
            dbContext.SaveChanges();

            using var response = await _client.GetAsync($"/ShowSchedules/Details/{showSchedule.Id}");
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Create_should_save_new_showSchedule()
        {
            var formValues = new Dictionary<string, string>
            {
                { "date", "2025-04-11" } // peab olema kehtiv kuupäev stringina
            };

            using var content = new FormUrlEncodedContent(formValues);
            using var response = await _client.PostAsync("/ShowSchedules/Create", content);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                Assert.Fail($"Form submission failed with BadRequest. Response: {responseBody}");
            }

            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);

            var savedShow = _dbContext.ShowSchedule.FirstOrDefault();
            Assert.NotNull(savedShow);
            Assert.Equal(new DateTime(2025, 4, 11), savedShow.date);
        }


        [Fact]
        public async Task Create_should_not_save_invalid_new_showSchedule()
        {
            var formValues = new Dictionary<string, string>
            {
                { "date", "" } // kehtetu kuupäev
            };

            using var content = new FormUrlEncodedContent(formValues);
            using var response = await _client.PostAsync("/ShowSchedules/Create", content);

            // Ärge eelda, et see on edukas (nt Redirect), vaid pigem tagastatakse vaade
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            using var dbContext = GetDbContext();
            Assert.False(dbContext.ShowSchedule.Any());
        }

    }
}
