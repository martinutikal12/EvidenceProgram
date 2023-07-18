using EvidenceProgram;

class Program
{
    static void Main(string[] args)
    {
        //Vytvoření nové evidence pojištěných//
        EvidencePojistenych evidence = new EvidencePojistenych();

        char volba = '0';
        //Dokud uživatel nezvolí konec, bude se opakovat cyklus voleb//
        while (volba != '5')
        {
            evidence.VypisUvodniObrazovku();
            Console.WriteLine("Vyberte si akci:");
            Console.WriteLine("1 - Přidat nového pojištěnce");
            Console.WriteLine("2 - Odebrat pojištěnce z evidence.");
            Console.WriteLine("3 - Vypsat seznam všech pojištěnců");
            Console.WriteLine("4 - Vyhledat pojištěnce podle jména a příjmení");
            Console.WriteLine("5 - Konec");
            volba = Console.ReadKey().KeyChar;
            Console.WriteLine("\n");
         //Jednotlivé volby podle toho, co uživatel zvolí//
            switch (volba)
            {
                case '1':
                    evidence.PridejNovehoPojistenceDoDatabaze();
                    Console.WriteLine("\nData byla uložena. Stisknutím libovolné klávesy zvolíte další akci.");
                    break;

                case '2':
                    evidence.OdeberPojistenceZDatabaze();
                    Console.WriteLine("\nData byla aktualizována. Stisknutím libovolné klávesy zvolíte další akci.");
                    break;

                case '3':
                    evidence.VypisVsechnyPojistenceZDatabaze();
                    Console.WriteLine("\nStisknutím libovolné klávesy zvolíte další akci.");
                    break;

                case '4':
                    evidence.VyhledejPojistenceZDatabaze();
                    Console.WriteLine("\nStisknutím libovolné klávesy zvolíte další akci.");
                    break;

                case '5':
                    Console.WriteLine("Program ukončíte stisknutím libovolné klávesy");
                    break;

                default:
                    Console.WriteLine("Neplatná volba! Stiskněte libovolnou klávesu a opakujte volbu.");
                    break;
            }
            Console.ReadKey();
        }
    }
}