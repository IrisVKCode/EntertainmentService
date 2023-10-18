using Application.IHttpClients;
using Application.IRepositories;
using Application.Services;
using Domain;
using Moq;

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

        [Test]
        public async Task RetrieveNewestShows_NewShowsAvailable_AddToDatabaseAsync()
        {
            // Arrange
            var latestAddedShow = new TvShowBuilder().WithExternalId(512).Build();
            var newShowValidDate = new TvShowBuilder().WithExternalId(513).WithPremiereDate(new DateTime(2020,12,12)).Build();
            var newShowValidDate2 = new TvShowBuilder().WithExternalId(515).WithPremiereDate(new DateTime(2020, 12, 13)).Build();
            var newShowInvalidDate = new TvShowBuilder().WithExternalId(514).WithPremiereDate(new DateTime(2001, 01, 01)).Build();

            _mockTvShowRepo.Setup(repo => repo.GetLatestAdded())
                .ReturnsAsync(latestAddedShow);

            _mockTvShowClient.SetupSequence(cl => cl.GetTvShowsAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<TvShow> { latestAddedShow, newShowValidDate, newShowValidDate2, newShowInvalidDate })
                .ReturnsAsync(new List<TvShow>());

            // Act
            await _sut.RetrieveNewestShows();

            // Assert
            _mockTvShowClient.Verify(cl => cl.GetTvShowsAsync(It.IsAny<int>()), Times.Exactly(2));
            _mockTvShowClient.Verify(cl => cl.GetTvShowsAsync(2));
            _mockTvShowClient.Verify(cl => cl.GetTvShowsAsync(3));

            _mockTvShowRepo.Verify(repo => repo.AddBatch(It.Is<IEnumerable<TvShow>>(shows =>
                shows.Contains(newShowValidDate) &&
                shows.Contains(newShowValidDate2) &&
                !shows.Contains(newShowInvalidDate))), Times.Once);
        }
    }
}