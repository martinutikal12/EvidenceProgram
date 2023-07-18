using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidenceProgram
{
    public class Databaze
    {
        //List kam se budou ukládat instance pojištěnců//
        private List<Pojistenec> pojistenci;
        /// <summary>
        /// Vytvoření instance databáze, kde bude List s pojištěnci
        /// </summary>
        public Databaze() 
        {
            pojistenci = new List<Pojistenec>();
        }
        /// <summary>
        /// Přidá do databáze nového pojištěnce
        /// </summary>
        /// <param name="jmeno">Jméno pojištěnce</param>
        /// <param name="prijmeni">Přijmení pojištěnce</param>
        /// <param name="vek">Věk pojištěnce</param>
        /// <param name="telCislo">Tel. číslo pojištěnce</param>
        public void PridejPojistence(string jmeno, string prijmeni, long vek, long telCislo)
        {
            pojistenci.Add(new Pojistenec(jmeno, prijmeni, vek, telCislo));
        }
        /// <summary>
        /// Vypíše seznam všech pojištěnců 
        /// </summary>
        public List<Pojistenec> VypisVsechnyPojistence()
        {
            return pojistenci;
        }
        /// <summary>
        /// Vyhledá pojištěnce dle jména a příjmení
        /// </summary>
        /// <param name="jmeno">Jméno pojištěnce</param>
        /// <param name="prijmeni">Přijmení pojištěnce</param>
        /// <returns></returns>
        public List<Pojistenec> VyhledejPojistence(string jmeno, string prijmeni)
        {
            var vyhledaniPojistenci = pojistenci.Where(pojistenec => pojistenec.Jmeno == jmeno && pojistenec.Prijmeni == prijmeni).Select(pojistenec => pojistenec).ToList();
            return vyhledaniPojistenci;
        }
        /// <summary>
        /// Odebere pojištěnce s daným jménem a příjmením, pokud se v databázi žádný pojištěnec po tímto jménem nenachází, program vypíše chybovou hlášku. 
        /// </summary>
        /// <param name="jmeno">Jméno pojištěnce</param>
        /// <param name="prijmeni">Příjmení pojištěnce</param>
        public void OdeberPojistence(string jmeno, string prijmeni)
        {
            List<Pojistenec> nalezeniPojistenci = VyhledejPojistence(jmeno, prijmeni);

            if (nalezeniPojistenci.Count > 0)
            {
                Console.WriteLine("Z evidence bude odebrán pojištěnec: {0} {1}", jmeno, prijmeni);
                foreach (Pojistenec pojistenec in nalezeniPojistenci)
                {
                    pojistenci.Remove(pojistenec);
                }
            }
           else//Pokud uživatel zadá jméno, které se v databázi nenachází//
            {
                Console.WriteLine("V evidenci se nenachází žádný pojištěnec s tímto jménem.");
            }
        }
    }
}
