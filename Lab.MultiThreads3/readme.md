# 新時代 .NET ThreadPool 程式寫法以及為什麼你該用力 async/await
- [原文 - 黑暗執行緒](https://blog.darkthread.net/blog/tpl-threadpool-usage/)

> - 改用 `Task` 可以大幅減少 `Thread` 佔用
> - 非同步 (asynchronous) 目的是提高 產能(Throughput), 而非效能 (Performance)

### 實驗作法
參考 [黑暗執行緒 - 深入 .NET ThreadPool 執行緒數量管理](https://blog.darkthread.net/blog/threadpool-thread-management/) \
這次改成用 `await Task()` 來執行, 僅用 11 條 Thread 即完成任務!

- `Task.Delay` - 釋放執行序給其他 Task 使用
- `Thread.Sleep` - 占用 Thread 直至結束
> 結論: `Task` 可以更有效的增加 **產能(Throughput)**
