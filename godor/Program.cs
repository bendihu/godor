namespace godor;

public class Program
{
    static List<int> list = new List<int>();
    static int bTav;
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Feladat1();
        Feladat2();
        Feladat3();
        Feladat4();
        Feladat5();
        Feladat6();

        Console.ReadKey();
    }
    private static void Feladat1()
    {
        Console.WriteLine("1. feladat");

        StreamReader sr = new StreamReader(@"melyseg.txt");
        while (!sr.EndOfStream) list.Add(int.Parse(sr.ReadLine()));
        sr.Close();

        Console.WriteLine($"A fájl adatainak száma: {list.Count}\n");
    }
    private static void Feladat2()
    {
        Console.WriteLine("2. feladat");

        Console.Write("Adjon meg egy távolságértéket: ");
        bTav = int.Parse(Console.ReadLine()) - 1;

        Console.WriteLine($"Ezen a helyen a felszín {list[bTav]} méter mélyen van.\n");
    }
    private static void Feladat3()
    {
        Console.WriteLine("3. feladat");

        var erintetlen = list.Where(x => x == 0).Count();
        decimal szazalek = (decimal)erintetlen / list.Count * 100;

        Console.WriteLine($"Az érintetlen terület aránya {szazalek:N2}%.\n");
    }
    private static void Feladat4()
    {
        StreamWriter sw = new StreamWriter(@"godrok.txt");

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] != 0)
            {
                sw.Write(list[i]);

                if (list[i + 1] == 0) sw.WriteLine();
            }
        }

        sw.Close();
    }
    private static void Feladat5()
    {
        Console.WriteLine("5. feladat");

        int count = 0;

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] != 0 && list[i + 1] == 0) count++;
        }

        Console.WriteLine($"A gödrök száma: {count}\n");
    }
    private static void Feladat6()
    {
        Console.WriteLine("6. feladat");

        if (list[bTav] == 0)
        {
            Console.WriteLine("Az adott helyen nincs gödör.");
            return;
        }

        #region a)
        List<int> godor = new List<int>();
        int kezd = 0, veg = 0;
        int count = bTav;

        while (list[count] != 0)
        {
            godor.Add(list[count]);
            kezd = count;
            count--;
        }

        godor.RemoveAt(0);
        godor = godor.OrderByDescending(x => x).ToList();
        count = bTav;

        while (list[count] != 0)
        {
            godor.Add(list[count]);
            veg = count;
            count++;
        }

        Console.WriteLine($"a)\nA gödör kezdete: {kezd + 1} méter, a gödör vége: {veg + 1} méter.");
        #endregion

        #region b)
        int max = godor.Max();

        int e = 1;
        while (e < godor.Count && godor[e - 1] <= godor[e]) e++;

        int v = godor.Count - 2;
        while (v >= 0 && godor[v + 1] <= godor[v]) v--;

        Console.WriteLine(e > v ? "b)\nFolyamatosan mélyül." : "b)\nNem mélyül folyamatosan.");
        #endregion

        #region c)
        Console.WriteLine($"c)\nA legnagyobb mélysége {godor.Max()} méter.");
        #endregion

        #region d)
        Console.WriteLine($"d)\nA térfogata {godor.Sum() * 10} m^3.");
        #endregion

        #region e)
        Console.WriteLine($"e)\nA vízmennyiség {(godor.Sum() - godor.Count) * 10} m^3.");
        #endregion
    }
}