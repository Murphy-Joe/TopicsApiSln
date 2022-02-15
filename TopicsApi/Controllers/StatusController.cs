namespace TopicsApi.Controllers;

public class StatusController : ControllerBase
{
    private readonly ILookupOnCallDevs _onCallDevLookup;

    public StatusController(ILookupOnCallDevs onCallDevLookup)
    {
        _onCallDevLookup = onCallDevLookup;
    }

    [HttpGet("status/oncalldev")]

    public async Task<ActionResult<GetCurrentDevModel>> GetOnCallDev()
    {
        GetCurrentDevModel resp = await _onCallDevLookup.GetCurrentOnCallDevAsync();
        return Ok(resp); // this returns a 200 status response
    }
}
