using AutoFixture;
using Moq;
using STD.PL.Exceptions;
using STD.PL.Interfaces.API;
using STD.PL.Records;
using STD.PL.Services;

namespace STD.PL.Test;

[TestClass]
public class HackerNewsServiceTest
{
    private readonly IFixture _fixture;

    public HackerNewsServiceTest()
    {
        _fixture = new Fixture();
    }

    [TestMethod]
    public async Task GetBestStories()
    {
        // Arrange
        var hackerNewsApiMock = new Mock<IHackerNewsApi>();

        var quantity = new Random().Next(10, 50);

        var storiesId = _fixture.CreateMany<int>(quantity).ToList();

        var story = _fixture.Create<StoryDetail>();

        // Act
        hackerNewsApiMock
              .Setup(_ => _.GetBestStoriesAsync())
              .ReturnsAsync(storiesId);

        hackerNewsApiMock
              .Setup(_ => _.GetStoryByIdAsync(It.IsAny<int>()))
              .ReturnsAsync(story);

        var service = new HackerNewsService(hackerNewsApiMock.Object);

        var result = await service.GetBestStoriesAsync(quantity);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Count == quantity);
    }

    [TestMethod]
    public async Task GetBestStoriesWithoutResult()
    {
        // Arrange
        var hackerNewsApiMock = new Mock<IHackerNewsApi>();

        var quantity = 0;

        var storiesId = _fixture.CreateMany<int>(quantity).ToList();

        var story = _fixture.Create<StoryDetail>();

        // Act
        hackerNewsApiMock
              .Setup(_ => _.GetBestStoriesAsync())
              .ReturnsAsync(storiesId);

        hackerNewsApiMock
              .Setup(_ => _.GetStoryByIdAsync(It.IsAny<int>()))
              .ReturnsAsync(story);

        var service = new HackerNewsService(hackerNewsApiMock.Object);

        // Assert 
        await Assert.ThrowsExceptionAsync<DomainException>(async () => await service.GetBestStoriesAsync(quantity));
    }

    [TestMethod]
    public async Task CheckBestStoriesOrderByScore()
    {
        // Arrange
        var hackerNewsApiMock = new Mock<IHackerNewsApi>();

        var quantity = 2;

        var storiesId = new List<int> { 1, 2 };

        var storyTest1 = _fixture.Create<StoryDetail>();
        var storyTest2 = _fixture.Create<StoryDetail>();

        // Act
        hackerNewsApiMock
              .Setup(_ => _.GetBestStoriesAsync())
              .ReturnsAsync(storiesId);

        hackerNewsApiMock
              .Setup(_ => _.GetStoryByIdAsync(1))
              .ReturnsAsync(storyTest1);

        hackerNewsApiMock
              .Setup(_ => _.GetStoryByIdAsync(2))
              .ReturnsAsync(storyTest2);

        var service = new HackerNewsService(hackerNewsApiMock.Object);
        var result = await service.GetBestStoriesAsync(quantity);

        // Assert 
        var first = result.First();
        var last = result.Last();

        Assert.IsTrue(first.Score > last.Score);
    }
}