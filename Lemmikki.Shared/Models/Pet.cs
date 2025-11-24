namespace Lemmikki.Models;

public class Pet
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Tyyppi { get; set; } = "";
    public int Omistaja_id { get; set; }
}