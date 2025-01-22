using Microsoft.AspNetCore.Mvc;
using Refit;
using STD.PL.Records;

namespace STD.PL.Interfaces.API;

public interface IHackerNewsApi
{
    [Headers("Content-Type: application/json;charset=utf-8")]
    [Get("/v0/beststories.json")]
    public Task<List<int>> GetBestStoriesAsync();

    [Headers("Content-Type: application/json;charset=utf-8")]
    [Get("/v0/item/{itemId}.json")]
    public Task<StoryDetail> GetStoryByIdAsync([FromRoute] int itemId);
}