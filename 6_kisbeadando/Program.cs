class A
{
    public string inFile { get; set; }
    public List<Szamla> Szamlak { get; set; }
    public double Bevetel { get; set; }

    public void Beolvas()
    {
        
        var sorok = File.ReadAllLines(inFile);
        Szamlak = new List<Szamla>();
        foreach (var sor in sorok)
        {
            var adatok = sor.Split(' ');
            var szamla = new Szamla
            {
                Nev = adatok[0],
                Aruk = new List<Aru>()
            };
            try
            {
                for (int i = 1; i < adatok.Length - 1; i++)
                {
                    var aru = new Aru
                    {
                        cikkszam = adatok[i].Split(' ')[0],
                        Ar = int.Parse(adatok[i + 1].Split(' ')[0])
                    };
                    szamla.Aruk.Add(aru);
                    i++;
                }
                Szamlak.Add(szamla);
            }
            catch (Exception e) { Console.WriteLine(e.Message); }        }
    }
}
class Szamla
{
    public string Nev { get; set; }
    public List<Aru> Aruk { get; set; }

    public double Osszeg()
    {
        double sum = 0;
        foreach (var aru in Aruk)
        {
            sum += aru.Ar;
        }
        return sum;
    }

}

class Aru
{
    public string cikkszam { get; set; }
    public int Ar { get; set; }
}


class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Nem adtál meg fájlnevet!");
            Console.WriteLine("A helyes hasznalat program.exe <filenév>");

            return;
        }
        else
        {
            string inFile = args[0];
            if (!File.Exists(inFile))
            {
                Console.WriteLine("A megadott fájl nem létezik!");
                return;
            }

            A adatok = new A();
            adatok.inFile = inFile;
            adatok.Beolvas();


            //Adatok kiirása teszt céllal
            //foreach (var szamla in adatok.Szamlak)
            //{
            //    Console.WriteLine(szamla.Nev);
            //    foreach (var aru in szamla.Aruk)
            //    {
            //        Console.WriteLine($"\t{aru.cikkszam} {aru.Ar}");
            //    }
            //}

            foreach (var szamla in adatok.Szamlak)
            {
                adatok.Bevetel += szamla.Osszeg();
            }

            Console.WriteLine($"A teljes napi bevetel: {adatok.Bevetel}");

        }
    }
}
