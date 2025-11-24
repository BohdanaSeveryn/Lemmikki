using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Lemmikki.Models;

public class MainWindowViewModel
{
    public List<Pet> Pets { get; set; } = new();
    public List<Owner> Owners { get; set; } = new();

    public async Task LoadPets()
    {
        using var client = new HttpClient();
        var response = await client.GetStringAsync("http://localhost:5001/api/pets");
        Pets = JsonSerializer.Deserialize<List<Pet>>(response) ?? new List<Pet>();
    }

    public async Task LoadOwners()
    {
        using var client = new HttpClient();
        var response = await client.GetStringAsync("http://localhost:5001/api/owners");
        Owners = JsonSerializer.Deserialize<List<Owner>>(response) ?? new List<Owner>();
    }
}
