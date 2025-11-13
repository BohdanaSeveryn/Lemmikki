using Microsoft.AspNetCore.Mvc;
using Lemmikki.Models;
using Lemmikki;
//using Lemmikki.Models;

namespace Lemmikki.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PetsController : ControllerBase{
    private readonly VeterinaryDatabase _db;

    public PetsController(VeterinaryDatabase db)
    {
        _db = db;
    }

    [HttpPost]
    public IActionResult AddPet([FromBody] Pet pet)
    {
        _db.LisaaPet(pet.Nimi, pet.Omistaja_id, pet.Tyyppi);
        return Ok("Pet added");
    }
}