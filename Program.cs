using System;
using Microsoft.Data.Sqlite;
using SQLitePCL;

class Program
{
    static void Main(string[] args)
    {
        VeterinaryDatabase veterinaryDatabase = new VeterinaryDatabase();

        while (true)
        {
            Console.WriteLine("Haluatko lisätä lemmikin (L), \n lisätä lemmikin omistajan (O), \n löytää lemmikin omistajan puhelinnumeron (P),\n muuttaa omistajan puhelinnumeroa (V) \n vai Lopettaa(X)?");
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

                    int omistaja_id = veterinaryDatabase.OwnerIdSearching(omistajanNimi, osoite, puhelinnumero);

                    Console.WriteLine("Anna lemmikin tyyppi:");
                    string? tyyppi = Console.ReadLine();

                    veterinaryDatabase.LisaaPet(nimi, omistaja_id, tyyppi);
                    break;

                case "O":
                    Console.WriteLine("Anna omistajan nimi:");
                    string? nimiO = Console.ReadLine();
                    Console.WriteLine("Anna omistajan osoite:");
                    string? osoiteO = Console.ReadLine();
                    Console.WriteLine("Anna omistajan puhelinnumero:");
                    string? puhelinnumeroO = Console.ReadLine();

                    veterinaryDatabase.LisaaOwner(nimiO, osoiteO, puhelinnumeroO);
                    Console.WriteLine("Omistaja lisätty.");
                    break;

                case "P":
                    Console.WriteLine("Anna lemmikin nimi:");
                    string? nimiOmistajan = Console.ReadLine();
                    
                    Console.WriteLine(veterinaryDatabase.SearchingNumber(nimiOmistajan));
                    break;

                case "X":
                    return;
            }
        }
    }
}
