// See https://aka.ms/new-console-template for more information

using Lab.MultiThreads2;

for (var i = 0; i < 9999; i++)
{
    var idx = i;
    ThreadPool.QueueUserWorkItem(o =>
    {
        try
        {
            // TestRunner1.Test(idx);
            TestRunner2.Test(idx);
            // TestRunner3.Test(idx);
        }
        catch (IndexOutOfRangeException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    });
}
Console.Read();