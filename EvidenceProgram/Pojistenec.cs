using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidenceProgram
{
    public class Pojistenec
    {
        //Jméno pojištěnce//
        public string Jmeno { get; set; }
        //Příjmení pojištěnce//
        public string Prijmeni { get; set; }
        //Věk pojištěnce//
        public long Vek { get; set; }
        //Telefonní číslo pojištěnce//
        public long TelCislo { get; set; }
        /// <summary>
        /// Vytvoření nové instance pojištěnce
        /// </summary>
        /// <param name="jmeno">Jméno</param>
        /// <param name="prijmeni">Příjmení</param>
        /// <param name="vek">Věk</param>
        /// <param name="telCislo">Telefonní číslo</param>
        public Pojistenec(string jmeno, string prijmeni, long vek, long telCislo)
        {
            Jmeno = jmeno;
            Prijmeni = prijmeni;
            Vek = vek;
            TelCislo = telCislo;
        }
        /// <summary>
        /// Textová reprezentace instance pojištěnce
        /// </summary>
        /// <returns>Vrátí textem jméno, přijmení, věk a tel.číslo daného pojištěnce</returns>
        public override string ToString() 
        {
            return Jmeno.PadRight(15) + Prijmeni.PadRight(15) + "   " + Vek + "      " + TelCislo;
        }
    }
}
