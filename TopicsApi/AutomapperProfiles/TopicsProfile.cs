using AutoMapper;

namespace TopicsApi.AutomapperProfiles;

public class TopicsProfile : Profile
{
    public TopicsProfile()
    {
        // topic => GetTopicListModel model
        CreateMap<Topic, GetTopicListItemModel>()
            .ForMember(dest => dest.id, cfg => cfg.MapFrom(src => src.Id.ToString()));

        // postTopicReqModel -> Topic
        CreateMap<PostTopicRequestModel, Topic>()
            .ForMember(dest => dest.IsDeleted, cfg => cfg.MapFrom(_ => false));
    }
}
