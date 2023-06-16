using Hangfire;
using HangfireExemplar.Model;
using HangfireExemplar.Services;
using Microsoft.AspNetCore.Mvc;

namespace HangfireExemplar.Controllers;

[ApiController]
[Route("[controller]")]
public class HangfireController : ControllerBase
{
    private readonly ILogger<HangfireController> _logger;
    private static readonly List<Driver> _drivers = new List<Driver>();

    public HangfireController(ILogger<HangfireController> logger)
    {
        _logger = logger;
    }
    
    [HttpPost]
    public IActionResult AddDriver(Driver driver) // Use automapper later
    {
        if (!ModelState.IsValid) return BadRequest();
        _drivers.Add(driver);
        var jobId = BackgroundJob.Enqueue<IServiceManagement>(x => x.SendEmail());
        _logger.LogInformation("JobId: {JobId}", jobId);
        return CreatedAtAction("GetDriver", new { id = driver.Id }, driver);
    }
    
    [HttpGet]
    public IActionResult GetDriver(Guid id)
    {
        var driver = _drivers.FirstOrDefault(x => x.Id == id);
        if(driver == null)
        {
            return NotFound();
        }

        return Ok(driver);
    }
    
    [HttpDelete]
    public IActionResult DeleteDriver(Guid id)
    {
        var driver = _drivers.FirstOrDefault(x => x.Id == id);
        if(driver == null)
        {
            return NotFound();
        }

        driver.Status = 0;
        RecurringJob.AddOrUpdate<IServiceManagement>(x => x.UpdateDatabase(), Cron.Hourly);
        return NoContent();
    }
    
}