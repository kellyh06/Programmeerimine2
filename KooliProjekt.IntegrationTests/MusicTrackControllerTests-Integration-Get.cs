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
    public class MusicTrackControllerTests_Integration_Get : TestBase
    {
        private readonly HttpClient _client;
        private readonly ApplicationDbContext _dbContext;

        public MusicTrackControllerTests_Integration_Get()
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

            dbContext.MusicTracks.RemoveRange(dbContext.MusicTracks);
            dbContext.SaveChanges();

            return dbContext;
        }

        [Fact]
        public async Task Index_should_return_success()
        {
            using var response = await _client.GetAsync("/MusicTracks");
            response.EnsureSuccessStatusCode();
        }

        [Theory]
        [InlineData("/MusicTracks/Details")]
        [InlineData("/MusicTracks/Details/100")]
        [InlineData("/MusicTracks/Delete")]
        [InlineData("/MusicTracks/Delete/100")]
        [InlineData("/MusicTracks/Edit")]
        [InlineData("/MusicTracks/Edit/100")]
        public async Task Should_return_notfound(string url)
        {
            using var response = await _client.GetAsync(url);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Details_should_return_notfound_when_musicTrack_was_not_found()
        {
            using var response = await _client.GetAsync("/MusicTracks/Details/100");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Details_should_return_success_when_musicTrack_was_found()
        {
            using var dbContext = GetDbContext();

            var musicTrack = new MusicTrack { Title = "MusicTracks" };
            dbContext.MusicTracks.Add(musicTrack);
            dbContext.SaveChanges();

            using var response = await _client.GetAsync($"/MusicTracks/Details/{musicTrack.Id}");
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Create_should_save_new_musicTrack()
        {
            var formValues = new Dictionary<string, string>
            {
                { "Title", "MusicTrack" },
            };

            using var content = new FormUrlEncodedContent(formValues);
            using var response = await _client.PostAsync("/MusicTracks/Create", content);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                Assert.Fail($"Form submission failed with BadRequest. Response: {responseBody}");
            }

            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);

            var artist = _dbContext.MusicTracks.FirstOrDefault();
            Assert.NotNull(response);
            Assert.Equal("MusicTrack", artist.Title);
        }

        [Fact]
        public async Task Create_should_not_save_invalid_new_musicTrack()
        {
            var formValues = new Dictionary<string, string>
            {
                { "Name", "" }

            };

            using var content = new FormUrlEncodedContent(formValues);
            using var response = await _client.PostAsync("/MusicTracks/Create", content);

            response.EnsureSuccessStatusCode();

            using var dbContext = GetDbContext();
            Assert.False(dbContext.MusicTracks.Any());
        }
    }
}
