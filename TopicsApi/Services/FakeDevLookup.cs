namespace TopicsApi.Services
{
    public class FakeDevLookup : ILookupOnCallDevs
    {
        public async Task<GetCurrentDevModel> GetCurrentOnCallDevAsync()
        {
            var dev =  new GetCurrentDevModel("Joe", "888-8888", "me@aol.com", DateTime.Now);
            return dev;
        }
    }
}