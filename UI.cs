using Lemmikki;
using Lemmikki.Models;

public class UI
{
    private readonly VeterinaryDatabase _veterinaryDatabase;

    public UI(VeterinaryDatabase veterinaryDatabase)
    {
        _veterinaryDatabase = veterinaryDatabase;
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("Haluatko lisätä lemmikin (L), \n " + 
            "lisätä lemmikin omistajan (O), \n " + 
            "löytää lemmikin omistajan puhelinnumeron (P),\n " +
            "muuttaa omistajan puhelinnumeroa (V) \n "+
            "vai Lopettaa(X)?");
            string? command = Console.ReadLine();

            switch (command)
            {
                case "L":
                    Console.WriteLine("Anna lemmikin nimi:");
                    string? nimi = Console.ReadLine();

                    Console.WriteLine("Anna omistajan nimi:");
                    string? omistajanNimi = Console.ReadLine();

                    Console.WriteLine("Anna omistajan osoite:");
                    string? osoite = Console.ReadLine();

                    Console.WriteLine("Anna omistajan puhelinnumero:");
                    string? puhelinnumero = Console.ReadLine();

                    int omistaja_id = _veterinaryDatabase.OwnerIdSearching(omistajanNimi, osoite, puhelinnumero);

                    Console.WriteLine("Anna lemmikin tyyppi:");
                    string? tyyppi = Console.ReadLine();

                    _veterinaryDatabase.LisaaPet(nimi, omistaja_id, tyyppi);
                    break;

                case "O":
                    Console.WriteLine("Anna omistajan nimi:");
                    string? nimiO = Console.ReadLine();
                    Console.WriteLine("Anna omistajan osoite:");
                    string? osoiteO = Console.ReadLine();
                    Console.WriteLine("Anna omistajan puhelinnumero:");
                    string? puhelinnumeroO = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(nimiO) & !string.IsNullOrWhiteSpace(osoiteO) & !string.IsNullOrWhiteSpace(puhelinnumeroO))
                    {
                        _veterinaryDatabase.LisaaOwner(nimiO, osoiteO, puhelinnumeroO);
                        Console.WriteLine("Omistaja lisätty.");
                    }
                    break;

                case "P":
                    Console.WriteLine("Anna lemmikin nimi:");
                    string? nimiOmistajan = Console.ReadLine();
                    
                    Console.WriteLine(_veterinaryDatabase.SearchingNumber(nimiOmistajan));
                    break;

                case "X":
                    return;
            }
        }
    }
}
