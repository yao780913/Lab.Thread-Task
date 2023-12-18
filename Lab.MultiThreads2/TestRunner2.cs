namespace Lab.MultiThreads2;

public class TestRunner2
{
    public static Dictionary<string, string> Cache = new ();

    static void UpdateCache (string key, string data)
    {
        lock (Cache)
        {
            Cache.Remove(key);
            Cache.Add(key, data);
        }
    }

    public static void Test (int i)
    {
        Random rnd = new ();
        
        Thread.Sleep(rnd.Next(5));
        UpdateCache("MyCache", rnd.Next().ToString());
        Console.WriteLine("Done-{0:0000}", i);
    }
}