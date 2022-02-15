namespace TopicsApi.Services
{
    public interface ILookupOnCallDevs
    {
        Task<GetCurrentDevModel> GetCurrentOnCallDevAsync();
    }
}
