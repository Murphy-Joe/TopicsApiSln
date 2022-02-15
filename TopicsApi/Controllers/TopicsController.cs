namespace TopicsApi.Controllers
{
    public class TopicsController: ControllerBase
    {
        //private readonly TopicsDataContext _context;
        private readonly IProvideTopicsData _topicsData;

        public TopicsController(IProvideTopicsData topicsData)
        {
            _topicsData = topicsData;
        }

        [HttpPost("topics")]
        public async Task<IActionResult> AddTopicAsync([FromBody] PostTopicRequestModel request)
        {
            // 1. validate the incoming data
            // 2. if its invalid
            //      a) send a 400
            //      b) can be 'nice' and tell them they did something wrong, risky
            // 3. If its valied
            //      a) do the work (side effects). (for us, add it to the database, etc)
            //      b) return a
            //          201 Created status code
            //          Add a location header to the response with the URI of the new resource (Location: http://localhost:1337/topics/3)
            //          Maybe just give them a copy of what they'd get from that URI
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resp = await _topicsData.AddTopicAsync(request);

            // 201 should have a location header and a copy of the object that was created
            return CreatedAtRoute("topics.getbyidasync", new {topicId = resp.id}, resp);
        }

        [HttpGet("topics/{topicId:int}", Name = "topics.getbyidasync")]
        public async Task<IActionResult> GetTopic(int topicId)
        {
            var resp = await _topicsData.GetTopicByIdAsync(topicId);
            return Ok(resp);
        }

        [HttpGet("topics")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 10)]
        public async Task<IActionResult> GetTopicsAsync()
        {
            var resp = await _topicsData.GetAllTopics();
            //var data = await _context.Topics.ToListAsync();
            return Ok(resp);
        }
    }
} 
