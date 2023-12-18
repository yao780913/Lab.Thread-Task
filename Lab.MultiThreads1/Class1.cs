namespace Lab.MultiThreads;

public class Class1
{
    public static void Run ()
    {
        ThreadPool.GetMinThreads(out var minWorkerThreads, out var minCompletionPortThreads);
        ThreadPool.GetMaxThreads(out var maxWorkerThreads, out var maxCompletionPortThreads);

        Console.WriteLine($"ThreadPool Min: {minWorkerThreads} {minCompletionPortThreads}");
        Console.WriteLine($"ThreadPool Man: {maxWorkerThreads} {maxCompletionPortThreads}");

        const int totalCount = 200;

        var remaining = totalCount;
        var running = 0;

        var startTime = DateTime.Now;
        var stop = false;
        Task.Factory.StartNew(() =>
        {
            Console.WriteLine("Time | Threads | Running | Pending ");
            Console.WriteLine("-----+---------+---------+---------");

            while (!stop)
            {
                Console.WriteLine(
                    $"{(DateTime.Now - startTime).TotalSeconds,3:n0}s | {ThreadPool.ThreadCount,7} | {running,7} | {ThreadPool.PendingWorkItemCount,7}");
                Thread.Sleep(1 * 1000);
            }
        });

        Enumerable.Range(1, totalCount)
            .ToList()
            .ForEach(i =>
            {
                ThreadPool.QueueUserWorkItem(state =>
                {
                    // 模擬執行 30 秒的工作
                    Interlocked.Increment(ref running);
                    Thread.Sleep(30 * 1000);
                    Interlocked.Decrement(ref remaining);
                    Interlocked.Decrement(ref running);
                });
            });

        while (remaining > 0) Thread.Sleep(1 * 1000);

        stop = true;

/*
 *
 * ThreadPool Min: 20 1
 * ThreadPool Min: 32767 1000
 * Time | Threads | Running | Pending
 * -----+---------+---------+---------
 *   0s |       2 |       0 |       0
 *   1s |      21 |      20 |     180
 *   2s |      22 |      21 |     179
 *   3s |      23 |      22 |     178
 *   4s |      24 |      22 |     178
 *   5s |      24 |      23 |     177
 *   6s |      25 |      24 |     176
 *   7s |      27 |      26 |     174
 *   8s |      28 |      27 |     173
 *   9s |      30 |      29 |     171
 *  10s |      31 |      30 |     170
 *  11s |      32 |      31 |     169
 *  12s |      33 |      32 |     168
 *  13s |      34 |      33 |     167
 *  14s |      35 |      34 |     166
 *  15s |      37 |      36 |     164
 *  16s |      39 |      38 |     162
 *  17s |      41 |      40 |     160
 *  18s |      42 |      41 |     159
 *  19s |      43 |      42 |     158
 *  20s |      45 |      44 |     156
 *  21s |      47 |      46 |     154
 *  22s |      48 |      47 |     153
 *  23s |      49 |      48 |     152
 *  24s |      50 |      49 |     151
 *  25s |      51 |      50 |     150
 *  26s |      52 |      51 |     149
 *  27s |      53 |      52 |     148
 *  28s |      54 |      53 |     147
 *  29s |      55 |      54 |     146
 *  30s |      56 |      55 |     126
 *  31s |      56 |      55 |     125
 *  32s |      57 |      56 |     123
 *  33s |      58 |      57 |     121
 *  34s |      59 |      58 |     119
 *  35s |      60 |      59 |     117
 *  36s |      61 |      60 |     115
 *  37s |      61 |      60 |     113
 *  38s |      62 |      61 |     111
 *  39s |      63 |      62 |     109
 *  40s |      64 |      63 |     107
 *  41s |      64 |      63 |     105
 *  42s |      65 |      64 |     103
 *  43s |      66 |      65 |     101
 *  44s |      67 |      66 |      99
 *  45s |      67 |      66 |      97
 *  46s |      67 |      66 |      95
 *  48s |      68 |      67 |      93
 *  49s |      68 |      67 |      91
 *  50s |      69 |      68 |      89
 *  51s |      69 |      68 |      87
 *  52s |      69 |      68 |      85
 *  53s |      70 |      69 |      83
 *  54s |      71 |      70 |      81
 *  55s |      72 |      71 |      79
 *  56s |      73 |      72 |      77
 *  57s |      74 |      73 |      75
 *  58s |      75 |      74 |      73
 *  59s |      76 |      75 |      71
 *  60s |      77 |      76 |      69
 *  61s |      77 |      76 |      50
 *  62s |      78 |      77 |      47
 *  63s |      78 |      77 |      45
 *  64s |      78 |      77 |      43
 *  65s |      78 |      77 |      41
 *  66s |      78 |      77 |      39
 *  67s |      78 |      77 |      37
 *  68s |      78 |      77 |      35
 *  69s |      78 |      77 |      33
 *  70s |      78 |      77 |      31
 *  71s |      78 |      77 |      29
 *  72s |      78 |      77 |      28
 *  73s |      78 |      77 |      26
 *  74s |      78 |      77 |      24
 *  75s |      78 |      77 |      21
 *  76s |      78 |      77 |      19
 *  77s |      78 |      77 |      17
 *  78s |      78 |      77 |      16
 *  79s |      78 |      77 |      14
 *  80s |      78 |      77 |      12
 *  81s |      78 |      77 |      10
 *  82s |      78 |      77 |       8
 *  83s |      78 |      77 |       6
 *  84s |      78 |      77 |       4
 *  85s |      78 |      77 |       2
 *  86s |      78 |      77 |       0
 *  87s |      78 |      75 |       0
 *  88s |      78 |      73 |       0
 *  89s |      78 |      71 |       0
 *  90s |      78 |      69 |       0
 *  91s |      78 |      49 |       0
 *  92s |      78 |      47 |       0
 *  93s |      78 |      45 |       0
 *  94s |      78 |      43 |       0
 *  95s |      78 |      41 |       0
 *  96s |      78 |      39 |       0
 *  97s |      78 |      37 |       0
 *  98s |      78 |      35 |       0
 *  99s |      78 |      33 |       0
 * 100s |      78 |      31 |       0
 * 101s |      78 |      29 |       0
 * 102s |      78 |      27 |       0
 * 103s |      78 |      25 |       0
 * 104s |      78 |      23 |       0
 * 105s |      78 |      21 |       0
 * 106s |      78 |      19 |       0
 * 107s |      76 |      17 |       0
 * 108s |      74 |      15 |       0
 * 109s |      72 |      13 |       0
 * 110s |      51 |      11 |       0
 * 111s |      49 |       9 |       0
 * 112s |      47 |       7 |       0
 * 113s |      45 |       5 |       0
 * 114s |      43 |       3 |       0
 * 115s |      41 |       1 |       0
 */
    }
}