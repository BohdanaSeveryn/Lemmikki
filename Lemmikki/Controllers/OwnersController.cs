using Microsoft.AspNetCore.Mvc;
using Lemmikki;
using Lemmikki.Models;

namespace Lemmikki.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OwnersController : ControllerBase
{
    private readonly VeterinaryDatabase _db;

    public OwnersController(VeterinaryDatabase db)
    {
        _db = db;
    }

    [HttpGet("owner-phone")]
    public IActionResult GetOwnerPhone([FromQuery] string petName)
    {
        var phone = _db.SearchingNumber(petName);
        if (phone == null)
            return NotFound("Owner not found for this pet");

        return Ok(new { phone });
    }
    [HttpGet]
    public IActionResult GetAllOwners()
    {
        var owners = _db.GetAllOwners();
        return Ok(owners);
    }

        [HttpPost]
    public IActionResult AddOwner([FromBody] Owner owner)
    {
        if (owner == null)
            return BadRequest("Owner data is missing or invalid JSON");

        try
        {
            _db.LisaaOwner(owner.Name, owner.Puhelinnumero, owner.Osoite);
            return Ok("Owner added");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal error: {ex.Message}");
        }
    }
}