using System.Collections.Concurrent;

public class TestRunner3
{
    public static ConcurrentDictionary<string, string> Cache = new ();
    
    static void UpdateCache(string key, string data)
    {
        Cache.AddOrUpdate(key, data, (k, v) => data);
    }

    public static void Test (int i)
    {
        Random rnd = new ();
        
        Thread.Sleep(rnd.Next(5));
        UpdateCache("MyCache", rnd.Next().ToString());
        Console.WriteLine("Done-{0:0000}", i);
    }

}