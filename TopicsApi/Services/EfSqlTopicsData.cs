using AutoMapper;

namespace TopicsApi.Services;

public class EfSqlTopicsData : IProvideTopicsData
{
    private readonly TopicsDataContext _context;
    private readonly IMapper _mapper;
    private readonly MapperConfiguration _config;

    public EfSqlTopicsData(TopicsDataContext context, IMapper mapper, MapperConfiguration config)
    {
        _context = context;
        _mapper = mapper;
        _config = config;
    }

    private IQueryable<Topic> GetTopics()
    {
        return _context.Topics!
        .Where(t => t.IsDeleted == false);
    }

    public async Task<GetTopicListItemModel> AddTopicAsync(PostTopicRequestModel request)
    {
        var topic = new Topic { Name = request.Name, Description = request.Description };
        _context.Topics!.Add(topic); // no id for the topic (the default id, 0
        await _context.SaveChangesAsync(); // after this, it has the database id
        var result = new GetTopicListItemModel(topic.Id.ToString(), topic.Name, topic.Description);
        return result;
    }

    public async Task<GetTopicsModel> GetAllTopics()
    {
        var data = await GetTopics()
            .Select(t => new GetTopicListItemModel(t.Id.ToString(), t.Name, t.Description)).ToListAsync();
        return new GetTopicsModel(data);
    }

    public async Task<GetTopicListItemModel?> GetTopicByIdAsync(int topicId)
    {
        var data = await GetTopics()
        .Where(t => t.Id == topicId)
        .Select(t => new GetTopicListItemModel(t.Id.ToString(), t.Name, t.Description))
        .SingleOrDefaultAsync();
        return data;
    }
}
