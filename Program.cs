using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grenkegetränkeautomat
{
    class Program
    {
        static void Main(string[] args)
        {

            double preis_cola = 2.20;
            double preis_fanta = 2.20;
            double preis_spezi = 2.20;
            double preis_apfelschorle = 2;
            double preis_sprudel = 1.50;

            double[] preise = { preis_fanta, preis_cola, preis_spezi, preis_apfelschorle, preis_sprudel };

            string[] getraenke = { "Fanta", "Cola", "Spezi", "Apfelschorle", "Sprudel" };

            int fanta_anzahl = 999;
            int cola_anzahl = 0;
            int spezi_anzahl = 0;
            int apfelschorle_anzahl = 0;
            int sprudel_anzahl = 0;

            int[] getraenke_anzahl = { fanta_anzahl, cola_anzahl, spezi_anzahl, apfelschorle_anzahl, sprudel_anzahl };

            int[] getraenke_voll = { 0, 0, 0, 0, 0 };

            int anzahl_2 = 0;
            int anzahl_1 = 0;
            int anzahl_50 = 0;
            int anzahl_20 = 0;
            int anzahl_10 = 0;

            int[] anzahl_muenzen = { anzahl_10, anzahl_20, anzahl_50, anzahl_1, anzahl_2 };

            double[] muenzen = { 0.1, 0.2, 0.5, 1, 2 };

            double guthaben = 0;

            int counter;

            int dazu;
            int alle;

            while (2 == 2)
            {
                counter = 0;
                alle = 0;
                string eingabe = Console.ReadLine();

                string[] eingabe_teile = eingabe.Split(' ');
                if (eingabe_teile.Length != 2)
                {
                    switch (eingabe)
                    {
                        case "Preise":
                            for (int i = getraenke.Length - 1; i >= 0; i--)
                            {
                                Console.WriteLine(getraenke[i] + ": " + preise[i] + " Euro");
                            }
                            break;
                        case "Inhalt":
                            for (int i = getraenke.Length - 1; i >= 0; i--)
                            {
                                Console.WriteLine("Anzahl " + getraenke[i] + ": " + getraenke_anzahl[i]);

                            }
                            break;
                        case "Guthaben":
                            Console.WriteLine("Dein Guthaben beträgt " + guthaben + " Euro.");
                            break;
                        default:
                            Console.WriteLine("Fehler: Befehl ist nicht bekannt.");
                            break;
                    }
                }
                else
                {
                    switch (eingabe_teile[0])
                    {
                        case "insertEuro":
                            try
                            {
                                double betrag = Convert.ToDouble(eingabe_teile[1]);
                                for (int i = muenzen.Length - 1; i >= 0; i--)
                                {
                                    if (betrag == muenzen[i])
                                    {
                                        guthaben += muenzen[i];
                                        Console.WriteLine(betrag + " Euro wurde eingeworfen, dein Guthaben beträgt " + guthaben + " Euro");
                                        counter++;
                                    }
                                }
                                if (counter == 0)
                                {
                                    Console.WriteLine(betrag + " Euro ist keine akzeptierte Münze");
                                }

                            }
                            catch
                            {
                                Console.WriteLine(eingabe_teile[1] + "ist kein Wert für eine Münze");
                            }
                            break;
                        case "buy":
                            for (int i = getraenke.Length - 1; i >= 0; i--)
                            {
                                if (eingabe_teile[1] == getraenke[i])
                                {
                                    counter++;
                                    if (getraenke_anzahl[i] >= 1)
                                    {
                                        if (guthaben >= preise[i])
                                        {
                                            getraenke_anzahl[i]--;
                                            guthaben -= preise[i];
                                            for (int b = muenzen.Length - 1; b >= 0; b--)
                                            {
                                                while (muenzen[b] <= guthaben)
                                                {
                                                    guthaben -= muenzen[b];
                                                    anzahl_muenzen[b]++;
                                                }
                                            }
                                            Console.WriteLine("Du bekommst eine Flasche " + getraenke[i] + ", dein Rückgeld ist:" + anzahl_muenzen[4] + "x 2 Euro, " + anzahl_muenzen[3] + "x 1 Euro, " + anzahl_muenzen[2] + "x 0.5 Euro, " + anzahl_muenzen[1] + "x 0.2 Euro, " + +anzahl_muenzen[0] + "x 0.1 Euro ");
                                        }
                                        else
                                        {
                                            double fehlend = preise[i] - guthaben;
                                            Console.WriteLine("Nicht genug Guthaben, es fehlen " + fehlend + " Euro.");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine(getraenke[i] + "-Fach ist leer.");
                                    }
                                }
                            }
                            if (counter == 0)
                            {
                                Console.WriteLine(eingabe_teile[1] + " kann man hier nicht kaufen.");
                            }
                            break;
                        case "insertGetraenk":
                            for (int i = getraenke.Length - 1; i >= 0; i--)
                            {
                                if (eingabe_teile[1] == getraenke[i])
                                {
                                    for (int p = getraenke_voll.Length - 1; p >= 0; p--)
                                    {
                                        getraenke_voll[p] = 0;
                                    }
                                    counter++;
                                    getraenke_voll[i]++;
                                    for (int u = getraenke_voll.Length - 1; u >= 0; u--)
                                    {
                                        getraenke_voll[u] += getraenke_anzahl[u];
                                    }
                                    for (int r = getraenke_voll.Length - 1; r >= 0; r--)
                                    {
                                        dazu = 50 - (getraenke_voll[r] % 50);
                                        if (dazu == 50)
                                        {
                                            dazu = 0;
                                        }
                                        getraenke_voll[r] += dazu;
                                    }
                                    for (int z = getraenke_voll.Length - 1; z >= 0; z--)
                                    {
                                        alle += getraenke_voll[z];
                                    }
                                    if (alle <= 1000)
                                    {
                                        getraenke_anzahl[i]++;
                                        Console.WriteLine(getraenke[i] + " wurde in den Automat gelegt. Anzahl " + getraenke[i] + ": " + getraenke_anzahl[i]);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Alle Fächer belegt.");
                                    }


                                }
                            }
                            if (counter == 0)
                            {
                                Console.WriteLine(eingabe_teile[1] + " kann nicht in den Automat gelegt werden, da " + eingabe_teile[1] + " hier nicht verkauft wird.");
                            }
                            break;
                        default:
                            Console.WriteLine("Fehler: Befehl ist nicht bekannt.");
                            break;

                    }
                }
            }

        }
    }
}
