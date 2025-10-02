namespace Lemmikki.Models;

public class Owner
{
    public int Id { get; set; }
    public string Nimi { get; set; } = "";
    public string Puhelinnumero { get; set; } = "";
    public int Osoite { get; set; }
}