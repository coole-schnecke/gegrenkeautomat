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
            int faecher = 20;
            int plaetze_je_fach = 50;
            int plaetze_ges = faecher * plaetze_je_fach;

            Getraenk fanta = new Getraenk("Fanta", 2.2, 10, 0, 0.5);
            Getraenk cola = new Getraenk("Cola", 2.2, 10, 0, 0.5);
            Getraenk spezi = new Getraenk("Spezi", 2.2, 10, 0, 0.5);
            Getraenk apfelschorle = new Getraenk("Apfelschorle", 1.8, 10, 0, 0.3);
            Getraenk sprudel = new Getraenk("Sprudel", 1.5, 10, 0, 0.7);
            Getraenk oragensaft = new Getraenk("Orangensaft", 1.8, 10, 0, 0.3);

            Getraenk[] getraenke = { fanta, cola, spezi, apfelschorle, sprudel, oragensaft };

            Muenze einCent = new Muenze(0.01, 0);
            Muenze zweiCent = new Muenze(0.02, 0);
            Muenze fünfCent = new Muenze(0.05, 0);
            Muenze zehnCent = new Muenze(0.1, 0);
            Muenze zwanzigCent = new Muenze(0.2, 0);
            Muenze fünfzigCent = new Muenze(0.5, 0);
            Muenze einEuro = new Muenze(1, 0);
            Muenze zweiEuro = new Muenze(2, 0);

            Muenze[] muenzen = { zehnCent, zwanzigCent, fünfzigCent, einEuro, zweiEuro };
            Muenze[] muenzen_nicht_akzeptiert = { einCent, zweiCent, fünfCent };

            double guthaben = 0;
            
            bool counter_kaufen = false; //counter überprüfen ob if in Schleifen mindestes einmal ausfegührt wurde  ?????stimmt das
            bool counter_rueckgeld = false;
            bool counter_muenze_pruefen = false;
            bool counter_auffuellen = false;
            bool counter_akzeptierte_muenzen = false;
            bool counter_preisabfrage = false;
            bool counter_nicht_akzepierte_muenze = false;
            bool counter_ja_nein = false;

            int blockiert;
            int blockiert_ges = 0;
            int frei;
            int hinzufügen = 0;
            int anzahl_muenzen_gesamt = 0;

            void fehlermeldung()
            {
                Console.WriteLine("Fehlerhafte Eingabe.");
            }
            void leereZeile()
            {
                Console.WriteLine("");
            }
            void hinweis()
            {
                Console.Write("Hinweis: ");
            }
            void ausgabe_insertGetraenk (int rein, int i)
            {
                getraenke[i].Anzahl += rein;
                if (rein > 1)
                {
                    Console.Write(rein + "x ");
                }
                Console.WriteLine(getraenke[i].Name + " wurde in den Automat gelegt. Anzahl " + getraenke[i].Name + ": " + getraenke[i].Anzahl);

            }
            void akzeptierteMuenzen()
            {
                Console.WriteLine("Hinweis: Der Automat akzeptiert folgende Münzen: ");
                for (int i = muenzen.Length - 1; i >= 0; i--)
                {
                    if (counter_akzeptierte_muenzen == true)
                    {
                        Console.Write(", ");
                    }
                    Console.Write(muenzen[i].Wert + " Euro");
                    counter_akzeptierte_muenzen = true;
                }
                Console.WriteLine(".");
                counter_akzeptierte_muenzen = false;
            }
            void erklaerungCredit() {
                Console.WriteLine("Mit dem Befehl \"credit\" kannst du dein Guthaben abfragen.");
            }
            void erklaerungInsertEuro()
            {
                Console.WriteLine("Um eine Münze in den Automaten zu werfen, nutze den Befehl insertEuro und gib den Wert der Münze an (Beispiel: \"insertEuro " + muenzen[2] + "\").");
            }
            void erklaerungBuy()
            {
                Console.WriteLine("Um ein Getränk zu kaufen, nutze den Befehl buy und gib den Namen des Getränks an (Beispiel: \"buy " + getraenke[getraenke.Length - 1].Name + "\").");
            }
            void inhaltAutomat()
            {
                for (int i = getraenke.Length - 1; i >= 0; i--)
                {
                    Console.WriteLine("Anzahl " + getraenke[i].Name + ": " + getraenke[i].Anzahl);
                }
            }
            void erklaerungContents()
            {
                Console.WriteLine("Mit dem Befehl \"contents\" kannst du den Inhalt des Automaten abfragen.");
            }
            void erklaerungInsertGetraenk1()
            {
                Console.WriteLine("Du kannst den Automaten befüllen, indem du den Befehl insertGetraenk nutzt und den Namen des Getränks angibst (Beispiel: \"insertGetraenk " + getraenke[getraenke.Length - 1].Name + "\").");
            }
            void erklaerungInsertGetraenk2()
            {
                Console.WriteLine("Um mehrere Flaschen eines Getränks auf einmal in den Automaten legen, gib zusätzlich die Anzahl der Flaschen an (Beispiel: \"insertGetraenk " + getraenke[getraenke.Length - 1].Name + " 5\").");
            }
            void anleitung()
            {
                Console.WriteLine("Herzlich Willkommen beim Gegrenkeautomat!");
                leereZeile();
                Console.WriteLine("Folgende Getränke kannst du hier kaufen:");
                for (int g = getraenke.Length - 1; g >= 0; g--)
                {
                    Console.WriteLine(getraenke[g].Name + " (" + getraenke[g].Liter + " Liter): " + getraenke[g].Preis + " Euro");
                }
                leereZeile();
                Console.WriteLine("Um den Preis eines einzelnen Getränks später erneut abzufragen, nutze den Befehl price und gib den Namen des Getränks an (Beispiel: \"price " + getraenke[getraenke.Length - 1].Name + "\").");
                leereZeile();
                Console.WriteLine("Dein derzeitiges Guthaben beträgt " + guthaben + " Euro.");
                erklaerungCredit();
                leereZeile();
                erklaerungInsertEuro();
                Console.Write("");
                akzeptierteMuenzen();
                leereZeile();
                erklaerungBuy();
                leereZeile();
                Console.WriteLine("Derzeitger Inhalt des Automats: ");
                inhaltAutomat();
                leereZeile();
                erklaerungContents();
                leereZeile();
                erklaerungInsertGetraenk1();
                erklaerungInsertGetraenk2();
                leereZeile();
            }

            anleitung();
            while (true == true)
            {
                string eingabe = Console.ReadLine();
                eingabe = eingabe.Trim();
                string[] eingabe_teile = eingabe.Split(' ');

                switch (eingabe_teile[0])
                {
                    case "credit":
                        if (eingabe_teile.Length == 1)
                        {
                            Console.WriteLine("Dein Guthaben beträgt " + guthaben + " Euro."); //gibt Guthaben aus
                        }
                        else
                        {
                            fehlermeldung();
                            hinweis();
                            erklaerungCredit();
                        }
                        break;
                    case "contents":
                        if (eingabe_teile.Length == 1)
                        {
                            inhaltAutomat();
                        }
                        else
                        {
                            fehlermeldung();
                            hinweis();
                            erklaerungContents();
                        }
                        break;
                    case "price":
                        if (eingabe_teile.Length == 2)
                        {
                            for (int i = getraenke.Length -1; i >= 0; i--)
                            {
                                if (eingabe_teile[1] == getraenke[i].Name)
                                {
                                    counter_preisabfrage = true;
                                    Console.WriteLine(getraenke[i].Name + ": " + getraenke[i].Preis + " Euro");
                                }
                            }
                            if (counter_preisabfrage == false)
                            {
                                Console.WriteLine(eingabe_teile[1] + " wird hier nicht verkauft.");
                            }
                        }
                        else
                        {
                            fehlermeldung();
                            hinweis();
                            Console.WriteLine("Um den Preis eines Getränks abzufragen, nutze den Befehl price und gib den Namen des Getränks an (Beispiel: \"price " + getraenke[getraenke.Length - 1].Name + "\").");
                        }
                        break;
                    case "insertEuro":
                        if (eingabe_teile.Length == 2)
                        {
                            try //überpfüft ob es sich um Zahlenwert handelt
                            {
                                double betrag = Convert.ToDouble(eingabe_teile[1]); //versucht Umwandlungn von String zu Zahl
                                for (int i = muenzen.Length - 1; i >= 0; i--)
                                {
                                    if (betrag == muenzen[i].Wert) //überprüft ob es die Münze gibt bzw ob der Automat die Münze annimmt
                                    {
                                        guthaben += muenzen[i].Wert; //erhöht Eingabe um Wert der eingeworfenen Münze
                                        Console.WriteLine(betrag + " Euro Münze wurde eingeworfen, dein Guthaben beträgt " + guthaben + " Euro"); //gibt neues Guthaben aus
                                        counter_muenze_pruefen = true;
                                    }
                                }
                                if (counter_muenze_pruefen == false) //Fehlermeldung wenn Münze nicht angenommen wurde
                                {
                                    for (int i = muenzen_nicht_akzeptiert.Length -1; i >= 0; i--)
                                    {
                                        if (betrag == muenzen_nicht_akzeptiert[i].Wert)
                                        {
                                        counter_nicht_akzepierte_muenze = true;
                                        Console.WriteLine(betrag + " Euro ist keine akzeptierte Münze");
                                        akzeptierteMuenzen();
                                        }
                                    }
                                    if (counter_nicht_akzepierte_muenze == false)
                                    {
                                        Console.WriteLine("Es gibt keine " + betrag + " Euro Münze.");
                                    }
                                    counter_nicht_akzepierte_muenze = false;
                                }
                                counter_muenze_pruefen = false;
                            }
                            catch
                            {
                                Console.WriteLine(eingabe_teile[1] + " ist kein Wert für eine Münze"); //Fehlermeldung wenn Münzwert keine Zahl war
                            }
                        }
                        else
                        {
                            fehlermeldung();
                            hinweis();
                            erklaerungInsertEuro();
                        }
                        break;
                    case "buy":
                        if (eingabe_teile.Length == 2)
                        {
                            for (int i = getraenke.Length - 1; i >= 0; i--)
                            {
                                if (eingabe_teile[1] == getraenke[i].Name)//überpfrüft ob das gewünschte Getränk verkauft wird
                                {
                                    counter_kaufen = true;
                                    if (getraenke[i].Anzahl >= 1) //überfrüft ob das Getränk noch vorhanden ist
                                    {
                                        if (guthaben >= getraenke[i].Preis)//überprüft ob genug Geld eingezahlt wurde
                                        {
                                            getraenke[i].Anzahl--;
                                            guthaben -= getraenke[i].Preis; //Restguthaben
                                            for (int j = muenzen.Length - 1; j >= 0; j--) //berechnet Anzahl der jeweiligen Münzen 
                                            {
                                                while (muenzen[j].Wert <= guthaben)
                                                {
                                                    guthaben = Math.Round(guthaben - muenzen[j].Wert, 1);
                                                    muenzen[j].Anzahl++;
                                                }
                                            }
                                            Console.Write("Du bekommst eine Flasche " + getraenke[i].Name);
                                            for (int j = muenzen.Length - 1; j >= 0; j--)
                                            {
                                                anzahl_muenzen_gesamt += muenzen[j].Anzahl;
                                            }
                                            if (anzahl_muenzen_gesamt != 0)//überprüft ob Automat Geld zurückgeben muss 
                                            {
                                                Console.Write(". Dein Rückgeld ist: ");
                                            }
                                            anzahl_muenzen_gesamt = 0;
                                            for (int j = muenzen.Length - 1; j >= 0; j--)
                                            {
                                                if (muenzen[j].Anzahl != 0)
                                                {
                                                    if (counter_rueckgeld == true)//überprüft ob bereits eine Art von Münze ausgegeben wurde -> falls ja Komma benötigt
                                                    {
                                                        Console.Write(", ");
                                                    }
                                                    Console.Write(muenzen[j].Anzahl + "x " + muenzen[j].Wert + " Euro");
                                                    counter_rueckgeld = true;
                                                }
                                            }
                                            counter_rueckgeld = false;
                                            Console.WriteLine(".");
                                            for (int u = muenzen.Length - 1; u >= 0; u--)//setzt Anzahl der Münzen von Rückgeld zurück
                                            {
                                                muenzen[u].Anzahl = 0;
                                            }
                                        }
                                        else
                                        {
                                            double fehlend = getraenke[i].Preis - guthaben;
                                            Console.WriteLine("Nicht genug Guthaben, es fehlen " + fehlend + " Euro.");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine(getraenke[i].Name + "-Fach ist leer.");
                                    }
                                }
                            }
                            if (counter_kaufen == false)
                            {
                                Console.WriteLine(eingabe_teile[1] + " kann man hier nicht kaufen.");
                            }
                            counter_kaufen = false;
                        }
                        else
                        {
                            fehlermeldung();
                            hinweis();
                            erklaerungBuy();
                        }
                        break;
                    case "insertGetraenk":
                        if ((eingabe_teile.Length == 2) || (eingabe_teile.Length == 3))
                        {
                            for (int i = getraenke.Length - 1; i >= 0; i--)
                            {
                                if (eingabe_teile[1] == getraenke[i].Name) //überprüft ob Getränk teil des Sortiments ist
                                {
                                    counter_auffuellen = true;
                                    if (eingabe_teile.Length == 2)
                                    {
                                        hinzufügen = 1;
                                    }
                                    try
                                    {
                                        if (hinzufügen != 1)
                                        {
                                            hinzufügen = Convert.ToInt32(eingabe_teile[2]);
                                        }
                                        if (hinzufügen > 0)
                                        {
                                            for (int j = getraenke.Length - 1; j >= 0; j--)
                                            {
                                                if (j != i)
                                                {
                                                    blockiert = plaetze_je_fach - (getraenke[j].Anzahl % plaetze_je_fach);
                                                    if (blockiert == plaetze_je_fach)
                                                    {
                                                        blockiert = 0;
                                                    }
                                                    getraenke[j].Blockiert = getraenke[j].Anzahl + blockiert;
                                                    blockiert_ges += getraenke[j].Blockiert;
                                                }
                                            }
                                            frei = plaetze_ges - blockiert_ges - getraenke[i].Anzahl;
                                            if (frei >= hinzufügen)
                                            {
                                                ausgabe_insertGetraenk(hinzufügen, i);
                                            }
                                            else if (frei > 0)
                                            {
                                                Console.WriteLine("Es kann maximal " + frei + "x " + getraenke[i].Name + " hinzugefügt werden.");
                                                Console.WriteLine(frei + "x " + getraenke[i].Name + " hinzufügen? (Erwartete Antwort: \"ja\" oder \"nein\")");
                                                while (counter_ja_nein == false)
                                                {
                                                    string ja_nein_eingabe = Console.ReadLine();
                                                    switch (ja_nein_eingabe)
                                                    {
                                                        case "ja":
                                                            ausgabe_insertGetraenk(frei, i);
                                                            counter_ja_nein = true;
                                                            break;
                                                        case "nein":
                                                            counter_ja_nein = true;
                                                            Console.WriteLine("Okay.");
                                                            break;
                                                        default:
                                                            fehlermeldung();
                                                            Console.WriteLine("Erwartete Antwort: \"ja\" oder \"nein\"");
                                                            break;
                                                    }
                                                }
                                                counter_ja_nein = false;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Alle Fächer belegt.");
                                            }
                                            blockiert_ges = 0;
                                        }
                                        else if (hinzufügen == 0)
                                        {
                                            Console.WriteLine("Zu viel Langeweile? join GRENKE");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Erwischt, du Getränkedieb!");
                                        }
                                        hinzufügen = 0;
                                    }
                                    catch
                                    {
                                        fehlermeldung();
                                        hinweis();
                                        erklaerungInsertGetraenk2(); 
                                    }
                                }
                            }
                            if (counter_auffuellen == false)
                            {
                                if (eingabe_teile.Length == 2)
                                {
                                    Console.WriteLine(eingabe_teile[1] + " kann nicht in den Automat gelegt werden, da " + eingabe_teile[1] + " hier nicht verkauft wird.");
                                }
                                else
                                {
                                    fehlermeldung();
                                    hinweis();
                                    erklaerungInsertGetraenk1();
                                    erklaerungInsertGetraenk2();
                                }
                            }
                            counter_auffuellen = false;
                        }
                        else
                        {
                            fehlermeldung();
                            hinweis();
                            erklaerungInsertGetraenk1();
                            erklaerungInsertGetraenk2();
                        }
                        break;
                    default:
                        Console.WriteLine("Fehler: Befehl ist nicht bekannt");
                        break;
                }
            }
        }
    }
    class Getraenk
    {
        public string Name { get; set; }
        public double Preis { get; set; }
        public int Anzahl { get; set; }
        public int Blockiert { get; set; }
        public double Liter { get; set; }

        public Getraenk (string _name, double _preis, int _anzahl, int _blockiert, double _liter)
        {
            Name = _name;
            Preis = _preis;
            Anzahl = _anzahl;
            Blockiert = _blockiert;
            Liter = _liter;
        }
    }
    class Muenze
    {
        public double Wert { get; set; }
        public int Anzahl { get; set; } //für Rückgeld
        public Muenze (double _wert, int _anzahl)
        {
            Wert = _wert;
            Anzahl = _anzahl;
        }
    }
}
