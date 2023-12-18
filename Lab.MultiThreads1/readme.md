# [黑暗執行緒 - 深入 .NET ThreadPool 執行緒數量管理](https://blog.darkthread.net/blog/threadpool-thread-management/)
> 這個實驗要證明為何透過 `ThreadPool.SetMinThreads` 設定最小執行緒數量，可以增進效能。

### `ThreadPool` 怎麼決定何時要增加 `Thread` 呢
- Starvation-Avoidance Mechanism - 當 Queue 中等待項目沒有減少, 則增加工作數量
- Hill-Climbing Mechanism - 設法用較少 Thread 達到最大 Throughput

### 實驗作法
透過 `ThreadPool.QueueUserWorkItem` 來新增工作，並且透過 `Thread.Sleep` 模擬任務執行時間。
- 執行 200 次
- 每一秒增加一個工作
- 透過 `Thread.Sleep(30 * 1000)` 模擬執行 30 秒的任務

### 實驗結果
- **未設定最小執行緒數量**
  - 當有需求時才會建立 Thread
  - 他會檢查目前 Queue 內的任務數量，如果沒有減少，則會增加 Thread 數量
  - 最終總共耗費 115s 才完成
- **設定 `ThreadPool.SetMinThreads(200, 1)`**
  - 一口氣直接建立 200 個 Thread
  - 最終耗費 30s 多就完成了
