using Microsoft.AspNetCore.Mvc;
using Lemmikki.Models;

namespace Lemmikki.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PetsController : ControllerBase
{
    private readonly VeterinaryDatabase _db;

    public PetsController(VeterinaryDatabase db)
    {
        _db = db;
    }

    [HttpPost]
    public IActionResult AddPet([FromBody] Pet pet)
    {
        if (pet == null)
            return BadRequest("Pet data is missing or invalid JSON");

        try
        {
            _db.LisaaPet(pet.Name, pet.Omistaja_id, pet.Tyyppi);
            return Ok("Pet added");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal error: {ex.Message}");
        }
    }

    [HttpGet]
    public IActionResult GetAllPets()
    {
        var pets = _db.GetAllPets();
        return Ok(pets);
    }

}