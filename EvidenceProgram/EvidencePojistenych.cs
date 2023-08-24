using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EvidenceProgram
{
    public class EvidencePojistenych
    {
        //Instance databáze//
        private Databaze databaze;
        /// <summary>
        /// Vytvoření instance databáze, která se vytvoří spolu s instancí EvidencePojistenych
        /// </summary>
        public EvidencePojistenych() 
        {
            databaze = new Databaze();
        }
        /// <summary>
        /// Vytvoří textový záznam (jméno nebo příjmení) a ošetří jej proti tomu, když uživatel údaj nevyplní, nebo do jména zadá číslo
        /// </summary>
        /// <returns>Textový řetězec reprezentující jméno nebo příjmení pojištěnce</returns>
        private string VytvorTextovyZaznam()
        {
            string textovyZaznam = "";
            bool platneZadani = false;

            while (!platneZadani)
            {
                textovyZaznam = Console.ReadLine().Trim();

                if (string.IsNullOrWhiteSpace(textovyZaznam))//Nevyplněný údaj//
                {
                    Console.WriteLine("Toto je povinný údaj! Zadejte znovu.");
                    platneZadani = false;
                }
                else if ((Regex.IsMatch(textovyZaznam, @"^[0-9]+$")) ||
                        (!Regex.IsMatch(textovyZaznam, @"^[a-zA-ZěščřžýáíéúůĚŠČŘŽÝÁÍÉÚŮ]+$"))) //Uživatel se pokusí přidat do jména číslo nebo speciální znak//
                {
                    Console.WriteLine("Tento údaj nesmí obsahovat čísla nebo speciální znaky! Zadejte znovu.");
                    platneZadani = false;
                }
                else
                {
                    platneZadani = true;
                }
            }

            return textovyZaznam;
        }
        /// <summary>
        /// Vytvoří číselný záznam (věk nebo tel.číslo) a ošetří jej proti tomu, když uživatel údaj nevyplní, nebo místo čísla zadá jiný text
        /// </summary>
        /// <returns>Číslo reprezentující buď věk a nebo tel. číslo pojištěnce</returns>
        private long VytvorCiselnyZaznam()
        {
            //Použit datový typ long, aby se vypsala správná chybová hláška i v případě, že uživatel zadá delší než devítimístné tel.číslo//
            long ciselnyZaznam = 0;
            bool platnyZaznam = false;

            while(!platnyZaznam)
            {
                string ciselnyZaznamText = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(ciselnyZaznamText))//Nevyplněný údaj//
                {
                    Console.WriteLine("Toto je povinný údaj! Zadejte znovu.");
                    platnyZaznam = false;
                }
                else if (!long.TryParse(ciselnyZaznamText, out _))//Uživatel zadá něco jiného než číslo//
                {
                    Console.WriteLine("Tento údaj musí obsahovat jen číselné znaky! Zadejte znovu.");
                    platnyZaznam = false;
                }
                else
                {
                    ciselnyZaznam = long.Parse(ciselnyZaznamText);
                    platnyZaznam = true;
                }
            }
            return ciselnyZaznam;
        }
        /// <summary>
        /// Přidá do evidence nového pojištěnce
        /// </summary>
        public void PridejNovehoPojistenceDoDatabaze()
        {
            Console.WriteLine("Zadejte jméno pojištěnce:");
            string jmeno = VytvorTextovyZaznam();
            
            Console.WriteLine("Zadejte příjmení pojištěnce:");
            string prijmeni = VytvorTextovyZaznam();
            
            Console.WriteLine("Zadejte věk pojištěnce:");
            long vek = 0;
            bool platnyVek = false;
            //Dotatečné ošetření aby uživatel nemohl zadat nesmyslný věk//
            while(!platnyVek)
            {
                vek = VytvorCiselnyZaznam();
                if (vek > 114) //Věk aktuálně nejstaršího člověka na světě//
                {
                    Console.WriteLine("Zadané číslo je příliš vysoké! Prosím zadejte věk v rozmezí 0 - 114.");
                    platnyVek = false;
                }
                else
                {
                    platnyVek = true;
                }
            }
            
            Console.WriteLine("Zadejte telefonní číslo pojištěnce:");
            long telCislo = 0;
            bool platneTelCislo = false;
            //Dodatečné ošetření aby uživatel zadal tel. číslo ve správném formátu (devítímístné)
            while (!platneTelCislo)
            {
                telCislo = VytvorCiselnyZaznam();
                string telCisloJakoText = telCislo.ToString();

                if (telCisloJakoText.Length == 9)
                {
                    telCislo = long.Parse(telCisloJakoText);
                    platneTelCislo = true;
                }
                else//Neplatný formát tel.čísla//
                {
                    Console.WriteLine("Neplatný formát telefonního čísla! Telefonní číslo musí být devítimístné.");
                    platneTelCislo = false;
                }
            }
            //Pokud jsou všechny hodnoty zadány správně, je do databáze přidán nový pojištěnec//
            databaze.PridejPojistence(jmeno, prijmeni, vek, telCislo);
        }
        /// <summary>
        /// Vypíše všechny pojištěnce, kteří jsou aktuálně v databázi 
        /// </summary>
        public void VypisVsechnyPojistenceZDatabaze() 
        {
            List<Pojistenec> nalezeniPojistenci = databaze.VypisVsechnyPojistence();

            if (nalezeniPojistenci.Count > 0 )
            {
                Console.Clear();
                VypisUvodniObrazovku();
                Console.WriteLine("Aktuální seznam pojištěných osob v evidenci:");
                Console.WriteLine();
                Console.WriteLine("---------------------------------------------------------");
                Console.WriteLine("Jméno:         Příjmení:         Věk:    Telefonní číslo:");
                Console.WriteLine("---------------------------------------------------------");
                foreach (Pojistenec pojistenec in nalezeniPojistenci)
                {
                    Console.WriteLine(pojistenec);
                }
            }
            else//V databázi není žádný pojištěnec//
            {
                Console.Clear();
                VypisUvodniObrazovku();
                Console.WriteLine("Momentálně se v evidenci nenachází žádný pojištěnec.");
            }
        }
        /// <summary>
        /// Vyhledá pojištěnce v databázi podle jména a příjmení a vrátí list všech pojištěnců, jejichž jméno a příjmení se shoduje s dotazem (pro případ, že by v databázi 
        /// bylo více lidí se stejným jménem a příjmením) 
        /// </summary>
        public void VyhledejPojistenceZDatabaze()
        {
            Console.WriteLine("Zadejte jméno pojištěnce:");
            string jmeno = VytvorTextovyZaznam();

            Console.WriteLine("Zadejte příjmení pojištěnce:");
            string prijmeni = VytvorTextovyZaznam();

            List<Pojistenec> nalezeniPojistenci = databaze.VyhledejPojistence(jmeno, prijmeni);

            if (nalezeniPojistenci.Count > 0)
            {
                Console.Clear();
                VypisUvodniObrazovku();
                Console.WriteLine("Nalezeni následující pojištěnci:");
                Console.WriteLine();
                Console.WriteLine("---------------------------------------------------------");
                Console.WriteLine("Jméno:         Příjmení:         Věk:    Telefonní číslo:");
                Console.WriteLine("---------------------------------------------------------");
                foreach (Pojistenec pojistenec in nalezeniPojistenci)
                {
                    Console.WriteLine(pojistenec);
                }
            }
            else//Nenalezeno//
            {
                Console.Clear();
                VypisUvodniObrazovku();
                Console.WriteLine("Nenalezen žádný pojištěnec s tímto jménem.");
            }
        }

        /// <summary>
        /// Odebere z databáze pojištěnce s daným jménem a příjmením
        /// </summary>
        public void OdeberPojistenceZDatabaze()
        {
            Console.WriteLine("Zadejte jméno pojištěnce:");
            string jmeno = VytvorTextovyZaznam();

            Console.WriteLine("Zadejte příjmení pojištěnce:");
            string prijmeni = VytvorTextovyZaznam();

            Console.Clear();
            VypisUvodniObrazovku();
            databaze.OdeberPojistence(jmeno, prijmeni);
        }
        /// <summary>
        /// Vypíše úvodní obrazovku programu
        /// </summary>
        public void VypisUvodniObrazovku()
        {
            Console.Clear();
            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine("Evidence pojištěných                    Datum: {0}", DateTime.Now.ToShortDateString());
            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine();
        }
    }
}
