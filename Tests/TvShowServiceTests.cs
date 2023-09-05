using Application.IHttpClients;
using Application.IRepositories;
using Application.Services;
using Domain;
using Moq;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class TvShowServiceTests
    {
        private Mock<ITvShowRepository> _mockTvShowRepo;
        private Mock<ITvShowClient> _mockTvShowClient;
        private TvShowService _sut;

        [OneTimeSetUp] public void SetUp()
        {
            _mockTvShowRepo = new Mock<ITvShowRepository>();
            _mockTvShowClient = new Mock<ITvShowClient>();
            _sut = new TvShowService(_mockTvShowRepo.Object, _mockTvShowClient.Object);
        }

        [Test] public async Task AddNewShowsFromApiAsync_NewShowsAvailable_AddToDatabaseAsync()
        {
            // Arrange
            _mockTvShowRepo.Setup(r => r.GetLatestAdded())
                .ReturnsAsync(new TvShow(1, "One", "NL", new DateTime(2020, 01, 01), new List<string> { "action" }, "summary" ));
            _mockTvShowClient.SetupSequence(t => t.GetTvShowsAsync(It.IsAny<int>())
                .ReturnsAsync(new List<TvShow> {
                    new TvShow(1, "One", "NL", new DateTime(2020, 01, 01), new List<string> { "action" }, "summary") })
                .ReturnsAsync<TvShow>(null));

            // Act
            await _sut.AddNewShowsFromApiAsync();

            // Assert

            _mockTvShowClient.Verify(t => t.GetTvShowsAsync(It.IsAny<int>()), Times.Exactly(2));
            _mockTvShowRepo.Verify(t => t.AddBatch(It.IsAny<List<TvShow>>()), Times.Exactly(2));
            // check if all db calls to AddBatch have correct date
            // check if correct page number has been used
        }
    }
}