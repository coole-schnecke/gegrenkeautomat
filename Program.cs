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

            int anzahl_getraenke_anfang = 10; //gibt an wie oft jedes Getränk zu beginn im Automat ist

            Getraenk fanta = new Getraenk("Fanta", 2.2, 0, 0, 0.5, -1);
            Getraenk cola = new Getraenk("Cola", 2.2, 0, 0, 0.5, -1);
            Getraenk spezi = new Getraenk("Spezi", 2.2, 0, 0, 0.5, -1);
            Getraenk apfelschorle = new Getraenk("Apfelschorle", 1.8, 0, 0, 0.3, -1);
            Getraenk sprudel = new Getraenk("Sprudel", 1.5, 0, 0, 0.7, -1);
            Getraenk oragensaft = new Getraenk("Orangensaft", 1.8, 0, 0, 0.3, -1);
            
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

            string[,] inhalt_automat = new string [faecher, plaetze_je_fach];

            for (int i = 0; i < faecher; i++) //setzt alle plätze in inhalt_automat[] auf "leer"
            {
                for (int j = 0; j < plaetze_je_fach; j++)
                {
                    inhalt_automat[i, j] = "leer";
                }
            }

            double guthaben = 0;
            
            bool counter_kaufen = false; //counter überprüfen ob if in Schleifen mind. einmal ausfegührt wurde
            bool counter_rueckgeld = false;
            bool counter_muenze_pruefen = false;
            bool counter_auffuellen = false;
            bool counter_akzeptierte_muenzen = false;
            bool counter_preisabfrage = false;
            bool counter_nicht_akzepierte_muenze = false;
            bool counter_ja_nein = false;
            bool counter_in_fach_legen = false;
            bool counter_aus_fach_nehmen = false;

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
            void ausgabe_insertGetraenk (int hinzu, int i) //Ausgabe wie viele Flaschen des Getränks hinzugefügt wurden + Anzahl im Automat
            {
                if (hinzu > 1)
                {
                    Console.Write(hinzu + "x ");
                }
                Console.WriteLine(getraenke[i].Name + " wurde in den Automat gelegt. Anzahl " + getraenke[i].Name + ": " + getraenke[i].Anzahl);

            }
            void akzeptierteMuenzen() //gibt akzepieterte Münzen aus
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
            void erklaerungInsertEuro()
            {
                Console.WriteLine("Um eine Münze in den Automaten zu werfen, nutze den Befehl insertEuro und gib den Wert der Münze an (Beispiel: \"insertEuro " + muenzen[2].Wert + "\").");
            }
            void erklaerungBuy()
            {
                Console.WriteLine("Um ein Getränk zu kaufen, nutze den Befehl buy und gib den Namen des Getränks an (Beispiel: \"buy " + getraenke[getraenke.Length - 1].Name + "\").");
            }
            void inhaltAutomat() //gibt Anzahl für jedes Getränk aus
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
                for (int g = getraenke.Length - 1; g >= 0; g--) //gibt Name Getränke + Literangabe + Preis aus
                {
                    Console.WriteLine(getraenke[g].Name + " (" + getraenke[g].Liter + " Liter): " + getraenke[g].Preis + " Euro");
                }
                leereZeile();
                Console.WriteLine("Um den Preis eines einzelnen Getränks später erneut abzufragen, nutze den Befehl price und gib den Namen des Getränks an (Beispiel: \"price " + getraenke[getraenke.Length - 1].Name + "\").");
                leereZeile();
                Console.WriteLine("Dein derzeitiges Guthaben beträgt " + guthaben + " Euro."); 
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
            void inFachLegen(int hinzu, int i) //legt Getränke in Fächer
            {
                while(hinzu > 0)
                {
                    if (getraenke[i].AngefangenesFach >= 0) //es gibt ein nicht vollständig gefülltes Fach -> Getränk an ersten freien Platz
                    {
                        for (int j = 0; j < plaetze_je_fach; j++)
                        {
                            if (counter_in_fach_legen == false)
                            {
                                if (inhalt_automat[getraenke[i].AngefangenesFach -1, j] == "leer")
                                {
                                    hinzu--;
                                    inhalt_automat[getraenke[i].AngefangenesFach -1, j] = getraenke[i].Name;
                                    getraenke[i].Anzahl++;
                                    if (j == plaetze_je_fach -1)
                                    {
                                        getraenke[i].AngefangenesFach = -1;
                                    }
                                    counter_in_fach_legen = true;
                                }
                            }
                        }
                        counter_in_fach_legen = false;
                    }
                    else //alle Fächer mit dem Getränk sind vollständig gefüllt -> legt Getränk an ersten Platz im ersten freien Fach
                    {
                        for (int j = 0; j < faecher; j++)
                        {
                            if (counter_in_fach_legen == false)
                            {
                                if (inhalt_automat[j, 0] == "leer")
                                {
                                    getraenke[i].AngefangenesFach = j + 1;
                                    hinzu--;
                                    inhalt_automat[getraenke[i].AngefangenesFach -1, 0] = getraenke[i].Name;
                                    getraenke[i].Anzahl++;
                                    counter_in_fach_legen = true;
                                }
                            }
                        }
                        counter_in_fach_legen = false;
                    }
                }
            }
            void ausFachNehmen (int i)
            {
                if (getraenke[i].AngefangenesFach >= 0) //es gibt ein nicht vollständig gefülltes Fach -> nimmt Getränk von letztem gefüllten Platz
                {
                    for (int j = plaetze_je_fach - 1; j >= 0; j--)
                    {
                        if (counter_aus_fach_nehmen == false)
                        {
                            if (getraenke[i].Name == inhalt_automat[getraenke[i].AngefangenesFach - 1, j])
                            {
                                getraenke[i].Anzahl--;
                                inhalt_automat[getraenke[i].AngefangenesFach - 1, j] = "leer";
                                counter_aus_fach_nehmen = true;
                                if (j == 0)
                                {
                                    getraenke[i].AngefangenesFach = -1;
                                }
                            }
                        }
                    }
                    counter_aus_fach_nehmen = false;
                }
                else //alle Fächer des Getränk sind vollständig gefüllt -> nimmt Getränk von letztem Platz aus dem letztem vollen Fach
                {
                    for (int j = faecher -1; j >= 0; j--)
                    {
                        if (counter_aus_fach_nehmen == false)
                        {
                            if (getraenke[i].Name == inhalt_automat[j, 0])
                            {
                                inhalt_automat[j, plaetze_je_fach - 1] = "leer";
                                getraenke[i].Anzahl--;
                                getraenke[i].AngefangenesFach = j + 1;
                                counter_aus_fach_nehmen = true;
                            }
                        }
                    }
                    counter_aus_fach_nehmen = false;
                }
            }

            for (int i = 0; i < getraenke.Length; i++) //legt Getränke, die sich zu beginn im Automaten bedinden sollen, in Fächer
            {
                inFachLegen(anzahl_getraenke_anfang, i);
            }

            anleitung();
            while (true == true)
            {
                string eingabe = Console.ReadLine();
                eingabe = eingabe.Trim(); 
                string[] eingabe_teile = eingabe.Split(' ');

                switch (eingabe_teile[0])
                {
                    /*case "inhalt": // gibt Inhalt jedes Platzes aus (bzw leer)
                        for (int i = 0; i < faecher; i++)
                        {
                            Console.WriteLine("Fach " + (i + 1) + ":");
                            for (int j = 0; j < plaetze_je_fach; j++)
                            {
                                Console.WriteLine(inhalt_automat[i, j]);
                            }
                        }
                        break;*/
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
                            for (int i = getraenke.Length -1; i >= 0; i--) //prüft ob es angegebenes Getränk gibt
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
                            Console.WriteLine("Hinweis: Um den Preis eines Getränks abzufragen, nutze den Befehl price und gib den Namen des Getränks an (Beispiel: \"price " + getraenke[getraenke.Length - 1].Name + "\").");
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
                                    if (betrag == muenzen[i].Wert) //überprüft ob der Automat die Münze annimmt
                                    {
                                        guthaben += muenzen[i].Wert; //erhöht Eingabe um Wert der eingeworfenen Münze
                                        Console.WriteLine(betrag + " Euro Münze wurde eingeworfen, dein Guthaben beträgt " + guthaben + " Euro"); //gibt neues Guthaben aus
                                        counter_muenze_pruefen = true;
                                    }
                                }
                                if (counter_muenze_pruefen == false) //Fehlermeldung wenn Münze nicht angenommen wurde
                                {
                                    for (int i = muenzen_nicht_akzeptiert.Length -1; i >= 0; i--) //prüft ob es angegebene Münze gibt
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
                                if (eingabe_teile[1] == getraenke[i].Name)//überpfrüft ob es das eingegebene Getränk gibt
                                {
                                    counter_kaufen = true;
                                    if (getraenke[i].Anzahl >= 1) //überfrüft ob das Getränk noch vorhanden ist
                                    {
                                        if (guthaben >= getraenke[i].Preis)//überprüft ob genug Geld eingezahlt wurde
                                        {
                                            ausFachNehmen(i);
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
                                            for (int j = muenzen.Length - 1; j >= 0; j--) //gibt Rückgeld
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
                                    if (eingabe_teile.Length == 2) //setzt hinzufügen auf 1 wenn keine Anzahl angegeben wurde
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
                                            for (int j = getraenke.Length - 1; j >= 0; j--) //berechnet wie viele Plätze durch andere Getränke blockiert werden
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
                                            frei = plaetze_ges - blockiert_ges - getraenke[i].Anzahl; //berechnet wie viele Flaschen des angegebenen Getränks höchstens in den Automaten gelegt werden können
                                            if (frei >= hinzufügen) //genug freie Pätze
                                            {
                                                inFachLegen(hinzufügen, i);
                                                ausgabe_insertGetraenk(hinzufügen, i);
                                            }
                                            else if (frei > 0) //nicht genug freie Plätze aber mind. 1 freier Platz
                                            {
                                                hinzufügen = frei;
                                                Console.WriteLine("Es kann maximal " + frei + "x " + getraenke[i].Name + " hinzugefügt werden.");
                                                Console.WriteLine(hinzufügen + "x " + getraenke[i].Name + " hinzufügen? (Erwartete Antwort: \"ja\" oder \"nein\")");
                                                while (counter_ja_nein == false)
                                                {
                                                    string ja_nein_eingabe = Console.ReadLine();
                                                    switch (ja_nein_eingabe)
                                                    {
                                                        case "ja":
                                                            inFachLegen(hinzufügen, i);
                                                            ausgabe_insertGetraenk(hinzufügen, i);
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
                                        else //negative eingabe
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
        public int Blockiert { get; set; } // gibt an wie viele Plätze durch das Getraenk blockiert werden (belgegte Plätze + freie Plätze in nicht vollständig gefüllten Fächern)
        public double Liter { get; set; } 
        public int AngefangenesFach { get; set; } // wert: 1 bis anzahl_faecher, 0 -> alle Fächer des Getraenks vollständig gefüllt

        public Getraenk (string _name, double _preis, int _anzahl, int _blockiert, double _liter, int _angefangenes_fach)
        {
            Name = _name;
            Preis = _preis;
            Anzahl = _anzahl;
            Blockiert = _blockiert;
            Liter = _liter;
            AngefangenesFach = _angefangenes_fach;
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
