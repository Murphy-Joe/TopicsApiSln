namespace TopicsApi.Services;

public interface IProvideTopicsData
{
    Task<GetTopicsModel> GetAllTopics();
    Task<GetTopicListItemModel> GetTopicByIdAsync(int topicId);
    Task<GetTopicListItemModel> AddTopicAsync(PostTopicRequestModel request);
}
