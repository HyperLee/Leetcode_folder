# 146. LRU Cache

## 題目說明

實作一個 LRU (Least Recently Used, 最近最少使用) 快取機制，支援 get 和 put 兩種操作，要求時間複雜度皆為 O (1)。

- **get(key)** ：如果 key 存在於快取中，則返回其值，否則返回 -1。每次 get 操作都會將該 key 對應的資料移動到最近使用的位置。
- **put(key, value)** ：如果 key 已存在，更新其值，否則插入新資料。當快取容量超過上限時，會移除最久未使用的資料。

你必須設計一個資料結構，使這兩個操作的時間複雜度皆為 O (1)。

LeetCode 題目連結：

- [LeetCode 英文](https://leetcode.com/problems/lru-cache/description/)
- [LeetCode 中文](https://leetcode.cn/problems/lru-cache/description/)

## 解題思路

- 使用「雙向鏈結串列」維護快取資料的使用順序，最近使用的在前面，最久未使用的在後面。
- 使用「雜湊表 (Dictionary)」實現 O (1) 查詢。
- Put/Get 操作皆為 O (1) 時間複雜度。
- 超過容量時自動移除最久未使用的資料。

## 程式碼架構

- `LRUCache` 類別：
  - 內部 Node 類別為雙向鏈結串列節點，儲存 key/value 及前後參考。
  - 雜湊表 `_keyToNode` 儲存 key 與 Node 的對應關係。
  - 雙向鏈結串列用哨兵節點簡化操作。
  - `Get`/`Put`/`Remove`/`PushFront` 等方法維護 LRU 行為。

## 範例

```csharp
LRUCache lRUCache = new LRUCache(2);  // 建立容量為 2 的 LRU 快取
lRUCache.Put(1, 1);                   // cache is {1=1}
lRUCache.Put(2, 2);                   // cache is {1=1, 2=2}
Console.WriteLine(lRUCache.Get(1));   // 返回 1
lRUCache.Put(3, 3);                   // LRU key was 2, evicts key 2, cache is {1=1, 3=3}
Console.WriteLine(lRUCache.Get(2));   // 返回 -1 (未找到)
lRUCache.Put(4, 4);                   // LRU key was 1, evicts key 1, cache is {4=4, 3=3}
Console.WriteLine(lRUCache.Get(1));   // 返回 -1 (未找到)
Console.WriteLine(lRUCache.Get(3));   // 返回 3
Console.WriteLine(lRUCache.Get(4));   // 返回 4
```

### 預期輸出

```
1
-1
-1
3
4
```

## 相關知識與參考

- [Cache replacement policies - LRU](https://en.wikipedia.org/wiki/Cache_replacement_policies#LRU)
- [快取檔案置換機制 (ithelp)](https://ithelp.ithome.com.tw/articles/10244749)
- [LeetCode 圖解參考 1](https://leetcode.cn/problems/lru-cache/solutions/2456294/tu-jie-yi-zhang-tu-miao-dong-lrupythonja-czgt/)
- [LeetCode 圖解參考 2](https://leetcode.cn/problems/lru-cache/solutions/259678/lruhuan-cun-ji-zhi-by-leetcode-solution/)
- [LeetCode 圖解參考 3](https://leetcode.cn/problems/lru-cache/solutions/1449572/by-stormsunshine-d7c6/)

## 執行方式

1. 使用 .NET 8.0 執行環境
2. `dotnet build` 進行建構
3. `dotnet run` 執行主程式

---

本專案為 LeetCode 146. LRU Cache C# 解法，歡迎參考與學習。