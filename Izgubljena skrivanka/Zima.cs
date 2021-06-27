using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Izgubljena_skrivanka
{
    public class Zima
    {
        public static Random N = new Random();

        public static string Iskana_crka, Moja_crka;

        public static int Poskusi = 4, Trenutna_cifra;

        private static List<string> Crke_kopija = new List<string>()
        {
            "A", "B", "C", "D", "E", "F", "G", "H"
        };

        public static List<int> X = new List<int>();

        public static List<int> Y = new List<int>();

        public static List<string> Crke = new List<string>();

        public static bool Preveri_crki()
        {
            if (Moja_crka == Iskana_crka)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        public static void Uredi_stvari()
        {
            Poskusi = 4;
            Crke.Clear();

            foreach (string Crka in Crke_kopija)
            {
                Crke.Add(Crka);
            }

            Iskana_crka = Crke[N.Next(Crke.Count)];
        }
    }        
}
