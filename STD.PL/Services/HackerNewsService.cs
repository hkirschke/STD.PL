using STD.PL.Helpers;
using STD.PL.Interfaces;
using STD.PL.Interfaces.API;
using STD.PL.Records;

namespace STD.PL.Services;

public class HackerNewsService : IHackerNewsService
{
    private readonly IHackerNewsApi _hackerNewsAPI;

    public HackerNewsService(IHackerNewsApi hackerNewsAPI)
    {
        _hackerNewsAPI = hackerNewsAPI;
    }

    public async Task<List<StoryDetail>> GetBestStoriesAsync(int quantity)
    {
        var storyIds = await _hackerNewsAPI.GetBestStoriesAsync();

        Validations.Create()
            .When(storyIds.Count == 0, "No stories Ids avaliable.")
            .ThrowIfHasExceptions();

        storyIds = storyIds.Take(quantity).ToList();

        var tasks = new List<Task>();

        var result = new List<StoryDetail>();

        storyIds.ForEach(storyId =>
        {
            tasks.Add(Task.Run(async () =>
            {
                var story = await _hackerNewsAPI.GetStoryByIdAsync(storyId);

                if (story != null)
                    result.Add(story);
            }));
        });

        await Task.WhenAll(tasks);

        var orderResult = result.OrderByDescending(x => x.Score).ToList();

        return orderResult;
    }
}