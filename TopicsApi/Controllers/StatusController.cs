namespace TopicsApi.Controllers;

public class StatusController : ControllerBase
{
    [HttpGet("status/oncalldev")]

    public ActionResult GetOnCallDev()
    {
        return Ok(); // this returns a 200 status response
    }
}
