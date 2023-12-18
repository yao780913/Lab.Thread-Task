// See https://aka.ms/new-console-template for more information

ThreadPool.GetMinThreads(out var minWorkerThreads, out var minCompletionPortThreads);
ThreadPool.GetMaxThreads(out var maxWorkerThreads, out var maxCompletionPortThreads);

Console.WriteLine($"ThreadPool Min: {minWorkerThreads} {minCompletionPortThreads}");
Console.WriteLine($"ThreadPool Man: {maxWorkerThreads} {maxCompletionPortThreads}");

const int totalCount = 200;

var remaining = totalCount;
var running = 0;

var startTime = DateTime.Now;
using var cts = new CancellationTokenSource();
Task.Run(() =>
{
    Console.WriteLine("Time | Threads | Running | Pending ");
    Console.WriteLine("-----+---------+---------+---------");

    while (!cts.Token.IsCancellationRequested)
    {
        Console.WriteLine(
            $"{(DateTime.Now - startTime).TotalSeconds,3:n0}s | {ThreadPool.ThreadCount,7} | {running,7} | {ThreadPool.PendingWorkItemCount,7}");
        Thread.Sleep(1 * 1000);
    }
});

var tasks = Enumerable
    .Range(1, totalCount)
    .Select(i => 
        Task.Run( async () =>
        {
            // 模擬執行 30 秒的工作
            Interlocked.Increment(ref running);
            await Task.Delay(30 * 1000);
            Interlocked.Decrement(ref remaining);
            Interlocked.Decrement(ref running);
        })).ToArray();

Task.WaitAll(tasks);

cts.Cancel();

/*
 * Time | Threads | Running | Pending 
 * -----+---------+---------+---------
 *   0s |      11 |     200 |       0
 *   1s |      11 |     200 |       0
 *   2s |      11 |     200 |       0
 *   3s |      11 |     200 |       0
 *   4s |      11 |     200 |       0
 *   5s |      11 |     200 |       0
 *   6s |      11 |     200 |       0
 *   7s |      11 |     200 |       0
 *   8s |      11 |     200 |       0
 *   9s |      11 |     200 |       0
 *  10s |      11 |     200 |       0
 *  11s |      11 |     200 |       0
 *  12s |      11 |     200 |       0
 *  13s |      11 |     200 |       0
 *  14s |      11 |     200 |       0
 *  15s |      11 |     200 |       0
 *  16s |      11 |     200 |       0
 *  17s |      11 |     200 |       0
 *  18s |      11 |     200 |       0
 *  19s |      11 |     200 |       0
 *  20s |       1 |     200 |       0
 *  21s |       1 |     200 |       0
 *  22s |       1 |     200 |       0
 *  23s |       1 |     200 |       0
 *  24s |       1 |     200 |       0
 *  25s |       1 |     200 |       0
 *  26s |       1 |     200 |       0
 *  27s |       1 |     200 |       0
 *  28s |       1 |     200 |       0
 *  29s |       1 |     200 |       0
 * 
 */