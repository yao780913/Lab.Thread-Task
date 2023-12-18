# [黑暗執行緒 - TIPS - 在多執行緒環境更新共用資料物件](https://blog.darkthread.net/blog/dictionary-thread-safe/)

> - Dictionary 不是安全的執行緒集合類別  
> 1. 安全執行緒集合類別 `ConcurrentDictionary`, `ImmutableDictionary` ...
> 2. 鎖定 (`lock`)

* `TestRunner1` 拋出各種錯誤
* `TestRunner2` 透過 `lock` 來解決
* `TestRunner3` 透過 `ConcurrentDictionary` 來解決