using STD.PL.Records;

namespace STD.PL.Interfaces;

public interface IHackerNewsService
{
    Task<List<StoryDetail>> GetBestStoriesAsync(int quantity);
}